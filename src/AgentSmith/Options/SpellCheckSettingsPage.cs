using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using AgentSmith.SpellCheck;
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
            Settings.CommentsSettings.DictionaryName = _lsComments.SelectedDictionariesString;
            Settings.StringsDictionary = _lsStrings.SelectedDictionariesString;
            Settings.IdentifierDictionary = _cbIdentifiers.SelectedItem.ToString();
            
            Settings.IdentifiersToSpellCheck = _mceToSpellCheck.Matches;
            Settings.IdentifiersNotToSpellCheck = _mceDoNotSpellCheck.Matches;
            Settings.PatternsToIgnore = _ignorePatterns.Items;

            SpellCheckManager.Reset();
            return true;
        }

        public Control Control
        {
            get { return this; }
        }

        #endregion

        
        private void initializeUI()
        {
            bindDictionaries(DicUtil.LoadDictionaries());
            
            _lsComments.SelectedDictionariesString = Settings.CommentsSettings.DictionaryName;
            _lsStrings.SelectedDictionariesString = Settings.StringsDictionary;
            _cbIdentifiers.SelectedItem = Settings.IdentifierDictionary;
            _ignorePatterns.Items = Settings.PatternsToIgnore;
            
            _mceToSpellCheck.Matches = Settings.IdentifiersToSpellCheck;
            _mceDoNotSpellCheck.Matches = Settings.IdentifiersNotToSpellCheck;
        }

        private void bindDictionaries(List<string> dicts)
        {
            object selected = _cbIdentifiers.SelectedItem;
            _cbIdentifiers.Items.Clear();
            _cbIdentifiers.Items.AddRange(dicts.ToArray());
            _cbIdentifiers.SelectedItem = selected;


            _lsComments.Dictionaries = dicts.ToArray();
            _lsStrings.Dictionaries = dicts.ToArray();
        }

        private bool ignorePatterns_OnValidate(string value)
        {
            try
            {
                new Regex(value);
                return true;
            }
            catch
            {
                MessageBox.Show("Invalid .net regular expression.", "Agent Smith", MessageBoxButtons.OK,
                                MessageBoxIcon.Asterisk);
                return false;
            };
        }            
     }
}