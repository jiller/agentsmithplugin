using System;
using System.Windows.Forms;

namespace AgentSmith.Options
{
    public partial class LanguageSelector : UserControl
    {
        private string[] _dictionaries;

        public LanguageSelector()
        {
            InitializeComponent();
        }

        public string SelectedDictionariesString
        {
            get { return _tbDicts.Text; }
            set { _tbDicts.Text = value; }
        }

        public string[] SelectedDictionaries
        {
            get { return _tbDicts.Text.Split(','); }
            set { _tbDicts.Text = String.Join(",", value); }
        }

        public string[] Dictionaries
        {
            get { return _dictionaries; }
            set { _dictionaries = value; }
        }

        private void btnSelect_Click(object sender, EventArgs e)
        {
            LanguageSelectionDialog dialog = new LanguageSelectionDialog();
            dialog.Dictionaries = Dictionaries;
            dialog.SelectedDictionaries = SelectedDictionaries;
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                SelectedDictionaries = dialog.SelectedDictionaries;
            }
        }
    }
}