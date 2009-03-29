using System;

namespace AgentSmith.Options
{
    partial class SpellCheckSettingsPage
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

        #region Component Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this._tbUserDictionary = new System.Windows.Forms.TextBox();
            this._userWords = new System.Windows.Forms.Label();
            this._cbDictionary = new System.Windows.Forms.ComboBox();
            this._lbDictionary = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this._lbPPS = new System.Windows.Forms.Label();
            this._lbPS = new System.Windows.Forms.Label();
            this._btnImport = new System.Windows.Forms.Button();
            this._lbIdentifiers = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this._cbStrings = new System.Windows.Forms.ComboBox();
            this._lbComments = new System.Windows.Forms.Label();           
            this._lbResx = new System.Windows.Forms.Label();
            this._cbResX = new System.Windows.Forms.ComboBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this._cbCaseSensitive = new System.Windows.Forms.CheckBox();
            this._lsComments = new AgentSmith.Options.LanguageSelector();
            this._cbIdentifiers = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // _tbUserDictionary
            // 
            this._tbUserDictionary.AcceptsReturn = true;
            this._tbUserDictionary.Location = new System.Drawing.Point(15, 272);
            this._tbUserDictionary.Multiline = true;
            this._tbUserDictionary.Name = "_tbUserDictionary";
            this._tbUserDictionary.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this._tbUserDictionary.Size = new System.Drawing.Size(310, 201);
            this._tbUserDictionary.TabIndex = 6;
            this._tbUserDictionary.WordWrap = false;
            // 
            // _userWords
            // 
            this._userWords.AutoSize = true;
            this._userWords.Location = new System.Drawing.Point(12, 245);
            this._userWords.Name = "_userWords";
            this._userWords.Size = new System.Drawing.Size(108, 13);
            this._userWords.TabIndex = 6;
            this._userWords.Text = "User dictionary words";
            // 
            // _cbDictionary
            // 
            this._cbDictionary.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this._cbDictionary.FormattingEnabled = true;
            this._cbDictionary.Location = new System.Drawing.Point(83, 213);
            this._cbDictionary.Name = "_cbDictionary";
            this._cbDictionary.Size = new System.Drawing.Size(121, 21);
            this._cbDictionary.TabIndex = 4;
            this._cbDictionary.SelectedIndexChanged += new System.EventHandler(this.cbDictionary_SelectedIndexChanged);            
            // 
            // _lbDictionary
            // 
            this._lbDictionary.AutoSize = true;
            this._lbDictionary.Location = new System.Drawing.Point(12, 216);
            this._lbDictionary.Name = "_lbDictionary";
            this._lbDictionary.Size = new System.Drawing.Size(55, 13);
            this._lbDictionary.TabIndex = 8;
            this._lbDictionary.Text = "Language";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label2.Location = new System.Drawing.Point(12, 183);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(123, 13);
            this.label2.TabIndex = 62;
            this.label2.Text = "Manage Dictionaries";
            // 
            // _lbPPS
            // 
            this._lbPPS.AutoSize = true;
            this._lbPPS.Location = new System.Drawing.Point(20, 115);
            this._lbPPS.Name = "_lbPPS";
            this._lbPPS.Size = new System.Drawing.Size(361, 13);
            this._lbPPS.TabIndex = 64;
            this._lbPPS.Text = "* To enable/disable spell checking please go to the Inspection Severity tab";
            // 
            // _lbPS
            // 
            this._lbPS.AutoSize = true;
            this._lbPS.Location = new System.Drawing.Point(23, 132);
            this._lbPS.Name = "_lbPS";
            this._lbPS.Size = new System.Drawing.Size(289, 13);
            this._lbPS.TabIndex = 65;
            this._lbPS.Text = " and select appropriate level for an Agent Smith highlighting.";
            // 
            // _btnImport
            // 
            this._btnImport.Location = new System.Drawing.Point(219, 213);
            this._btnImport.Name = "_btnImport";
            this._btnImport.Size = new System.Drawing.Size(75, 23);
            this._btnImport.TabIndex = 5;
            this._btnImport.Text = "Import...";
            this._btnImport.UseVisualStyleBackColor = true;
            this._btnImport.Click += new System.EventHandler(this.btnImport_Click);
            // 
            // _lbIdentifiers
            // 
            this._lbIdentifiers.AutoSize = true;
            this._lbIdentifiers.Location = new System.Drawing.Point(12, 40);
            this._lbIdentifiers.Name = "_lbIdentifiers";
            this._lbIdentifiers.Size = new System.Drawing.Size(52, 13);
            this._lbIdentifiers.TabIndex = 68;
            this._lbIdentifiers.Text = "Identifiers";            
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 76);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(39, 13);
            this.label1.TabIndex = 70;
            this.label1.Text = "Strings";
            // 
            // _cbStrings
            // 
            this._cbStrings.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this._cbStrings.FormattingEnabled = true;
            this._cbStrings.Location = new System.Drawing.Point(70, 73);
            this._cbStrings.Name = "_cbStrings";
            this._cbStrings.Size = new System.Drawing.Size(121, 21);
            this._cbStrings.TabIndex = 2;
            // 
            // _lbComments
            // 
            this._lbComments.AutoSize = true;
            this._lbComments.Location = new System.Drawing.Point(231, 40);
            this._lbComments.Name = "_lbComments";
            this._lbComments.Size = new System.Drawing.Size(81, 13);
            this._lbComments.TabIndex = 72;
            this._lbComments.Text = "XML Comments";
            // 
            // _lbResx
            // 
            this._lbResx.AutoSize = true;
            this._lbResx.Location = new System.Drawing.Point(231, 76);
            this._lbResx.Name = "_lbResx";
            this._lbResx.Size = new System.Drawing.Size(70, 13);
            this._lbResx.TabIndex = 74;
            this._lbResx.Text = "ResX Default";
            // 
            // _cbResX
            // 
            this._cbResX.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this._cbResX.FormattingEnabled = true;
            this._cbResX.Location = new System.Drawing.Point(318, 73);
            this._cbResX.Name = "_cbResX";
            this._cbResX.Size = new System.Drawing.Size(121, 21);
            this._cbResX.TabIndex = 3;
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Location = new System.Drawing.Point(15, 163);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(515, 2);
            this.panel1.TabIndex = 61;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label3.Location = new System.Drawing.Point(12, 2);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(92, 13);
            this.label3.TabIndex = 63;
            this.label3.Text = "Spell Checking";
            // 
            // _cbCaseSensitive
            // 
            this._cbCaseSensitive.AutoSize = true;
            this._cbCaseSensitive.Location = new System.Drawing.Point(172, 245);
            this._cbCaseSensitive.Name = "_cbCaseSensitive";
            this._cbCaseSensitive.Size = new System.Drawing.Size(153, 17);
            this._cbCaseSensitive.TabIndex = 75;
            this._cbCaseSensitive.Text = "Dictionary is case sensitive";
            this._cbCaseSensitive.UseVisualStyleBackColor = true;
            // 
            // _lsComments
            // 
            this._lsComments.Dictionaries = null;
            this._lsComments.Location = new System.Drawing.Point(318, 30);
            this._lsComments.Name = "_lsComments";
            this._lsComments.SelectedDictionaries = new string[] {
        ""};
            this._lsComments.SelectedDictionariesString = "";
            this._lsComments.Size = new System.Drawing.Size(152, 28);
            this._lsComments.TabIndex = 77;
            // 
            // _cbIdentifiers
            // 
            this._cbIdentifiers.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this._cbIdentifiers.FormattingEnabled = true;
            this._cbIdentifiers.Location = new System.Drawing.Point(70, 37);
            this._cbIdentifiers.Name = "_cbIdentifiers";
            this._cbIdentifiers.Size = new System.Drawing.Size(121, 21);
            this._cbIdentifiers.TabIndex = 78;
            // 
            // SpellCheckSettingsPage
            // 
            this.Controls.Add(this._cbIdentifiers);
            this.Controls.Add(this._lsComments);
            this.Controls.Add(this._cbCaseSensitive);
            this.Controls.Add(this._cbResX);
            this.Controls.Add(this._lbResx);
            this.Controls.Add(this._lbComments);
            this.Controls.Add(this._cbStrings);
            this.Controls.Add(this.label1);
            this.Controls.Add(this._lbIdentifiers);
            this.Controls.Add(this._btnImport);
            this.Controls.Add(this._lbPS);
            this.Controls.Add(this._lbPPS);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this._lbDictionary);
            this.Controls.Add(this._cbDictionary);
            this.Controls.Add(this._userWords);
            this.Controls.Add(this._tbUserDictionary);
            this.Name = "SpellCheckSettingsPage";
            this.Size = new System.Drawing.Size(552, 519);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        
        private System.Windows.Forms.TextBox _tbUserDictionary;
        private System.Windows.Forms.Label _userWords;
        private System.Windows.Forms.ComboBox _cbDictionary;
        private System.Windows.Forms.Label _lbDictionary;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label _lbPPS;
        private System.Windows.Forms.Label _lbPS;
        private System.Windows.Forms.Button _btnImport;
        private System.Windows.Forms.Label _lbIdentifiers;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox _cbStrings;
        private System.Windows.Forms.Label _lbComments;
        private System.Windows.Forms.Label _lbResx;
        private System.Windows.Forms.ComboBox _cbResX;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.CheckBox _cbCaseSensitive;
        private LanguageSelector _lsComments;
        private System.Windows.Forms.ComboBox _cbIdentifiers;
    }
}