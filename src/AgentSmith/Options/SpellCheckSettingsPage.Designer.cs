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
            this._lbPPS = new System.Windows.Forms.Label();
            this._lbIdentifiers = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this._cbIdentifiers = new System.Windows.Forms.ComboBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this._lbComments = new System.Windows.Forms.Label();
            this._lsComments = new AgentSmith.Options.LanguageSelector();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this._lsStrings = new AgentSmith.Options.LanguageSelector();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this._ignorePatterns = new AgentSmith.Options.StringListEdit();
            this._mceDoNotSpellCheck = new AgentSmith.Options.MatchCollectionEdit();
            this._mceToSpellCheck = new AgentSmith.Options.MatchCollectionEdit();
            this.label2 = new System.Windows.Forms.Label();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // _lbPPS
            // 
            this._lbPPS.AutoSize = true;
            this._lbPPS.Location = new System.Drawing.Point(15, 388);
            this._lbPPS.Name = "_lbPPS";
            this._lbPPS.Size = new System.Drawing.Size(361, 26);
            this._lbPPS.TabIndex = 64;
            this._lbPPS.Text = "* To enable/disable spell checking please go to the Inspection Severity tab\r\n   a" +
                "nd select appropriate level for an Agent Smith highlighting.";
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
            // _cbIdentifiers
            // 
            this._cbIdentifiers.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this._cbIdentifiers.FormattingEnabled = true;
            this._cbIdentifiers.Location = new System.Drawing.Point(58, 3);
            this._cbIdentifiers.Name = "_cbIdentifiers";
            this._cbIdentifiers.Size = new System.Drawing.Size(121, 21);
            this._cbIdentifiers.TabIndex = 78;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.Controls.Add(this._lbComments, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this._lsComments, 1, 0);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(267, 34);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.Size = new System.Drawing.Size(282, 54);
            this.tableLayoutPanel1.TabIndex = 86;
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
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 2;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel2.Controls.Add(this._lsStrings, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this._lbIdentifiers, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this._cbIdentifiers, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.label1, 0, 1);
            this.tableLayoutPanel2.Location = new System.Drawing.Point(15, 34);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 2;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.Size = new System.Drawing.Size(246, 54);
            this.tableLayoutPanel2.TabIndex = 87;
            // 
            // _lsStrings
            // 
            this._lsStrings.Dictionaries = null;
            this._lsStrings.Location = new System.Drawing.Point(58, 30);
            this._lsStrings.MinimumSize = new System.Drawing.Size(0, 22);
            this._lsStrings.Name = "_lsStrings";
            this._lsStrings.SelectedDictionaries = new string[] {
        ""};
            this._lsStrings.SelectedDictionariesString = "";
            this._lsStrings.Size = new System.Drawing.Size(121, 22);
            this._lsStrings.TabIndex = 79;
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
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Location = new System.Drawing.Point(18, 241);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(515, 2);
            this.panel1.TabIndex = 61;
            // 
            // _ignorePatterns
            // 
            this._ignorePatterns.Caption = "Patterns to ignore";
            this._ignorePatterns.Items = new string[0];
            this._ignorePatterns.Location = new System.Drawing.Point(18, 260);
            this._ignorePatterns.Name = "_ignorePatterns";
            this._ignorePatterns.Size = new System.Drawing.Size(485, 126);
            this._ignorePatterns.TabIndex = 94;
            this._ignorePatterns.OnValidate += new AgentSmith.Options.ValidateHandler(this.ignorePatterns_OnValidate);
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
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(15, 429);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(173, 65);
            this.label2.TabIndex = 95;
            this.label2.Text = "* Spell checking is disabled inside\r\n     //agentsmith spellcheck disable\r\n     a" +
                "nd\r\n     //agentsmith spellcheck restore\r\n    comments";
            // 
            // SpellCheckSettingsPage
            // 
            this.Controls.Add(this.label2);
            this.Controls.Add(this._ignorePatterns);
            this.Controls.Add(this._mceDoNotSpellCheck);
            this.Controls.Add(this._mceToSpellCheck);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.tableLayoutPanel2);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this._lbPPS);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.panel1);
            this.Name = "SpellCheckSettingsPage";
            this.Size = new System.Drawing.Size(552, 596);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label _lbPPS;
        private System.Windows.Forms.Label _lbIdentifiers;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox _cbIdentifiers;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label _lbComments;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private MatchCollectionEdit _mceDoNotSpellCheck;
        private MatchCollectionEdit _mceToSpellCheck;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private LanguageSelector _lsComments;
        private LanguageSelector _lsStrings;
        private StringListEdit _ignorePatterns;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label2;
    }
}
