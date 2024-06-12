namespace Sea_Battle
{
    partial class Form1
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.YourFieldPBox = new System.Windows.Forms.PictureBox();
            this.CoordinatesPBox = new System.Windows.Forms.PictureBox();
            this.testLabel = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.YourFieldPBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.CoordinatesPBox)).BeginInit();
            this.SuspendLayout();
            // 
            // YourFieldPBox
            // 
            this.YourFieldPBox.Location = new System.Drawing.Point(65, 140);
            this.YourFieldPBox.Name = "YourFieldPBox";
            this.YourFieldPBox.Size = new System.Drawing.Size(403, 403);
            this.YourFieldPBox.TabIndex = 0;
            this.YourFieldPBox.TabStop = false;
            // 
            // CoordinatesPBox
            // 
            this.CoordinatesPBox.Location = new System.Drawing.Point(25, 100);
            this.CoordinatesPBox.Name = "CoordinatesPBox";
            this.CoordinatesPBox.Size = new System.Drawing.Size(443, 443);
            this.CoordinatesPBox.TabIndex = 1;
            this.CoordinatesPBox.TabStop = false;
            // 
            // testLabel
            // 
            this.testLabel.Location = new System.Drawing.Point(687, 212);
            this.testLabel.Name = "testLabel";
            this.testLabel.Size = new System.Drawing.Size(288, 290);
            this.testLabel.TabIndex = 2;
            this.testLabel.Text = "0";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1407, 816);
            this.Controls.Add(this.testLabel);
            this.Controls.Add(this.YourFieldPBox);
            this.Controls.Add(this.CoordinatesPBox);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.YourFieldPBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.CoordinatesPBox)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.PictureBox YourFieldPBox;
        private System.Windows.Forms.PictureBox CoordinatesPBox;
        private System.Windows.Forms.Label testLabel;
    }
}

