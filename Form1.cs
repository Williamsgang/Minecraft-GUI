using Newtonsoft.Json;
using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace TestWinForm
{
    public partial class MainForm : Form
    {
        private string filePath;
        private EventLog eventLog;
        public MainForm()
        {
            InitializeComponent();
            
            /*EventLog.CreateEventSource("Console", "console");
            eventLog = new EventLog();
            eventLog.Source = "Console";
            eventLog.WriteEntry("Event log created");*/
        }

        private void SetFilePath(string path)
        {
            this.filePath = path;
        }

        private void treeViewBtn_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog dialog = new FolderBrowserDialog();
            if (dialog.ShowDialog() != DialogResult.OK) { return; }

            fileTree.Nodes.Clear();

            var node = TraverseDirectory(dialog.SelectedPath);
            SetFilePath(dialog.SelectedPath);
            node.Text = Path.GetFileName(node.Text);

            fileTree.Nodes.Add(node);

            node.Expand();
            eventLog.WriteEntry("Treeview Btn clicked");
        }

        private TreeNode TraverseDirectory(string path)
        {
            TreeNode result = new TreeNode(path);

            foreach (var subdirectory in Directory.GetDirectories(path))
            {
                var node = TraverseDirectory(subdirectory);
                node.Text = Path.GetFileName(node.Text);
                result.Nodes.Add(node);
            }

            foreach (var subfile in Directory.GetFiles(path))
            {
                var node = Path.GetFileName(subfile);
                result.Nodes.Add(node);
            }

            return result;
        }

        private string serverFile;

        private void fileTree_AfterSelect(object sender, TreeViewEventArgs e)
        {
            TreeNode treeNode = fileTree.SelectedNode;
            string fullPath = filePath + @"\" + Path.GetFileName(treeNode.FullPath);

            StringBuilder stringBuilder = new StringBuilder();

            string[] allowedExtensions = { ".txt", ".properties", ".log", ".bat", ".log.gz", ".json" };
            string fileExtension = Path.GetExtension(fullPath);

            switch ((e.Action))
            {
                case TreeViewAction.ByKeyboard:
                    if (File.Exists(fullPath))
                    {
                        if (allowedExtensions.Any(ext => string.Equals(fileExtension, ext, StringComparison.OrdinalIgnoreCase)))
                        {
                            using (StreamReader file = new StreamReader(fullPath))
                            {
                                while (!file.EndOfStream)
                                {
                                    stringBuilder.Append(file.ReadLine());
                                }
                                fileOuput.Text = stringBuilder.ToString();
                            }
                            fileOuput.Focus();
                        }
                        else if (string.Equals(fileExtension, ".jar", StringComparison.OrdinalIgnoreCase))
                        {
                            serverFile = fullPath;
                            fileOuput.Text += serverFile;
                        }
                        else
                        {
                            fileOuput.Text = "Invalid file extension. Expected: "
                                + allowedExtensions[0] + ", "
                                + allowedExtensions[1] + ", "
                                + allowedExtensions[2] + ", "
                                + allowedExtensions[3] + ", "
                                + allowedExtensions[4] + ", "
                                + allowedExtensions[5] + ".";
                            fileOuput.Focus();
                        }
                    }
                    else
                    {
                        fileOuput.Text = "File does not exist";
                        fileOuput.Focus();
                    }
                    break;
                case TreeViewAction.ByMouse:
                    if (File.Exists(fullPath))
                    {
                        if (allowedExtensions.Any(ext => string.Equals(fileExtension, ext, StringComparison.OrdinalIgnoreCase)))
                        {
                            using (StreamReader file = new StreamReader(fullPath))
                            {
                                while (!file.EndOfStream)
                                {
                                    stringBuilder.Append(file.ReadLine() + "\n");
                                }
                                fileOuput.Text = stringBuilder.ToString();
                            }
                            fileOuput.Focus();
                        }
                        else if (string.Equals(fileExtension, ".jar", StringComparison.OrdinalIgnoreCase))
                        {
                            serverFile = fullPath;
                            fileOuput.Text += serverFile;
                        }
                        else
                        {
                            fileOuput.Text = "Invalid file extension. Expected: "
                                + allowedExtensions[0] + ", "
                                + allowedExtensions[1] + ", "
                                + allowedExtensions[2] + ", "
                                + allowedExtensions[3] + ", "
                                + allowedExtensions[4] + ", "
                                + allowedExtensions[5] + ".";
                            fileOuput.Focus();
                        }
                    }
                    else
                    {
                        fileOuput.Text = "File does not exist";
                        fileOuput.Focus();
                    }
                    break;
            }

        }

        private Process javaProcess = new Process();

        private void cmdInput_Click(object sender, EventArgs e)
        {

            using (StreamWriter javaStreamWriter = javaProcess.StandardInput)
            using (StreamReader javaStreamReader = javaProcess.StandardOutput)
            {
                string input = cmdInputText.Text;
                javaStreamWriter.WriteLine(input);

                string output = javaStreamReader.ReadLine();
                cmdOuput.Text = output;
            }
        }

        private void serverStartBtn_Click(object sender, EventArgs e)
        {

            javaProcess.StartInfo.FileName = "java.exe";
            javaProcess.StartInfo.Arguments = ""; // java args
            javaProcess.StartInfo.UseShellExecute = false;
            javaProcess.StartInfo.RedirectStandardInput = true;
            javaProcess.StartInfo.RedirectStandardOutput = true;
            javaProcess.StartInfo.CreateNoWindow = false;

            javaProcess.Start();

            using (StreamWriter javaStreamWriter = javaProcess.StandardInput)
            using (StreamReader javaStreamReader = javaProcess.StandardOutput)
            {
                string output = javaStreamReader.ReadLine();
                cmdOuput.Text = output;
            }

            javaProcess.WaitForExit();
        }

        private void cmdInputText_TextChanged(object sender, EventArgs e)
        {

        }

        private void fileOuput_TextChanged(object sender, EventArgs e)
        {

        }

        private void createFileBtn_Click(object sender, EventArgs e)
        {
            CreateFileForm createFileForm = new CreateFileForm();
            createFileForm.ShowDialog();

            string file = createFileForm.file;

        }

        private void saveFileBtn_Click(object sender, EventArgs e)
        {

        }

        private void MainForm_Load(object sender, EventArgs e)
        {

        }
    }

    public partial class CreateFileForm : Form
    {
        public string file;

        private string fileName;
        private string FileName
        {
            get { return fileName; }
            set { fileName = value; }
        }

        private string[] fileExtensions = { ".txt", ".properties", ".bat" };

        private string fileExtension;
        private string FileExtension
        {
            get { return fileExtension; }
            set { fileExtension = value; }
        }

        private bool Checked { get; set; }

        // CreateFileForm properties
        private TextBox fileNameTextBox;
        private Button saveFileInfo;
        private Button closeFormBtn;
        private RadioButton textFile;
        private RadioButton propertiesFile;
        private RadioButton batFile;

        public CreateFileForm()
        {
            // Initializing properties
            this.saveFileInfo = new Button();
            this.closeFormBtn = new Button();
            this.textFile = new RadioButton();
            this.batFile = new RadioButton();
            this.propertiesFile = new RadioButton();
            this.fileNameTextBox = new TextBox();

            // textFile radiobutton congfigs
            this.textFile.Enabled = true;
            this.textFile.Text = ".txt";
            this.textFile.Size = new System.Drawing.Size(50, 20);
            this.textFile.Location = new System.Drawing.Point(20, 20);
            this.textFile.Checked = false;
            this.textFile.CheckedChanged += new System.EventHandler(this.textFile_CheckChanged);
            this.Controls.Add(textFile);

            // batFile radiobutton congfigs
            this.batFile.Enabled = true;
            this.batFile.Text = ".bat";
            this.batFile.Size = new System.Drawing.Size(50, 20);
            this.batFile.Location = new System.Drawing.Point(20, 40);
            this.batFile.Checked = false;
            this.batFile.CheckedChanged += new System.EventHandler(this.batFile_CheckChanged);
            this.Controls.Add(batFile);

            // propertiesFile radiobutton congfigs
            this.propertiesFile.Enabled = true;
            this.propertiesFile.Text = ".properties";
            this.propertiesFile.Size = new System.Drawing.Size(100, 20);
            this.propertiesFile.Location = new System.Drawing.Point(20, 60);
            this.propertiesFile.Checked = false;
            this.propertiesFile.CheckedChanged += new System.EventHandler(this.propertiesFile_CheckChanged);
            this.Controls.Add(propertiesFile);

            // fileName text box configs
            this.fileNameTextBox.Enabled = true;
            this.fileNameTextBox.Size = new System.Drawing.Size(Width, Height);
            this.fileNameTextBox.Location = new System.Drawing.Point(20, 80);
            this.Controls.Add(fileNameTextBox);

            // saveFileInfo button configs
            this.saveFileInfo.Enabled = true;
            this.saveFileInfo.Text = "Save File";
            this.saveFileInfo.Location = new System.Drawing.Point(20, 100);
            this.saveFileInfo.Click += new System.EventHandler(this.saveFileInfo_Click);
            this.Controls.Add(saveFileInfo);

            // closeFormBtn button configs
            this.closeFormBtn.Enabled = true;
            this.closeFormBtn.Text = "Close Form";
            this.closeFormBtn.Location = new System.Drawing.Point(100, 100);
            this.closeFormBtn.Click += new System.EventHandler(this.closeFormBtn_Click);
            this.Controls.Add(closeFormBtn);

            // CreateFileForm form configs
            this.Text = "New File";
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(720, 360);
        }

        private void saveFileInfo_Click(object sender, EventArgs e)
        {
            string tempFileName = fileNameTextBox.Text;
            string fileExtension = this.fileExtension;

            string fileName = tempFileName + fileExtension;

            this.file = fileName;
            Console.WriteLine($"SaveFileInfo Btn has been clicked. File: {this.file}");

            if (!File.Exists(this.file))
            {
                File.Create(@"\settings\" + this.file);
                Console.WriteLine("A file has been created at : " + File.Exists(file));
                this.Close();
            } else
            {
                Console.WriteLine("A file with this name has been created");
                fileNameTextBox.Text = "File exists. Enter a new file name/type";
            }
        }

        private void closeFormBtn_Click(object sender, EventArgs e)
        {
            this.Close();
            Console.WriteLine("closeFormBtn clicked");
        }

        private void textFile_CheckChanged(object sender, EventArgs e)
        {
            if (this.textFile.Checked == true)
            {
                this.fileExtension = fileExtensions[0];
                Console.WriteLine($"textFile radiobutton has been updated. File extension: {this.fileExtension}");
            }
        }

        private void propertiesFile_CheckChanged(object sender, EventArgs e)
        {
            if (this.propertiesFile.Checked == true)
            {
                this.fileExtension = fileExtensions[1];
                Console.WriteLine($"propertiesFile radiobutton has been updated. File extension: {this.fileExtension}");
            }
        }

        private void batFile_CheckChanged(object sender, EventArgs e)
        {
            if (this.batFile.Checked == true)
            {
                this.fileExtension = fileExtensions[2];
                Console.WriteLine($"batFile radiobutton has been updated. File extension: {this.fileExtension}");
            }
        }
    }
}
