using System;

namespace AgentSmith.Options
{
    partial class AgentSmithSettingsPage
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
            this._lbMustHaveComments = new System.Windows.Forms.Label();
            this._lbMustHaveCommentsExcept = new System.Windows.Forms.Label();
            this._mceNotMatches = new AgentSmith.Options.MatchCollectionEdit();
            this._mceMatches = new AgentSmith.Options.MatchCollectionEdit();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this._lbPPS = new System.Windows.Forms.Label();
            this._lbPS = new System.Windows.Forms.Label();
            this._cbLookAtBase = new System.Windows.Forms.CheckBox();
            this._btnImport = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // _tbUserDictionary
            // 
            this._tbUserDictionary.AcceptsReturn = true;
            this._tbUserDictionary.Location = new System.Drawing.Point(15, 255);
            this._tbUserDictionary.Multiline = true;
            this._tbUserDictionary.Name = "_tbUserDictionary";
            this._tbUserDictionary.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this._tbUserDictionary.Size = new System.Drawing.Size(310, 201);
            this._tbUserDictionary.TabIndex = 5;
            this._tbUserDictionary.WordWrap = false;
            // 
            // _userWords
            // 
            this._userWords.AutoSize = true;
            this._userWords.Location = new System.Drawing.Point(12, 239);
            this._userWords.Name = "_userWords";
            this._userWords.Size = new System.Drawing.Size(108, 13);
            this._userWords.TabIndex = 6;
            this._userWords.Text = "User dictionary words";
            // 
            // _cbDictionary
            // 
            this._cbDictionary.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this._cbDictionary.FormattingEnabled = true;
            this._cbDictionary.Location = new System.Drawing.Point(83, 207);
            this._cbDictionary.Name = "_cbDictionary";
            this._cbDictionary.Size = new System.Drawing.Size(121, 21);
            this._cbDictionary.TabIndex = 7;
            // 
            // _lbDictionary
            // 
            this._lbDictionary.AutoSize = true;
            this._lbDictionary.Location = new System.Drawing.Point(12, 210);
            this._lbDictionary.Name = "_lbDictionary";
            this._lbDictionary.Size = new System.Drawing.Size(55, 13);
            this._lbDictionary.TabIndex = 8;
            this._lbDictionary.Text = "Language";
            // 
            // _lbMustHaveComments
            // 
            this._lbMustHaveComments.AutoSize = true;
            this._lbMustHaveComments.Location = new System.Drawing.Point(12, 25);
            this._lbMustHaveComments.Name = "_lbMustHaveComments";
            this._lbMustHaveComments.Size = new System.Drawing.Size(192, 13);
            this._lbMustHaveComments.TabIndex = 9;
            this._lbMustHaveComments.Text = "Members that must have xml comments";
            // 
            // _lbMustHaveCommentsExcept
            // 
            this._lbMustHaveCommentsExcept.AutoSize = true;
            this._lbMustHaveCommentsExcept.Location = new System.Drawing.Point(286, 25);
            this._lbMustHaveCommentsExcept.Name = "_lbMustHaveCommentsExcept";
            this._lbMustHaveCommentsExcept.Size = new System.Drawing.Size(40, 13);
            this._lbMustHaveCommentsExcept.TabIndex = 10;
            this._lbMustHaveCommentsExcept.Text = "Except";
            // 
            // _mceNotMatches
            // 
            this._mceNotMatches.Location = new System.Drawing.Point(289, 41);
            this._mceNotMatches.Matches = new AgentSmith.MemberMatch.Match[0];
            this._mceNotMatches.Name = "_mceNotMatches";
            this._mceNotMatches.Size = new System.Drawing.Size(251, 83);
            this._mceNotMatches.TabIndex = 60;
            // 
            // _mceMatches
            // 
            this._mceMatches.Location = new System.Drawing.Point(15, 41);
            this._mceMatches.Matches = new AgentSmith.MemberMatch.Match[0];
            this._mceMatches.Name = "_mceMatches";
            this._mceMatches.Size = new System.Drawing.Size(251, 83);
            this._mceMatches.TabIndex = 59;
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Location = new System.Drawing.Point(15, 159);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(515, 2);
            this.panel1.TabIndex = 61;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label2.Location = new System.Drawing.Point(12, 177);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(91, 13);
            this.label2.TabIndex = 62;
            this.label2.Text = "Spell checking";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label3.Location = new System.Drawing.Point(12, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(149, 13);
            this.label3.TabIndex = 63;
            this.label3.Text = "XML comment validation.";
            // 
            // _lbPPS
            // 
            this._lbPPS.AutoSize = true;
            this._lbPPS.Location = new System.Drawing.Point(12, 472);
            this._lbPPS.Name = "_lbPPS";
            this._lbPPS.Size = new System.Drawing.Size(492, 13);
            this._lbPPS.TabIndex = 64;
            this._lbPPS.Text = "* To enable/disable XML comment validation or spell checking please go to the Ins" +
                "pection Severity tab";
            // 
            // _lbPS
            // 
            this._lbPS.AutoSize = true;
            this._lbPS.Location = new System.Drawing.Point(15, 489);
            this._lbPS.Name = "_lbPS";
            this._lbPS.Size = new System.Drawing.Size(289, 13);
            this._lbPS.TabIndex = 65;
            this._lbPS.Text = " and select appropriate level for an Agent Smith highlighting.";
            // 
            // _cbLookAtBase
            // 
            this._cbLookAtBase.AutoSize = true;
            this._cbLookAtBase.Location = new System.Drawing.Point(15, 130);
            this._cbLookAtBase.Name = "_cbLookAtBase";
            this._cbLookAtBase.Size = new System.Drawing.Size(269, 17);
            this._cbLookAtBase.TabIndex = 66;
            this._cbLookAtBase.Text = "Do not show warning if base member has comment.";
            this._cbLookAtBase.UseVisualStyleBackColor = true;
            // 
            // _btnImport
            // 
            this._btnImport.Location = new System.Drawing.Point(219, 207);
            this._btnImport.Name = "_btnImport";
            this._btnImport.Size = new System.Drawing.Size(75, 23);
            this._btnImport.TabIndex = 67;
            this._btnImport.Text = "Import...";
            this._btnImport.UseVisualStyleBackColor = true;
            this._btnImport.Click += new System.EventHandler(this.btnImport_Click);
            // 
            // AgentSmithSettingsPage
            // 
            this.Controls.Add(this._btnImport);
            this.Controls.Add(this._cbLookAtBase);
            this.Controls.Add(this._lbPS);
            this.Controls.Add(this._lbPPS);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this._mceNotMatches);
            this.Controls.Add(this._mceMatches);
            this.Controls.Add(this._lbMustHaveCommentsExcept);
            this.Controls.Add(this._lbMustHaveComments);
            this.Controls.Add(this._lbDictionary);
            this.Controls.Add(this._cbDictionary);
            this.Controls.Add(this._userWords);
            this.Controls.Add(this._tbUserDictionary);
            this.Name = "AgentSmithSettingsPage";
            this.Size = new System.Drawing.Size(552, 498);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        
        private System.Windows.Forms.TextBox _tbUserDictionary;
        private System.Windows.Forms.Label _userWords;
        private System.Windows.Forms.ComboBox _cbDictionary;
        private System.Windows.Forms.Label _lbDictionary;
        private System.Windows.Forms.Label _lbMustHaveComments;
        private System.Windows.Forms.Label _lbMustHaveCommentsExcept;
        private MatchCollectionEdit _mceMatches;
        private MatchCollectionEdit _mceNotMatches;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label _lbPPS;
        private System.Windows.Forms.Label _lbPS;
        private System.Windows.Forms.CheckBox _cbLookAtBase;
        private System.Windows.Forms.Button _btnImport;
    }
}
