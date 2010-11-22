namespace LETSI_WS_Stub_Client
{
    partial class Form1
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
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.txtExternalRegId = new System.Windows.Forms.TextBox();
            this.btnTest = new System.Windows.Forms.Button();
            this.lblReg = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtSecret = new System.Windows.Forms.TextBox();
            this.txtURL = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(12, 120);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBox1.Size = new System.Drawing.Size(933, 394);
            this.textBox1.TabIndex = 1;
            // 
            // txtExternalRegId
            // 
            this.txtExternalRegId.Location = new System.Drawing.Point(465, 27);
            this.txtExternalRegId.Name = "txtExternalRegId";
            this.txtExternalRegId.Size = new System.Drawing.Size(498, 20);
            this.txtExternalRegId.TabIndex = 2;
            // 
            // btnTest
            // 
            this.btnTest.Location = new System.Drawing.Point(31, 40);
            this.btnTest.Name = "btnTest";
            this.btnTest.Size = new System.Drawing.Size(132, 23);
            this.btnTest.TabIndex = 5;
            this.btnTest.Text = "Test";
            this.btnTest.UseVisualStyleBackColor = true;
            this.btnTest.Click += new System.EventHandler(this.btnTest_Click);
            // 
            // lblReg
            // 
            this.lblReg.AutoSize = true;
            this.lblReg.Location = new System.Drawing.Point(424, 30);
            this.lblReg.Name = "lblReg";
            this.lblReg.Size = new System.Drawing.Size(41, 13);
            this.lblReg.TabIndex = 6;
            this.lblReg.Text = "Reg ID";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(424, 61);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(38, 13);
            this.label2.TabIndex = 7;
            this.label2.Text = "Secret";
            // 
            // txtSecret
            // 
            this.txtSecret.Location = new System.Drawing.Point(465, 58);
            this.txtSecret.Name = "txtSecret";
            this.txtSecret.Size = new System.Drawing.Size(498, 20);
            this.txtSecret.TabIndex = 8;
            // 
            // txtURL
            // 
            this.txtURL.Location = new System.Drawing.Point(465, 84);
            this.txtURL.Name = "txtURL";
            this.txtURL.Size = new System.Drawing.Size(498, 20);
            this.txtURL.TabIndex = 10;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(424, 87);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 13);
            this.label1.TabIndex = 9;
            this.label1.Text = "URL";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(989, 541);
            this.Controls.Add(this.txtURL);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtSecret);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lblReg);
            this.Controls.Add(this.btnTest);
            this.Controls.Add(this.txtExternalRegId);
            this.Controls.Add(this.textBox1);
            this.Name = "Form1";
            this.Text = "Rustici LETSI RTWS test client";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TextBox txtExternalRegId;
        private System.Windows.Forms.Button btnTest;
        private System.Windows.Forms.Label lblReg;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtSecret;
        private System.Windows.Forms.TextBox txtURL;
        private System.Windows.Forms.Label label1;
    }
}

