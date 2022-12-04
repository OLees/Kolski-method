
namespace WindowsFormsApp2
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
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.trackBar1 = new System.Windows.Forms.TrackBar();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.Strip_Save = new System.Windows.Forms.ToolStripMenuItem();
            this.Strip_Exit = new System.Windows.Forms.ToolStripMenuItem();
            this.trackBar2 = new System.Windows.Forms.TrackBar();
            this.Kolski_Check_Btn = new System.Windows.Forms.Button();
            this.Strain_Draw_Btn = new System.Windows.Forms.Button();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.Strain_Rate_Draw_Btn = new System.Windows.Forms.Button();
            this.Stress_Draw_Btn = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.Kolski_Check_Value = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).BeginInit();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(92, 26);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(569, 443);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // trackBar1
            // 
            this.trackBar1.Location = new System.Drawing.Point(667, 12);
            this.trackBar1.Maximum = 200;
            this.trackBar1.Name = "trackBar1";
            this.trackBar1.Size = new System.Drawing.Size(279, 45);
            this.trackBar1.TabIndex = 2;
            this.trackBar1.Value = 100;
            this.trackBar1.Scroll += new System.EventHandler(this.Change_dx);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 223);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(56, 13);
            this.label3.TabIndex = 10;
            this.label3.Text = "Сигнал, В";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(341, 472);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(66, 13);
            this.label2.TabIndex = 11;
            this.label2.Text = "Время, мкс";
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1295, 24);
            this.menuStrip1.TabIndex = 12;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.Strip_Save,
            this.Strip_Exit});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // Strip_Save
            // 
            this.Strip_Save.Name = "Strip_Save";
            this.Strip_Save.Size = new System.Drawing.Size(98, 22);
            this.Strip_Save.Text = "Save";
            this.Strip_Save.Click += new System.EventHandler(this.Strip_Save_Click);
            // 
            // Strip_Exit
            // 
            this.Strip_Exit.Name = "Strip_Exit";
            this.Strip_Exit.Size = new System.Drawing.Size(98, 22);
            this.Strip_Exit.Text = "Exit";
            this.Strip_Exit.Click += new System.EventHandler(this.Strip_Exit_Click);
            // 
            // trackBar2
            // 
            this.trackBar2.Location = new System.Drawing.Point(667, 63);
            this.trackBar2.Maximum = 200;
            this.trackBar2.Name = "trackBar2";
            this.trackBar2.Size = new System.Drawing.Size(279, 45);
            this.trackBar2.TabIndex = 14;
            this.trackBar2.Value = 100;
            this.trackBar2.Scroll += new System.EventHandler(this.Change_dx1);
            // 
            // Kolski_Check_Btn
            // 
            this.Kolski_Check_Btn.Location = new System.Drawing.Point(668, 115);
            this.Kolski_Check_Btn.Name = "Kolski_Check_Btn";
            this.Kolski_Check_Btn.Size = new System.Drawing.Size(100, 23);
            this.Kolski_Check_Btn.TabIndex = 15;
            this.Kolski_Check_Btn.Text = "Kolski_Check";
            this.Kolski_Check_Btn.UseVisualStyleBackColor = true;
            this.Kolski_Check_Btn.Click += new System.EventHandler(this.Kolski_Check_Click);
            // 
            // Strain_Draw_Btn
            // 
            this.Strain_Draw_Btn.Location = new System.Drawing.Point(723, 184);
            this.Strain_Draw_Btn.Name = "Strain_Draw_Btn";
            this.Strain_Draw_Btn.Size = new System.Drawing.Size(100, 24);
            this.Strain_Draw_Btn.TabIndex = 16;
            this.Strain_Draw_Btn.Text = "Draw Strain";
            this.Strain_Draw_Btn.UseVisualStyleBackColor = true;
            this.Strain_Draw_Btn.Click += new System.EventHandler(this.Strain_Draw_Click);
            // 
            // pictureBox2
            // 
            this.pictureBox2.Location = new System.Drawing.Point(829, 115);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(454, 354);
            this.pictureBox2.TabIndex = 17;
            this.pictureBox2.TabStop = false;
            // 
            // Strain_Rate_Draw_Btn
            // 
            this.Strain_Rate_Draw_Btn.Location = new System.Drawing.Point(723, 223);
            this.Strain_Rate_Draw_Btn.Name = "Strain_Rate_Draw_Btn";
            this.Strain_Rate_Draw_Btn.Size = new System.Drawing.Size(100, 23);
            this.Strain_Rate_Draw_Btn.TabIndex = 18;
            this.Strain_Rate_Draw_Btn.Text = "Draw Strain Rate";
            this.Strain_Rate_Draw_Btn.UseVisualStyleBackColor = true;
            this.Strain_Rate_Draw_Btn.Click += new System.EventHandler(this.Strain_Rate_Draw_Click);
            // 
            // Stress_Draw_Btn
            // 
            this.Stress_Draw_Btn.Location = new System.Drawing.Point(723, 264);
            this.Stress_Draw_Btn.Name = "Stress_Draw_Btn";
            this.Stress_Draw_Btn.Size = new System.Drawing.Size(100, 23);
            this.Stress_Draw_Btn.TabIndex = 19;
            this.Stress_Draw_Btn.Text = "Draw Stress";
            this.Stress_Draw_Btn.UseVisualStyleBackColor = true;
            this.Stress_Draw_Btn.Click += new System.EventHandler(this.Stress_Draw_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(1036, 472);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(66, 13);
            this.label1.TabIndex = 20;
            this.label1.Text = "Время, мкс";
            // 
            // Kolski_Check_Value
            // 
            this.Kolski_Check_Value.AccessibleRole = System.Windows.Forms.AccessibleRole.StatusBar;
            this.Kolski_Check_Value.Location = new System.Drawing.Point(668, 145);
            this.Kolski_Check_Value.Name = "Kolski_Check_Value";
            this.Kolski_Check_Value.Size = new System.Drawing.Size(100, 20);
            this.Kolski_Check_Value.TabIndex = 21;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1295, 504);
            this.Controls.Add(this.Kolski_Check_Value);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.Stress_Draw_Btn);
            this.Controls.Add(this.Strain_Rate_Draw_Btn);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.Strain_Draw_Btn);
            this.Controls.Add(this.Kolski_Check_Btn);
            this.Controls.Add(this.trackBar2);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.trackBar1);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.TrackBar trackBar1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem Strip_Save;
        private System.Windows.Forms.ToolStripMenuItem Strip_Exit;
        private System.Windows.Forms.TrackBar trackBar2;
        private System.Windows.Forms.Button Kolski_Check_Btn;
        private System.Windows.Forms.Button Strain_Draw_Btn;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.Button Strain_Rate_Draw_Btn;
        private System.Windows.Forms.Button Stress_Draw_Btn;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox Kolski_Check_Value;
    }
}

