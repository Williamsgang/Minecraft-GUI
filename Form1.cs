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
        // Properties for global assets
        private string filePath;
        private AppFolderFiles appFolderFiles;
        private string serverFile;

        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {

            appFolderFiles = new AppFolderFiles();

            appFolderFiles.createApplicationFolders();
            appFolderFiles.createApplicationFiles();

            Console.WriteLine("Application has loaded.");
        }

        private void SetFilePath(string path)
        {
            this.filePath = path;
        }

        private void serverFilesBtn_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog dialog = new FolderBrowserDialog();
            if (dialog.ShowDialog() != DialogResult.OK) { return; }

            serverFilesTree.Nodes.Clear();

            var node = TraverseDirectory(dialog.SelectedPath);
            SetFilePath(dialog.SelectedPath);
            node.Text = Path.GetFileName(node.Text);

            serverFilesTree.Nodes.Add(node);

            node.Expand();
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

        private void serverFilesTree_AfterSelect(object sender, TreeViewEventArgs e)
        {
            TreeNode treeNode = serverFilesTree.SelectedNode;
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
                        fileOuput.Text = fullPath;
                        // fileOuput.Text = "File does not exist";
                        // fileOuput.Focus();
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
                        fileOuput.Text = fullPath;
                        // fileOuput.Text = "File does not exist";
                        // fileOuput.Focus();
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
            appFolderFiles = new AppFolderFiles();

            StringBuilder stringBuilder = new StringBuilder();
            string jsonToString = "";

            using (StreamReader sr = new StreamReader(appFolderFiles.getSettingsFile()))
            {
                while (!sr.EndOfStream)
                {
                    stringBuilder.Append(sr.ReadLine());
                }
                jsonToString = stringBuilder.ToString();
            }
            
            JsonTextReader jsonReader = new JsonTextReader(new StringReader(jsonToString));
            string java_params = "";
            
            while (jsonReader.Read())
            {
                if (jsonReader.Value != null)
                {
                    Console.WriteLine("Token: {0}, Value: {1}", jsonReader.TokenType, jsonReader.Value);
                }
                else
                {
                    Console.WriteLine("Token: {0}", jsonReader.TokenType);
                }

                if (jsonReader.TokenType == JsonToken.String) 
                {
                    java_params += jsonReader.Value + " ";
                }
            }
            Console.WriteLine();
            Console.WriteLine("-jar " + java_params);

            javaProcess.StartInfo.FileName = "java.exe";
            javaProcess.StartInfo.Arguments = "-jar " + java_params; // java args
            javaProcess.StartInfo.UseShellExecute = false;
            javaProcess.StartInfo.RedirectStandardInput = true;
            javaProcess.StartInfo.RedirectStandardOutput = true;
            javaProcess.StartInfo.CreateNoWindow = false;

            // javaProcess.Start();

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
            CreateFileForm createFileForm = new CreateFileForm(this.serverFile);
            createFileForm.ShowDialog();

            string file = createFileForm.file;
            Console.WriteLine("You have created a new file: " + file);

        }

        private void saveFileBtn_Click(object sender, EventArgs e)
        {

        }
    }

    public partial class CreateFileForm : Form
    {
        public string file;
        private string fileName;
        private string[] fileExtensions = { ".txt", ".properties", ".bat" };
        private string fileExtension;

        // CreateFileForm properties
        private TextBox fileNameTextBox;
        private Button saveFileInfo;
        private Button closeFormBtn;
        private RadioButton textFile;
        private RadioButton propertiesFile;
        private RadioButton batFile;
        private string filePath;
        private AppFolderFiles appFolderFiles;

        public CreateFileForm(string filePath)
        {
            this.filePath = filePath;

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

            appFolderFiles = new AppFolderFiles();

            string fileName = tempFileName + fileExtension;

            this.file = fileName;
            Console.WriteLine($"SaveFileInfo Btn has been clicked. File: {this.file}");

            if (!File.Exists(this.file))
            {
                File.Create(appFolderFiles.getSettingsDir() + this.file);
                Console.WriteLine("A file has been created at : " + File.Exists(file));
                this.Close();
            }
            else
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

    public partial class AppFolderFiles
    {
        // Config property values 
        private string appFolder, appDir, settingsDir, serversDir, 
            configDir, configDefaultSettings, settingsFile, defaultSettings;
        
        // Config default files
        private string[] configFiles;

        // Returns the Desktop folder path
        public string getDesktopDir()
        {
            this.appFolder = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            return this.appFolder;
        }

        // Returns the application folder path
        public string getAppDir()
        {
            this.appDir = Path.Combine(getDesktopDir(), @"MinecraftGUI\");
            return this.appDir;
        }

        // Returns the settings folder path
        public string getSettingsDir()
        {
            this.settingsDir = Path.Combine(getAppDir(), @"settings\");
            return this.settingsDir;
        }

        // Returns the servers folder path
        public string getServersDir()
        {
            this.serversDir = Path.Combine(getAppDir(), @"servers\");
            return this.serversDir;
        }

        // Returns the server settings file
        public string getSettingsFile()
        {
            this.settingsFile = Path.Combine(getSettingsDir(), @"settings.json");
            return this.settingsFile;
        }

        // Returns the config folder path
        public string getConfigDir()
        {
            this.configDir = Path.Combine(getAppDir(), @"config\");
            return this.configDir;
        }

        // Returns the config default settings file
        public string getConfigDefaultSettings()
        {
            this.configDefaultSettings = Path.Combine(getConfigDir(), @"defaultServerSettings.json");
            return this.configDefaultSettings;
        }

        // Creates the application folders
        public void createApplicationFolders()
        {
            // Checks for application folder
            if (!Directory.Exists(getAppDir()))
            {
                Directory.CreateDirectory(getAppDir());
                Console.WriteLine(getAppDir());
            }
            else
            {
                Console.WriteLine("Application folder already exists.");
            }

            // Checks for server folders
            if (!Directory.Exists(getServersDir()))
            {
                Directory.CreateDirectory(getServersDir());
                Console.WriteLine(getServersDir());
            }
            else
            {
                Console.WriteLine("Server folder already exists.");
            }
            
            // Checks for settings folder
            if (!Directory.Exists(getSettingsDir()))
            {
                Directory.CreateDirectory(getSettingsDir());
                Console.WriteLine(getSettingsDir());
            }
            else
            {
                Console.WriteLine("Settings folder already exists.");
            }

            // Checks for config folder
            if (!Directory.Exists(getConfigDir()))
            {
                Directory.CreateDirectory(getConfigDir());
                Console.WriteLine(getConfigDir());
            }
            else
            {
                Console.WriteLine("Config folder already exists.");
            }
        }

        // Creates the application files
        public void createApplicationFiles()
        {
            if (!File.Exists(getConfigDefaultSettings()))
            {
                createConfigFiles();
                Console.WriteLine(getConfigDefaultSettings());
            }
            else
            {
                Console.WriteLine("Config default settings file already exists");
            }

            if (!File.Exists(getSettingsFile()))
                {
                    createSettingFiles();
                    Console.WriteLine(getSettingsFile());
                }
                else
                {
                    Console.WriteLine("Settings file already exists");
                }
        }

        public void createConfigFiles()
        {
            string defaultServerSettings = "{\r\n \"server_jar\": \"server.jar\",\r\n \"max_ram\": \"-Xmx4G\",\r\n \"min_ram\": \"-Xms1G\",\r\n \"java_params\": \"\"\r\n }";
            File.WriteAllText((getConfigDefaultSettings()), defaultServerSettings);
        }

        public void createSettingFiles()
        {
            string copyText = File.ReadAllText(getConfigDefaultSettings());
            File.WriteAllText(getSettingsFile(), copyText);
        }
    }
}
