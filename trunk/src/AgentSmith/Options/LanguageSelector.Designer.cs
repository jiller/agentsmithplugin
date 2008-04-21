namespace AgentSmith.Options
{
    partial class LanguageSelector
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
            this._tbDicts = new System.Windows.Forms.TextBox();
            this._btnSelect = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // _tbDicts
            // 
            this._tbDicts.Anchor = System.Windows.Forms.AnchorStyles.None;
            this._tbDicts.Location = new System.Drawing.Point(0, 2);
            this._tbDicts.Name = "_tbDicts";
            this._tbDicts.ReadOnly = true;
            this._tbDicts.Size = new System.Drawing.Size(59, 20);
            this._tbDicts.TabIndex = 0;
            // 
            // _btnSelect
            // 
            this._btnSelect.Anchor = System.Windows.Forms.AnchorStyles.None;
            this._btnSelect.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this._btnSelect.Location = new System.Drawing.Point(60, 1);
            this._btnSelect.Name = "_btnSelect";
            this._btnSelect.Size = new System.Drawing.Size(27, 21);
            this._btnSelect.TabIndex = 1;
            this._btnSelect.Text = "...";
            this._btnSelect.UseVisualStyleBackColor = true;
            this._btnSelect.Click += new System.EventHandler(this.btnSelect_Click);
            // 
            // LanguageSelector
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this._btnSelect);
            this.Controls.Add(this._tbDicts);
            this.MinimumSize = new System.Drawing.Size(0, 22);
            this.Name = "LanguageSelector";
            this.Size = new System.Drawing.Size(87, 22);
            this.Layout += new System.Windows.Forms.LayoutEventHandler(this.languageSelector_Layout);            
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox _tbDicts;
        private System.Windows.Forms.Button _btnSelect;
    }
}
