namespace AgentSmith.Options
{
    partial class ImportOpenOfficeDictionary
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
            this.label1 = new System.Windows.Forms.Label();
            this._dictionaryLink = new System.Windows.Forms.LinkLabel();
            this._tbDictName = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this._btnImport = new System.Windows.Forms.Button();
            this._btnCancel = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this._tbAffixFile = new System.Windows.Forms.TextBox();
            this._btnBrowseAffix = new System.Windows.Forms.Button();
            this._btnBrowseDict = new System.Windows.Forms.Button();
            this._tbDicFile = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this._errorProvider = new System.Windows.Forms.ErrorProvider(this.components);
            ((System.ComponentModel.ISupportInitialize)(this._errorProvider)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(241, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Open office dictionaries can be downloaded from ";
            // 
            // _dictionaryLink
            // 
            this._dictionaryLink.AutoSize = true;
            this._dictionaryLink.Location = new System.Drawing.Point(12, 22);
            this._dictionaryLink.Name = "_dictionaryLink";
            this._dictionaryLink.Size = new System.Drawing.Size(252, 13);
            this._dictionaryLink.TabIndex = 1;
            this._dictionaryLink.TabStop = true;
            this._dictionaryLink.Text = "http://wiki.services.openoffice.org/wiki/Dictionaries";
            this._dictionaryLink.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.dictionaryLink_LinkClicked);
            // 
            // _tbDictName
            // 
            this._tbDictName.Location = new System.Drawing.Point(184, 113);
            this._tbDictName.Name = "_tbDictName";
            this._tbDictName.Size = new System.Drawing.Size(154, 20);
            this._tbDictName.TabIndex = 2;
            this._tbDictName.Validating += new System.ComponentModel.CancelEventHandler(this.tbDictName_Validating);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 113);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(166, 39);
            this.label2.TabIndex = 3;
            this.label2.Text = "Dictionary Name (en-US, fr, ...).\nThis name is used to \nfind dictionary to check " +
                ".resx files.";
            // 
            // _btnImport
            // 
            this._btnImport.Location = new System.Drawing.Point(272, 168);
            this._btnImport.Name = "_btnImport";
            this._btnImport.Size = new System.Drawing.Size(75, 23);
            this._btnImport.TabIndex = 4;
            this._btnImport.Text = "&Import";
            this._btnImport.UseVisualStyleBackColor = true;
            this._btnImport.Click += new System.EventHandler(this.btnImport_Click);
            // 
            // _btnCancel
            // 
            this._btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this._btnCancel.Location = new System.Drawing.Point(354, 168);
            this._btnCancel.Name = "_btnCancel";
            this._btnCancel.Size = new System.Drawing.Size(75, 23);
            this._btnCancel.TabIndex = 5;
            this._btnCancel.Text = "&Cancel";
            this._btnCancel.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 52);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(67, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Affix file path";
            // 
            // _tbAffixFile
            // 
            this._tbAffixFile.Location = new System.Drawing.Point(184, 52);
            this._tbAffixFile.Name = "_tbAffixFile";
            this._tbAffixFile.Size = new System.Drawing.Size(154, 20);
            this._tbAffixFile.TabIndex = 7;
            this._tbAffixFile.Validating += new System.ComponentModel.CancelEventHandler(this.tbAffixFile_Validating);
            // 
            // _btnBrowseAffix
            // 
            this._btnBrowseAffix.Location = new System.Drawing.Point(354, 49);
            this._btnBrowseAffix.Name = "_btnBrowseAffix";
            this._btnBrowseAffix.Size = new System.Drawing.Size(75, 23);
            this._btnBrowseAffix.TabIndex = 8;
            this._btnBrowseAffix.Text = "Browse...";
            this._btnBrowseAffix.UseVisualStyleBackColor = true;
            this._btnBrowseAffix.Click += new System.EventHandler(this.btnBrowseAffix_Click);
            // 
            // _btnBrowseDict
            // 
            this._btnBrowseDict.Location = new System.Drawing.Point(354, 71);
            this._btnBrowseDict.Name = "_btnBrowseDict";
            this._btnBrowseDict.Size = new System.Drawing.Size(75, 23);
            this._btnBrowseDict.TabIndex = 9;
            this._btnBrowseDict.Text = "Browse...";
            this._btnBrowseDict.UseVisualStyleBackColor = true;
            this._btnBrowseDict.Click += new System.EventHandler(this.btnBrowseDict_Click);
            // 
            // _tbDicFile
            // 
            this._tbDicFile.Location = new System.Drawing.Point(184, 73);
            this._tbDicFile.Name = "_tbDicFile";
            this._tbDicFile.Size = new System.Drawing.Size(154, 20);
            this._tbDicFile.TabIndex = 10;
            this._tbDicFile.Validating += new System.ComponentModel.CancelEventHandler(this.tbDicFile_Validating);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 76);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(63, 13);
            this.label4.TabIndex = 11;
            this.label4.Text = "Dic file path";
            // 
            // _errorProvider
            // 
            this._errorProvider.ContainerControl = this;
            // 
            // ImportOpenOfficeDictionary
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(441, 198);
            this.Controls.Add(this.label4);
            this.Controls.Add(this._tbDicFile);
            this.Controls.Add(this._btnBrowseDict);
            this.Controls.Add(this._btnBrowseAffix);
            this.Controls.Add(this._tbAffixFile);
            this.Controls.Add(this.label3);
            this.Controls.Add(this._btnCancel);
            this.Controls.Add(this._btnImport);
            this.Controls.Add(this.label2);
            this.Controls.Add(this._tbDictName);
            this.Controls.Add(this._dictionaryLink);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "ImportOpenOfficeDictionary";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Import Open Office Dictionary";
            ((System.ComponentModel.ISupportInitialize)(this._errorProvider)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.LinkLabel _dictionaryLink;
        private System.Windows.Forms.TextBox _tbDictName;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button _btnImport;
        private System.Windows.Forms.Button _btnCancel;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox _tbAffixFile;
        private System.Windows.Forms.Button _btnBrowseAffix;
        private System.Windows.Forms.Button _btnBrowseDict;
        private System.Windows.Forms.TextBox _tbDicFile;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ErrorProvider _errorProvider;
    }
}