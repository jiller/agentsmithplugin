namespace AgentSmith.Options
{
    partial class StringListEdit
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(StringListEdit));
            this._topPanel = new System.Windows.Forms.Panel();
            this._captionLabel = new System.Windows.Forms.Label();
            this._bottomPanel = new System.Windows.Forms.Panel();
            this._itemsListBox = new System.Windows.Forms.ListBox();
            this._toolStrip = new System.Windows.Forms.ToolStrip();
            this._addButton = new System.Windows.Forms.ToolStripButton();
            this._editButton = new System.Windows.Forms.ToolStripButton();
            this._removeButton = new System.Windows.Forms.ToolStripButton();
            this._topPanel.SuspendLayout();
            this._bottomPanel.SuspendLayout();
            this._toolStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // _topPanel
            // 
            this._topPanel.Controls.Add(this._captionLabel);
            this._topPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this._topPanel.Location = new System.Drawing.Point(0, 0);
            this._topPanel.Name = "_topPanel";
            this._topPanel.Size = new System.Drawing.Size(315, 23);
            this._topPanel.TabIndex = 0;
            // 
            // _captionLabel
            // 
            this._captionLabel.AutoSize = true;
            this._captionLabel.Location = new System.Drawing.Point(4, 4);
            this._captionLabel.Name = "_captionLabel";
            this._captionLabel.Size = new System.Drawing.Size(43, 13);
            this._captionLabel.TabIndex = 0;
            this._captionLabel.Text = "Caption";
            // 
            // _bottomPanel
            // 
            this._bottomPanel.Controls.Add(this._itemsListBox);
            this._bottomPanel.Controls.Add(this._toolStrip);
            this._bottomPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this._bottomPanel.Location = new System.Drawing.Point(0, 23);
            this._bottomPanel.Name = "_bottomPanel";
            this._bottomPanel.Size = new System.Drawing.Size(315, 251);
            this._bottomPanel.TabIndex = 1;
            // 
            // _itemsListBox
            // 
            this._itemsListBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this._itemsListBox.FormattingEnabled = true;
            this._itemsListBox.Location = new System.Drawing.Point(0, 25);
            this._itemsListBox.Name = "_itemsListBox";
            this._itemsListBox.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this._itemsListBox.Size = new System.Drawing.Size(315, 225);
            this._itemsListBox.TabIndex = 2;
            // 
            // _toolStrip
            // 
            this._toolStrip.AllowMerge = false;
            this._toolStrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this._toolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._addButton,
            this._editButton,
            this._removeButton});
            this._toolStrip.Location = new System.Drawing.Point(0, 0);
            this._toolStrip.Name = "_toolStrip";
            this._toolStrip.Size = new System.Drawing.Size(315, 25);
            this._toolStrip.TabIndex = 1;
            this._toolStrip.Text = "Menu";
            // 
            // _addButton
            // 
            this._addButton.Image = ((System.Drawing.Image)(resources.GetObject("_addButton.Image")));
            this._addButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._addButton.Name = "_addButton";
            this._addButton.Size = new System.Drawing.Size(46, 22);
            this._addButton.Text = "Add";
            // 
            // _editButton
            // 
            this._editButton.Image = ((System.Drawing.Image)(resources.GetObject("_editButton.Image")));
            this._editButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._editButton.Name = "_editButton";
            this._editButton.Size = new System.Drawing.Size(45, 22);
            this._editButton.Text = "Edit";
            // 
            // _removeButton
            // 
            this._removeButton.Image = ((System.Drawing.Image)(resources.GetObject("_removeButton.Image")));
            this._removeButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._removeButton.Name = "_removeButton";
            this._removeButton.Size = new System.Drawing.Size(66, 22);
            this._removeButton.Text = "Remove";
            // 
            // StringListEdit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this._bottomPanel);
            this.Controls.Add(this._topPanel);
            this.Name = "StringListEdit";
            this.Size = new System.Drawing.Size(315, 274);
            this._topPanel.ResumeLayout(false);
            this._topPanel.PerformLayout();
            this._bottomPanel.ResumeLayout(false);
            this._bottomPanel.PerformLayout();
            this._toolStrip.ResumeLayout(false);
            this._toolStrip.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel _topPanel;
        private System.Windows.Forms.Panel _bottomPanel;
        private System.Windows.Forms.ToolStrip _toolStrip;
        private System.Windows.Forms.ToolStripButton _addButton;
        private System.Windows.Forms.ToolStripButton _editButton;
        private System.Windows.Forms.ToolStripButton _removeButton;
        private System.Windows.Forms.Label _captionLabel;
        private System.Windows.Forms.ListBox _itemsListBox;

    }
}
