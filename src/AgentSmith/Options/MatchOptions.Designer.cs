namespace AgentSmith.Options
{
    partial class MatchOptions
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
            this._tbInheritedFrom = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this._tbMarkedWithAttribute = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this._lbMember = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this._btnOK = new System.Windows.Forms.Button();
            this._btnCancel = new System.Windows.Forms.Button();
            this._cbVisibility = new System.Windows.Forms.CheckedListBox();
            this._cbStatic = new System.Windows.Forms.CheckBox();
            this._cbReadonly = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // _tbInheritedFrom
            // 
            this._tbInheritedFrom.Location = new System.Drawing.Point(155, 143);
            this._tbInheritedFrom.Name = "_tbInheritedFrom";
            this._tbInheritedFrom.Size = new System.Drawing.Size(217, 20);
            this._tbInheritedFrom.TabIndex = 22;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(12, 146);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(119, 13);
            this.label7.TabIndex = 21;
            this.label7.Text = "Inherited from/Is of type";
            // 
            // _tbMarkedWithAttribute
            // 
            this._tbMarkedWithAttribute.Location = new System.Drawing.Point(155, 168);
            this._tbMarkedWithAttribute.Name = "_tbMarkedWithAttribute";
            this._tbMarkedWithAttribute.Size = new System.Drawing.Size(217, 20);
            this._tbMarkedWithAttribute.TabIndex = 20;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(12, 171);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(106, 13);
            this.label6.TabIndex = 19;
            this.label6.Text = "Marked with attribute";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 119);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(45, 13);
            this.label2.TabIndex = 18;
            this.label2.Text = "Member";
            // 
            // _lbMember
            // 
            this._lbMember.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this._lbMember.FormattingEnabled = true;
            this._lbMember.Location = new System.Drawing.Point(155, 116);
            this._lbMember.Name = "_lbMember";
            this._lbMember.Size = new System.Drawing.Size(217, 21);
            this._lbMember.TabIndex = 17;
            this._lbMember.SelectedIndexChanged += new System.EventHandler(this.lbMember_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 42);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(43, 13);
            this.label1.TabIndex = 16;
            this.label1.Text = "Visibility";
            // 
            // _btnOK
            // 
            this._btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this._btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this._btnOK.Location = new System.Drawing.Point(216, 219);
            this._btnOK.Name = "_btnOK";
            this._btnOK.Size = new System.Drawing.Size(75, 23);
            this._btnOK.TabIndex = 23;
            this._btnOK.Text = "OK";
            this._btnOK.UseVisualStyleBackColor = true;
            this._btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // _btnCancel
            // 
            this._btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this._btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this._btnCancel.Location = new System.Drawing.Point(297, 219);
            this._btnCancel.Name = "_btnCancel";
            this._btnCancel.Size = new System.Drawing.Size(75, 23);
            this._btnCancel.TabIndex = 24;
            this._btnCancel.Text = "Cancel";
            this._btnCancel.UseVisualStyleBackColor = true;
            // 
            // _cbVisibility
            // 
            this._cbVisibility.CheckOnClick = true;
            this._cbVisibility.FormattingEnabled = true;
            this._cbVisibility.Location = new System.Drawing.Point(155, 17);
            this._cbVisibility.Name = "_cbVisibility";
            this._cbVisibility.Size = new System.Drawing.Size(217, 94);
            this._cbVisibility.TabIndex = 25;
            // 
            // _cbStatic
            // 
            this._cbStatic.AutoSize = true;
            this._cbStatic.Location = new System.Drawing.Point(155, 194);
            this._cbStatic.Name = "_cbStatic";
            this._cbStatic.Size = new System.Drawing.Size(51, 17);
            this._cbStatic.TabIndex = 26;
            this._cbStatic.Text = "static";
            this._cbStatic.ThreeState = true;
            this._cbStatic.UseVisualStyleBackColor = true;
            // 
            // _cbReadonly
            // 
            this._cbReadonly.AutoSize = true;
            this._cbReadonly.Location = new System.Drawing.Point(242, 194);
            this._cbReadonly.Name = "_cbReadonly";
            this._cbReadonly.Size = new System.Drawing.Size(66, 17);
            this._cbReadonly.TabIndex = 27;
            this._cbReadonly.Text = "readonly";
            this._cbReadonly.ThreeState = true;
            this._cbReadonly.UseVisualStyleBackColor = true;
            // 
            // MatchOptions
            // 
            this.AcceptButton = this._btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this._btnCancel;
            this.ClientSize = new System.Drawing.Size(383, 254);
            this.Controls.Add(this._cbReadonly);
            this.Controls.Add(this._cbStatic);
            this.Controls.Add(this._cbVisibility);
            this.Controls.Add(this._btnCancel);
            this.Controls.Add(this._btnOK);
            this.Controls.Add(this._tbInheritedFrom);
            this.Controls.Add(this.label7);
            this.Controls.Add(this._tbMarkedWithAttribute);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label2);
            this.Controls.Add(this._lbMember);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MatchOptions";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Match Options";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox _tbInheritedFrom;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox _tbMarkedWithAttribute;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox _lbMember;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button _btnOK;
        private System.Windows.Forms.Button _btnCancel;
        private System.Windows.Forms.CheckedListBox _cbVisibility;
        private System.Windows.Forms.CheckBox _cbStatic;
        private System.Windows.Forms.CheckBox _cbReadonly;
    }
}