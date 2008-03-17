namespace AgentSmith.Options
{
    partial class SortedListBox
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
            this._btnVisDown = new System.Windows.Forms.Button();
            this._btnVisUp = new System.Windows.Forms.Button();
            this._lbVisibility = new System.Windows.Forms.ListBox();
            this.SuspendLayout();
            // 
            // _btnVisDown
            // 
            this._btnVisDown.Location = new System.Drawing.Point(150, 29);
            this._btnVisDown.Name = "_btnVisDown";
            this._btnVisDown.Size = new System.Drawing.Size(49, 23);
            this._btnVisDown.TabIndex = 10;
            this._btnVisDown.Text = "Down";
            this._btnVisDown.UseVisualStyleBackColor = true;
            this._btnVisDown.Click += new System.EventHandler(this.btnVisDown_Click);
            // 
            // _btnVisUp
            // 
            this._btnVisUp.Location = new System.Drawing.Point(150, 3);
            this._btnVisUp.Name = "_btnVisUp";
            this._btnVisUp.Size = new System.Drawing.Size(49, 23);
            this._btnVisUp.TabIndex = 9;
            this._btnVisUp.Text = "Up";
            this._btnVisUp.UseVisualStyleBackColor = true;
            this._btnVisUp.Click += new System.EventHandler(this.btnVisUp_Click);
            // 
            // _lbVisibility
            // 
            this._lbVisibility.FormattingEnabled = true;
            this._lbVisibility.Location = new System.Drawing.Point(3, 3);
            this._lbVisibility.Name = "_lbVisibility";
            this._lbVisibility.Size = new System.Drawing.Size(141, 186);
            this._lbVisibility.TabIndex = 8;
            // 
            // SortedListBox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this._btnVisDown);
            this.Controls.Add(this._btnVisUp);
            this.Controls.Add(this._lbVisibility);
            this.Name = "SortedListBox";
            this.Size = new System.Drawing.Size(204, 212);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button _btnVisDown;
        private System.Windows.Forms.Button _btnVisUp;
        private System.Windows.Forms.ListBox _lbVisibility;

    }
}
