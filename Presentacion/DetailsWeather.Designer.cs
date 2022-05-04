
namespace WeatherConcurrencyApp
{
    partial class DetailsWeather
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.lblDetail = new System.Windows.Forms.Label();
            this.lblDetailValue = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lblDetail
            // 
            this.lblDetail.Location = new System.Drawing.Point(3, 11);
            this.lblDetail.Name = "lblDetail";
            this.lblDetail.Size = new System.Drawing.Size(153, 17);
            this.lblDetail.TabIndex = 0;
            this.lblDetail.Text = "DetailWeather";
            this.lblDetail.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblDetailValue
            // 
            this.lblDetailValue.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDetailValue.Location = new System.Drawing.Point(3, 39);
            this.lblDetailValue.Name = "lblDetailValue";
            this.lblDetailValue.Size = new System.Drawing.Size(153, 18);
            this.lblDetailValue.TabIndex = 1;
            this.lblDetailValue.Text = "Value";
            this.lblDetailValue.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // DetailsWeather
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.Controls.Add(this.lblDetailValue);
            this.Controls.Add(this.lblDetail);
            this.Name = "DetailsWeather";
            this.Size = new System.Drawing.Size(180, 74);
            this.Load += new System.EventHandler(this.DetailsWeather_Load);
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.Label lblDetail;
        public System.Windows.Forms.Label lblDetailValue;
    }
}
