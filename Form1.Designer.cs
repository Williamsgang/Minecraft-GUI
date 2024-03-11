namespace TestWinForm
{
    partial class MainForm
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
            this.fileOuput = new System.Windows.Forms.TextBox();
            this.cmdOuput = new System.Windows.Forms.TextBox();
            this.serverFilesTree = new System.Windows.Forms.TreeView();
            this.serverFilesBtn = new System.Windows.Forms.Button();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.serverFiles = new System.Windows.Forms.TabPage();
            this.createFileBtn = new System.Windows.Forms.Button();
            this.saveFileBtn = new System.Windows.Forms.Button();
            this.cmdOutput = new System.Windows.Forms.TabPage();
            this.cmdInput = new System.Windows.Forms.Button();
            this.cmdInputText = new System.Windows.Forms.TextBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.serverStartBtn = new System.Windows.Forms.Button();
            this.tabControl1.SuspendLayout();
            this.serverFiles.SuspendLayout();
            this.cmdOutput.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // fileOuput
            // 
            this.fileOuput.AcceptsTab = true;
            this.fileOuput.HideSelection = false;
            this.fileOuput.Location = new System.Drawing.Point(8, 7);
            this.fileOuput.Margin = new System.Windows.Forms.Padding(4);
            this.fileOuput.Multiline = true;
            this.fileOuput.Name = "fileOuput";
            this.fileOuput.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.fileOuput.Size = new System.Drawing.Size(1172, 853);
            this.fileOuput.TabIndex = 1;
            this.fileOuput.TextChanged += new System.EventHandler(this.fileOuput_TextChanged);
            // 
            // cmdOuput
            // 
            this.cmdOuput.Location = new System.Drawing.Point(8, 7);
            this.cmdOuput.Margin = new System.Windows.Forms.Padding(4);
            this.cmdOuput.Multiline = true;
            this.cmdOuput.Name = "cmdOuput";
            this.cmdOuput.ReadOnly = true;
            this.cmdOuput.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.cmdOuput.Size = new System.Drawing.Size(1172, 854);
            this.cmdOuput.TabIndex = 0;
            // 
            // serverFilesTree
            // 
            this.serverFilesTree.Location = new System.Drawing.Point(4, 4);
            this.serverFilesTree.Margin = new System.Windows.Forms.Padding(4);
            this.serverFilesTree.Name = "serverFilesTree";
            this.serverFilesTree.Size = new System.Drawing.Size(364, 933);
            this.serverFilesTree.TabIndex = 2;
            this.serverFilesTree.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.serverFilesTree_AfterSelect);
            // 
            // serverFilesBtn
            // 
            this.serverFilesBtn.Location = new System.Drawing.Point(4, 946);
            this.serverFilesBtn.Margin = new System.Windows.Forms.Padding(4);
            this.serverFilesBtn.Name = "serverFilesBtn";
            this.serverFilesBtn.Size = new System.Drawing.Size(100, 28);
            this.serverFilesBtn.TabIndex = 3;
            this.serverFilesBtn.Text = "Server Files";
            this.serverFilesBtn.UseVisualStyleBackColor = true;
            this.serverFilesBtn.Click += new System.EventHandler(this.serverFilesBtn_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.serverFiles);
            this.tabControl1.Controls.Add(this.cmdOutput);
            this.tabControl1.Location = new System.Drawing.Point(378, 4);
            this.tabControl1.Margin = new System.Windows.Forms.Padding(4);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1200, 934);
            this.tabControl1.TabIndex = 4;
            // 
            // serverFiles
            // 
            this.serverFiles.Controls.Add(this.createFileBtn);
            this.serverFiles.Controls.Add(this.saveFileBtn);
            this.serverFiles.Controls.Add(this.fileOuput);
            this.serverFiles.Location = new System.Drawing.Point(4, 25);
            this.serverFiles.Margin = new System.Windows.Forms.Padding(4);
            this.serverFiles.Name = "serverFiles";
            this.serverFiles.Padding = new System.Windows.Forms.Padding(4);
            this.serverFiles.Size = new System.Drawing.Size(1192, 905);
            this.serverFiles.TabIndex = 0;
            this.serverFiles.Text = "Server Files";
            this.serverFiles.UseVisualStyleBackColor = true;
            // 
            // createFileBtn
            // 
            this.createFileBtn.AutoSize = true;
            this.createFileBtn.Location = new System.Drawing.Point(9, 866);
            this.createFileBtn.Margin = new System.Windows.Forms.Padding(4);
            this.createFileBtn.Name = "createFileBtn";
            this.createFileBtn.Size = new System.Drawing.Size(100, 31);
            this.createFileBtn.TabIndex = 3;
            this.createFileBtn.Text = "Create File";
            this.createFileBtn.UseVisualStyleBackColor = true;
            this.createFileBtn.Click += new System.EventHandler(this.createFileBtn_Click);
            // 
            // saveFileBtn
            // 
            this.saveFileBtn.AutoSize = true;
            this.saveFileBtn.Location = new System.Drawing.Point(116, 866);
            this.saveFileBtn.Margin = new System.Windows.Forms.Padding(4);
            this.saveFileBtn.Name = "saveFileBtn";
            this.saveFileBtn.Size = new System.Drawing.Size(99, 32);
            this.saveFileBtn.TabIndex = 2;
            this.saveFileBtn.Text = "Save File";
            this.saveFileBtn.UseVisualStyleBackColor = true;
            this.saveFileBtn.Click += new System.EventHandler(this.saveFileBtn_Click);
            // 
            // cmdOutput
            // 
            this.cmdOutput.Controls.Add(this.cmdOuput);
            this.cmdOutput.Controls.Add(this.cmdInput);
            this.cmdOutput.Controls.Add(this.cmdInputText);
            this.cmdOutput.Location = new System.Drawing.Point(4, 25);
            this.cmdOutput.Margin = new System.Windows.Forms.Padding(4);
            this.cmdOutput.Name = "cmdOutput";
            this.cmdOutput.Padding = new System.Windows.Forms.Padding(4);
            this.cmdOutput.Size = new System.Drawing.Size(1192, 905);
            this.cmdOutput.TabIndex = 1;
            this.cmdOutput.Text = "Cmd Output";
            this.cmdOutput.UseVisualStyleBackColor = true;
            // 
            // cmdInput
            // 
            this.cmdInput.Location = new System.Drawing.Point(1081, 869);
            this.cmdInput.Margin = new System.Windows.Forms.Padding(4);
            this.cmdInput.Name = "cmdInput";
            this.cmdInput.Size = new System.Drawing.Size(100, 25);
            this.cmdInput.TabIndex = 2;
            this.cmdInput.Text = "CMD";
            this.cmdInput.UseVisualStyleBackColor = true;
            this.cmdInput.Click += new System.EventHandler(this.cmdInput_Click);
            // 
            // cmdInputText
            // 
            this.cmdInputText.Location = new System.Drawing.Point(8, 870);
            this.cmdInputText.Margin = new System.Windows.Forms.Padding(4);
            this.cmdInputText.Name = "cmdInputText";
            this.cmdInputText.Size = new System.Drawing.Size(1064, 22);
            this.cmdInputText.TabIndex = 1;
            this.cmdInputText.TextChanged += new System.EventHandler(this.cmdInputText_TextChanged);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.AutoSize = true;
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 23.66231F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 76.33769F));
            this.tableLayoutPanel1.Controls.Add(this.tabControl1, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.serverFilesBtn, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.serverFilesTree, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.serverStartBtn, 1, 1);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(12, 12);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 95.02763F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 4.972376F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1583, 992);
            this.tableLayoutPanel1.TabIndex = 5;
            // 
            // serverStartBtn
            // 
            this.serverStartBtn.Location = new System.Drawing.Point(378, 946);
            this.serverStartBtn.Margin = new System.Windows.Forms.Padding(4);
            this.serverStartBtn.Name = "serverStartBtn";
            this.serverStartBtn.Size = new System.Drawing.Size(100, 28);
            this.serverStartBtn.TabIndex = 5;
            this.serverStartBtn.Text = "Server Start";
            this.serverStartBtn.UseVisualStyleBackColor = true;
            this.serverStartBtn.Click += new System.EventHandler(this.serverStartBtn_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(1605, 1009);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "MainForm";
            this.Text = "Minecraft Server App";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.tabControl1.ResumeLayout(false);
            this.serverFiles.ResumeLayout(false);
            this.serverFiles.PerformLayout();
            this.cmdOutput.ResumeLayout(false);
            this.cmdOutput.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        // Form1
        private System.Windows.Forms.TextBox fileOuput;
        private System.Windows.Forms.TreeView serverFilesTree;
        private System.Windows.Forms.Button serverFilesBtn;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage serverFiles;
        private System.Windows.Forms.TabPage cmdOutput;
        private System.Windows.Forms.TextBox cmdOuput;
        private System.Windows.Forms.Button cmdInput;
        private System.Windows.Forms.TextBox cmdInputText;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Button saveFileBtn;
        private System.Windows.Forms.Button serverStartBtn;
        private System.Windows.Forms.Button createFileBtn;
    }
}

