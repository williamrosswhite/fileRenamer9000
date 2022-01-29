namespace File_Renamer_9000
{
    partial class fileRenamer9000
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
            this.folderPickerTextBox = new System.Windows.Forms.TextBox();
            this.selectFolderButton = new System.Windows.Forms.Button();
            this.startLabel = new System.Windows.Forms.Label();
            this.startNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.namingFormatTextBox = new System.Windows.Forms.TextBox();
            this.startButton = new System.Windows.Forms.Button();
            this.instructiosLlabel = new System.Windows.Forms.Label();
            this.overwriteRadioButton = new System.Windows.Forms.RadioButton();
            this.appendRadioButton = new System.Windows.Forms.RadioButton();
            this.prependRadioButton = new System.Windows.Forms.RadioButton();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.stripperTextBox = new System.Windows.Forms.TextBox();
            this.stripperButton = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.padDateButton = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.dashToUnderscoreButton = new System.Windows.Forms.Button();
            this.SanitizedDashButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.startNumericUpDown)).BeginInit();
            this.SuspendLayout();
            // 
            // folderPickerTextBox
            // 
            this.folderPickerTextBox.Location = new System.Drawing.Point(30, 52);
            this.folderPickerTextBox.Name = "folderPickerTextBox";
            this.folderPickerTextBox.Size = new System.Drawing.Size(800, 31);
            this.folderPickerTextBox.TabIndex = 0;
            // 
            // selectFolderButton
            // 
            this.selectFolderButton.Location = new System.Drawing.Point(947, 38);
            this.selectFolderButton.Name = "selectFolderButton";
            this.selectFolderButton.Size = new System.Drawing.Size(171, 59);
            this.selectFolderButton.TabIndex = 3;
            this.selectFolderButton.Text = "Select Folder";
            this.selectFolderButton.UseVisualStyleBackColor = true;
            this.selectFolderButton.Click += new System.EventHandler(this.SelectFolder_Click);
            // 
            // startLabel
            // 
            this.startLabel.AutoSize = true;
            this.startLabel.Location = new System.Drawing.Point(59, 364);
            this.startLabel.Name = "startLabel";
            this.startLabel.Size = new System.Drawing.Size(138, 25);
            this.startLabel.TabIndex = 4;
            this.startLabel.Text = "Start Number";
            // 
            // startNumericUpDown
            // 
            this.startNumericUpDown.Location = new System.Drawing.Point(228, 364);
            this.startNumericUpDown.Maximum = new decimal(new int[] {
            1661992959,
            1808227885,
            5,
            0});
            this.startNumericUpDown.Name = "startNumericUpDown";
            this.startNumericUpDown.Size = new System.Drawing.Size(125, 31);
            this.startNumericUpDown.TabIndex = 6;
            // 
            // namingFormatTextBox
            // 
            this.namingFormatTextBox.Location = new System.Drawing.Point(30, 271);
            this.namingFormatTextBox.Name = "namingFormatTextBox";
            this.namingFormatTextBox.Size = new System.Drawing.Size(800, 31);
            this.namingFormatTextBox.TabIndex = 8;
            // 
            // startButton
            // 
            this.startButton.Location = new System.Drawing.Point(947, 257);
            this.startButton.Name = "startButton";
            this.startButton.Size = new System.Drawing.Size(171, 59);
            this.startButton.TabIndex = 9;
            this.startButton.Text = "Start Rename";
            this.startButton.UseVisualStyleBackColor = true;
            this.startButton.Click += new System.EventHandler(this.StartButton_Click);
            // 
            // instructiosLlabel
            // 
            this.instructiosLlabel.Location = new System.Drawing.Point(59, 165);
            this.instructiosLlabel.Name = "instructiosLlabel";
            this.instructiosLlabel.Size = new System.Drawing.Size(508, 63);
            this.instructiosLlabel.TabIndex = 10;
            this.instructiosLlabel.Text = "Write your desired rename format here.  Use # for where you\'d like numbers, but o" +
    "nly in one place";
            // 
            // overwriteRadioButton
            // 
            this.overwriteRadioButton.AutoSize = true;
            this.overwriteRadioButton.Checked = true;
            this.overwriteRadioButton.Location = new System.Drawing.Point(424, 364);
            this.overwriteRadioButton.Name = "overwriteRadioButton";
            this.overwriteRadioButton.Size = new System.Drawing.Size(130, 29);
            this.overwriteRadioButton.TabIndex = 14;
            this.overwriteRadioButton.TabStop = true;
            this.overwriteRadioButton.Text = "overwrite";
            this.overwriteRadioButton.UseVisualStyleBackColor = true;
            // 
            // appendRadioButton
            // 
            this.appendRadioButton.AutoSize = true;
            this.appendRadioButton.Location = new System.Drawing.Point(898, 364);
            this.appendRadioButton.Name = "appendRadioButton";
            this.appendRadioButton.Size = new System.Drawing.Size(221, 29);
            this.appendRadioButton.TabIndex = 15;
            this.appendRadioButton.Text = "Append to Original";
            this.appendRadioButton.UseVisualStyleBackColor = true;
            // 
            // prependRadioButton
            // 
            this.prependRadioButton.AutoSize = true;
            this.prependRadioButton.Location = new System.Drawing.Point(613, 364);
            this.prependRadioButton.Name = "prependRadioButton";
            this.prependRadioButton.Size = new System.Drawing.Size(228, 29);
            this.prependRadioButton.TabIndex = 16;
            this.prependRadioButton.Text = "Prepend to Original";
            this.prependRadioButton.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label1.Location = new System.Drawing.Point(30, 444);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(1173, 2);
            this.label1.TabIndex = 17;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(59, 501);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(135, 25);
            this.label2.TabIndex = 18;
            this.label2.Text = "Text Stripper";
            // 
            // imageList1
            // 
            this.imageList1.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
            this.imageList1.ImageSize = new System.Drawing.Size(16, 16);
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // stripperTextBox
            // 
            this.stripperTextBox.Location = new System.Drawing.Point(30, 583);
            this.stripperTextBox.Name = "stripperTextBox";
            this.stripperTextBox.Size = new System.Drawing.Size(800, 31);
            this.stripperTextBox.TabIndex = 19;
            // 
            // stripperButton
            // 
            this.stripperButton.Location = new System.Drawing.Point(947, 569);
            this.stripperButton.Name = "stripperButton";
            this.stripperButton.Size = new System.Drawing.Size(171, 59);
            this.stripperButton.TabIndex = 20;
            this.stripperButton.Text = "Strip Text";
            this.stripperButton.UseVisualStyleBackColor = true;
            this.stripperButton.Click += new System.EventHandler(this.StripperButton_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(363, 725);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(171, 59);
            this.button1.TabIndex = 24;
            this.button1.Text = "Split Date";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(59, 742);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(184, 25);
            this.label3.TabIndex = 22;
            this.label3.Text = "Multi Date Worker";
            // 
            // label4
            // 
            this.label4.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label4.Location = new System.Drawing.Point(30, 685);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(1173, 2);
            this.label4.TabIndex = 21;
            // 
            // padDateButton
            // 
            this.padDateButton.Location = new System.Drawing.Point(631, 725);
            this.padDateButton.Name = "padDateButton";
            this.padDateButton.Size = new System.Drawing.Size(171, 59);
            this.padDateButton.TabIndex = 26;
            this.padDateButton.Text = "Pad Date";
            this.padDateButton.UseVisualStyleBackColor = true;
            this.padDateButton.Click += new System.EventHandler(this.padDatesClick);
            // 
            // label5
            // 
            this.label5.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label5.Location = new System.Drawing.Point(30, 126);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(1173, 2);
            this.label5.TabIndex = 27;
            // 
            // dashToUnderscoreButton
            // 
            this.dashToUnderscoreButton.Location = new System.Drawing.Point(898, 725);
            this.dashToUnderscoreButton.Name = "dashToUnderscoreButton";
            this.dashToUnderscoreButton.Size = new System.Drawing.Size(241, 59);
            this.dashToUnderscoreButton.TabIndex = 28;
            this.dashToUnderscoreButton.Text = "Dash To Underscore";
            this.dashToUnderscoreButton.UseVisualStyleBackColor = true;
            this.dashToUnderscoreButton.Click += new System.EventHandler(this.dashToUnderscoreClick);
            // 
            // SanitizedDashButton
            // 
            this.SanitizedDashButton.Location = new System.Drawing.Point(898, 821);
            this.SanitizedDashButton.Name = "SanitizedDashButton";
            this.SanitizedDashButton.Size = new System.Drawing.Size(241, 59);
            this.SanitizedDashButton.TabIndex = 29;
            this.SanitizedDashButton.Text = "Sanitize Dashed";
            this.SanitizedDashButton.UseVisualStyleBackColor = true;
            this.SanitizedDashButton.Click += new System.EventHandler(this.sanitizedDashClick);
            // 
            // fileRenamer9000
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1303, 1067);
            this.Controls.Add(this.SanitizedDashButton);
            this.Controls.Add(this.dashToUnderscoreButton);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.padDateButton);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.stripperButton);
            this.Controls.Add(this.stripperTextBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.prependRadioButton);
            this.Controls.Add(this.appendRadioButton);
            this.Controls.Add(this.overwriteRadioButton);
            this.Controls.Add(this.instructiosLlabel);
            this.Controls.Add(this.startButton);
            this.Controls.Add(this.namingFormatTextBox);
            this.Controls.Add(this.startNumericUpDown);
            this.Controls.Add(this.startLabel);
            this.Controls.Add(this.selectFolderButton);
            this.Controls.Add(this.folderPickerTextBox);
            this.Name = "fileRenamer9000";
            this.Text = "Form Renamer 9000";
            ((System.ComponentModel.ISupportInitialize)(this.startNumericUpDown)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox folderPickerTextBox;
        private System.Windows.Forms.Button selectFolderButton;
        private System.Windows.Forms.Label startLabel;
        private System.Windows.Forms.NumericUpDown startNumericUpDown;
        private System.Windows.Forms.TextBox namingFormatTextBox;
        private System.Windows.Forms.Button startButton;
        private System.Windows.Forms.Label instructiosLlabel;
        private System.Windows.Forms.RadioButton overwriteRadioButton;
        private System.Windows.Forms.RadioButton appendRadioButton;
        private System.Windows.Forms.RadioButton prependRadioButton;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.TextBox stripperTextBox;
        private System.Windows.Forms.Button stripperButton;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button padDateButton;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button dashToUnderscoreButton;
        private System.Windows.Forms.Button SanitizedDashButton;
    }
}

