namespace ImgToASCII
{
    partial class Form2
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
            this.label1 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.higthNum = new System.Windows.Forms.NumericUpDown();
            this.widthNum = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.higthNum)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.widthNum)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label1.Location = new System.Drawing.Point(23, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(67, 63);
            this.label1.TabIndex = 0;
            this.label1.Text = "Height :\r\n\r\nWidth   :\r\n";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(228, 20);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(101, 61);
            this.button1.TabIndex = 3;
            this.button1.Text = "Resize";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // higthNum
            // 
            this.higthNum.Location = new System.Drawing.Point(96, 21);
            this.higthNum.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.higthNum.Minimum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.higthNum.Name = "higthNum";
            this.higthNum.Size = new System.Drawing.Size(90, 23);
            this.higthNum.TabIndex = 4;
            this.higthNum.Value = new decimal(new int[] {
            200,
            0,
            0,
            0});
            // 
            // widthNum
            // 
            this.widthNum.Location = new System.Drawing.Point(96, 58);
            this.widthNum.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.widthNum.Minimum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.widthNum.Name = "widthNum";
            this.widthNum.Size = new System.Drawing.Size(90, 23);
            this.widthNum.TabIndex = 5;
            this.widthNum.Value = new decimal(new int[] {
            200,
            0,
            0,
            0});
            // 
            // Form2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(341, 106);
            this.Controls.Add(this.widthNum);
            this.Controls.Add(this.higthNum);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label1);
            this.Name = "Form2";
            this.Text = "Set Resolution";
            ((System.ComponentModel.ISupportInitialize)(this.higthNum)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.widthNum)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Label label1;
        private Button button1;
        private NumericUpDown higthNum;
        private NumericUpDown widthNum;
    }
}