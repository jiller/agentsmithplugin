using System;
using System.ComponentModel;
using System.IO;
using System.Windows.Forms;
using AgentSmith.SpellCheck.NetSpell;

namespace AgentSmith.Options
{
    public partial class ImportOpenOfficeDictionary : Form
    {
        private readonly string _dicDir;

        public ImportOpenOfficeDictionary(string dicDir)
        {
            InitializeComponent();
            _dicDir = dicDir;
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void _tbDictName_Validating(object sender, CancelEventArgs e)
        {
            e.Cancel = _tbDictName.Text.Trim().Length == 0;
        }

        private void _btnImport_Click(object sender, EventArgs e)
        {
            string outPath = Path.Combine(_dicDir, _tbDictName.Text + ".dic");
            try
            {
                OpenOfficeDictionaryImporter.Import(_tbAffixFile.Text, _tbDicFile.Text, outPath);
            }
            catch(Exception ex)
            {
                MessageBox.Show(String.Format("Failed to import dictionary. {0}", ex.Message));
            }
        }

        private void _btnBrowseAffix_Click(object sender, EventArgs e)
        {
            OpenFileDialog fileOpenDialog = new OpenFileDialog();
            fileOpenDialog.AddExtension = true;
            fileOpenDialog.DefaultExt = ".aff";
            fileOpenDialog.Filter = "Open Office Affix File | *.aff";
            if (fileOpenDialog.ShowDialog() == DialogResult.OK)
            {
                _tbAffixFile.Text = fileOpenDialog.FileName;
            }
        }
    }
}