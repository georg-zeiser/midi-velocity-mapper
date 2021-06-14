
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
            this.debug = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.max)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.exponent)).BeginInit();
            this.SuspendLayout();
            // 
            // max
            // 
            this.max.Location = new System.Drawing.Point(238, 14);
            this.max.Maximum = new decimal(new int[] {
            127,
            0,
            0,
            0});
            this.max.Name = "max";
            this.max.Size = new System.Drawing.Size(120, 23);
            this.max.TabIndex = 1;
            // 
            // exponent
            // 
            this.exponent.DecimalPlaces = 2;
            this.exponent.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.exponent.Location = new System.Drawing.Point(238, 43);
            this.exponent.Maximum = new decimal(new int[] {
            3,
            0,
            0,
            0});
            this.exponent.Name = "exponent";
            this.exponent.Size = new System.Drawing.Size(120, 23);
            this.exponent.TabIndex = 2;
            // 
            // input
            // 
            this.input.DisplayMember = "Name";
            this.input.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.input.FormattingEnabled = true;
            this.input.Location = new System.Drawing.Point(13, 13);
            this.input.Name = "input";
            this.input.Size = new System.Drawing.Size(185, 23);
            this.input.TabIndex = 3;
            this.input.ValueMember = "Id";
            // 
            // output
            // 
            this.output.DisplayMember = "Name";
            this.output.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.output.FormattingEnabled = true;
            this.output.Location = new System.Drawing.Point(13, 43);
            this.output.Name = "output";
            this.output.Size = new System.Drawing.Size(186, 23);
            this.output.TabIndex = 4;
            this.output.ValueMember = "Id";
            // 
            // debug
            // 
            this.debug.AutoSize = true;
            this.debug.ForeColor = System.Drawing.Color.Red;
            this.debug.Location = new System.Drawing.Point(13, 135);
            this.debug.Name = "debug";
            this.debug.Size = new System.Drawing.Size(0, 15);
            this.debug.TabIndex = 5;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(372, 162);
            this.Controls.Add(this.debug);
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
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.NumericUpDown max;
        private System.Windows.Forms.NumericUpDown exponent;
        private System.Windows.Forms.ComboBox input;
        private System.Windows.Forms.ComboBox output;
        private System.Windows.Forms.Label debug;
    }
}

