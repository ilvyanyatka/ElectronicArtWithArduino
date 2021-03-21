namespace ElectronicArt1
{
    partial class ArtForm
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
            this.toolTipAngle = new System.Windows.Forms.ToolTip(this.components);
            this.toolTipWidth = new System.Windows.Forms.ToolTip(this.components);
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lbAngleInfo = new System.Windows.Forms.Label();
            this.lbWidthInfo = new System.Windows.Forms.Label();
            this.lbDrawTypeInfo = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.lbError = new System.Windows.Forms.Label();
            this.tmrUnqueue = new System.Windows.Forms.Timer(this.components);
            this.lbDrawRate = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lbDrawRate);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.lbAngleInfo);
            this.groupBox1.Controls.Add(this.lbWidthInfo);
            this.groupBox1.Controls.Add(this.lbDrawTypeInfo);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.ForeColor = System.Drawing.SystemColors.Info;
            this.groupBox1.Location = new System.Drawing.Point(28, 13);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(200, 131);
            this.groupBox1.TabIndex = 8;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Data";
            // 
            // lbAngleInfo
            // 
            this.lbAngleInfo.AutoSize = true;
            this.lbAngleInfo.Location = new System.Drawing.Point(93, 24);
            this.lbAngleInfo.Name = "lbAngleInfo";
            this.lbAngleInfo.Size = new System.Drawing.Size(0, 20);
            this.lbAngleInfo.TabIndex = 5;
            // 
            // lbWidthInfo
            // 
            this.lbWidthInfo.AutoSize = true;
            this.lbWidthInfo.Location = new System.Drawing.Point(93, 50);
            this.lbWidthInfo.Name = "lbWidthInfo";
            this.lbWidthInfo.Size = new System.Drawing.Size(0, 20);
            this.lbWidthInfo.TabIndex = 4;
            // 
            // lbDrawTypeInfo
            // 
            this.lbDrawTypeInfo.AutoSize = true;
            this.lbDrawTypeInfo.Location = new System.Drawing.Point(93, 76);
            this.lbDrawTypeInfo.Name = "lbDrawTypeInfo";
            this.lbDrawTypeInfo.Size = new System.Drawing.Size(0, 20);
            this.lbDrawTypeInfo.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(7, 76);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(81, 20);
            this.label3.TabIndex = 2;
            this.label3.Text = "Line Type:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(7, 50);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(54, 20);
            this.label2.TabIndex = 1;
            this.label2.Text = "Width:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(54, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "Angle:";
            // 
            // lbError
            // 
            this.lbError.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F);
            this.lbError.ForeColor = System.Drawing.SystemColors.Control;
            this.lbError.Location = new System.Drawing.Point(24, 292);
            this.lbError.Name = "lbError";
            this.lbError.Size = new System.Drawing.Size(251, 111);
            this.lbError.TabIndex = 10;
            // 
            // tmrUnqueue
            // 
            this.tmrUnqueue.Enabled = true;
            this.tmrUnqueue.Tick += new System.EventHandler(this.tmrUnqueue_Tick);
            // 
            // lbDrawRate
            // 
            this.lbDrawRate.AutoSize = true;
            this.lbDrawRate.Location = new System.Drawing.Point(93, 100);
            this.lbDrawRate.Name = "lbDrawRate";
            this.lbDrawRate.Size = new System.Drawing.Size(0, 20);
            this.lbDrawRate.TabIndex = 7;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(7, 100);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(82, 20);
            this.label5.TabIndex = 6;
            this.label5.Text = "Draw rate:";
            // 
            // ArtForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(785, 615);
            this.Controls.Add(this.lbError);
            this.Controls.Add(this.groupBox1);
            this.Name = "ArtForm";
            this.Text = "Form1";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ArtForm_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ArtForm_KeyDown);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.ArtForm_KeyPress);
            this.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseDoubleClick);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.ToolTip toolTipAngle;
        private System.Windows.Forms.ToolTip toolTipWidth;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label lbAngleInfo;
        private System.Windows.Forms.Label lbWidthInfo;
        private System.Windows.Forms.Label lbDrawTypeInfo;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lbError;
        private System.Windows.Forms.Timer tmrUnqueue;
        private System.Windows.Forms.Label lbDrawRate;
        private System.Windows.Forms.Label label5;
    }
}

