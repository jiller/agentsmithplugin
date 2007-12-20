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

        private void btnBrowseDict_Click(object sender, EventArgs e)
        {
            OpenFileDialog fileOpenDialog = new OpenFileDialog();
            fileOpenDialog.AddExtension = true;
            fileOpenDialog.DefaultExt = ".dic";
            fileOpenDialog.Filter = "Open Office Affix File (*.dic) | *.dic";
            fileOpenDialog.FileName = _tbDicFile.Text;
            if (fileOpenDialog.ShowDialog() == DialogResult.OK)
            {
                _tbDicFile.Text = fileOpenDialog.FileName;                
            }
        }

        private void tbDictName_Validating(object sender, CancelEventArgs e)
        {
           
        }

        private void btnImport_Click(object sender, EventArgs e)
        {
            string outPath = Path.Combine(_dicDir, _tbDictName.Text.Trim() + ".dic");
            if (!File.Exists(outPath) || MessageBox.Show("Dictionary with this name already exists. Overwrite?", "Confirm", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                try
                {
                    OpenOfficeDictionaryImporter.Import(_tbAffixFile.Text, _tbDicFile.Text, outPath);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(String.Format("Failed to import dictionary. {0}", ex.Message));
                    return;
                }
                DialogResult = DialogResult.OK;
                Close();
            }       
        }

        /// <summary>
        /// Проверка русской орфографии
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnBrowseAffix_Click(object sender, EventArgs e)
        {
            OpenFileDialog fileOpenDialog = new OpenFileDialog();
            fileOpenDialog.AddExtension = true;
            fileOpenDialog.DefaultExt = ".aff";
            fileOpenDialog.Filter = "Open Office Affix File (*.aff) | *.aff";
            fileOpenDialog.FileName = _tbAffixFile.Text;
            if (fileOpenDialog.ShowDialog() == DialogResult.OK)
            {
                _tbAffixFile.Text = fileOpenDialog.FileName;
                string dicFile = Path.Combine(Path.GetDirectoryName(fileOpenDialog.FileName), Path.GetFileNameWithoutExtension(fileOpenDialog.FileName) + ".dic");
                if (_tbDicFile.Text.Length == 0 && File.Exists(dicFile))
                {
                    _tbDicFile.Text = dicFile;
                }
            }
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void dictionaryLink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start(_dictionaryLink.Text);
        }
    }
}