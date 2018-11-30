namespace Lab3_PolyRel
{
    partial class Lab3_PolyRel
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.ShapeTick = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // ShapeTick
            // 
            this.ShapeTick.Tick += new System.EventHandler(this.ShapeTick_Tick);
            // 
            // Lab3_PolyRel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 262);
            this.Name = "Lab3_PolyRel";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Lab3_PolyRel_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Timer ShapeTick;
    }
}

