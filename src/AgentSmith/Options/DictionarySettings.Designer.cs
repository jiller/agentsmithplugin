namespace AgentSmith.Options
{
    partial class DictionarySettings
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
            this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
            this._cbDictionary = new System.Windows.Forms.ComboBox();
            this._lbDictionary = new System.Windows.Forms.Label();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this._btnImport = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this._btnExportFxCop = new System.Windows.Forms.Button();
            this._btnImportFxCop = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this._cbCaseSensitive = new System.Windows.Forms.CheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this._userWords = new System.Windows.Forms.Label();
            this._tbUserDictionary = new System.Windows.Forms.TextBox();
            this.tableLayoutPanel4.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel4
            // 
            this.tableLayoutPanel4.ColumnCount = 2;
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel4.Controls.Add(this._cbDictionary, 0, 0);
            this.tableLayoutPanel4.Controls.Add(this._lbDictionary, 0, 0);
            this.tableLayoutPanel4.Location = new System.Drawing.Point(19, 94);
            this.tableLayoutPanel4.Name = "tableLayoutPanel4";
            this.tableLayoutPanel4.RowCount = 1;
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel4.Size = new System.Drawing.Size(319, 30);
            this.tableLayoutPanel4.TabIndex = 100;
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
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.ColumnCount = 2;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel3.Controls.Add(this._btnImport, 0, 0);
            this.tableLayoutPanel3.Controls.Add(this.label4, 0, 0);
            this.tableLayoutPanel3.Location = new System.Drawing.Point(16, 17);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 1;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel3.Size = new System.Drawing.Size(322, 29);
            this.tableLayoutPanel3.TabIndex = 99;
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
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(333, 152);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(102, 13);
            this.label6.TabIndex = 98;
            this.label6.Text = "FxCop import/export";
            // 
            // _btnExportFxCop
            // 
            this._btnExportFxCop.Location = new System.Drawing.Point(332, 197);
            this._btnExportFxCop.Name = "_btnExportFxCop";
            this._btnExportFxCop.Size = new System.Drawing.Size(74, 23);
            this._btnExportFxCop.TabIndex = 97;
            this._btnExportFxCop.Text = "Export...";
            this._btnExportFxCop.UseVisualStyleBackColor = true;
            this._btnExportFxCop.Click += new System.EventHandler(this.btnExportFxCop_Click);
            // 
            // _btnImportFxCop
            // 
            this._btnImportFxCop.Location = new System.Drawing.Point(332, 168);
            this._btnImportFxCop.Name = "_btnImportFxCop";
            this._btnImportFxCop.Size = new System.Drawing.Size(74, 23);
            this._btnImportFxCop.TabIndex = 96;
            this._btnImportFxCop.Text = "Import...";
            this._btnImportFxCop.UseVisualStyleBackColor = true;
            this._btnImportFxCop.Click += new System.EventHandler(this.btnImportFxCop_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label5.Location = new System.Drawing.Point(16, 67);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(104, 13);
            this.label5.TabIndex = 95;
            this.label5.Text = "User Dictionaries";
            // 
            // panel2
            // 
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Location = new System.Drawing.Point(16, 52);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(515, 2);
            this.panel2.TabIndex = 92;
            // 
            // _cbCaseSensitive
            // 
            this._cbCaseSensitive.AutoSize = true;
            this._cbCaseSensitive.Location = new System.Drawing.Point(176, 130);
            this._cbCaseSensitive.Name = "_cbCaseSensitive";
            this._cbCaseSensitive.Size = new System.Drawing.Size(153, 17);
            this._cbCaseSensitive.TabIndex = 94;
            this._cbCaseSensitive.Text = "Dictionary is case sensitive";
            this._cbCaseSensitive.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label2.Location = new System.Drawing.Point(13, 4);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(123, 13);
            this.label2.TabIndex = 93;
            this.label2.Text = "Manage Dictionaries";
            // 
            // _userWords
            // 
            this._userWords.AutoSize = true;
            this._userWords.Location = new System.Drawing.Point(16, 131);
            this._userWords.Name = "_userWords";
            this._userWords.Size = new System.Drawing.Size(108, 13);
            this._userWords.TabIndex = 90;
            this._userWords.Text = "User dictionary words";
            // 
            // _tbUserDictionary
            // 
            this._tbUserDictionary.AcceptsReturn = true;
            this._tbUserDictionary.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this._tbUserDictionary.Location = new System.Drawing.Point(16, 152);
            this._tbUserDictionary.Multiline = true;
            this._tbUserDictionary.Name = "_tbUserDictionary";
            this._tbUserDictionary.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this._tbUserDictionary.Size = new System.Drawing.Size(310, 325);
            this._tbUserDictionary.TabIndex = 91;
            this._tbUserDictionary.WordWrap = false;
            // 
            // DictionarySettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLayoutPanel4);
            this.Controls.Add(this.tableLayoutPanel3);
            this.Controls.Add(this.label6);
            this.Controls.Add(this._btnExportFxCop);
            this.Controls.Add(this._btnImportFxCop);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this._cbCaseSensitive);
            this.Controls.Add(this.label2);
            this.Controls.Add(this._userWords);
            this.Controls.Add(this._tbUserDictionary);
            this.Name = "DictionarySettings";
            this.Size = new System.Drawing.Size(554, 495);
            this.tableLayoutPanel4.ResumeLayout(false);
            this.tableLayoutPanel4.PerformLayout();
            this.tableLayoutPanel3.ResumeLayout(false);
            this.tableLayoutPanel3.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel4;
        private System.Windows.Forms.ComboBox _cbDictionary;
        private System.Windows.Forms.Label _lbDictionary;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.Button _btnImport;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button _btnExportFxCop;
        private System.Windows.Forms.Button _btnImportFxCop;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.CheckBox _cbCaseSensitive;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label _userWords;
        private System.Windows.Forms.TextBox _tbUserDictionary;
    }
}
