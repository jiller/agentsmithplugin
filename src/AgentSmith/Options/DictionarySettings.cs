﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using AgentSmith.SpellCheck.NetSpell;
using JetBrains.ReSharper.OptionPages.CodeStyle;
using JetBrains.UI.Options;
using AgentSmith.SpellCheck;

namespace AgentSmith.Options
{
    [OptionsPage(
        Constants.DICTIONARIES_SETTINGS_PAGE_ID,
        "Dictionaries",
        "AgentSmith.Options.samplePage.gif",
        ParentId = Constants.AGENT_SMITH_SETTINGS_PAGE_ID,
        Sequence = 3
        )]
    public partial class DictionarySettings : UserControl, IOptionsPage
    {
        private readonly IOptionsDialog _optionsDialog;
        private CustomDictionary _currentCustomDictionary;

        public DictionarySettings(IOptionsDialog optionsDialog)
        {            
            InitializeComponent();
            _optionsDialog = optionsDialog;
            initializeUI();
        }

        public CodeStyleSettings Settings
        {
            get
            {
                CodeStyleSharingPage page = (CodeStyleSharingPage)_optionsDialog.GetPage(Constants.CODE_STYLE_PAGE_ID);
                return page.CodeStyleSettings.Get<CodeStyleSettings>();
            }
        }

        private void initializeUI()
        {
            bindDictionaries(DicUtil.LoadDictionaries());
            _cbDictionary.SelectedItem = Settings.LastSelectedCustomDictionary;
        }

        public bool OnOk()
        {
            saveCustomDictionary();            
            Settings.LastSelectedCustomDictionary = _cbDictionary.SelectedItem.ToString();
            //TODO: don't need to reset all spellcheckers. Clearing references to custom dictionaries
            //would be enough.
            SpellCheckManager.Reset();
            return true;
        }

        public bool ValidatePage()
        {
            return true;
        }

        public Control Control
        {
            get { return this; }
        }

        public string Id
        {
            get { return Constants.DICTIONARIES_SETTINGS_PAGE_ID; }
        }

        private void bindDictionaries(List<string> dicts)
        {
            object selected = _cbDictionary.SelectedItem;
            _cbDictionary.Items.Clear();
            _cbDictionary.Items.AddRange(dicts.ToArray());
            _cbDictionary.SelectedItem = selected;         
        }

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

                XmlElement dictionary = (XmlElement)doc.SelectSingleNode("Dictionary");
                if (dictionary == null)
                {
                    dictionary = (XmlElement)doc.AppendChild(doc.CreateElement("Dictionary"));
                }

                XmlElement words = (XmlElement)dictionary.SelectSingleNode("Words");
                if (words == null)
                {
                    words = (XmlElement)dictionary.AppendChild(doc.CreateElement("Words"));
                }

                XmlElement recognized = (XmlElement)words.SelectSingleNode("Recognized");
                if (recognized == null)
                {
                    recognized = (XmlElement)words.AppendChild(doc.CreateElement("Recognized"));
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
                        XmlElement wordElement = (XmlElement)recognized.AppendChild(doc.CreateElement("Word"));
                        wordElement.InnerText = word;
                    }
                }


                using (XmlTextWriter writer = new XmlTextWriter(name, Encoding.UTF8))
                {
                    writer.Formatting = Formatting.Indented;
                    doc.Save(writer);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Export failed: " + ex.Message);
            }
        }

        private void btnImport_Click(object sender, EventArgs e)
        {
            ImportOpenOfficeDictionary frm = new ImportOpenOfficeDictionary(DicUtil.GetUserDicDirectory());
            if (frm.ShowDialog() == DialogResult.OK)
            {
                bindDictionaries(DicUtil.LoadDictionaries());
            }
        }       
    }
}
