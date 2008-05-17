using System;
using System.Collections.Generic;
using System.Windows.Forms;
using AgentSmith.MemberMatch;

namespace AgentSmith.Options
{
    public partial class MatchCollectionEdit : UserControl
    {
        private List<Match> _matches = new List<Match>();

        public MatchCollectionEdit()
        {
            InitializeComponent();
        }

        public Match[] Matches
        {
            get { return _matches.ToArray(); }
            set
            {
                _matches = value == null ? new List<Match>() : new List<Match>(value);
                bindView();
            }
        }

        private void bindView()
        {
            foreach (Match match in _matches)
            {
                ListViewItem lvi = new ListViewItem(match.ToString());
                lvi.Tag = match;
                _lvMatches.Items.Add(lvi);
            }
            if (_lvMatches.Items.Count > 0)
            {
                _lvMatches.Items[0].Selected = true;
            }
        }

        private void lvMatches_DoubleClick(object sender, EventArgs e)
        {
            edit();
        }

        private void edit()
        {
            if (_lvMatches.SelectedItems.Count == 1)
            {
                Match match = (Match) _lvMatches.SelectedItems[0].Tag;
                if (new MatchOptions(match).ShowDialog() == DialogResult.OK)
                {
                    _lvMatches.SelectedItems[0].Text = match.ToString();
                }
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            Match newMatch = new Match();

            if (new MatchOptions(newMatch).ShowDialog() == DialogResult.OK)
            {
                _matches.Add(newMatch);

                ListViewItem item = new ListViewItem(newMatch.ToString());
                item.ToolTipText = newMatch.ToString();
                _lvMatches.SelectedItems.Clear();
                item.Selected = true;
                item.Tag = newMatch;
                _lvMatches.Items.Add(item);
            }
        }

        private void lvMatches_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                delete();
            }
        }

        private void delete()
        {
            if (_lvMatches.SelectedItems.Count == 1)
            {
                _matches.Remove((Match) _lvMatches.SelectedItems[0].Tag);
                _lvMatches.Items.Remove(_lvMatches.SelectedItems[0]);
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            edit();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            delete();
        }
     
        private void matchCollectionEdit_Layout(object sender, LayoutEventArgs e)
        {            
            _btnAdd.Left =
            _btnEdit.Left =
            _btnDelete.Left = this.Width - _btnAdd.Width;

            _lvMatches.Width = _btnAdd.Left - 3;
            _lvMatches.Height = this.Height;
            _lvMatches.Left = 0;
            _lvMatches.Top = 0;

            _btnAdd.Top = 0;
            _btnEdit.Top = _btnAdd.Bottom + 3;
            _btnDelete.Top = _btnEdit.Bottom + 3;
        }        
    }
}