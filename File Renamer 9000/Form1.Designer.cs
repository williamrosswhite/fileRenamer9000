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
            this.textStripperLabel = new System.Windows.Forms.Label();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.stripperTextBox = new System.Windows.Forms.TextBox();
            this.stripperButton = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.SanitizeDateButton = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.nameFromDateButton = new System.Windows.Forms.Button();
            this.UndoButton = new System.Windows.Forms.Button();
            this.scrapeCleaner = new System.Windows.Forms.Button();
            this.fix_missing_dash_button = new System.Windows.Forms.Button();
            this.delete_all_button = new System.Windows.Forms.Button();
            this.enbiggify = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.startNumericUpDown)).BeginInit();
            this.SuspendLayout();
            // 
            // folderPickerTextBox
            // 
            this.folderPickerTextBox.Location = new System.Drawing.Point(15, 27);
            this.folderPickerTextBox.Margin = new System.Windows.Forms.Padding(2);
            this.folderPickerTextBox.Name = "folderPickerTextBox";
            this.folderPickerTextBox.Size = new System.Drawing.Size(402, 20);
            this.folderPickerTextBox.TabIndex = 0;
            // 
            // selectFolderButton
            // 
            this.selectFolderButton.Location = new System.Drawing.Point(474, 20);
            this.selectFolderButton.Margin = new System.Windows.Forms.Padding(2);
            this.selectFolderButton.Name = "selectFolderButton";
            this.selectFolderButton.Size = new System.Drawing.Size(86, 31);
            this.selectFolderButton.TabIndex = 3;
            this.selectFolderButton.Text = "Select Folder";
            this.selectFolderButton.UseVisualStyleBackColor = true;
            this.selectFolderButton.Click += new System.EventHandler(this.SelectFolder_Click);
            // 
            // startLabel
            // 
            this.startLabel.AutoSize = true;
            this.startLabel.Location = new System.Drawing.Point(30, 189);
            this.startLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.startLabel.Name = "startLabel";
            this.startLabel.Size = new System.Drawing.Size(69, 13);
            this.startLabel.TabIndex = 4;
            this.startLabel.Text = "Start Number";
            // 
            // startNumericUpDown
            // 
            this.startNumericUpDown.Location = new System.Drawing.Point(114, 189);
            this.startNumericUpDown.Margin = new System.Windows.Forms.Padding(2);
            this.startNumericUpDown.Maximum = new decimal(new int[] {
            1661992959,
            1808227885,
            5,
            0});
            this.startNumericUpDown.Name = "startNumericUpDown";
            this.startNumericUpDown.Size = new System.Drawing.Size(62, 20);
            this.startNumericUpDown.TabIndex = 6;
            // 
            // namingFormatTextBox
            // 
            this.namingFormatTextBox.Location = new System.Drawing.Point(15, 141);
            this.namingFormatTextBox.Margin = new System.Windows.Forms.Padding(2);
            this.namingFormatTextBox.Name = "namingFormatTextBox";
            this.namingFormatTextBox.Size = new System.Drawing.Size(402, 20);
            this.namingFormatTextBox.TabIndex = 8;
            // 
            // startButton
            // 
            this.startButton.Location = new System.Drawing.Point(474, 134);
            this.startButton.Margin = new System.Windows.Forms.Padding(2);
            this.startButton.Name = "startButton";
            this.startButton.Size = new System.Drawing.Size(86, 31);
            this.startButton.TabIndex = 9;
            this.startButton.Text = "Start Rename";
            this.startButton.UseVisualStyleBackColor = true;
            this.startButton.Click += new System.EventHandler(this.StartRenameButton_Click);
            // 
            // instructiosLlabel
            // 
            this.instructiosLlabel.Location = new System.Drawing.Point(30, 88);
            this.instructiosLlabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.instructiosLlabel.Name = "instructiosLlabel";
            this.instructiosLlabel.Size = new System.Drawing.Size(254, 33);
            this.instructiosLlabel.TabIndex = 10;
            this.instructiosLlabel.Text = "Write your desired rename format here.  Use # for where you\'d like numbers, but o" +
    "nly in one place";
            // 
            // overwriteRadioButton
            // 
            this.overwriteRadioButton.AutoSize = true;
            this.overwriteRadioButton.Checked = true;
            this.overwriteRadioButton.Location = new System.Drawing.Point(208, 189);
            this.overwriteRadioButton.Margin = new System.Windows.Forms.Padding(2);
            this.overwriteRadioButton.Name = "overwriteRadioButton";
            this.overwriteRadioButton.Size = new System.Drawing.Size(68, 17);
            this.overwriteRadioButton.TabIndex = 14;
            this.overwriteRadioButton.TabStop = true;
            this.overwriteRadioButton.Text = "overwrite";
            this.overwriteRadioButton.UseVisualStyleBackColor = true;
            // 
            // appendRadioButton
            // 
            this.appendRadioButton.AutoSize = true;
            this.appendRadioButton.Location = new System.Drawing.Point(449, 189);
            this.appendRadioButton.Margin = new System.Windows.Forms.Padding(2);
            this.appendRadioButton.Name = "appendRadioButton";
            this.appendRadioButton.Size = new System.Drawing.Size(112, 17);
            this.appendRadioButton.TabIndex = 15;
            this.appendRadioButton.Text = "Append to Original";
            this.appendRadioButton.UseVisualStyleBackColor = true;
            // 
            // prependRadioButton
            // 
            this.prependRadioButton.AutoSize = true;
            this.prependRadioButton.Location = new System.Drawing.Point(306, 189);
            this.prependRadioButton.Margin = new System.Windows.Forms.Padding(2);
            this.prependRadioButton.Name = "prependRadioButton";
            this.prependRadioButton.Size = new System.Drawing.Size(115, 17);
            this.prependRadioButton.TabIndex = 16;
            this.prependRadioButton.Text = "Prepend to Original";
            this.prependRadioButton.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label1.Location = new System.Drawing.Point(15, 231);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(550, 1);
            this.label1.TabIndex = 17;
            // 
            // textStripperLabel
            // 
            this.textStripperLabel.AutoSize = true;
            this.textStripperLabel.Location = new System.Drawing.Point(30, 261);
            this.textStripperLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.textStripperLabel.Name = "textStripperLabel";
            this.textStripperLabel.Size = new System.Drawing.Size(67, 13);
            this.textStripperLabel.TabIndex = 18;
            this.textStripperLabel.Text = "Text Stripper";
            // 
            // imageList1
            // 
            this.imageList1.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
            this.imageList1.ImageSize = new System.Drawing.Size(16, 16);
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // stripperTextBox
            // 
            this.stripperTextBox.Location = new System.Drawing.Point(15, 303);
            this.stripperTextBox.Margin = new System.Windows.Forms.Padding(2);
            this.stripperTextBox.Name = "stripperTextBox";
            this.stripperTextBox.Size = new System.Drawing.Size(402, 20);
            this.stripperTextBox.TabIndex = 19;
            // 
            // stripperButton
            // 
            this.stripperButton.Location = new System.Drawing.Point(474, 296);
            this.stripperButton.Margin = new System.Windows.Forms.Padding(2);
            this.stripperButton.Name = "stripperButton";
            this.stripperButton.Size = new System.Drawing.Size(86, 31);
            this.stripperButton.TabIndex = 20;
            this.stripperButton.Text = "Strip Text";
            this.stripperButton.UseVisualStyleBackColor = true;
            this.stripperButton.Click += new System.EventHandler(this.StripTextButton_Click);
            // 
            // label4
            // 
            this.label4.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label4.Location = new System.Drawing.Point(15, 356);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(550, 1);
            this.label4.TabIndex = 21;
            // 
            // label5
            // 
            this.label5.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label5.Location = new System.Drawing.Point(15, 66);
            this.label5.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(550, 1);
            this.label5.TabIndex = 27;
            // 
            // SanitizeDateButton
            // 
            this.SanitizeDateButton.Location = new System.Drawing.Point(156, 391);
            this.SanitizeDateButton.Margin = new System.Windows.Forms.Padding(2);
            this.SanitizeDateButton.Name = "SanitizeDateButton";
            this.SanitizeDateButton.Size = new System.Drawing.Size(120, 31);
            this.SanitizeDateButton.TabIndex = 29;
            this.SanitizeDateButton.Text = "Photo Sanitize ";
            this.SanitizeDateButton.UseVisualStyleBackColor = true;
            this.SanitizeDateButton.Click += new System.EventHandler(this.PhotoSanitizeButton_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(297, 391);
            this.button1.Margin = new System.Windows.Forms.Padding(2);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(120, 31);
            this.button1.TabIndex = 30;
            this.button1.Text = "Insta Sanitize ";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.InstaSanitizeButton_Click);
            // 
            // nameFromDateButton
            // 
            this.nameFromDateButton.Location = new System.Drawing.Point(15, 391);
            this.nameFromDateButton.Margin = new System.Windows.Forms.Padding(2);
            this.nameFromDateButton.Name = "nameFromDateButton";
            this.nameFromDateButton.Size = new System.Drawing.Size(120, 31);
            this.nameFromDateButton.TabIndex = 31;
            this.nameFromDateButton.Text = "Name From Date";
            this.nameFromDateButton.UseVisualStyleBackColor = true;
            this.nameFromDateButton.Click += new System.EventHandler(this.NameFromDateButton_Click);
            // 
            // UndoButton
            // 
            this.UndoButton.Location = new System.Drawing.Point(441, 391);
            this.UndoButton.Margin = new System.Windows.Forms.Padding(2);
            this.UndoButton.Name = "UndoButton";
            this.UndoButton.Size = new System.Drawing.Size(120, 31);
            this.UndoButton.TabIndex = 32;
            this.UndoButton.Text = "Undo";
            this.UndoButton.UseVisualStyleBackColor = true;
            this.UndoButton.Click += new System.EventHandler(this.UndoButton_Click);
            // 
            // scrapeCleaner
            // 
            this.scrapeCleaner.Location = new System.Drawing.Point(297, 438);
            this.scrapeCleaner.Margin = new System.Windows.Forms.Padding(2);
            this.scrapeCleaner.Name = "scrapeCleaner";
            this.scrapeCleaner.Size = new System.Drawing.Size(120, 31);
            this.scrapeCleaner.TabIndex = 33;
            this.scrapeCleaner.Text = "Scrape Cleaner";
            this.scrapeCleaner.UseVisualStyleBackColor = true;
            this.scrapeCleaner.Click += new System.EventHandler(this.ScrapeCleaner_Click);
            // 
            // fix_missing_dash_button
            // 
            this.fix_missing_dash_button.Location = new System.Drawing.Point(15, 438);
            this.fix_missing_dash_button.Margin = new System.Windows.Forms.Padding(2);
            this.fix_missing_dash_button.Name = "fix_missing_dash_button";
            this.fix_missing_dash_button.Size = new System.Drawing.Size(120, 31);
            this.fix_missing_dash_button.TabIndex = 34;
            this.fix_missing_dash_button.Text = "Fix Missing Dash";
            this.fix_missing_dash_button.UseVisualStyleBackColor = true;
            this.fix_missing_dash_button.Click += new System.EventHandler(this.fix_missing_dash_button_Click);
            // 
            // delete_all_button
            // 
            this.delete_all_button.Location = new System.Drawing.Point(156, 438);
            this.delete_all_button.Margin = new System.Windows.Forms.Padding(2);
            this.delete_all_button.Name = "delete_all_button";
            this.delete_all_button.Size = new System.Drawing.Size(120, 31);
            this.delete_all_button.TabIndex = 35;
            this.delete_all_button.Text = "Nuke It From Orbit";
            this.delete_all_button.UseVisualStyleBackColor = true;
            this.delete_all_button.Click += new System.EventHandler(this.delete_all_button_Click);
            // 
            // enbiggify
            // 
            this.enbiggify.Location = new System.Drawing.Point(438, 438);
            this.enbiggify.Margin = new System.Windows.Forms.Padding(2);
            this.enbiggify.Name = "enbiggify";
            this.enbiggify.Size = new System.Drawing.Size(120, 31);
            this.enbiggify.TabIndex = 36;
            this.enbiggify.Text = "Enbiggify";
            this.enbiggify.UseVisualStyleBackColor = true;
            this.enbiggify.Click += new System.EventHandler(this.enbiggify_Click);
            // 
            // fileRenamer9000
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(569, 491);
            this.Controls.Add(this.enbiggify);
            this.Controls.Add(this.delete_all_button);
            this.Controls.Add(this.fix_missing_dash_button);
            this.Controls.Add(this.scrapeCleaner);
            this.Controls.Add(this.UndoButton);
            this.Controls.Add(this.nameFromDateButton);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.SanitizeDateButton);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.stripperButton);
            this.Controls.Add(this.stripperTextBox);
            this.Controls.Add(this.textStripperLabel);
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
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(2);
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
        private System.Windows.Forms.Label textStripperLabel;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.TextBox stripperTextBox;
        private System.Windows.Forms.Button stripperButton;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button SanitizeDateButton;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button nameFromDateButton;
        private System.Windows.Forms.Button UndoButton;
        private System.Windows.Forms.Button scrapeCleaner;
        private System.Windows.Forms.Button fix_missing_dash_button;
        private System.Windows.Forms.Button delete_all_button;
        private System.Windows.Forms.Button enbiggify;
    }
}

