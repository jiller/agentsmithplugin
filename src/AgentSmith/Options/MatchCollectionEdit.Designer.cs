namespace AgentSmith.Options
{
    partial class MatchCollectionEdit
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
            this._lvMatches = new System.Windows.Forms.ListView();
            this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
            this._btnAdd = new System.Windows.Forms.Button();
            this._btnEdit = new System.Windows.Forms.Button();
            this._btnDelete = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // _lvMatches
            // 
            this._lvMatches.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this._lvMatches.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1});
            this._lvMatches.FullRowSelect = true;
            this._lvMatches.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this._lvMatches.HideSelection = false;
            this._lvMatches.Location = new System.Drawing.Point(0, 0);
            this._lvMatches.Name = "_lvMatches";
            this._lvMatches.ShowItemToolTips = true;
            this._lvMatches.Size = new System.Drawing.Size(152, 93);
            this._lvMatches.TabIndex = 56;
            this._lvMatches.UseCompatibleStateImageBehavior = false;
            this._lvMatches.View = System.Windows.Forms.View.Details;
            this._lvMatches.DoubleClick += new System.EventHandler(this.lvMatches_DoubleClick);
            this._lvMatches.KeyDown += new System.Windows.Forms.KeyEventHandler(this.lvMatches_KeyDown);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Description";
            this.columnHeader1.Width = 140;
            // 
            // _btnAdd
            // 
            this._btnAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this._btnAdd.Location = new System.Drawing.Point(156, 0);
            this._btnAdd.Name = "_btnAdd";
            this._btnAdd.Size = new System.Drawing.Size(38, 23);
            this._btnAdd.TabIndex = 57;
            this._btnAdd.Text = "Add";
            this._btnAdd.UseVisualStyleBackColor = true;
            this._btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // _btnEdit
            // 
            this._btnEdit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this._btnEdit.Location = new System.Drawing.Point(156, 25);
            this._btnEdit.Name = "_btnEdit";
            this._btnEdit.Size = new System.Drawing.Size(38, 23);
            this._btnEdit.TabIndex = 58;
            this._btnEdit.Text = "Edit";
            this._btnEdit.UseVisualStyleBackColor = true;
            this._btnEdit.Click += new System.EventHandler(this.btnEdit_Click);
            // 
            // _btnDelete
            // 
            this._btnDelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this._btnDelete.Location = new System.Drawing.Point(156, 50);
            this._btnDelete.Name = "_btnDelete";
            this._btnDelete.Size = new System.Drawing.Size(38, 23);
            this._btnDelete.TabIndex = 59;
            this._btnDelete.Text = "Del";
            this._btnDelete.UseVisualStyleBackColor = true;
            this._btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // MatchCollectionEdit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this._btnDelete);
            this.Controls.Add(this._btnEdit);
            this.Controls.Add(this._btnAdd);
            this.Controls.Add(this._lvMatches);
            this.Name = "MatchCollectionEdit";
            this.Size = new System.Drawing.Size(199, 93);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView _lvMatches;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.Button _btnAdd;
        private System.Windows.Forms.Button _btnEdit;
        private System.Windows.Forms.Button _btnDelete;
    }
}
