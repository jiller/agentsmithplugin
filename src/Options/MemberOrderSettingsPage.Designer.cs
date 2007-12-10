namespace AgentSmith.Options
{
    partial class MemberOrderSettingsPage
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
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this._rbVisibilityType = new System.Windows.Forms.RadioButton();
            this._rbTypeVisibility = new System.Windows.Forms.RadioButton();
            this._cbMemberOrderEnabled = new System.Windows.Forms.CheckBox();
            this._lbVisibility = new AgentSmith.Options.SortedListBox();
            this._lbType = new AgentSmith.Options.SortedListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(0, 0);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(104, 24);
            this.checkBox1.TabIndex = 0;
            this.checkBox1.Text = "checkBox1";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // _rbVisibilityType
            // 
            this._rbVisibilityType.AutoSize = true;
            this._rbVisibilityType.Location = new System.Drawing.Point(13, 30);
            this._rbVisibilityType.Name = "_rbVisibilityType";
            this._rbVisibilityType.Size = new System.Drawing.Size(136, 17);
            this._rbVisibilityType.TabIndex = 1;
            this._rbVisibilityType.TabStop = true;
            this._rbVisibilityType.Text = "By visibility then by type";
            this._rbVisibilityType.UseVisualStyleBackColor = true;
            // 
            // _rbTypeVisibility
            // 
            this._rbTypeVisibility.AutoSize = true;
            this._rbTypeVisibility.Location = new System.Drawing.Point(13, 53);
            this._rbTypeVisibility.Name = "_rbTypeVisibility";
            this._rbTypeVisibility.Size = new System.Drawing.Size(136, 17);
            this._rbTypeVisibility.TabIndex = 2;
            this._rbTypeVisibility.TabStop = true;
            this._rbTypeVisibility.Text = "By type then by visibility";
            this._rbTypeVisibility.UseVisualStyleBackColor = true;
            // 
            // _cbMemberOrderEnabled
            // 
            this._cbMemberOrderEnabled.AutoSize = true;
            this._cbMemberOrderEnabled.Location = new System.Drawing.Point(13, 7);
            this._cbMemberOrderEnabled.Name = "_cbMemberOrderEnabled";
            this._cbMemberOrderEnabled.Size = new System.Drawing.Size(179, 17);
            this._cbMemberOrderEnabled.TabIndex = 3;
            this._cbMemberOrderEnabled.Text = "Member order checking enabled";
            this._cbMemberOrderEnabled.UseVisualStyleBackColor = true;
            // 
            // _lbVisibility
            // 
            this._lbVisibility.Items = new object[0];
            this._lbVisibility.Location = new System.Drawing.Point(14, 110);
            this._lbVisibility.Name = "_lbVisibility";
            this._lbVisibility.Size = new System.Drawing.Size(202, 195);
            this._lbVisibility.TabIndex = 4;
            // 
            // _lbType
            // 
            this._lbType.Items = new object[0];
            this._lbType.Location = new System.Drawing.Point(222, 107);
            this._lbType.Name = "_lbType";
            this._lbType.Size = new System.Drawing.Size(209, 206);
            this._lbType.TabIndex = 5;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(219, 91);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(72, 13);
            this.label1.TabIndex = 10;
            this.label1.Text = "Member Type";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 91);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(43, 13);
            this.label2.TabIndex = 11;
            this.label2.Text = "Visibility";
            // 
            // MemberOrderSettingsPage
            // 
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this._lbType);
            this.Controls.Add(this._lbVisibility);
            this.Controls.Add(this._cbMemberOrderEnabled);
            this.Controls.Add(this._rbTypeVisibility);
            this.Controls.Add(this._rbVisibilityType);
            this.Name = "MemberOrderSettingsPage";
            this.Size = new System.Drawing.Size(435, 371);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.RadioButton _rbVisibilityType;
        private System.Windows.Forms.RadioButton _rbTypeVisibility;
        private System.Windows.Forms.CheckBox _cbMemberOrderEnabled;
        private SortedListBox _lbVisibility;
        private SortedListBox _lbType;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
    }
}
