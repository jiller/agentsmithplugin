using System;

namespace AgentSmith.Options
{
    partial class AgentSmithSettingsPage
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
            this._lbMustHaveComments = new System.Windows.Forms.Label();
            this._lbMustHaveCommentsExcept = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this._lbPPS = new System.Windows.Forms.Label();
            this._lbPS = new System.Windows.Forms.Label();
            this._cbLookAtBase = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this._mceNotMatches = new AgentSmith.Options.MatchCollectionEdit();
            this._mceMatches = new AgentSmith.Options.MatchCollectionEdit();
            this.SuspendLayout();
            // 
            // _lbMustHaveComments
            // 
            this._lbMustHaveComments.AutoSize = true;
            this._lbMustHaveComments.Location = new System.Drawing.Point(12, 25);
            this._lbMustHaveComments.Name = "_lbMustHaveComments";
            this._lbMustHaveComments.Size = new System.Drawing.Size(192, 13);
            this._lbMustHaveComments.TabIndex = 9;
            this._lbMustHaveComments.Text = "Members that must have xml comments";
            // 
            // _lbMustHaveCommentsExcept
            // 
            this._lbMustHaveCommentsExcept.AutoSize = true;
            this._lbMustHaveCommentsExcept.Location = new System.Drawing.Point(286, 25);
            this._lbMustHaveCommentsExcept.Name = "_lbMustHaveCommentsExcept";
            this._lbMustHaveCommentsExcept.Size = new System.Drawing.Size(40, 13);
            this._lbMustHaveCommentsExcept.TabIndex = 10;
            this._lbMustHaveCommentsExcept.Text = "Except";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label3.Location = new System.Drawing.Point(12, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(151, 13);
            this.label3.TabIndex = 63;
            this.label3.Text = "XML Comment Validation.";
            // 
            // _lbPPS
            // 
            this._lbPPS.AutoSize = true;
            this._lbPPS.Location = new System.Drawing.Point(12, 196);
            this._lbPPS.Name = "_lbPPS";
            this._lbPPS.Size = new System.Drawing.Size(409, 13);
            this._lbPPS.TabIndex = 64;
            this._lbPPS.Text = "* To enable/disable XML comment validation please go to the Inspection Severity t" +
                "ab";
            // 
            // _lbPS
            // 
            this._lbPS.AutoSize = true;
            this._lbPS.Location = new System.Drawing.Point(12, 238);
            this._lbPS.Name = "_lbPS";
            this._lbPS.Size = new System.Drawing.Size(289, 13);
            this._lbPS.TabIndex = 65;
            this._lbPS.Text = " and select appropriate level for an Agent Smith highlighting.";
            // 
            // _cbLookAtBase
            // 
            this._cbLookAtBase.AutoSize = true;
            this._cbLookAtBase.Location = new System.Drawing.Point(15, 160);
            this._cbLookAtBase.Name = "_cbLookAtBase";
            this._cbLookAtBase.Size = new System.Drawing.Size(269, 17);
            this._cbLookAtBase.TabIndex = 66;
            this._cbLookAtBase.Text = "Do not show warning if base member has comment.";
            this._cbLookAtBase.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 225);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(473, 13);
            this.label1.TabIndex = 67;
            this.label1.Text = "* These settings\' sharing as well as import/export are managed with ReSharper Cod" +
                "e Style Sharing.";
            // 
            // _mceNotMatches
            // 
            this._mceNotMatches.AutoSize = true;
            this._mceNotMatches.EffectiveAccess = true;
            this._mceNotMatches.Location = new System.Drawing.Point(289, 41);
            this._mceNotMatches.Matches = new AgentSmith.MemberMatch.Match[0];
            this._mceNotMatches.MinimumSize = new System.Drawing.Size(100, 0);
            this._mceNotMatches.Name = "_mceNotMatches";
            this._mceNotMatches.Size = new System.Drawing.Size(251, 113);
            this._mceNotMatches.TabIndex = 60;
            // 
            // _mceMatches
            // 
            this._mceMatches.AutoSize = true;
            this._mceMatches.EffectiveAccess = true;
            this._mceMatches.Location = new System.Drawing.Point(15, 41);
            this._mceMatches.Matches = new AgentSmith.MemberMatch.Match[0];
            this._mceMatches.MinimumSize = new System.Drawing.Size(100, 0);
            this._mceMatches.Name = "_mceMatches";
            this._mceMatches.Size = new System.Drawing.Size(251, 113);
            this._mceMatches.TabIndex = 59;
            // 
            // AgentSmithSettingsPage
            // 
            this.Controls.Add(this.label1);
            this.Controls.Add(this._cbLookAtBase);
            this.Controls.Add(this._lbPS);
            this.Controls.Add(this._lbPPS);
            this.Controls.Add(this.label3);
            this.Controls.Add(this._mceNotMatches);
            this.Controls.Add(this._mceMatches);
            this.Controls.Add(this._lbMustHaveCommentsExcept);
            this.Controls.Add(this._lbMustHaveComments);
            this.Name = "AgentSmithSettingsPage";
            this.Size = new System.Drawing.Size(552, 519);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label _lbMustHaveComments;
        private System.Windows.Forms.Label _lbMustHaveCommentsExcept;
        private MatchCollectionEdit _mceMatches;
        private MatchCollectionEdit _mceNotMatches;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label _lbPPS;
        private System.Windows.Forms.Label _lbPS;
        private System.Windows.Forms.CheckBox _cbLookAtBase;
        private System.Windows.Forms.Label label1;
    }
}
