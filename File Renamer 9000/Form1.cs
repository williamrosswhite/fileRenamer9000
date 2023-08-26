using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;

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
                string newFileName = NextTitle('b' + Path.GetFileNameWithoutExtension(file.Name), file.DirectoryName, file.Extension);
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
                if (ex is DirectoryNotFoundException || ex is ArgumentException)
                {
                    MessageBox.Show("You must select a valid filepath please");
                }

                if (ex is IOException)
                {
                    string targetDirectoryString = folderPickerTextBox.Text;
                    ReprocessForExistingFileNames(targetDirectoryString);
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
                    if (incrementer >= 99)
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
                if (incrementer >= 99)
                {
                    incrementer = 0;
                }

                splitDate[0] = splitDate[0].PadRight(30, '0');
                splitDate[0] = splitDate[0].Substring(0, 16); // trim trailing characters
                splitDate[0] = $"{splitDate[0]}{incrementer}";
                match = File.Exists($"{originalPath}\\{splitDate[0]}{originalExtension}");
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
                    if (incrementer >= 99)
                    {
                        incrementer = 0;
                    }

                    newDateFileNameString = $"{ newDateFileNameString.Substring(0, 16) }{ incrementer }";
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

        private void ScrapeCleaner_Click(object sender, EventArgs e)
        {
            try
            {
                string targetDirectoryString = folderPickerTextBox.Text;
                DirectoryInfo targetDirectoryInfo = new DirectoryInfo(targetDirectoryString);
                DirectoryInfo[] directoryArray = targetDirectoryInfo.GetDirectories("*.*", SearchOption.TopDirectoryOnly);

                foreach (DirectoryInfo dirInfo in directoryArray)
                {
                    Thread t = new Thread(() => threadableProcesser(dirInfo));
                    t.Start();
                }
            }
            catch (Exception ex)
            {
                if (ex is DirectoryNotFoundException || ex is ArgumentException)
                {
                    MessageBox.Show("You must select a valid filepath please");
                }
            }

            MessageBox.Show("Operation Complete");
        }

        private void threadableProcesser(DirectoryInfo dirInfo)
        {
            DirectoryInfo[] userFolderArray = null;
            userFolderArray = dirInfo.GetDirectories("*.*", SearchOption.AllDirectories);

            DirectoryInfo thumbs = userFolderArray.FirstOrDefault(f => f.Name == ".thumb.stogram");
            if (thumbs != null)
            {
                thumbs.Delete(true);
            }

            DirectoryInfo highlights = userFolderArray.FirstOrDefault(f => f.Name == "highlights");
            DirectoryInfo reels = userFolderArray.FirstOrDefault(f => f.Name == "reels");
            DirectoryInfo story = userFolderArray.FirstOrDefault(f => f.Name == "story");
            DirectoryInfo tagged = userFolderArray.FirstOrDefault(f => f.Name == "tagged");

            if (highlights != null)
            {
                List<string> highligthsFolder = Directory.GetFiles(highlights.FullName, "*.*", SearchOption.AllDirectories).ToList();
                foreach (string file in highligthsFolder)
                {
                    try
                    {
                        FileInfo uFile = new FileInfo(file);
                        if (new FileInfo(dirInfo + "\\" + uFile.Name).Exists == false)
                        {
                            var match = File.Exists($"{dirInfo.FullName}\\{uFile.Name}");

                            string fileString = uFile.Name;

                            var folderString = dirInfo.FullName.ToString();

                            while (match == true)
                            {
                                if (incrementer >= 99)
                                {
                                    incrementer = 0;
                                }

                                folderString = $"{ uFile.Name.Substring(0, 49) }{ incrementer }";
                                incrementer++;

                                match = File.Exists($"{folderString}\\{fileString}");
                            }

                            File.Move(uFile.ToString(), $"{folderString}\\{fileString}");
                        }
                    }
                    catch(Exception ex)
                    {
                        continue;
                    }

                }
            }

            if (reels != null)
            {
                List<string> highligthsFolder = Directory.GetFiles(reels.FullName, "*.*", SearchOption.AllDirectories).ToList();
                foreach (string file in highligthsFolder)
                {
                    FileInfo uFile = new FileInfo(file);
                    if (new FileInfo(dirInfo + "\\" + uFile.Name).Exists == false)
                    {
                        var match = File.Exists($"{dirInfo.FullName}\\{uFile.Name}");

                        string fileString = uFile.Name;

                        var folderString = dirInfo.FullName.ToString();

                        while (match == true)
                        {
                            if (incrementer >= 99)
                            {
                                incrementer = 0;
                            }

                            folderString = $"{ uFile.Name.Substring(0, 49) }{ incrementer }";
                            incrementer++;

                            match = File.Exists($"{folderString}\\{fileString}");
                        }

                        File.Move(uFile.ToString(), $"{folderString}\\{fileString}");
                    }
                }
            }

            if (story != null)
            {
                List<string> highligthsFolder = Directory.GetFiles(story.FullName, "*.*", SearchOption.AllDirectories).ToList();
                foreach (string file in highligthsFolder)
                {
                    FileInfo uFile = new FileInfo(file);
                    if (new FileInfo(dirInfo + "\\" + uFile.Name).Exists == false)
                    {
                        try
                        {
                            var match = File.Exists($"{dirInfo.FullName}\\{uFile.Name}");

                            string fileString = uFile.Name;

                            var folderString = dirInfo.FullName.ToString();

                            while (match == true)
                            {
                                if (incrementer >= 99)
                                {
                                    incrementer = 0;
                                }

                                folderString = $"{ uFile.Name.Substring(0, 49) }{ incrementer }";
                                incrementer++;

                                match = File.Exists($"{folderString}\\{fileString}");
                            }

                            File.Move(uFile.ToString(), $"{folderString}\\{fileString}");

                        } 

                        catch (Exception ex)
                        {
                            return;
                        }
                    }
                }
            }

            if (tagged != null)
            {
                List<string> highligthsFolder = Directory.GetFiles(tagged.FullName, "*.*", SearchOption.AllDirectories).ToList();
                foreach (string file in highligthsFolder)
                {
                    FileInfo uFile = new FileInfo(file);
                    if (new FileInfo(dirInfo + "\\" + uFile.Name).Exists == false)
                    {
                        var match = File.Exists($"{dirInfo.FullName}\\{uFile.Name}");

                        string fileString = uFile.Name;

                        var folderString = dirInfo.FullName.ToString();

                        while (match == true)
                        {
                            if (incrementer >=99)
                            {
                                incrementer = 0;
                            }

                            folderString = $"{ uFile.Name.Substring(0, 49) }{ incrementer }";
                            incrementer++;

                            match = File.Exists($"{folderString}\\{fileString}");
                        }

                        File.Move(uFile.ToString(), $"{folderString}\\{fileString}");
                    }
                }
            }

            foreach (FileInfo file in dirInfo.GetFiles())
            {
                string newFileName = sanitizeInstaDate(Path.GetFileNameWithoutExtension(file.Name), file.DirectoryName, file.Extension);
                file.MoveTo(newFileName);
            }
        }

        private void fix_missing_dash_button_Click(object sender, EventArgs e)
        {
            string targetDirectoryString = folderPickerTextBox.Text;
            DirectoryInfo targetDirectoryInfo = new DirectoryInfo(targetDirectoryString);

            numStart = (int)startNumericUpDown.Value;

            var newFileNameLinkedList = new LinkedList<string>();
            filePathLinkedList.AddLast(targetDirectoryInfo);

            try
            {
                foreach (FileInfo file in targetDirectoryInfo.GetFiles())
                {
                    newFileNameLinkedList.AddLast(file.FullName);

                    string newName = file.Name.Insert(numStart, " - ");
                    string newFullPath = $"{file.Directory}\\{newName}";
                    Console.WriteLine(file.Name);
                    file.MoveTo(newFullPath);
                }

                linkedListOfFileNameLinkedLists.AddLast(newFileNameLinkedList);
            }
            catch (Exception ex)
            {
                if (ex is DirectoryNotFoundException || ex is ArgumentException)
                {
                    MessageBox.Show("You must select a valid filepath please");
                }
            }
        }

        private void delete_all_button_Click(object sender, EventArgs e)
        {
            string targetDirectoryString = folderPickerTextBox.Text;
            DirectoryInfo targetDirectoryInfo = new DirectoryInfo(targetDirectoryString);
            DirectoryInfo[] directoryArray = targetDirectoryInfo.GetDirectories("*.*", SearchOption.TopDirectoryOnly);

            try
            {
                foreach (DirectoryInfo directoryInfo in directoryArray)
                {
                    foreach (FileInfo file in directoryInfo.GetFiles())
                    {
                        Console.WriteLine("deleting " + file.Name);
                        File.Delete(file.FullName);
                    }
                }
            }
            catch (Exception ex)
            {
                if (ex is DirectoryNotFoundException || ex is ArgumentException)
                {
                    MessageBox.Show("You must select a valid filepath please");
                }
            }

        }

        private void enbiggify_Click(object sender, EventArgs e)
        {
            try
            {
                string targetDirectoryString = folderPickerTextBox.Text;
                DirectoryInfo targetDirectoryInfo = new DirectoryInfo(targetDirectoryString);
                enbiggifier(targetDirectoryInfo);
            }
            catch (Exception ex)
            {
                if (ex is DirectoryNotFoundException || ex is ArgumentException)
                {
                    MessageBox.Show("You must select a valid filepath please");
                }
            }
        }

        private void enbiggifier(DirectoryInfo dirInfo)
        {
            List<string> imageFiles = Directory.GetFiles(dirInfo.FullName, "*.*", SearchOption.AllDirectories).ToList();
            foreach (string file in imageFiles)
            {
                try
                {
                    System.Drawing.Image currImage = System.Drawing.Image.FromFile(file);
                    FileInfo fileInfo = new FileInfo(file);
                    Bitmap bmp = new Bitmap(file);

                    if(bmp.Height < 1920 || bmp.Width < 1080)
                    {
                        var enbiggifiedImage = enbiggifyImage(bmp);
                        enbiggifiedImage.Save(fileInfo.Name, ImageFormat.Bmp);
                    }
                    bmp.Dispose();
                    currImage.Dispose();
                }
                catch (Exception)
                {
                    continue;
                }
            }

            MessageBox.Show("Operation Complete");
        }

        private Bitmap enbiggifyImage(Bitmap image)
        {
            int newHeight = image.Height;
            int newWidth = image.Width;


            do
            {
                newHeight = newHeight * 2;
                newWidth = newWidth *2;
            } 
            while (newHeight < 1920 || newWidth < 1080);

            Console.WriteLine(newHeight);
            Console.WriteLine(newWidth);

            var destRect = new Rectangle(0, 0, newWidth, newHeight);
            var destImage = new Bitmap(newWidth, newHeight, PixelFormat.Format16bppRgb555);

            destImage.SetResolution(image.HorizontalResolution, image.VerticalResolution);

            using (var graphics = Graphics.FromImage(destImage))
            {
                graphics.CompositingMode = CompositingMode.SourceCopy;
                graphics.CompositingQuality = CompositingQuality.HighQuality;
                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphics.SmoothingMode = SmoothingMode.HighQuality;
                graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;

                using (var wrapMode = new ImageAttributes())
                {
                    wrapMode.SetWrapMode(WrapMode.TileFlipXY);
                    graphics.DrawImage(image, destRect, 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, wrapMode);
                }

                graphics.Dispose();
            }

            return destImage;
        }



        // TO DO: create a replace string function
        // TO DO: ability to open from folder context menu with folder path pre loaded
        // TO DO: allow '- tag' to persist on sanitize
        // TO DO: allow activation on pressing Enter
        // TO DO: create a function to auto process across an entire file structure
        // TO DO: sanitize in context menu
        // TO DO: allow insertion and deletion immediately before or after string fragment
        // TO DO: allow me to batch rename all with mising '- ', keep them all in the same order in memory, THEN batch rename, or rewrite them with their existing number
        // TO DO: replace with
    }
}