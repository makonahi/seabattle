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
            this.testLabel = new System.Windows.Forms.Label();
            this.updaterTick = new System.Windows.Forms.Timer(this.components);
            this.hintLabel = new System.Windows.Forms.Label();
            this.OpponentFieldPBox = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.YourFieldPBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.OpponentFieldPBox)).BeginInit();
            this.SuspendLayout();
            // 
            // YourFieldPBox
            // 
            this.YourFieldPBox.Location = new System.Drawing.Point(28, 100);
            this.YourFieldPBox.Name = "YourFieldPBox";
            this.YourFieldPBox.Size = new System.Drawing.Size(443, 443);
            this.YourFieldPBox.TabIndex = 0;
            this.YourFieldPBox.TabStop = false;
            // 
            // testLabel
            // 
            this.testLabel.Location = new System.Drawing.Point(471, 563);
            this.testLabel.Name = "testLabel";
            this.testLabel.Size = new System.Drawing.Size(341, 230);
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
            this.hintLabel.Location = new System.Drawing.Point(28, 48);
            this.hintLabel.Name = "hintLabel";
            this.hintLabel.Size = new System.Drawing.Size(521, 49);
            this.hintLabel.TabIndex = 3;
            // 
            // OpponentFieldPBox
            // 
            this.OpponentFieldPBox.BackColor = System.Drawing.SystemColors.Control;
            this.OpponentFieldPBox.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.OpponentFieldPBox.Location = new System.Drawing.Point(810, 100);
            this.OpponentFieldPBox.Name = "OpponentFieldPBox";
            this.OpponentFieldPBox.Size = new System.Drawing.Size(443, 443);
            this.OpponentFieldPBox.TabIndex = 5;
            this.OpponentFieldPBox.TabStop = false;
            this.OpponentFieldPBox.Click += new System.EventHandler(this.OpponentFieldPBox_Click);
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1407, 816);
            this.Controls.Add(this.OpponentFieldPBox);
            this.Controls.Add(this.hintLabel);
            this.Controls.Add(this.testLabel);
            this.Controls.Add(this.YourFieldPBox);
            this.DoubleBuffered = true;
            this.Name = "Main";
            this.Text = "Морской бой";
            this.Load += new System.EventHandler(this.Main_Load);
            ((System.ComponentModel.ISupportInitialize)(this.YourFieldPBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.OpponentFieldPBox)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        public System.Windows.Forms.Label testLabel;
        public System.Windows.Forms.PictureBox YourFieldPBox;
        private System.Windows.Forms.Timer updaterTick;
        private System.Windows.Forms.Label hintLabel;
        public System.Windows.Forms.PictureBox OpponentFieldPBox;
    }
}

