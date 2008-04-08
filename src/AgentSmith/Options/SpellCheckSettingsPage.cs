using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using AgentSmith.SpellCheck;
using AgentSmith.SpellCheck.NetSpell;
using JetBrains.ReSharper.OptionPages.CodeStyle;
using JetBrains.UI.Options;
using JetBrains.Util;

namespace AgentSmith.Options
{
    [OptionsPage(
        Constants.SPELLCHECK_SETTINGS_PAGE_ID,
        "Spell Check Settings",
        "AgentSmith.Options.samplePage.gif",
        ParentId = Constants.AGENT_SMITH_SETTINGS_PAGE_ID,
        Sequence = 1
        )]
    public partial class SpellCheckSettingsPage : UserControl, IOptionsPage
    {
        private readonly IOptionsDialog _optionsDialog;
        private CustomDictionary _currentCustomDictionary;

        public SpellCheckSettingsPage(IOptionsDialog optionsDialog)
        {
            InitializeComponent();
            _optionsDialog = optionsDialog;
            initializeUI();
        }

        public CodeStyleSettings Settings
        {
            get
            {
                CodeStyleSharingPage page = (CodeStyleSharingPage) _optionsDialog.GetPage(Constants.CODE_STYLE_PAGE_ID);
                return page.CodeStyleSettings.Get<CodeStyleSettings>();
            }
        }

        #region IOptionsPage Members

        public string Id
        {
            get { return Constants.SPELLCHECK_SETTINGS_PAGE_ID; }
        }

        public void OnActivated(bool activated)
        {
        }

        public bool ValidatePage()
        {
            return true;
        }

        public bool OnOk()
        {
            saveCustomDictionary();
            Settings.CommentsSettings.DictionaryName = _lsComments.SelectedDictionariesString;
            Settings.StringsDictionary = _cbStrings.SelectedItem.ToString();
            Settings.IdentifierDictionary = _cbIdentifiers.SelectedItem.ToString();
            Settings.DefaultResXDictionary = _cbResX.SelectedItem.ToString();
            Settings.LastSelectedCustomDictionary = _cbDictionary.SelectedItem.ToString();

            SpellCheckManager.Reset();
            return true;
        }

        public Control Control
        {
            get { return this; }
        }

        #endregion

        private void saveCustomDictionary()
        {
            StringBuilder sb = new StringBuilder();
            foreach (string word in _tbUserDictionary.Lines)
            {
                string trimmed = word.Trim();
                if (trimmed.Length > 0)
                {
                    sb.AppendFormat("{0}\n", word);
                }
            }
            
            if (_currentCustomDictionary != null)
            {
                _currentCustomDictionary.UserWords = sb.ToString();
                _currentCustomDictionary.CaseSensitive = _cbCaseSensitive.Checked;
            }
        }

        private void initializeUI()
        {
            bindDictionaries(loadDictionaries());

            //if (_cbDictionary.Items.Count > 0)
            //{
             //   _cbDictionary.SelectedItem = _cbDictionary.Items[0];
            //}
            _lsComments.SelectedDictionariesString = Settings.CommentsSettings.DictionaryName;
            _cbStrings.SelectedItem = Settings.StringsDictionary;
            _cbIdentifiers.SelectedItem = Settings.IdentifierDictionary;
            _cbResX.SelectedItem = Settings.DefaultResXDictionary;
            _cbDictionary.SelectedItem = Settings.LastSelectedCustomDictionary;
        }

        private void bindDictionaries(List<string> dicts)
        {
            ComboBox[] cbs = new ComboBox[]
                {
                    _cbResX,
                    _cbIdentifiers,
                    _cbStrings,
                    _cbDictionary
                };

            foreach (ComboBox cbDict in cbs)
            {
                object selected = cbDict.SelectedItem;
                cbDict.Items.Clear();
                cbDict.Items.AddRange(dicts.ToArray());                
                cbDict.SelectedItem = selected;
            }
            
            _lsComments.Dictionaries = dicts.ToArray();
        }

        private List<string> loadDictionaries()
        {
            List<string> list = new List<string>();
            string dicDirectory = getDicDirectory();
            foreach (string file in Directory.GetFiles(dicDirectory))
            {
                list.Add(Path.GetFileNameWithoutExtension(file));
            }
            return list;
        }

        private string getDicDirectory()
        {
            string assemblyDir = Path.GetDirectoryName(new Uri(Assembly.GetExecutingAssembly().CodeBase).LocalPath);
            return Path.Combine(assemblyDir, "dic");
        }

        private void btnImport_Click(object sender, EventArgs e)
        {
            ImportOpenOfficeDictionary frm = new ImportOpenOfficeDictionary(getDicDirectory());
            if (frm.ShowDialog() == DialogResult.OK)
            {
                bindDictionaries(loadDictionaries());
            }
        }

        private void cbDictionary_SelectedIndexChanged(object sender, EventArgs e)
        {            
            if (_cbDictionary.SelectedItem != null)
            {                
                saveCustomDictionary();

                string dictName = _cbDictionary.SelectedItem.ToString();
                _currentCustomDictionary = Settings.CustomDictionaries.GetOrCreateCustomDictionary(dictName);
                _tbUserDictionary.Lines = _currentCustomDictionary.UserWords.Split('\n');
                _cbCaseSensitive.Checked = _currentCustomDictionary.CaseSensitive;
            }
        }        

        private void btnImportFxCop_Click(object sender, EventArgs e)
        {
            OpenFileDialog openDialog = new OpenFileDialog();
            openDialog.CheckFileExists = true;
            openDialog.AddExtension = true;
            openDialog.DefaultExt = ".xml";
            openDialog.FileName = "CustomDictionary.xml";
            openDialog.Filter = "FxCop Dictionary File (*.xml) | *.xml";
            openDialog.Title = "Select FxCop dictionary file to import";
            if (openDialog.ShowDialog() == DialogResult.OK)
            {
                importFxCop(openDialog.FileName);
            }
        }

        private void importFxCop(string name)
        {
            try
            {
                HashSet<string> set = new HashSet<string>(_tbUserDictionary.Lines);
                StringBuilder newWords = new StringBuilder();
                XmlDocument doc = new XmlDocument();
                doc.Load(name);
                foreach (XmlElement word in doc.SelectNodes("Dictionary/Words/Recognized/Word"))
                {
                    string wordText = word.InnerText.Trim();
                    if (String.IsNullOrEmpty(wordText))
                    {
                        continue;
                    }                                        
                    if (!set.Contains(word.InnerText))
                    {
                        newWords.AppendLine(wordText);
                    }
                }
                _tbUserDictionary.Text += "\r\n" + newWords;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Import failed: " + ex.Message);
            }
        }

        private void btnExportFxCop_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveDialog = new SaveFileDialog();
            saveDialog.CheckFileExists = false;
            saveDialog.AddExtension = true;
            saveDialog.FileName = "CustomDictionary.xml";
            saveDialog.Filter = "FxCop Dictionary File (*.xml) | *.xml";
            saveDialog.Title = "Select new or existing FxCop file to merge dictionary into";
            if (saveDialog.ShowDialog() == DialogResult.OK)
            {
                exportFxCop(saveDialog.FileName);
            }            
        }

        private void exportFxCop(string name)
        {
            try
            {
                XmlDocument doc = new XmlDocument();

                if (File.Exists(name))
                {
                    doc.Load(name);
                }

                XmlElement dictionary = (XmlElement) doc.SelectSingleNode("Dictionary");
                if (dictionary == null)
                {
                    dictionary = (XmlElement) doc.AppendChild(doc.CreateElement("Dictionary"));
                }

                XmlElement words = (XmlElement) dictionary.SelectSingleNode("Words");
                if (words == null)
                {
                    words = (XmlElement) dictionary.AppendChild(doc.CreateElement("Words"));
                }

                XmlElement recognized = (XmlElement) words.SelectSingleNode("Recognized");
                if (recognized == null)
                {
                    recognized = (XmlElement) words.AppendChild(doc.CreateElement("Recognized"));
                }

                HashSet<string> existingWords = new HashSet<string>();
                foreach (XmlElement word in recognized.SelectNodes("Word"))
                {
                    string wordText = word.InnerText.Trim();
                    if (!string.IsNullOrEmpty(wordText))
                    {
                        existingWords.Add(wordText);
                    }
                }

                foreach (string word in _tbUserDictionary.Lines)
                {
                    if (!existingWords.Contains(word))
                    {
                        XmlElement wordElement = (XmlElement) recognized.AppendChild(doc.CreateElement("Word"));
                        wordElement.InnerText = word;
                    }
                }


                using (XmlTextWriter writer = new XmlTextWriter(name, Encoding.UTF8))
                {
                    writer.Formatting = Formatting.Indented;
                    doc.Save(writer);
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show("Export failed: " + ex.Message);
            }
        }
    }
}