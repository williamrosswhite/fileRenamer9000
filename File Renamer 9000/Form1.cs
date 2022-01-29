using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace File_Renamer_9000
{
    public partial class fileRenamer9000 : Form
    {

        int numStart;
        int numCurrent;
        int numOfDigits;
        int numOfHash;
        string newFileNameFormat;

        public fileRenamer9000()
        {
            InitializeComponent();
            numOfHash = 0;
        }

        private void SelectFolder_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog rootPicker = new FolderBrowserDialog();
            DialogResult result = rootPicker.ShowDialog();

            if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(rootPicker.SelectedPath))
            {
                folderPickerTextBox.Text = rootPicker.SelectedPath;
                newFileNameFormat = namingFormatTextBox.Text;
            }
        }

        private string nextTitle(string originalName, string originalPath, string originalExtension)
        {
            bool numProcessed = false;
            StringBuilder sb = new StringBuilder(originalPath);
            sb.Append('\\');
            if (appendRadioButton.Checked == true)
            {
                sb.Append(originalName);
            }
            for (int i = 0; i < newFileNameFormat.Length;)
            {
                if (newFileNameFormat[i] == '#')
                {
                    if (!numProcessed)
                    {
                        string formatString = String.Format("{0:D" + numOfHash + "}", numCurrent);
                        sb.Append(formatString);
                        i += numOfDigits;
                        numProcessed = true;
                    }
                    else
                    {
                        i++;
                    }
                }
                else
                {
                    sb.Append(newFileNameFormat[i]);
                    i++;
                }
            }
            if (prependRadioButton.Checked == true)
            {
                sb.Append(originalName);
                Console.WriteLine("prepending original name: " + originalName);
            }
            sb.Append(originalExtension);
            string returnString = sb.ToString();
            return returnString;
        }

        private string strippedTitle(string originalName, string originalPath, string originalExtension, string stripText)
        {
            StringBuilder sb = new StringBuilder(originalPath);
            sb.Append('\\');
            sb.Append(originalName.Replace(stripText, ""));
            sb.Append(originalExtension);
            return sb.ToString();
        }

        private void getHashCount()
        {
            numOfHash = 0;
            foreach (char c in newFileNameFormat)
            {
                if (c == '#')
                {
                    numOfHash++;
                }
            }
        }

        private void StartButton_Click(object sender, EventArgs e)
        {
            numCurrent = (int)startNumericUpDown.Value;
            newFileNameFormat = namingFormatTextBox.Text;
            numStart = (int)startNumericUpDown.Value;
            getHashCount();
            if (numOfHash == 0 && overwriteRadioButton.Checked == true)
            {
                MessageBox.Show("You must have at least one hashtag within your rename format please");
            }
            else
            {
                try
                {
                    string targetDirectoryString = folderPickerTextBox.Text;
                    DirectoryInfo targetDirectoryInfo = new DirectoryInfo(targetDirectoryString);
                    int numberOfNums = targetDirectoryString.Length;
                    numOfDigits = (int)Math.Floor(Math.Log10(numStart + numberOfNums) + 1);
                    numOfHash = numOfDigits > numOfHash ? numOfDigits : numOfHash;
                    /*  i missed that i had two versions here.  if a problem arises it is probably here
                    if (numOfDigits > numOfHash)
                    {
                        numOfHash = numOfDigits;
                    }
                    */
                    foreach (FileInfo file in targetDirectoryInfo.GetFiles())
                    {
                        string newFileName = nextTitle(Path.GetFileNameWithoutExtension(file.Name), file.DirectoryName, file.Extension);
                        numCurrent++;
                        Console.WriteLine(newFileName);
                        file.MoveTo(newFileName);
                    }
                }
                catch (Exception ex)
                {
                    if (ex is DirectoryNotFoundException || ex is System.ArgumentException)
                    {
                        MessageBox.Show("You must select a valid filepath please");
                    }
                    else
                    {
                        throw;
                    }
                }
            }
        }

        private void StripperButton_Click(object sender, EventArgs e)
        {
            try
            {
                string targetDirectoryString = folderPickerTextBox.Text;
                string stripText = stripperTextBox.Text;
                DirectoryInfo targetDirectoryInfo = new DirectoryInfo(targetDirectoryString);
                foreach (FileInfo file in targetDirectoryInfo.GetFiles())
                {
                    string newFileName = strippedTitle(Path.GetFileNameWithoutExtension(file.Name), file.DirectoryName, file.Extension, stripText);
                    numCurrent++;
                    Console.WriteLine(newFileName);
                    file.MoveTo(newFileName);
                }
            }
            catch (Exception ex)
            {
                if (ex is DirectoryNotFoundException || ex is System.ArgumentException)
                {
                    MessageBox.Show("You must select a valid filepath please");
                }
                else
                {
                    throw;
                }

            }
        }

        private void splitDateClick(object sender, EventArgs e)
        {
            try
            {
                string targetDirectoryString = folderPickerTextBox.Text;
                DirectoryInfo targetDirectoryInfo = new DirectoryInfo(targetDirectoryString);

                foreach (FileInfo file in targetDirectoryInfo.GetFiles())
                {
                    string newFileName = splitDate(Path.GetFileNameWithoutExtension(file.Name), file.DirectoryName, file.Extension);
                    file.MoveTo(newFileName);
                }
            }
            catch (Exception ex)
            {
                if (ex is DirectoryNotFoundException || ex is System.ArgumentException)
                {
                    MessageBox.Show("You must select a valid filepath please");
                }
                else
                {
                    throw;
                }
            }
        }

        private string splitDate(string originalName, string originalPath, string originalExtension)
        {
            StringBuilder sb = new StringBuilder(originalPath);
            sb.Append('\\');
            var modifiedDate = originalName.Insert(7, "_");
            sb.Append(modifiedDate);
            sb.Append(originalExtension);
            string returnString = sb.ToString();
            return returnString;
        }

        private void padDatesClick(object sender, EventArgs e)
        {
            try
            {
                string targetDirectoryString = folderPickerTextBox.Text;
                DirectoryInfo targetDirectoryInfo = new DirectoryInfo(targetDirectoryString);

                foreach (FileInfo file in targetDirectoryInfo.GetFiles())
                {
                    string newFileName = padDate(Path.GetFileNameWithoutExtension(file.Name), file.DirectoryName, file.Extension);
                    file.MoveTo(newFileName);
                }
            }
            catch (Exception ex)
            {
                if (ex is DirectoryNotFoundException || ex is System.ArgumentException)
                {
                    MessageBox.Show("You must select a valid filepath please");
                }
                else
                {
                    throw;
                }
            }
        }

        private string padDate(string originalName, string originalPath, string originalExtension)
        {
            StringBuilder sb = new StringBuilder(originalPath);
            sb.Append('\\');
            var modifiedDate = originalName.PadRight(18, '0');
            Console.WriteLine(modifiedDate);
            sb.Append(modifiedDate);
            sb.Append(originalExtension);
            string returnString = sb.ToString();
            return returnString;
        }

        private void dashToUnderscoreClick(object sender, EventArgs e)
        {
            try
            {
                string targetDirectoryString = folderPickerTextBox.Text;
                DirectoryInfo targetDirectoryInfo = new DirectoryInfo(targetDirectoryString);

                foreach (FileInfo file in targetDirectoryInfo.GetFiles())
                {
                    string newFileName = dashToUnderscore(Path.GetFileNameWithoutExtension(file.Name), file.DirectoryName, file.Extension);
                    file.MoveTo(newFileName);
                }
            }
            catch (Exception ex)
            {
                if (ex is DirectoryNotFoundException || ex is System.ArgumentException)
                {
                    MessageBox.Show("You must select a valid filepath please");
                }
                else
                {
                    throw;
                }
            }
        }

        private string dashToUnderscore(string originalName, string originalPath, string originalExtension)
        {
            StringBuilder sb = new StringBuilder(originalPath);
            sb.Append('\\');
            var modifiedDate = originalName.Replace('-', '_');
            Console.WriteLine(modifiedDate);
            sb.Append(modifiedDate);
            sb.Append(originalExtension);
            string returnString = sb.ToString();
            return returnString;
        }

        private void sanitizeDateClick(object sender, EventArgs e)
        {
            try
            {
                string targetDirectoryString = folderPickerTextBox.Text;
                DirectoryInfo targetDirectoryInfo = new DirectoryInfo(targetDirectoryString);

                foreach (FileInfo file in targetDirectoryInfo.GetFiles())
                {
                    string newFileName = sanitizeDate(Path.GetFileNameWithoutExtension(file.Name), file.DirectoryName, file.Extension);
                    file.MoveTo(newFileName);
                }
            }
            catch (Exception ex)
            {
                if (ex is DirectoryNotFoundException || ex is System.ArgumentException)
                {
                    MessageBox.Show("You must select a valid filepath please");
                }
                else
                {
                    throw;
                }
            }
        }

        private string sanitizeDate(string originalName, string originalPath, string originalExtension)
        {
            StringBuilder sb = new StringBuilder(originalPath);
            sb.Append('\\');

            string[] splitDate = Regex.Split(originalName, " - ");

            //splitDate[0] = splitDate[0].Remove(0,3); // remove errant first characters
            //splitDate[1] = splitDate[0].Substring(0, 17); // trim trailing characters
            splitDate[0] = splitDate[0].Replace("-", "");
            splitDate[0] = splitDate[0].Replace("_", "");
            splitDate[0] = splitDate[0].Replace("(", "");
            splitDate[0] = splitDate[0].Replace(")", "");
            splitDate[0] = splitDate[0].Replace(" ", "");
            splitDate[0] = splitDate[0].Insert(8, "_");
            splitDate[0] = splitDate[0].PadRight(18, '0');
            sb.Append(splitDate[0]);

            if (splitDate.Length > 1)
            {
                for (int i = 1; i < splitDate.Length; i++)
                {
                    sb.Append(" - ");
                    sb.Append(splitDate[i]);
                }
            }

            sb.Append(originalExtension);
            string returnString = sb.ToString();

            Console.WriteLine(returnString);
            return returnString;
        }

        // TO DO: clean this shit up!! lol
        // TO DO: handle file already exists error
        // TO DO: create a replace string function
        // TO DO: create the other thing i've forgotten now in 10 seconds
        // TO DO: ability to open from folder context menu with folder path pre loaded
        // TO DO: an undo button!!! lol
        // TO DO: fix must select valid file path error
        // TO DO: allow '- tag' to persist on sanitize
        // TO DO: allow activation on pressing Enter
        // TO DO: create a function to auto sanitize entire previous years
    }
}
