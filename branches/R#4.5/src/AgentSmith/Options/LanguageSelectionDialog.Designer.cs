namespace AgentSmith.Options
{
    partial class LanguageSelectionDialog
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
            this._lvDictionaries = new System.Windows.Forms.ListView();
            this._btnOK = new System.Windows.Forms.Button();
            this._btnCancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // _lvDictionaries
            // 
            this._lvDictionaries.CheckBoxes = true;
            this._lvDictionaries.Location = new System.Drawing.Point(3, 2);
            this._lvDictionaries.Name = "_lvDictionaries";
            this._lvDictionaries.Size = new System.Drawing.Size(153, 97);
            this._lvDictionaries.TabIndex = 0;
            this._lvDictionaries.UseCompatibleStateImageBehavior = false;
            this._lvDictionaries.View = System.Windows.Forms.View.List;
            // 
            // _btnOK
            // 
            this._btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this._btnOK.Location = new System.Drawing.Point(3, 105);
            this._btnOK.Name = "_btnOK";
            this._btnOK.Size = new System.Drawing.Size(77, 23);
            this._btnOK.TabIndex = 1;
            this._btnOK.Text = "OK";
            this._btnOK.UseVisualStyleBackColor = true;
            // 
            // _btnCancel
            // 
            this._btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this._btnCancel.Location = new System.Drawing.Point(86, 105);
            this._btnCancel.Name = "_btnCancel";
            this._btnCancel.Size = new System.Drawing.Size(70, 23);
            this._btnCancel.TabIndex = 2;
            this._btnCancel.Text = "Cancel";
            this._btnCancel.UseVisualStyleBackColor = true;
            // 
            // LanguageSelectionDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(159, 133);
            this.Controls.Add(this._btnCancel);
            this.Controls.Add(this._btnOK);
            this.Controls.Add(this._lvDictionaries);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "LanguageSelectionDialog";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Languages";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView _lvDictionaries;
        private System.Windows.Forms.Button _btnOK;
        private System.Windows.Forms.Button _btnCancel;

    }
}