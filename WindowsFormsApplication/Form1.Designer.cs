namespace WindowsFormsApplication
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
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.Creator = new System.Windows.Forms.Button();
            this.Monitor = new System.Windows.Forms.Button();
            this.textbox_Output = new System.Windows.Forms.TextBox();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(46, 33);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(43, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Amount";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.Creator);
            this.groupBox1.Controls.Add(this.numericUpDown1);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Location = new System.Drawing.Point(45, 180);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(245, 100);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Text file creator";
            // 
            // numericUpDown1
            // 
            this.numericUpDown1.Location = new System.Drawing.Point(6, 50);
            this.numericUpDown1.Maximum = new decimal(new int[] {
            -1,
            -1,
            -1,
            0});
            this.numericUpDown1.Name = "numericUpDown1";
            this.numericUpDown1.Size = new System.Drawing.Size(130, 20);
            this.numericUpDown1.TabIndex = 6;
            this.numericUpDown1.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(316, 194);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(100, 20);
            this.textBox1.TabIndex = 6;
            // 
            // Creator
            // 
            this.Creator.Location = new System.Drawing.Point(155, 47);
            this.Creator.Name = "Creator";
            this.Creator.Size = new System.Drawing.Size(75, 23);
            this.Creator.TabIndex = 7;
            this.Creator.Text = "Create";
            this.Creator.UseVisualStyleBackColor = true;
            this.Creator.Click += new System.EventHandler(this.Creator_Click);
            // 
            // Monitor
            // 
            this.Monitor.Location = new System.Drawing.Point(395, 254);
            this.Monitor.Name = "Monitor";
            this.Monitor.Size = new System.Drawing.Size(176, 51);
            this.Monitor.TabIndex = 7;
            this.Monitor.Text = "Monitor";
            this.Monitor.UseVisualStyleBackColor = true;
            // 
            // textbox_Output
            // 
            this.textbox_Output.Location = new System.Drawing.Point(12, 12);
            this.textbox_Output.Multiline = true;
            this.textbox_Output.Name = "textbox_Output";
            this.textbox_Output.ReadOnly = true;
            this.textbox_Output.Size = new System.Drawing.Size(568, 146);
            this.textbox_Output.TabIndex = 8;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(592, 335);
            this.Controls.Add(this.textbox_Output);
            this.Controls.Add(this.Monitor);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.groupBox1);
            this.Name = "Form1";
            this.Text = "DataController";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.NumericUpDown numericUpDown1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button Creator;
        private System.Windows.Forms.Button Monitor;
        private System.Windows.Forms.TextBox textbox_Output;
    }
}

