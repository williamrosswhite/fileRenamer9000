using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace File_Renamer_9000
{
    class Rename
    {

        int numStart;
        int numCurrent;
        int numOfDigits;
        int numOfHash;
        string newFileNameFormat;

        public Rename()
        {
            numOfHash = 0;
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
    }
}
