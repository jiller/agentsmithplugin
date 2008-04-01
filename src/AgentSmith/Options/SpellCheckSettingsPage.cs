using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using AgentSmith.SpellCheck;
using AgentSmith.SpellCheck.NetSpell;
using JetBrains.ReSharper.OptionPages.CodeStyle;
using JetBrains.UI.Options;

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
     }
}