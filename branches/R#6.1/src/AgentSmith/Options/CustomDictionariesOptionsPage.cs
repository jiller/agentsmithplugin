using System;
using System.Collections;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;

using AgentSmith.SpellCheck;
using AgentSmith.SpellCheck.NetSpell;

using JetBrains.Annotations;
using JetBrains.Application.Settings;
using JetBrains.DataFlow;
using JetBrains.ReSharper.Features.Common.Options;
using JetBrains.UI.Options;
using JetBrains.UI.Options.Helpers;

namespace AgentSmith.Options
{
    [OptionsPage(PID, "User Dictionaries", "AgentSmith.Options.samplePage.gif", ParentId = AgentSmithOptionsPage.PID)]
    public class CustomDictionariesOptionsPage : AOptionsPage
    {

        private const string PID = "AgentSmithUserDictionariesId";

        private OptionsSettingsSmartContext _settings;

        private CustomDictionariesOptionsUI _optionsUI;

        public CustomDictionariesOptionsPage([NotNull] Lifetime lifetime, OptionsSettingsSmartContext settingsSmartContext)
            : base(lifetime, PID)
        {
            _settings = settingsSmartContext;
            _optionsUI = new CustomDictionariesOptionsUI();
            
            this.Control = _optionsUI;

            RefreshCustomDictionaryList();

            _optionsUI.btnAdd.Click += BtnAddOnClick;
            _optionsUI.btnEdit.Click += BtnEditOnClick;
            _optionsUI.btnDelete.Click += BtnDeleteOnClick;
            /*
            settingsSmartContext.SetBinding<SpellCheckSettings, IEnumerable>(
                lifetime, x => (IEnumerable)x.CustomDictionaries, optionsUI.lstCustomDictionaries, ListView.ItemsSourceProperty);
             */
        }

        private void RefreshCustomDictionaryList()
        {
            _optionsUI.lstCustomDictionaries.Items.Clear();
            foreach (string item in _settings.EnumEntryIndices<CustomDictionarySettings, string, CustomDictionary>(
                x => x.CustomDictionaries))
            {
                _optionsUI.lstCustomDictionaries.Items.Add(item);
            }
        }

        private string GetSelectedDictionaryName()
        {
            return (string)_optionsUI.lstCustomDictionaries.SelectedItem;
        }

        private CustomDictionary GetDictionary(string name)
        {
            return _settings.GetIndexedValue<CustomDictionarySettings, string, CustomDictionary>(
                x => x.CustomDictionaries, name);
        }
        private void SetDictionary(string name, CustomDictionary dictionary)
        {
            _settings.SetIndexedValue<CustomDictionarySettings, string, CustomDictionary>(
                x => x.CustomDictionaries, name, dictionary);
        }
        private void RemoveDictionary(string name)
        {
            _settings.RemoveIndexedValue<CustomDictionarySettings, string, CustomDictionary>(settings => settings.CustomDictionaries, name);            
        }

        private void BtnDeleteOnClick(object sender, RoutedEventArgs routedEventArgs)
        {
            string dictName = GetSelectedDictionaryName();

            RemoveDictionary(dictName);

            RefreshCustomDictionaryList();
            SpellCheckManager.Reset(); // Clear the cache.
        }

        private void BtnEditOnClick(object sender, RoutedEventArgs routedEventArgs)
        {
            string dictName = GetSelectedDictionaryName();

            CustomDictionary dict = GetDictionary(dictName);

            EditCustomDictionaryDialog dlg = new EditCustomDictionaryDialog();

            dlg.txtName.Text = dict.Name;
            dlg.txtUserWords.Text = dict.DecodedUserWords;
            dlg.chkCaseSensitive.IsChecked = dict.CaseSensitive;

            if (dlg.ShowDialog() == true)
            {
                bool changes = false;
                if (dlg.txtName.Text != dict.Name)
                {
                    RemoveDictionary(dict.Name);
                    dict.Name = dlg.txtName.Text;
                    changes = true;
                }
                if (dict.DecodedUserWords != dlg.txtUserWords.Text)
                {
                    dict.DecodedUserWords = dlg.txtUserWords.Text;
                    changes = true;
                }

                if (dict.CaseSensitive != dlg.chkCaseSensitive.IsChecked)
                {
                    dict.CaseSensitive = (bool)dlg.chkCaseSensitive.IsChecked;
                    changes = true;
                }

                if (changes)
                {
                    SetDictionary(dict.Name, dict);
                }
                RefreshCustomDictionaryList();
                SpellCheckManager.Reset(); // Clear the cache.
            }
        }

        void BtnAddOnClick(object sender, System.Windows.RoutedEventArgs e)
        {
            EditCustomDictionaryDialog dlg = new EditCustomDictionaryDialog();

            dlg.Title = "Add Custom Dictionary";

            if (dlg.ShowDialog() == true)
            {
                CustomDictionary dict = new CustomDictionary();
                dict.Name = dlg.txtName.Text;
                dict.DecodedUserWords = dlg.txtUserWords.Text;
                dict.CaseSensitive = (bool)dlg.chkCaseSensitive.IsChecked;

                SetDictionary(dict.Name, dict);
                RefreshCustomDictionaryList();
                SpellCheckManager.Reset(); // Clear the cache.
            }
        }



    }

    [OptionsPage(PID, "AgentSmith", "AgentSmith.Options.samplePage.gif", ParentId = ToolsPage.PID)]
    public class AgentSmithOptionsPage : AOptionsPage
    {

        public const string PID = "AgentSmithId";

        private OptionsSettingsSmartContext _settings;

        private AgentSmithOptionsUI _optionsUI;

        public AgentSmithOptionsPage([NotNull] Lifetime lifetime, OptionsSettingsSmartContext settingsSmartContext)
            : base(lifetime, PID)
        {
            _settings = settingsSmartContext;
            _optionsUI = new AgentSmithOptionsUI();

            AssemblyName assemblyName = typeof(AgentSmithOptionsPage).Assembly.GetName();

            _optionsUI.txtTitle.Text += assemblyName.Name + " V" + assemblyName.Version;
            this.Control = _optionsUI;
        }

  
    }

    [OptionsPage(PID, "Xml Documentation", "AgentSmith.Options.samplePage.gif", ParentId = AgentSmithOptionsPage.PID)]
    public class XmlDocumentationOptionsPage : AOptionsPage
    {

        public const string PID = "AgentSmithXmlDocumentationId";

        private OptionsSettingsSmartContext _settings;

        private XmlDocumentationOptionsUI _optionsUI;

        public XmlDocumentationOptionsPage([NotNull] Lifetime lifetime, OptionsSettingsSmartContext settingsSmartContext)
            : base(lifetime, PID)
        {
            _settings = settingsSmartContext;
            _optionsUI = new XmlDocumentationOptionsUI();
            this.Control = _optionsUI;

            settingsSmartContext.SetBinding<XmlDocumentationSettings, string>(
                lifetime, x => x.DictionaryName, _optionsUI.txtDictionaryName, TextBox.TextProperty);
            settingsSmartContext.SetBinding<XmlDocumentationSettings, bool?>(
                lifetime, x => x.SuppressIfBaseHasComment, _optionsUI.chkSuppressIfBaseHasComment, CheckBox.IsCheckedProperty);
            settingsSmartContext.SetBinding<XmlDocumentationSettings, int>(
                lifetime, x => x.MaxCharactersPerLine, _optionsUI.txtMaxCharsPerLine, IntegerTextBox.ValueProperty);
            settingsSmartContext.SetBinding<XmlDocumentationSettings, string>(
                lifetime, x => x.WordsToIgnore, _optionsUI.txtWordsToIgnore, TextBox.TextProperty);
            settingsSmartContext.SetBinding<XmlDocumentationSettings, string>(
                lifetime, x => x.WordsToIgnoreForMetatagging, _optionsUI.txtWordsToIgnoreForMetatagging, TextBox.TextProperty);

        }

  
    }

    [OptionsPage(PID, "Strings", "AgentSmith.Options.samplePage.gif", ParentId = AgentSmithOptionsPage.PID)]
    public class StringOptionsPage : AOptionsPage
    {

        public const string PID = "AgentSmithStringId";

        private OptionsSettingsSmartContext _settings;

        private StringOptionsUI _optionsUI;

        public StringOptionsPage([NotNull] Lifetime lifetime, OptionsSettingsSmartContext settingsSmartContext)
            : base(lifetime, PID)
        {
            _settings = settingsSmartContext;
            _optionsUI = new StringOptionsUI();
            this.Control = _optionsUI;

            settingsSmartContext.SetBinding<StringSettings, string>(
                lifetime, x => x.DictionaryName, _optionsUI.txtDictionaryName, TextBox.TextProperty);
            settingsSmartContext.SetBinding<StringSettings, bool?>(
                lifetime, x => x.IgnoreVerbatimStrings, _optionsUI.chkIgnoreVerbatimStrings, CheckBox.IsCheckedProperty);
            settingsSmartContext.SetBinding<StringSettings, string>(
                lifetime, x => x.WordsToIgnore, _optionsUI.txtWordsToIgnore, TextBox.TextProperty);

        }
    }


    [OptionsPage(PID, "Identifiers", "AgentSmith.Options.samplePage.gif", ParentId = AgentSmithOptionsPage.PID)]
    public class IdentifierOptionsPage : AOptionsPage
    {

        public const string PID = "AgentSmithIdentifierId";

        private OptionsSettingsSmartContext _settings;

        private IdentifierOptionsUI _optionsUI;

        public IdentifierOptionsPage([NotNull] Lifetime lifetime, OptionsSettingsSmartContext settingsSmartContext)
            : base(lifetime, PID)
        {
            _settings = settingsSmartContext;
            _optionsUI = new IdentifierOptionsUI();
            this.Control = _optionsUI;

            settingsSmartContext.SetBinding<IdentifierSettings, string>(
                lifetime, x => x.DictionaryName, _optionsUI.txtDictionaryName, TextBox.TextProperty);
            settingsSmartContext.SetBinding<IdentifierSettings, string>(
                lifetime, x => x.WordsToIgnore, _optionsUI.txtWordsToIgnore, TextBox.TextProperty);
            settingsSmartContext.SetBinding<IdentifierSettings, int>(
                lifetime, x => x.LookupScope, _optionsUI.cmbLookupScope, ComboBox.SelectedIndexProperty);

        }
    }

    [OptionsPage(PID, "Resources", "AgentSmith.Options.samplePage.gif", ParentId = AgentSmithOptionsPage.PID)]
    public class ResourceOptionsPage : AOptionsPage
    {

        public const string PID = "AgentSmithResourceId";

        private OptionsSettingsSmartContext _settings;

        private ResXOptionsUI _optionsUI;

        public ResourceOptionsPage([NotNull] Lifetime lifetime, OptionsSettingsSmartContext settingsSmartContext)
            : base(lifetime, PID)
        {
            _settings = settingsSmartContext;
            _optionsUI = new ResXOptionsUI();
            this.Control = _optionsUI;

            settingsSmartContext.SetBinding<ResXSettings, string>(
                lifetime, x => x.DictionaryName, _optionsUI.txtDictionaryName, TextBox.TextProperty);
            settingsSmartContext.SetBinding<ResXSettings, string>(
                lifetime, x => x.WordsToIgnore, _optionsUI.txtWordsToIgnore, TextBox.TextProperty);

        }
    }
}