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
            this.label3 = new System.Windows.Forms.Label();
            this._sceMustHaveSuffix = new JetBrains.CommonControls.StringCollectionEdit();
            this._sceMustNotHavePrefix = new JetBrains.CommonControls.StringCollectionEdit();
            this._sceMustHavePrefix = new JetBrains.CommonControls.StringCollectionEdit();
            this._sceMustNotHaveSuffix = new JetBrains.CommonControls.StringCollectionEdit();
            this.label4 = new System.Windows.Forms.Label();
            this._cbStyle = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this._tbRegex = new System.Windows.Forms.TextBox();
            this._btnOK = new System.Windows.Forms.Button();
            this._btnCancel = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this._cbDisabled = new System.Windows.Forms.CheckBox();
            this._mceNotMatches = new AgentSmith.Options.MatchCollectionEdit();
            this._mceMatches = new AgentSmith.Options.MatchCollectionEdit();
            this.SuspendLayout();
            // 
            // _tbDescription
            // 
            this._tbDescription.Location = new System.Drawing.Point(15, 25);
            this._tbDescription.Name = "_tbDescription";
            this._tbDescription.Size = new System.Drawing.Size(543, 20);
            this._tbDescription.TabIndex = 34;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 9);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(312, 13);
            this.label3.TabIndex = 33;
            this.label3.Text = "Description (this text will appear as description of the highlighting)";
            // 
            // _sceMustHaveSuffix
            // 
            this._sceMustHaveSuffix.Location = new System.Drawing.Point(16, 309);
            this._sceMustHaveSuffix.Name = "_sceMustHaveSuffix";
            this._sceMustHaveSuffix.Size = new System.Drawing.Size(265, 75);
            this._sceMustHaveSuffix.Strings = new string[0];
            this._sceMustHaveSuffix.TabIndex = 45;
            // 
            // _sceMustNotHavePrefix
            // 
            this._sceMustNotHavePrefix.Location = new System.Drawing.Point(326, 172);
            this._sceMustNotHavePrefix.Name = "_sceMustNotHavePrefix";
            this._sceMustNotHavePrefix.Size = new System.Drawing.Size(259, 66);
            this._sceMustNotHavePrefix.Strings = new string[0];
            this._sceMustNotHavePrefix.TabIndex = 44;
            // 
            // _sceMustHavePrefix
            // 
            this._sceMustHavePrefix.Location = new System.Drawing.Point(15, 172);
            this._sceMustHavePrefix.Name = "_sceMustHavePrefix";
            this._sceMustHavePrefix.Size = new System.Drawing.Size(266, 66);
            this._sceMustHavePrefix.Strings = new string[0];
            this._sceMustHavePrefix.TabIndex = 43;
            // 
            // _sceMustNotHaveSuffix
            // 
            this._sceMustNotHaveSuffix.Location = new System.Drawing.Point(326, 309);
            this._sceMustNotHaveSuffix.Name = "_sceMustNotHaveSuffix";
            this._sceMustNotHaveSuffix.Size = new System.Drawing.Size(259, 77);
            this._sceMustNotHaveSuffix.Strings = new string[0];
            this._sceMustNotHaveSuffix.TabIndex = 40;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 247);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(30, 13);
            this.label4.TabIndex = 36;
            this.label4.Text = "Style";
            // 
            // _cbStyle
            // 
            this._cbStyle.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this._cbStyle.FormattingEnabled = true;
            this._cbStyle.Items.AddRange(new object[] {
            "Pascal",
            "Camel",
            "Uppercase"});
            this._cbStyle.Location = new System.Drawing.Point(15, 263);
            this._cbStyle.Name = "_cbStyle";
            this._cbStyle.Size = new System.Drawing.Size(239, 21);
            this._cbStyle.TabIndex = 35;
            this._cbStyle.SelectedValueChanged += new System.EventHandler(this.cbStyle_SelectedValueChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(323, 156);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(103, 13);
            this.label1.TabIndex = 46;
            this.label1.Text = "Must not have prefix";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 156);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(85, 13);
            this.label2.TabIndex = 47;
            this.label2.Text = "Must have prefix";
            this.label2.Click += new System.EventHandler(this.label2_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 293);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(84, 13);
            this.label5.TabIndex = 48;
            this.label5.Text = "Must have suffix";
            this.label5.Click += new System.EventHandler(this.label5_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(323, 293);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(102, 13);
            this.label6.TabIndex = 49;
            this.label6.Text = "Must not have suffix";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(323, 247);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(98, 13);
            this.label7.TabIndex = 50;
            this.label7.Text = "Regular Expression";
            // 
            // _tbRegex
            // 
            this._tbRegex.Location = new System.Drawing.Point(326, 264);
            this._tbRegex.Name = "_tbRegex";
            this._tbRegex.Size = new System.Drawing.Size(232, 20);
            this._tbRegex.TabIndex = 51;
            // 
            // _btnOK
            // 
            this._btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this._btnOK.Location = new System.Drawing.Point(439, 404);
            this._btnOK.Name = "_btnOK";
            this._btnOK.Size = new System.Drawing.Size(75, 23);
            this._btnOK.TabIndex = 52;
            this._btnOK.Text = "OK";
            this._btnOK.UseVisualStyleBackColor = true;
            this._btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // _btnCancel
            // 
            this._btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this._btnCancel.Location = new System.Drawing.Point(520, 404);
            this._btnCancel.Name = "_btnCancel";
            this._btnCancel.Size = new System.Drawing.Size(75, 23);
            this._btnCancel.TabIndex = 53;
            this._btnCancel.Text = "Cancel";
            this._btnCancel.UseVisualStyleBackColor = true;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(12, 53);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(202, 13);
            this.label8.TabIndex = 54;
            this.label8.Text = "Matches (declarations this rule applied to)";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(323, 53);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(222, 13);
            this.label9.TabIndex = 56;
            this.label9.Text = "Except (declarations this rule doesn\'t apply to)";
            // 
            // _cbDisabled
            // 
            this._cbDisabled.AutoSize = true;
            this._cbDisabled.Location = new System.Drawing.Point(16, 390);
            this._cbDisabled.Name = "_cbDisabled";
            this._cbDisabled.Size = new System.Drawing.Size(100, 17);
            this._cbDisabled.TabIndex = 58;
            this._cbDisabled.Text = "Rule is disabled";
            this._cbDisabled.UseVisualStyleBackColor = true;
            // 
            // _mceNotMatches
            // 
            this._mceNotMatches.Location = new System.Drawing.Point(326, 71);
            this._mceNotMatches.Matches = new Match[0];
            this._mceNotMatches.Name = "_mceNotMatches";
            this._mceNotMatches.Size = new System.Drawing.Size(278, 78);
            this._mceNotMatches.TabIndex = 60;
            // 
            // _mceMatches
            // 
            this._mceMatches.Location = new System.Drawing.Point(15, 68);
            this._mceMatches.Matches = new Match[0];
            this._mceMatches.Name = "_mceMatches";
            this._mceMatches.Size = new System.Drawing.Size(286, 81);
            this._mceMatches.TabIndex = 59;
            // 
            // EditRule
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(607, 433);
            this.Controls.Add(this._mceNotMatches);
            this.Controls.Add(this._mceMatches);
            this.Controls.Add(this._cbDisabled);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label8);
            this.Controls.Add(this._btnCancel);
            this.Controls.Add(this._btnOK);
            this.Controls.Add(this._tbRegex);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this._sceMustHaveSuffix);
            this.Controls.Add(this._sceMustNotHavePrefix);
            this.Controls.Add(this._sceMustHavePrefix);
            this.Controls.Add(this._sceMustNotHaveSuffix);
            this.Controls.Add(this.label4);
            this.Controls.Add(this._cbStyle);
            this.Controls.Add(this._tbDescription);
            this.Controls.Add(this.label3);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "EditRule";
            this.Text = "Edit Rule";
            this.Validating += new System.ComponentModel.CancelEventHandler(this.editRule_Validating);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox _tbDescription;
        private System.Windows.Forms.Label label3;
        private StringCollectionEdit _sceMustHaveSuffix;
        private StringCollectionEdit _sceMustNotHavePrefix;
        private StringCollectionEdit _sceMustHavePrefix;
        private StringCollectionEdit _sceMustNotHaveSuffix;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox _cbStyle;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox _tbRegex;
        private System.Windows.Forms.Button _btnOK;
        private System.Windows.Forms.Button _btnCancel;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.CheckBox _cbDisabled;
        private MatchCollectionEdit _mceMatches;
        private MatchCollectionEdit _mceNotMatches;
    }
}