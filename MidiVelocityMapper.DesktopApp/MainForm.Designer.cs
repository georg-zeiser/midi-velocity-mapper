
namespace MidiVelocityMapper.DesktopApp
{
    partial class MainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.max = new System.Windows.Forms.NumericUpDown();
            this.exponent = new System.Windows.Forms.NumericUpDown();
            this.input = new System.Windows.Forms.ComboBox();
            this.output = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.max)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.exponent)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // max
            // 
            this.max.Location = new System.Drawing.Point(12, 153);
            this.max.Maximum = new decimal(new int[] {
            127,
            0,
            0,
            0});
            this.max.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.max.Name = "max";
            this.max.Size = new System.Drawing.Size(186, 23);
            this.max.TabIndex = 1;
            this.max.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // exponent
            // 
            this.exponent.DecimalPlaces = 2;
            this.exponent.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.exponent.Location = new System.Drawing.Point(12, 197);
            this.exponent.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.exponent.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.exponent.Name = "exponent";
            this.exponent.Size = new System.Drawing.Size(186, 23);
            this.exponent.TabIndex = 2;
            this.exponent.Value = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            // 
            // input
            // 
            this.input.DisplayMember = "Name";
            this.input.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.input.FormattingEnabled = true;
            this.input.Location = new System.Drawing.Point(13, 33);
            this.input.Name = "input";
            this.input.Size = new System.Drawing.Size(186, 23);
            this.input.TabIndex = 3;
            this.input.ValueMember = "Id";
            // 
            // output
            // 
            this.output.DisplayMember = "Name";
            this.output.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.output.FormattingEnabled = true;
            this.output.Location = new System.Drawing.Point(12, 77);
            this.output.Name = "output";
            this.output.Size = new System.Drawing.Size(186, 23);
            this.output.TabIndex = 4;
            this.output.ValueMember = "Id";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(63, 15);
            this.label1.TabIndex = 6;
            this.label1.Text = "MIDI Input";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 59);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(73, 15);
            this.label2.TabIndex = 7;
            this.label2.Text = "MIDI Output";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 135);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(108, 15);
            this.label3.TabIndex = 8;
            this.label3.Text = "Max. Input Velocity";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 179);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(57, 15);
            this.label4.TabIndex = 9;
            this.label4.Text = "Exponent";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(238, 15);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(256, 205);
            this.pictureBox1.TabIndex = 10;
            this.pictureBox1.TabStop = false;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(532, 246);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.output);
            this.Controls.Add(this.input);
            this.Controls.Add(this.exponent);
            this.Controls.Add(this.max);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "Midi Velocity Mapper";
            ((System.ComponentModel.ISupportInitialize)(this.max)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.exponent)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.NumericUpDown max;
        private System.Windows.Forms.NumericUpDown exponent;
        private System.Windows.Forms.ComboBox input;
        private System.Windows.Forms.ComboBox output;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}

