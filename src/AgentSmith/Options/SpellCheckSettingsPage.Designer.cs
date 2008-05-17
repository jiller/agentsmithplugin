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
            this.label2 = new System.Windows.Forms.Label();
            this._lbPPS = new System.Windows.Forms.Label();
            this._lbPS = new System.Windows.Forms.Label();
            this._lbIdentifiers = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this._cbStrings = new System.Windows.Forms.ComboBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this._cbCaseSensitive = new System.Windows.Forms.CheckBox();
            this._cbIdentifiers = new System.Windows.Forms.ComboBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label5 = new System.Windows.Forms.Label();
            this._btnImportFxCop = new System.Windows.Forms.Button();
            this._btnExportFxCop = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this._lbResx = new System.Windows.Forms.Label();
            this._lbComments = new System.Windows.Forms.Label();
            this._cbResX = new System.Windows.Forms.ComboBox();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this._btnImport = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
            this._cbDictionary = new System.Windows.Forms.ComboBox();
            this._lbDictionary = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this._mceDoNotSpellCheck = new AgentSmith.Options.MatchCollectionEdit();
            this._mceToSpellCheck = new AgentSmith.Options.MatchCollectionEdit();
            this._lsComments = new AgentSmith.Options.LanguageSelector();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.tableLayoutPanel4.SuspendLayout();
            this.SuspendLayout();
            // 
            // _tbUserDictionary
            // 
            this._tbUserDictionary.AcceptsReturn = true;
            this._tbUserDictionary.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this._tbUserDictionary.Location = new System.Drawing.Point(18, 433);
            this._tbUserDictionary.Multiline = true;
            this._tbUserDictionary.Name = "_tbUserDictionary";
            this._tbUserDictionary.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this._tbUserDictionary.Size = new System.Drawing.Size(310, 149);
            this._tbUserDictionary.TabIndex = 6;
            this._tbUserDictionary.WordWrap = false;
            // 
            // _userWords
            // 
            this._userWords.AutoSize = true;
            this._userWords.Location = new System.Drawing.Point(18, 412);
            this._userWords.Name = "_userWords";
            this._userWords.Size = new System.Drawing.Size(108, 13);
            this._userWords.TabIndex = 6;
            this._userWords.Text = "User dictionary words";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label2.Location = new System.Drawing.Point(15, 285);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(123, 13);
            this.label2.TabIndex = 62;
            this.label2.Text = "Manage Dictionaries";
            // 
            // _lbPPS
            // 
            this._lbPPS.AutoSize = true;
            this._lbPPS.Location = new System.Drawing.Point(18, 227);
            this._lbPPS.Name = "_lbPPS";
            this._lbPPS.Size = new System.Drawing.Size(361, 13);
            this._lbPPS.TabIndex = 64;
            this._lbPPS.Text = "* To enable/disable spell checking please go to the Inspection Severity tab";
            // 
            // _lbPS
            // 
            this._lbPS.AutoSize = true;
            this._lbPS.Location = new System.Drawing.Point(18, 240);
            this._lbPS.Name = "_lbPS";
            this._lbPS.Size = new System.Drawing.Size(289, 13);
            this._lbPS.TabIndex = 65;
            this._lbPS.Text = " and select appropriate level for an Agent Smith highlighting.";
            // 
            // _lbIdentifiers
            // 
            this._lbIdentifiers.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this._lbIdentifiers.AutoSize = true;
            this._lbIdentifiers.Location = new System.Drawing.Point(0, 7);
            this._lbIdentifiers.Margin = new System.Windows.Forms.Padding(0, 0, 3, 0);
            this._lbIdentifiers.Name = "_lbIdentifiers";
            this._lbIdentifiers.Size = new System.Drawing.Size(52, 13);
            this._lbIdentifiers.TabIndex = 68;
            this._lbIdentifiers.Text = "Identifiers";
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(0, 34);
            this.label1.Margin = new System.Windows.Forms.Padding(0, 0, 3, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(39, 13);
            this.label1.TabIndex = 70;
            this.label1.Text = "Strings";
            // 
            // _cbStrings
            // 
            this._cbStrings.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this._cbStrings.FormattingEnabled = true;
            this._cbStrings.Location = new System.Drawing.Point(58, 30);
            this._cbStrings.Name = "_cbStrings";
            this._cbStrings.Size = new System.Drawing.Size(121, 21);
            this._cbStrings.TabIndex = 2;
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Location = new System.Drawing.Point(18, 271);
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
            this._cbCaseSensitive.Location = new System.Drawing.Point(178, 411);
            this._cbCaseSensitive.Name = "_cbCaseSensitive";
            this._cbCaseSensitive.Size = new System.Drawing.Size(153, 17);
            this._cbCaseSensitive.TabIndex = 75;
            this._cbCaseSensitive.Text = "Dictionary is case sensitive";
            this._cbCaseSensitive.UseVisualStyleBackColor = true;
            // 
            // _cbIdentifiers
            // 
            this._cbIdentifiers.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this._cbIdentifiers.FormattingEnabled = true;
            this._cbIdentifiers.Location = new System.Drawing.Point(58, 3);
            this._cbIdentifiers.Name = "_cbIdentifiers";
            this._cbIdentifiers.Size = new System.Drawing.Size(121, 21);
            this._cbIdentifiers.TabIndex = 78;
            // 
            // panel2
            // 
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Location = new System.Drawing.Point(18, 333);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(515, 2);
            this.panel2.TabIndex = 62;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label5.Location = new System.Drawing.Point(18, 348);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(104, 13);
            this.label5.TabIndex = 80;
            this.label5.Text = "User Dictionaries";
            // 
            // _btnImportFxCop
            // 
            this._btnImportFxCop.Location = new System.Drawing.Point(334, 449);
            this._btnImportFxCop.Name = "_btnImportFxCop";
            this._btnImportFxCop.Size = new System.Drawing.Size(74, 23);
            this._btnImportFxCop.TabIndex = 82;
            this._btnImportFxCop.Text = "Import...";
            this._btnImportFxCop.UseVisualStyleBackColor = true;
            this._btnImportFxCop.Click += new System.EventHandler(this.btnImportFxCop_Click);
            // 
            // _btnExportFxCop
            // 
            this._btnExportFxCop.Location = new System.Drawing.Point(334, 478);
            this._btnExportFxCop.Name = "_btnExportFxCop";
            this._btnExportFxCop.Size = new System.Drawing.Size(74, 23);
            this._btnExportFxCop.TabIndex = 83;
            this._btnExportFxCop.Text = "Export...";
            this._btnExportFxCop.UseVisualStyleBackColor = true;
            this._btnExportFxCop.Click += new System.EventHandler(this.btnExportFxCop_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(335, 433);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(102, 13);
            this.label6.TabIndex = 84;
            this.label6.Text = "FxCop import/export";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.Controls.Add(this._lbResx, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this._lbComments, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this._cbResX, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this._lsComments, 1, 0);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(267, 34);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.Size = new System.Drawing.Size(282, 54);
            this.tableLayoutPanel1.TabIndex = 86;
            // 
            // _lbResx
            // 
            this._lbResx.AutoSize = true;
            this._lbResx.Location = new System.Drawing.Point(0, 35);
            this._lbResx.Margin = new System.Windows.Forms.Padding(0, 7, 3, 0);
            this._lbResx.Name = "_lbResx";
            this._lbResx.Size = new System.Drawing.Size(70, 13);
            this._lbResx.TabIndex = 79;
            this._lbResx.Text = "ResX Default";
            // 
            // _lbComments
            // 
            this._lbComments.AutoSize = true;
            this._lbComments.Location = new System.Drawing.Point(0, 7);
            this._lbComments.Margin = new System.Windows.Forms.Padding(0, 7, 3, 0);
            this._lbComments.Name = "_lbComments";
            this._lbComments.Size = new System.Drawing.Size(81, 13);
            this._lbComments.TabIndex = 73;
            this._lbComments.Text = "XML Comments";
            // 
            // _cbResX
            // 
            this._cbResX.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this._cbResX.FormattingEnabled = true;
            this._cbResX.Location = new System.Drawing.Point(87, 31);
            this._cbResX.Name = "_cbResX";
            this._cbResX.Size = new System.Drawing.Size(110, 21);
            this._cbResX.TabIndex = 82;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 2;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel2.Controls.Add(this._lbIdentifiers, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this._cbIdentifiers, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.label1, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this._cbStrings, 1, 1);
            this.tableLayoutPanel2.Location = new System.Drawing.Point(15, 34);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 2;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.Size = new System.Drawing.Size(246, 54);
            this.tableLayoutPanel2.TabIndex = 87;
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.ColumnCount = 2;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel3.Controls.Add(this._btnImport, 0, 0);
            this.tableLayoutPanel3.Controls.Add(this.label4, 0, 0);
            this.tableLayoutPanel3.Location = new System.Drawing.Point(18, 298);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 1;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel3.Size = new System.Drawing.Size(322, 29);
            this.tableLayoutPanel3.TabIndex = 88;
            // 
            // _btnImport
            // 
            this._btnImport.Location = new System.Drawing.Point(152, 3);
            this._btnImport.Name = "_btnImport";
            this._btnImport.Size = new System.Drawing.Size(75, 23);
            this._btnImport.TabIndex = 82;
            this._btnImport.Text = "Import...";
            this._btnImport.UseVisualStyleBackColor = true;
            this._btnImport.Click += new System.EventHandler(this.btnImport_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(0, 7);
            this.label4.Margin = new System.Windows.Forms.Padding(0, 7, 3, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(146, 13);
            this.label4.TabIndex = 81;
            this.label4.Text = "Import Open Office Dictionary";
            // 
            // tableLayoutPanel4
            // 
            this.tableLayoutPanel4.ColumnCount = 2;
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel4.Controls.Add(this._cbDictionary, 0, 0);
            this.tableLayoutPanel4.Controls.Add(this._lbDictionary, 0, 0);
            this.tableLayoutPanel4.Location = new System.Drawing.Point(21, 375);
            this.tableLayoutPanel4.Name = "tableLayoutPanel4";
            this.tableLayoutPanel4.RowCount = 1;
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel4.Size = new System.Drawing.Size(319, 30);
            this.tableLayoutPanel4.TabIndex = 89;
            // 
            // _cbDictionary
            // 
            this._cbDictionary.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this._cbDictionary.FormattingEnabled = true;
            this._cbDictionary.Location = new System.Drawing.Point(61, 3);
            this._cbDictionary.Name = "_cbDictionary";
            this._cbDictionary.Size = new System.Drawing.Size(121, 21);
            this._cbDictionary.TabIndex = 10;
            this._cbDictionary.SelectedIndexChanged += new System.EventHandler(this.cbDictionary_SelectedIndexChanged);
            // 
            // _lbDictionary
            // 
            this._lbDictionary.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this._lbDictionary.AutoSize = true;
            this._lbDictionary.Location = new System.Drawing.Point(0, 8);
            this._lbDictionary.Margin = new System.Windows.Forms.Padding(0, 0, 3, 0);
            this._lbDictionary.Name = "_lbDictionary";
            this._lbDictionary.Size = new System.Drawing.Size(55, 13);
            this._lbDictionary.TabIndex = 9;
            this._lbDictionary.Text = "Language";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(289, 93);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(40, 13);
            this.label7.TabIndex = 91;
            this.label7.Text = "Except";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(15, 93);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(121, 13);
            this.label8.TabIndex = 90;
            this.label8.Text = "Identifiers to spell check";
            // 
            // _mceDoNotSpellCheck
            // 
            this._mceDoNotSpellCheck.AutoSize = true;
            this._mceDoNotSpellCheck.EffectiveAccess = true;
            this._mceDoNotSpellCheck.Location = new System.Drawing.Point(292, 109);
            this._mceDoNotSpellCheck.Matches = new AgentSmith.MemberMatch.Match[0];
            this._mceDoNotSpellCheck.MinimumSize = new System.Drawing.Size(100, 0);
            this._mceDoNotSpellCheck.Name = "_mceDoNotSpellCheck";
            this._mceDoNotSpellCheck.Size = new System.Drawing.Size(251, 113);
            this._mceDoNotSpellCheck.TabIndex = 93;
            // 
            // _mceToSpellCheck
            // 
            this._mceToSpellCheck.AutoSize = true;
            this._mceToSpellCheck.EffectiveAccess = true;
            this._mceToSpellCheck.Location = new System.Drawing.Point(18, 109);
            this._mceToSpellCheck.Matches = new AgentSmith.MemberMatch.Match[0];
            this._mceToSpellCheck.MinimumSize = new System.Drawing.Size(100, 0);
            this._mceToSpellCheck.Name = "_mceToSpellCheck";
            this._mceToSpellCheck.Size = new System.Drawing.Size(251, 113);
            this._mceToSpellCheck.TabIndex = 92;
            // 
            // _lsComments
            // 
            this._lsComments.Dictionaries = null;
            this._lsComments.Location = new System.Drawing.Point(87, 3);
            this._lsComments.MinimumSize = new System.Drawing.Size(0, 22);
            this._lsComments.Name = "_lsComments";
            this._lsComments.SelectedDictionaries = new string[] {
        ""};
            this._lsComments.SelectedDictionariesString = "";
            this._lsComments.Size = new System.Drawing.Size(110, 22);
            this._lsComments.TabIndex = 78;
            // 
            // SpellCheckSettingsPage
            // 
            this.Controls.Add(this._mceDoNotSpellCheck);
            this.Controls.Add(this._mceToSpellCheck);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.tableLayoutPanel4);
            this.Controls.Add(this.tableLayoutPanel3);
            this.Controls.Add(this.tableLayoutPanel2);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.label6);
            this.Controls.Add(this._btnExportFxCop);
            this.Controls.Add(this._btnImportFxCop);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this._cbCaseSensitive);
            this.Controls.Add(this._lbPS);
            this.Controls.Add(this._lbPPS);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this._userWords);
            this.Controls.Add(this._tbUserDictionary);
            this.Name = "SpellCheckSettingsPage";
            this.Size = new System.Drawing.Size(552, 596);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.tableLayoutPanel3.ResumeLayout(false);
            this.tableLayoutPanel3.PerformLayout();
            this.tableLayoutPanel4.ResumeLayout(false);
            this.tableLayoutPanel4.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        
        private System.Windows.Forms.TextBox _tbUserDictionary;
        private System.Windows.Forms.Label _userWords;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label _lbPPS;
        private System.Windows.Forms.Label _lbPS;
        private System.Windows.Forms.Label _lbIdentifiers;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox _cbStrings;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.CheckBox _cbCaseSensitive;
        private System.Windows.Forms.ComboBox _cbIdentifiers;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button _btnImportFxCop;
        private System.Windows.Forms.Button _btnExportFxCop;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private LanguageSelector _lsComments;
        private System.Windows.Forms.Label _lbComments;
        private System.Windows.Forms.Label _lbResx;
        private System.Windows.Forms.ComboBox _cbResX;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.Button _btnImport;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel4;
        private System.Windows.Forms.ComboBox _cbDictionary;
        private System.Windows.Forms.Label _lbDictionary;
        private MatchCollectionEdit _mceDoNotSpellCheck;
        private MatchCollectionEdit _mceToSpellCheck;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
    }
}
