using System;
using System.Collections;
using System.Windows.Forms;

namespace AgentSmith.Options
{
    public partial class SortedListBox : UserControl
    {
        public SortedListBox()
        {
            InitializeComponent();
        }

        public object[] Items
        {
            get { return new ArrayList(_lbVisibility.Items).ToArray(); }
            set
            {
                _lbVisibility.Items.Clear();
                foreach (object o in value)
                {
                    _lbVisibility.Items.Add(o);
                }
            }
        }

        private void btnVisUp_Click(object sender, EventArgs e)
        {
            if (_lbVisibility.SelectedIndex > 0)
            {
                object item = _lbVisibility.SelectedItem;
                int index = _lbVisibility.SelectedIndex;
                _lbVisibility.Items.Remove(item);
                _lbVisibility.Items.Insert(index - 1, item);
                _lbVisibility.SelectedItem = item;
            }
        }

        private void btnVisDown_Click(object sender, EventArgs e)
        {
            if (_lbVisibility.SelectedItem!= null && _lbVisibility.SelectedIndex < _lbVisibility.Items.Count - 1)
            {
                object item = _lbVisibility.SelectedItem;
                int index = _lbVisibility.SelectedIndex;
                _lbVisibility.Items.Remove(item);
                _lbVisibility.Items.Insert(index + 1, item);
                _lbVisibility.SelectedItem = item;
            }
        }
    }
}