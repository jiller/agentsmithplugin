using JetBrains.CommonControls;

namespace AgentSmith.Options
{
    partial class NamingConventionsSettingsPage
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
            this._lvRules = new System.Windows.Forms.ListView();
            this._chDescription = new System.Windows.Forms.ColumnHeader();
            this._btnAdd = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this._btnUp = new System.Windows.Forms.Button();
            this._btnDown = new System.Windows.Forms.Button();
            this._btnEdit = new System.Windows.Forms.Button();
            this._btnDelete = new System.Windows.Forms.Button();
            this._btnImport = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this._sceExclusions = new AgentSmith.Options.StringListEdit();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // _lvRules
            // 
            this._lvRules.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this._lvRules.CheckBoxes = true;
            this._lvRules.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this._chDescription});
            this._lvRules.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this._lvRules.HideSelection = false;
            this._lvRules.Location = new System.Drawing.Point(13, 19);
            this._lvRules.Name = "_lvRules";
            this._lvRules.Size = new System.Drawing.Size(474, 281);
            this._lvRules.TabIndex = 32;
            this._lvRules.UseCompatibleStateImageBehavior = false;
            this._lvRules.View = System.Windows.Forms.View.Details;
            this._lvRules.ItemChecked += new System.Windows.Forms.ItemCheckedEventHandler(this.lvRules_ItemChecked);
            this._lvRules.KeyDown += new System.Windows.Forms.KeyEventHandler(this.lvRules_KeyDown);
            // 
            // _chDescription
            // 
            this._chDescription.Text = "Description";
            this._chDescription.Width = 370;
            // 
            // _btnAdd
            // 
            this._btnAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this._btnAdd.Location = new System.Drawing.Point(493, 19);
            this._btnAdd.Name = "_btnAdd";
            this._btnAdd.Size = new System.Drawing.Size(75, 23);
            this._btnAdd.TabIndex = 33;
            this._btnAdd.Text = "Add...";
            this._btnAdd.UseVisualStyleBackColor = true;
            this._btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(130, 13);
            this.label2.TabIndex = 34;
            this.label2.Text = "Naming Convention Rules";
            // 
            // _btnUp
            // 
            this._btnUp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this._btnUp.Location = new System.Drawing.Point(493, 49);
            this._btnUp.Name = "_btnUp";
            this._btnUp.Size = new System.Drawing.Size(75, 23);
            this._btnUp.TabIndex = 35;
            this._btnUp.Text = "Up";
            this._btnUp.UseVisualStyleBackColor = true;
            this._btnUp.Click += new System.EventHandler(this.btnUp_Click);
            // 
            // _btnDown
            // 
            this._btnDown.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this._btnDown.Location = new System.Drawing.Point(494, 79);
            this._btnDown.Name = "_btnDown";
            this._btnDown.Size = new System.Drawing.Size(75, 23);
            this._btnDown.TabIndex = 36;
            this._btnDown.Text = "Down";
            this._btnDown.UseVisualStyleBackColor = true;
            this._btnDown.Click += new System.EventHandler(this.btnDown_Click);
            // 
            // _btnEdit
            // 
            this._btnEdit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this._btnEdit.Location = new System.Drawing.Point(494, 109);
            this._btnEdit.Name = "_btnEdit";
            this._btnEdit.Size = new System.Drawing.Size(75, 23);
            this._btnEdit.TabIndex = 37;
            this._btnEdit.Text = "Edit...";
            this._btnEdit.UseVisualStyleBackColor = true;
            this._btnEdit.Click += new System.EventHandler(this.btnEdit_Click);
            // 
            // _btnDelete
            // 
            this._btnDelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this._btnDelete.Location = new System.Drawing.Point(493, 138);
            this._btnDelete.Name = "_btnDelete";
            this._btnDelete.Size = new System.Drawing.Size(75, 23);
            this._btnDelete.TabIndex = 38;
            this._btnDelete.Text = "Delete...";
            this._btnDelete.UseVisualStyleBackColor = true;
            this._btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // _btnImport
            // 
            this._btnImport.Location = new System.Drawing.Point(230, 316);
            this._btnImport.Name = "_btnImport";
            this._btnImport.Size = new System.Drawing.Size(75, 23);
            this._btnImport.TabIndex = 38;
            this._btnImport.Text = "Import...";
            this._btnImport.UseVisualStyleBackColor = true;
            this._btnImport.Click += new System.EventHandler(this.btnImport_Click);
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(16, 554);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(289, 13);
            this.label5.TabIndex = 67;
            this.label5.Text = " and select appropriate level for an Agent Smith highlighting.";
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(13, 537);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(431, 13);
            this.label4.TabIndex = 66;
            this.label4.Text = "* To enable/disable naming convention validation please go to the Inspection Seve" +
                "rity tab";
            // 
            // _sceExclusions
            // 
            this._sceExclusions.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this._sceExclusions.Caption = "Exclusions (these name will not be validated)";
            this._sceExclusions.Items = new string[0];
            this._sceExclusions.Location = new System.Drawing.Point(13, 345);
            this._sceExclusions.Name = "_sceExclusions";
            this._sceExclusions.Size = new System.Drawing.Size(555, 189);
            this._sceExclusions.TabIndex = 68;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(16, 321);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(181, 13);
            this.label1.TabIndex = 68;
            this.label1.Text = "Import ReSharper naming rules";
            // 
            // NamingConventionsSettingsPage
            // 
            this.Controls.Add(this.label1);
            this.Controls.Add(this._sceExclusions);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this._btnDelete);
            this.Controls.Add(this._btnImport);
            this.Controls.Add(this._btnEdit);
            this.Controls.Add(this._btnDown);
            this.Controls.Add(this._btnUp);
            this.Controls.Add(this.label2);
            this.Controls.Add(this._btnAdd);
            this.Controls.Add(this._lvRules);
            this.Name = "NamingConventionsSettingsPage";
            this.Size = new System.Drawing.Size(586, 583);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListView _lvRules;
        private System.Windows.Forms.ColumnHeader _chDescription;
        private System.Windows.Forms.Button _btnAdd;
        private System.Windows.Forms.Button _btnImport;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button _btnUp;
        private System.Windows.Forms.Button _btnDown;
        private System.Windows.Forms.Button _btnEdit;
        private System.Windows.Forms.Button _btnDelete;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private StringListEdit _sceExclusions;
        private System.Windows.Forms.Label label1;
    }
}
