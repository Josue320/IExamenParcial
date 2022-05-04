using System;
using System.Collections.Generic;
using System.IO;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace Infraestrutura.Repository
{
    public class RAFFContext
    {
        private string fileName;
        private const string tmp = "tmp";
        private int size;
        private const string directoryName = "DATA";
        private string DirectoryPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), directoryName);

        public RAFFContext(string fileName, int size)
        {
            this.fileName = fileName;
            this.size = size;
        }
        public Stream TemHeaderStream
        {
            get => File.Open($"{tmp}.hd", FileMode.OpenOrCreate, FileAccess.ReadWrite);
        }
        public Stream HeaderStream
        {
            get => File.Open($"{fileName}.hd", FileMode.OpenOrCreate, FileAccess.ReadWrite);
        }

        public Stream DataStream
        {
            get => File.Open($"{fileName}.dat", FileMode.OpenOrCreate, FileAccess.ReadWrite);
        }



        public void Create<T>(T t)
        {
            try
            {
                using (BinaryWriter bwHeader = new BinaryWriter(HeaderStream),
                                                  bwData = new BinaryWriter(DataStream))
                {
                    int n = 0, k = 0;
                    using (BinaryReader brHeader = new BinaryReader(bwHeader.BaseStream))
                    {
                        if (brHeader.BaseStream.Length > 0)
                        {
                            brHeader.BaseStream.Seek(0, SeekOrigin.Begin);
                            n = brHeader.ReadInt32();
                            k = brHeader.ReadInt32();
                        }

                        //calculamos la posicion en Data
                        long pos = k * size;
                        bwData.BaseStream.Seek(pos, SeekOrigin.Begin);

                        PropertyInfo[] info = t.GetType().GetProperties();
                        foreach (PropertyInfo pinfo in info)
                        {
                            Type type = pinfo.PropertyType;
                            object obj = pinfo.GetValue(t, null);

                            if (type.IsGenericType)
                            {
                                continue;
                            }

                            if (pinfo.Name.Equals("Id", StringComparison.CurrentCultureIgnoreCase))
                            {
                                bwData.Write(++k);
                                continue;
                            }

                            if (type == typeof(int))
                            {
                                bwData.Write((int)obj);
                            }
                            else if (type == typeof(long))
                            {
                                bwData.Write((long)obj);
                            }
                            else if (type == typeof(float))
                            {
                                bwData.Write((float)obj);
                            }
                            else if (type == typeof(double))
                            {
                                bwData.Write((double)obj);
                            }
                            else if (type == typeof(decimal))
                            {
                                bwData.Write((decimal)obj);
                            }
                            else if (type == typeof(char))
                            {
                                bwData.Write((char)obj);
                            }
                            else if (type == typeof(bool))
                            {
                                bwData.Write((bool)obj);
                            }
                            else if (type == typeof(string))
                            {
                                bwData.Write((string)obj);
                            }
                        }

                        long posh = 8 + n * 4;
                        bwHeader.BaseStream.Seek(posh, SeekOrigin.Begin);
                        bwHeader.Write(k);

                        bwHeader.BaseStream.Seek(0, SeekOrigin.Begin);
                        bwHeader.Write(++n);
                        bwHeader.Write(k);
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }

        }

        public T Get<T>(int id)
        {
            try
            {
                T newValue = (T)Activator.CreateInstance(typeof(T));
                int indiceID = BusquedaBinaria(id);
                int n = 0, k = 0, index;
                using (BinaryReader brHeader = new BinaryReader(HeaderStream),
                                    brData = new BinaryReader(DataStream))
                {

                    brHeader.BaseStream.Seek(0, SeekOrigin.Begin);
                    n = brHeader.ReadInt32();
                    k = brHeader.ReadInt32();
                    //    }



                    if (id <= 0 || id > k)
                    {
                        return default(T);
                    }

                    PropertyInfo[] properties = newValue.GetType().GetProperties();

                    long posh = 8 + indiceID * 4;

                    brHeader.BaseStream.Seek(posh, SeekOrigin.Begin);

                    index = brHeader.ReadInt32();


                    long posd = (index - 1) * size;
                    brData.BaseStream.Seek(posd, SeekOrigin.Begin);
                    foreach (PropertyInfo pinfo in properties)
                    {
                        Type type = pinfo.PropertyType;

                        if (type.IsGenericType)
                        {
                            continue;
                        }

                        if (type == typeof(int))
                        {
                            pinfo.SetValue(newValue, brData.GetValue<int>(TypeCode.Int32));
                        }
                        else if (type == typeof(long))
                        {
                            pinfo.SetValue(newValue, brData.GetValue<long>(TypeCode.Int64));
                        }
                        else if (type == typeof(float))
                        {
                            pinfo.SetValue(newValue, brData.GetValue<float>(TypeCode.Single));
                        }
                        else if (type == typeof(double))
                        {
                            pinfo.SetValue(newValue, brData.GetValue<double>(TypeCode.Double));
                        }
                        else if (type == typeof(decimal))
                        {
                            pinfo.SetValue(newValue, brData.GetValue<decimal>(TypeCode.Decimal));
                        }
                        else if (type == typeof(char))
                        {
                            pinfo.SetValue(newValue, brData.GetValue<char>(TypeCode.Char));
                        }
                        else if (type == typeof(bool))
                        {
                            pinfo.SetValue(newValue, brData.GetValue<bool>(TypeCode.Boolean));
                        }
                        else if (type == typeof(string))
                        {
                            pinfo.SetValue(newValue, brData.GetValue<string>(TypeCode.String));
                        }
                    }
                }
                return newValue;
            }
            catch (Exception)
            {
                throw;
            }


        }

        public List<T> GetAll<T>()
        {
            List<T> listT = new List<T>();
            int n = 0;
            try
            {
                using (BinaryReader brHeader = new BinaryReader(HeaderStream))
                {
                    if (brHeader.BaseStream.Length > 0)
                    {
                        brHeader.BaseStream.Seek(0, SeekOrigin.Begin);
                        n = brHeader.ReadInt32();
                    }
                }

                if (n == 0)
                {
                    return listT;
                }

                for (int i = 0; i < n; i++)
                {
                    int index;
                    using (BinaryReader brHeader = new BinaryReader(HeaderStream))
                    {
                        long posh = 8 + i * 4;
                        brHeader.BaseStream.Seek(posh, SeekOrigin.Begin);
                        index = brHeader.ReadInt32();
                    }

                    T t = Get<T>(index);
                    listT.Add(t);
                }

                return listT;
            }
            catch (Exception)
            {
                throw;
            }

        }


        public List<T> Find<T>(Expression<Func<T, bool>> where)
        {
            List<T> listT = new List<T>();
            int n = 0, k = 0;
            Func<T, bool> comparator = where.Compile();
            try
            {
                using (BinaryReader brHeader = new BinaryReader(HeaderStream))
                {
                    if (brHeader.BaseStream.Length > 0)
                    {
                        brHeader.BaseStream.Seek(0, SeekOrigin.Begin);
                        n = brHeader.ReadInt32();
                        k = brHeader.ReadInt32();
                    }
                }

                if (n == 0)
                {
                    return listT;
                }

                for (int i = 0; i < n; i++)
                {
                    int index;
                    using (BinaryReader brHeader = new BinaryReader(HeaderStream))
                    {
                        long posh = 8 + i * 4;
                        brHeader.BaseStream.Seek(posh, SeekOrigin.Begin);
                        index = brHeader.ReadInt32();
                    }

                    T t = Get<T>(index);
                    if (comparator(t))
                    {
                        listT.Add(t);
                    }

                }
                return listT;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public int Update<T>(T t)
        {
            int id;
            try
            {

                id = (int)t.GetType().GetProperty("Id").GetValue(t);
                int index = BusquedaBinaria(id);
                if (index < 0)
                {
                    throw new ArgumentException($"No se encontro un objeto con el Id: {id}");
                }

                using (BinaryWriter bwData = new BinaryWriter(DataStream))
                {
                    long posd = index * size;
                    bwData.BaseStream.Seek(posd, SeekOrigin.Begin);

                    PropertyInfo[] info = t.GetType().GetProperties();
                    foreach (PropertyInfo pinfo in info)
                    {
                        Type type = pinfo.PropertyType;
                        object obj = pinfo.GetValue(t, null);

                        if (type.IsGenericType)
                        {

                            continue;
                        }
                        if (type == typeof(int))
                        {
                            bwData.Write((int)obj);
                        }
                        else if (type == typeof(long))
                        {
                            bwData.Write((long)obj);
                        }
                        else if (type == typeof(float))
                        {
                            bwData.Write((float)obj);
                        }
                        else if (type == typeof(double))
                        {
                            bwData.Write((double)obj);
                        }
                        else if (type == typeof(decimal))
                        {
                            bwData.Write((decimal)obj);
                        }
                        else if (type == typeof(char))
                        {
                            bwData.Write((char)obj);
                        }
                        else if (type == typeof(bool))
                        {
                            bwData.Write((bool)obj);
                        }
                        else if (type == typeof(string))
                        {
                            bwData.Write((string)obj);
                        }
                    }
                }
                return id;
            }
            catch (Exception)
            {
                throw;
            }
        }


        public bool Delete<T>(T t)
        {
            try
            {

                int id = (int)t.GetType().GetProperty("Id").GetValue(t);
                int index = BusquedaBinaria(id);
                if (id < 0)
                {

                    throw new ArgumentException("No existe");
                }
                using (BinaryWriter bwHeader = new BinaryWriter(TemHeaderStream),
                                                  bwData = new BinaryWriter(DataStream))
                {
                    int n = 0, k = 0;
                    using (BinaryReader brHeader = new BinaryReader(HeaderStream))
                    {
                        if (brHeader.BaseStream.Length > 0)
                        {
                            brHeader.BaseStream.Seek(0, SeekOrigin.Begin);
                            n = brHeader.ReadInt32();
                            k = brHeader.ReadInt32();
                        }
                        k = (k == id) ? k - 1 : k;
                        bwHeader.Write(--n);
                        bwHeader.Write(k);
                        for (int i = 0; i < (brHeader.BaseStream.Length / 4 - 2); i++)
                        {

                            int idActual = brHeader.ReadInt32();
                            if (idActual != id)
                            {

                                bwHeader.Write(idActual);
                            }
                        }


                    }
                }

                File.Delete($"{fileName}.hd");
                //ver si no da error
                File.Move($"{tmp}.hd", $"{fileName}.hd");
                return true;
            }
            catch (Exception)
            {
                throw;
            }


        }


        private int BusquedaBinaria(int dato)
        {
            using (BinaryReader brHeader = new BinaryReader(HeaderStream))
            {
                brHeader.BaseStream.Seek(0, SeekOrigin.Begin);

                int fin = brHeader.ReadInt32() - 1;
                int inicio = 0;
                while (inicio <= fin)
                {

                    int indiceCentral = Convert.ToInt32(Math.Floor(Convert.ToDouble(inicio + fin) / 2));
                    brHeader.BaseStream.Seek(8 + 4 * indiceCentral, SeekOrigin.Begin);
                    int valorCentral = brHeader.ReadInt32();
                    if (valorCentral == dato)
                    {
                        return indiceCentral;
                    }
                    if (dato < valorCentral)
                    {

                        fin = indiceCentral - 1;
                    }
                    else
                    {

                        inicio = indiceCentral + 1;
                    }
                }
                return -1;
            }

        }
        public int GetLastId()
        {
            int K = 0;
            try
            {
                using (BinaryReader brHeader = new BinaryReader(HeaderStream))
                {
                    if (brHeader.BaseStream.Length <= 0)
                    {
                        return K;
                    }

                    long posh = 4;
                    brHeader.BaseStream.Seek(posh, SeekOrigin.Begin);
                    K = brHeader.ReadInt32();
                    return K;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}

