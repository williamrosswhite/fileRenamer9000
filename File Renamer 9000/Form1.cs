using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace File_Renamer_9000
{
    public partial class fileRenamer9000 : Form
    {
        int numStart;
        int numCurrent;
        int numOfDigits;
        int numOfHash;

        string originalFileNameFormat;
        string newFileNameFormat;

        int incrementer = 0;

        LinkedList<DirectoryInfo> filePathLinkedList = new LinkedList<DirectoryInfo>();
        LinkedList<LinkedList<string>> linkedListOfFileNameLinkedLists = new LinkedList<LinkedList<string>>();
        DirectoryInfo undoDirectoryInfo;

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

        private string NextTitle(string originalName, string originalPath, string originalExtension)
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

        private string StrippedTitle(string originalName, string originalPath, string originalExtension, string stripText)
        {
            StringBuilder sb = new StringBuilder(originalPath);
            sb.Append('\\');
            sb.Append(originalName.Replace(stripText, ""));
            sb.Append(originalExtension);
            return sb.ToString();
        }

        private void GetHashCount()
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

        private void StartRenameButton_Click(object sender, EventArgs e)
        {
            numCurrent = (int)startNumericUpDown.Value;
            newFileNameFormat = namingFormatTextBox.Text;
            numStart = (int)startNumericUpDown.Value;
            GetHashCount();

            string targetDirectoryString = folderPickerTextBox.Text;
            DirectoryInfo targetDirectoryInfo = new DirectoryInfo(targetDirectoryString);
            int numberOfNums = targetDirectoryString.Length;

            var newFileNameLinkedList = new LinkedList<string>();
            filePathLinkedList.AddLast(targetDirectoryInfo);
            undoDirectoryInfo = targetDirectoryInfo;

            if (numOfHash == 0 && overwriteRadioButton.Checked == true)
            {
                MessageBox.Show("You must have at least one hashtag within your rename format please");
            }
            else
            {
                try
                {
                    numOfDigits = (int)Math.Floor(Math.Log10(numStart + numberOfNums) + 1);
                    numOfHash = numOfDigits > numOfHash ? numOfDigits : numOfHash;

                    foreach (FileInfo file in targetDirectoryInfo.GetFiles())
                    {
                        newFileNameLinkedList.AddLast(file.FullName);
                        string newFileName = NextTitle(Path.GetFileNameWithoutExtension(file.Name), file.DirectoryName, file.Extension);
                        numCurrent++;
                        Console.WriteLine(newFileName);
                        file.MoveTo(newFileName);
                    }

                    linkedListOfFileNameLinkedLists.AddLast(newFileNameLinkedList);
                }
                catch (Exception ex)
                {
                    if (ex is DirectoryNotFoundException || ex is ArgumentException)
                    {
                        MessageBox.Show("You must select a valid filepath please");
                    }

                    if (ex is IOException)
                    {
                        ReprocessForExistingFileNames(targetDirectoryString);
                    }
                    
                    else
                    {
                        throw;
                    }
                }
            }
        }

        private void ReprocessForExistingFileNames(string targetDirectoryString)
        {
            originalFileNameFormat = newFileNameFormat;
            newFileNameFormat = 'a' + newFileNameFormat;

            var newFileNameLinkedList = new LinkedList<string>();

            DirectoryInfo targetDirectoryInfo = new DirectoryInfo(targetDirectoryString);
            filePathLinkedList.AddLast(targetDirectoryInfo);
            undoDirectoryInfo = targetDirectoryInfo;

            foreach (FileInfo file in targetDirectoryInfo.GetFiles())
            {
                newFileNameLinkedList.AddLast(file.FullName);
                string newFileName = NextTitle('a' + Path.GetFileNameWithoutExtension(file.Name), file.DirectoryName, file.Extension);
                numCurrent++;
                Console.WriteLine(newFileName);
                file.MoveTo(newFileName);
            }

            // horrible efficiency design i know, but just making it work before improving it

            newFileNameFormat = originalFileNameFormat;
            numCurrent = numStart;

            foreach (FileInfo file in targetDirectoryInfo.GetFiles())
            {
                string newFileName = NextTitle(Path.GetFileNameWithoutExtension(file.Name), file.DirectoryName, file.Extension);
                numCurrent++;
                Console.WriteLine(newFileName);
                file.MoveTo(newFileName);
            }

            linkedListOfFileNameLinkedLists.AddLast(newFileNameLinkedList);
        }

        private void StripTextButton_Click(object sender, EventArgs e)
        {
            try
            {
                string targetDirectoryString = folderPickerTextBox.Text;
                string stripText = stripperTextBox.Text;

                DirectoryInfo targetDirectoryInfo = new DirectoryInfo(targetDirectoryString);
                var newFileNameLinkedList = new LinkedList<string>();
                filePathLinkedList.AddLast(targetDirectoryInfo);

                foreach (FileInfo file in targetDirectoryInfo.GetFiles())
                {
                    newFileNameLinkedList.AddLast(file.FullName);
                    string newFileName = StrippedTitle(Path.GetFileNameWithoutExtension(file.Name), file.DirectoryName, file.Extension, stripText);
                    numCurrent++;
                    Console.WriteLine(newFileName);
                    file.MoveTo(newFileName);
                }

                linkedListOfFileNameLinkedLists.AddLast(newFileNameLinkedList);
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

        private void PhotoSanitizeButton_Click(object sender, EventArgs e)
        {
            try
            {
                string targetDirectoryString = folderPickerTextBox.Text;
                DirectoryInfo targetDirectoryInfo = new DirectoryInfo(targetDirectoryString);

                var newFileNameLinkedList = new LinkedList<string>();
                filePathLinkedList.AddLast(targetDirectoryInfo);

                foreach (FileInfo file in targetDirectoryInfo.GetFiles())
                {
                    newFileNameLinkedList.AddLast(file.FullName);
                    string newFileName = SanitizeCameraDate(Path.GetFileNameWithoutExtension(file.Name), file.DirectoryName, file.Extension);
                    file.MoveTo(newFileName);
                }

                linkedListOfFileNameLinkedLists.AddLast(newFileNameLinkedList);
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

        private string SanitizeCameraDate(string originalName, string originalPath, string originalExtension)
        {
            StringBuilder sb = new StringBuilder(originalPath);
            sb.Append('\\');

            string[] splitDate = Regex.Split(originalName, " - ");

            splitDate[0] = splitDate[0].Replace("-", "");
            splitDate[0] = splitDate[0].Replace("_", "");
            splitDate[0] = splitDate[0].Replace("(", "");
            splitDate[0] = splitDate[0].Replace(")", "");
            splitDate[0] = splitDate[0].Replace(":", "");
            splitDate[0] = splitDate[0].Replace("~", "");
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

        private void InstaSanitizeButton_Click(object sender, EventArgs e)
        {
            try
            {
                string targetDirectoryString = folderPickerTextBox.Text;
                DirectoryInfo targetDirectoryInfo = new DirectoryInfo(targetDirectoryString);

                foreach (FileInfo file in targetDirectoryInfo.GetFiles())
                {
                    if (incrementer >= 10)
                    {
                        incrementer = 0;
                    }
                    string newFileName = sanitizeInstaDate(Path.GetFileNameWithoutExtension(file.Name), file.DirectoryName, file.Extension);
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

        private string sanitizeInstaDate(string originalName, string originalPath, string originalExtension)
        {
            StringBuilder sb = new StringBuilder(originalPath);
            sb.Append('\\');

            string[] splitDate = Regex.Split(originalName, " - ");
;
            bool match = true;

            while (match == true)
            {
                splitDate[0] = splitDate[0].PadRight(30, '0');
                splitDate[0] = splitDate[0].Substring(0, 18); // trim trailing characters
                splitDate[0] = $"{splitDate[0]}{incrementer}";
                match = File.Exists(splitDate[0]);
                incrementer++;
            }

            sb.Append(splitDate[0]);

            sb.Append(originalExtension);
            string returnString = sb.ToString();

            Console.WriteLine(returnString);
            return returnString;
        }

        private void NameFromDateButton_Click(object sender, EventArgs e)
        {
            try
            {
                ProcesNameFromDate();
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

        private void ProcesNameFromDate()
        {
            string targetDirectoryString = folderPickerTextBox.Text;
            DirectoryInfo targetDirectoryInfo = new DirectoryInfo(targetDirectoryString);

            var newFileNameLinkedList = new LinkedList<string>();
            filePathLinkedList.AddLast(targetDirectoryInfo);

            foreach (FileInfo file in targetDirectoryInfo.GetFiles())
            {
                newFileNameLinkedList.AddLast(file.FullName);

                var dateTakenYear = file.LastWriteTime.Year.ToString();
                var dateTakenMonth = file.LastWriteTime.Month.ToString();
                var paddedDateTakenMonth = dateTakenMonth.PadLeft(2, '0');
                var dateTakenDay = file.LastWriteTime.Day.ToString();
                var paddedDateTakenDay = dateTakenDay.PadLeft(2, '0');
                var dateTakenTime = file.LastWriteTime.TimeOfDay.ToString();

                string newFileName;

                var newDateFileNameString = $"{ dateTakenYear }{ paddedDateTakenMonth }{ paddedDateTakenDay }_{ dateTakenTime }";
                newFileName = SanitizeCameraDate(newDateFileNameString, file.DirectoryName, file.Extension);
                var match = File.Exists(newFileName);

                while (match == true)
                {
                    newDateFileNameString = $"{ newDateFileNameString.Substring(0, 17) }{ incrementer }";
                    incrementer++;
                    newFileName = SanitizeCameraDate(newDateFileNameString, file.DirectoryName, file.Extension);
                    match = File.Exists(newFileName);
                }

                file.MoveTo(newFileName);
            }

            linkedListOfFileNameLinkedLists.AddLast(newFileNameLinkedList);
        }

        private void UndoButton_Click(object sender, EventArgs e)
        {
            try
            {
                ProcessUndo();
            }
            catch (Exception ex)
            {
                if (ex is DirectoryNotFoundException || ex is ArgumentException)
                {
                    MessageBox.Show("You must select a valid filepath please");
                }

                if (ex is DataMisalignedException)
                {
                    MessageBox.Show(ex.Message);
                }

                else
                {
                    throw;
                }
            }
        }

        private void ProcessUndo()
        {
            if (filePathLinkedList.Count <= 0 || linkedListOfFileNameLinkedLists.Count <= 0)
            {
                throw new DataMisalignedException("Sorry, no remaining undo data");
            }

            var currentDirectory = filePathLinkedList.Last.Value;
            filePathLinkedList.RemoveLast();

            var currentFileNameLinkedList = linkedListOfFileNameLinkedLists.Last.Value;
            linkedListOfFileNameLinkedLists.RemoveLast();

            foreach (FileInfo file in currentDirectory.GetFiles())
            {
                Console.WriteLine(currentFileNameLinkedList.Last.Value);
                file.MoveTo(currentFileNameLinkedList.First.Value);
                currentFileNameLinkedList.RemoveFirst();
            }
        }


        // TO DO: clean this shit up!! lol
        // TO DO: create a replace string function
        // TO DO: create the other thing i've forgotten now in 10 seconds
        // TO DO: ability to open from folder context menu with folder path pre loaded
        // TO DO: an undo button!!! lol
        // TO DO: fix must select valid file path error
        // TO DO: allow '- tag' to persist on sanitize
        // TO DO: allow activation on pressing Enter
        // TO DO: create a function to auto sanitize entire previous years
        // TO DO: sanitize in context menu
        // TO DO: allow insertion and deletion immediately before or after string fragment
        // TO DO: allow me to batch rename all with mising '- ', keep them all in the same order in memory, THEN batch rename, or rewrite them with their existing number
        // TO DO: replace with
    }
}



//splitDate[0] = splitDate[0].Remove(0,3); // remove errant first characters
//splitDate[1] = splitDate[0].Substring(0, 17); // trim trailing characters

/*        private void splitDateClick(object sender, EventArgs e)
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
        }*/

/*        private void padDatesClick(object sender, EventArgs e)
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
        }*/


/*
 * using System;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
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
        bool duplicates = false;
        int incrementer = 0;

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
            StringBuilder filenameWithoutExtension;
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
                    //sb.Append(newFileNameFormat[i]);
                    i++;
                }
            }
            if (prependRadioButton.Checked == true)
            {
                sb.Append(originalName);
                Console.WriteLine("prepending original name: " + originalName);
            }

            filenameWithoutExtension = sb;
            //sb.Append(originalExtension);
            sb.Append(".jpg");

            string returnString = sb.ToString();

            if (File.Exists(returnString))
            {
                incrementer++;
                if (incrementer >= 10)
                {
                    incrementer = 0;
                }
                filenameWithoutExtension.Append(incrementer.ToString());
                //filenameWithoutExtension.Append(originalExtension);
                sb.Append(".jpg");
                returnString = sb.ToString();
            }

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




/*
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

private void sanitizePhotoDateClick(object sender, EventArgs e)
{
    try
    {
        string targetDirectoryString = folderPickerTextBox.Text;
        DirectoryInfo targetDirectoryInfo = new DirectoryInfo(targetDirectoryString);

        foreach (FileInfo file in targetDirectoryInfo.GetFiles())
        {
            string newFileName = sanitizeCameraDate(Path.GetFileNameWithoutExtension(file.Name), file.DirectoryName, file.Extension);
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

private string sanitizeCameraDate(string originalName, string originalPath, string originalExtension)
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
    splitDate[0] = splitDate[0].Replace("~", "");
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

private void sanitizeInstaDateClick(object sender, EventArgs e)
{
    try
    {
        string targetDirectoryString = folderPickerTextBox.Text;
        DirectoryInfo targetDirectoryInfo = new DirectoryInfo(targetDirectoryString);

        foreach (FileInfo file in targetDirectoryInfo.GetFiles())
        {
            if (incrementer >= 10)
            {
                incrementer = 0;
            }
            string newFileName = sanitizeInstaDate(Path.GetFileNameWithoutExtension(file.Name), file.DirectoryName, file.Extension);
            file.MoveTo(newFileName);
        }
    }
    catch (Exception ex)
    {
        if (ex.Message == "Cannot create a file when that file already exists.\r\n")
        {
            duplicates = true;

        }

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

private string sanitizeInstaDate(string originalName, string originalPath, string originalExtension)
{
    StringBuilder sb = new StringBuilder(originalPath);
    sb.Append('\\');

    string[] splitDate = Regex.Split(originalName, " - ");
    ;
    bool match = true;

    while (match == true)
    {
        splitDate[0] = splitDate[0].PadRight(30, '0');
        splitDate[0] = splitDate[0].Substring(0, 18); // trim trailing characters
        splitDate[0] = $"{splitDate[0]}{incrementer}";
        match = File.Exists(splitDate[0]);
        incrementer++;
    }

    sb.Append(splitDate[0]);

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
        // TO DO: sanitize in context menu
        // TO DO: allow insertion and deletion immediately before or after string fragment
        // TO DO: allow me to batch rename all with mising '- ', keep them all in the same order in memory, THEN batch rename, or rewrite them with their existing number
        // TO DO: replace with
    }
}
*/