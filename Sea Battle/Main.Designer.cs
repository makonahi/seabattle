namespace Sea_Battle
{
    partial class Main
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
            this.components = new System.ComponentModel.Container();
            this.YourFieldPBox = new System.Windows.Forms.PictureBox();
            this.CoordinatesPBox = new System.Windows.Forms.PictureBox();
            this.testLabel = new System.Windows.Forms.Label();
            this.updaterTick = new System.Windows.Forms.Timer(this.components);
            this.hintLabel = new System.Windows.Forms.Label();
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
            this.testLabel.Location = new System.Drawing.Point(488, 548);
            this.testLabel.Name = "testLabel";
            this.testLabel.Size = new System.Drawing.Size(209, 230);
            this.testLabel.TabIndex = 2;
            this.testLabel.Text = "0";
            // 
            // updaterTick
            // 
            this.updaterTick.Enabled = true;
            this.updaterTick.Interval = 500;
            this.updaterTick.Tick += new System.EventHandler(this.updaterTick_Tick);
            // 
            // hintLabel
            // 
            this.hintLabel.ForeColor = System.Drawing.SystemColors.ControlDark;
            this.hintLabel.Location = new System.Drawing.Point(25, 48);
            this.hintLabel.Name = "hintLabel";
            this.hintLabel.Size = new System.Drawing.Size(524, 49);
            this.hintLabel.TabIndex = 3;
            this.hintLabel.Text = "label1";
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1407, 816);
            this.Controls.Add(this.hintLabel);
            this.Controls.Add(this.testLabel);
            this.Controls.Add(this.YourFieldPBox);
            this.Controls.Add(this.CoordinatesPBox);
            this.Name = "Main";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.YourFieldPBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.CoordinatesPBox)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.PictureBox CoordinatesPBox;
        public System.Windows.Forms.Label testLabel;
        public System.Windows.Forms.PictureBox YourFieldPBox;
        private System.Windows.Forms.Timer updaterTick;
        private System.Windows.Forms.Label hintLabel;
    }
}

