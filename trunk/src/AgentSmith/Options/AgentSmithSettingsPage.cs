using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using JetBrains.ReSharper.OptionPages.CodeStyle;
using JetBrains.UI.Options;
using JetBrains.Util;

namespace AgentSmith.Options
{
    [OptionsPage(
        Constants.AGENT_SMITH_SETTINGS_PAGE_ID,
        "Agent Smith Code Style Settings",
        "AgentSmith.Options.samplePage.gif",
        ParentId = Constants.CSHARP_CODE_STYLE_PAGE_ID,
        Sequence = 1
        )]
    public partial class AgentSmithSettingsPage : UserControl, IOptionsPage
    {
        private readonly IOptionsDialog _optionsDialog;

        public AgentSmithSettingsPage(IOptionsDialog optionsDialog)
        {
            InitializeComponent();
            _optionsDialog = optionsDialog;
            initializeUI();
        }

        public CodeStyleSettings Settings
        {
            get
            {
                return
                    ((CodeStyleSharingPage) _optionsDialog.GetPage(Constants.CODE_STYLE_PAGE_ID)).CodeStyleSettings.Get
                        <CodeStyleSettings>();
            }
        }

        #region IOptionsPage Members

        public string Id
        {
            get { return Constants.AGENT_SMITH_SETTINGS_PAGE_ID; }
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
            StringBuilder sb = new StringBuilder();
            foreach (string word in _tbUserDictionary.Lines)
            {
                string trimmed = word.Trim();
                if (trimmed.Length > 0)
                {
                    sb.AppendFormat("{0}\n", word);
                }
            }
            Settings.CommentsSettings.UserWords = sb.ToString();
            Settings.CommentsSettings.DictionaryName = _cbDictionary.SelectedItem.ToString();
            Settings.CommentsSettings.CommentMatch = _mceMatches.Matches;
            Settings.CommentsSettings.CommentNotMatch = _mceNotMatches.Matches;
            Settings.CommentsSettings.SuppressIfBaseHasComment = _cbLookAtBase.Checked;
            return true;
        }

        public Control Control
        {
            get { return this; }
        }

        #endregion

        private void initializeUI()
        {            
            _tbUserDictionary.Lines = Settings.CommentsSettings.UserWords.Split('\n');
            _cbDictionary.DataSource = loadDictionaries();            
            _cbDictionary.SelectedItem = Settings.CommentsSettings.DictionaryName;
            _mceMatches.Matches = Settings.CommentsSettings.CommentMatch;
            _mceNotMatches.Matches = Settings.CommentsSettings.CommentNotMatch;
            _cbLookAtBase.Checked = Settings.CommentsSettings.SuppressIfBaseHasComment;
        }

        private IList<string> loadDictionaries()
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
                object selected = _cbDictionary.SelectedItem;
                _cbDictionary.DataSource = loadDictionaries();
                _cbDictionary.SelectedItem = selected;                
            }
        } 
    }
}