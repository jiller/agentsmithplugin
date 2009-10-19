using JetBrains.CommonControls;
using Match=AgentSmith.MemberMatch.Match;

namespace AgentSmith.Options
{
    partial class EditRule
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
            this._tbDescription = new System.Windows.Forms.TextBox();
            this._lbDescription = new System.Windows.Forms.Label();
            this._lbStyle = new System.Windows.Forms.Label();
            this._cbStyle = new System.Windows.Forms.ComboBox();
            this._lbRegEx = new System.Windows.Forms.Label();
            this._tbRegex = new System.Windows.Forms.TextBox();
            this._btnOK = new System.Windows.Forms.Button();
            this._btnCancel = new System.Windows.Forms.Button();
            this._lbMatches = new System.Windows.Forms.Label();
            this._lbNotMatches = new System.Windows.Forms.Label();
            this._cbDisabled = new System.Windows.Forms.CheckBox();
            this._mceNotMatches = new AgentSmith.Options.MatchCollectionEdit();
            this._mceMatches = new AgentSmith.Options.MatchCollectionEdit();
            this._sceMustHavePrefix = new AgentSmith.Options.StringListEdit();
            this._sceMustNotHavePrefix = new AgentSmith.Options.StringListEdit();
            this._sceMustHaveSuffix = new AgentSmith.Options.StringListEdit();
            this._sceMustNotHaveSuffix = new AgentSmith.Options.StringListEdit();
            this.SuspendLayout();
            // 
            // _tbDescription
            // 
            this._tbDescription.Location = new System.Drawing.Point(15, 25);
            this._tbDescription.Name = "_tbDescription";
            this._tbDescription.Size = new System.Drawing.Size(543, 20);
            this._tbDescription.TabIndex = 34;
            // 
            // _lbDescription
            // 
            this._lbDescription.AutoSize = true;
            this._lbDescription.Location = new System.Drawing.Point(12, 9);
            this._lbDescription.Name = "_lbDescription";
            this._lbDescription.Size = new System.Drawing.Size(312, 13);
            this._lbDescription.TabIndex = 33;
            this._lbDescription.Text = "Description (this text will appear as description of the highlighting)";
            // 
            // _lbStyle
            // 
            this._lbStyle.AutoSize = true;
            this._lbStyle.Location = new System.Drawing.Point(12, 299);
            this._lbStyle.Name = "_lbStyle";
            this._lbStyle.Size = new System.Drawing.Size(30, 13);
            this._lbStyle.TabIndex = 36;
            this._lbStyle.Text = "Style";
            // 
            // _cbStyle
            // 
            this._cbStyle.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this._cbStyle.FormattingEnabled = true;
            this._cbStyle.Items.AddRange(new object[] {
            "Pascal",
            "Camel",
            "Uppercase"});
            this._cbStyle.Location = new System.Drawing.Point(15, 315);
            this._cbStyle.Name = "_cbStyle";
            this._cbStyle.Size = new System.Drawing.Size(283, 21);
            this._cbStyle.TabIndex = 35;
            this._cbStyle.SelectedValueChanged += new System.EventHandler(this.cbStyle_SelectedValueChanged);
            // 
            // _lbRegEx
            // 
            this._lbRegEx.AutoSize = true;
            this._lbRegEx.Location = new System.Drawing.Point(323, 299);
            this._lbRegEx.Name = "_lbRegEx";
            this._lbRegEx.Size = new System.Drawing.Size(98, 13);
            this._lbRegEx.TabIndex = 50;
            this._lbRegEx.Text = "Regular Expression";
            // 
            // _tbRegex
            // 
            this._tbRegex.Location = new System.Drawing.Point(326, 316);
            this._tbRegex.Name = "_tbRegex";
            this._tbRegex.Size = new System.Drawing.Size(286, 20);
            this._tbRegex.TabIndex = 51;
            // 
            // _btnOK
            // 
            this._btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this._btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this._btnOK.Location = new System.Drawing.Point(456, 494);
            this._btnOK.Name = "_btnOK";
            this._btnOK.Size = new System.Drawing.Size(75, 23);
            this._btnOK.TabIndex = 52;
            this._btnOK.Text = "OK";
            this._btnOK.UseVisualStyleBackColor = true;
            this._btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // _btnCancel
            // 
            this._btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this._btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this._btnCancel.Location = new System.Drawing.Point(537, 494);
            this._btnCancel.Name = "_btnCancel";
            this._btnCancel.Size = new System.Drawing.Size(75, 23);
            this._btnCancel.TabIndex = 53;
            this._btnCancel.Text = "Cancel";
            this._btnCancel.UseVisualStyleBackColor = true;
            // 
            // _lbMatches
            // 
            this._lbMatches.AutoSize = true;
            this._lbMatches.Location = new System.Drawing.Point(12, 53);
            this._lbMatches.Name = "_lbMatches";
            this._lbMatches.Size = new System.Drawing.Size(202, 13);
            this._lbMatches.TabIndex = 54;
            this._lbMatches.Text = "Matches (declarations this rule applied to)";
            // 
            // _lbNotMatches
            // 
            this._lbNotMatches.AutoSize = true;
            this._lbNotMatches.Location = new System.Drawing.Point(323, 53);
            this._lbNotMatches.Name = "_lbNotMatches";
            this._lbNotMatches.Size = new System.Drawing.Size(222, 13);
            this._lbNotMatches.TabIndex = 56;
            this._lbNotMatches.Text = "Except (declarations this rule doesn\'t apply to)";
            // 
            // _cbDisabled
            // 
            this._cbDisabled.AutoSize = true;
            this._cbDisabled.Location = new System.Drawing.Point(12, 494);
            this._cbDisabled.Name = "_cbDisabled";
            this._cbDisabled.Size = new System.Drawing.Size(100, 17);
            this._cbDisabled.TabIndex = 58;
            this._cbDisabled.Text = "Rule is disabled";
            this._cbDisabled.UseVisualStyleBackColor = true;
            // 
            // _mceNotMatches
            // 
            this._mceNotMatches.Location = new System.Drawing.Point(326, 71);
            this._mceNotMatches.Matches = new AgentSmith.MemberMatch.Match[0];
            this._mceNotMatches.Name = "_mceNotMatches";
            this._mceNotMatches.Size = new System.Drawing.Size(286, 78);
            this._mceNotMatches.TabIndex = 60;
            // 
            // _mceMatches
            // 
            this._mceMatches.Location = new System.Drawing.Point(15, 68);
            this._mceMatches.Matches = new AgentSmith.MemberMatch.Match[0];
            this._mceMatches.Name = "_mceMatches";
            this._mceMatches.Size = new System.Drawing.Size(286, 81);
            this._mceMatches.TabIndex = 59;
            // 
            // _sceMustHavePrefix
            // 
            this._sceMustHavePrefix.Caption = "Must have prefix";
            this._sceMustHavePrefix.Items = new string[0];
            this._sceMustHavePrefix.Location = new System.Drawing.Point(15, 155);
            this._sceMustHavePrefix.Name = "_sceMustHavePrefix";
            this._sceMustHavePrefix.Size = new System.Drawing.Size(286, 141);
            this._sceMustHavePrefix.TabIndex = 61;
            // 
            // _sceMustNotHavePrefix
            // 
            this._sceMustNotHavePrefix.Caption = "Must not have prefix";
            this._sceMustNotHavePrefix.Items = new string[0];
            this._sceMustNotHavePrefix.Location = new System.Drawing.Point(326, 155);
            this._sceMustNotHavePrefix.Name = "_sceMustNotHavePrefix";
            this._sceMustNotHavePrefix.Size = new System.Drawing.Size(286, 141);
            this._sceMustNotHavePrefix.TabIndex = 62;
            // 
            // _sceMustHaveSuffix
            // 
            this._sceMustHaveSuffix.Caption = "Must have suffix";
            this._sceMustHaveSuffix.Items = new string[0];
            this._sceMustHaveSuffix.Location = new System.Drawing.Point(12, 347);
            this._sceMustHaveSuffix.Name = "_sceMustHaveSuffix";
            this._sceMustHaveSuffix.Size = new System.Drawing.Size(286, 141);
            this._sceMustHaveSuffix.TabIndex = 63;
            // 
            // _sceMustNotHaveSuffix
            // 
            this._sceMustNotHaveSuffix.Caption = "Must not have suffix";
            this._sceMustNotHaveSuffix.Items = new string[0];
            this._sceMustNotHaveSuffix.Location = new System.Drawing.Point(326, 347);
            this._sceMustNotHaveSuffix.Name = "_sceMustNotHaveSuffix";
            this._sceMustNotHaveSuffix.Size = new System.Drawing.Size(286, 141);
            this._sceMustNotHaveSuffix.TabIndex = 64;
            // 
            // EditRule
            // 
            this.AcceptButton = this._btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this._btnCancel;
            this.ClientSize = new System.Drawing.Size(626, 527);
            this.Controls.Add(this._sceMustNotHaveSuffix);
            this.Controls.Add(this._sceMustHaveSuffix);
            this.Controls.Add(this._sceMustNotHavePrefix);
            this.Controls.Add(this._sceMustHavePrefix);
            this.Controls.Add(this._mceNotMatches);
            this.Controls.Add(this._mceMatches);
            this.Controls.Add(this._cbDisabled);
            this.Controls.Add(this._lbNotMatches);
            this.Controls.Add(this._lbMatches);
            this.Controls.Add(this._btnCancel);
            this.Controls.Add(this._btnOK);
            this.Controls.Add(this._tbRegex);
            this.Controls.Add(this._lbRegEx);
            this.Controls.Add(this._lbStyle);
            this.Controls.Add(this._cbStyle);
            this.Controls.Add(this._tbDescription);
            this.Controls.Add(this._lbDescription);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "EditRule";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Edit Rule";
            this.Validating += new System.ComponentModel.CancelEventHandler(this.editRule_Validating);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox _tbDescription;
        private System.Windows.Forms.Label _lbDescription;
        private System.Windows.Forms.Label _lbStyle;
        private System.Windows.Forms.ComboBox _cbStyle;
        private System.Windows.Forms.Label _lbRegEx;
        private System.Windows.Forms.TextBox _tbRegex;
        private System.Windows.Forms.Button _btnOK;
        private System.Windows.Forms.Button _btnCancel;
        private System.Windows.Forms.Label _lbMatches;
        private System.Windows.Forms.Label _lbNotMatches;
        private System.Windows.Forms.CheckBox _cbDisabled;
        private MatchCollectionEdit _mceMatches;
        private MatchCollectionEdit _mceNotMatches;
        private StringListEdit _sceMustHavePrefix;
        private StringListEdit _sceMustNotHavePrefix;
        private StringListEdit _sceMustHaveSuffix;
        private StringListEdit _sceMustNotHaveSuffix;
    }
}