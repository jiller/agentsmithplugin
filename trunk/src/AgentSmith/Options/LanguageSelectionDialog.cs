using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace AgentSmith.Options
{
    public partial class LanguageSelectionDialog : Form
    {
        public LanguageSelectionDialog()
        {
            InitializeComponent();
        }

        public string[] Dictionaries
        {
            get { throw new NotImplementedException(); }
            set
            {
                foreach (string dict in value)
                {
                    _lvDictionaries.Items.Add(dict, dict);
                }
            }
        }

        public string[] SelectedDictionaries
        {
            get
            {
                List<string> selectedItems = new List<string>();
                foreach (ListViewItem li in _lvDictionaries.Items)
                {
                    if (li.Checked)
                    {
                        selectedItems.Add(li.Text);
                    }
                }

                return selectedItems.ToArray();
            }
            set
            {
                foreach (ListViewItem li in _lvDictionaries.Items)
                {
                    li.Checked = Array.IndexOf(value, li.Text) >= 0;                    
                }
            }
        }        
    }
}