using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using JetBrains.ReSharper.OptionPages.CodeStyle;
using JetBrains.UI.Options;

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
        private readonly IOptionsDialog _ui;

        public AgentSmithSettingsPage(IOptionsDialog ui)
        {
            InitializeComponent();
            _ui = ui;
            InitializeUI();
        }

        public CodeStyleSettings Settings
        {
            get
            {
                return
                    ((CodeStyleSharingPage) _ui.GetPage(Constants.CODE_STYLE_PAGE_ID)).CodeStyleSettings.Get
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
            //Settings.CatchOrSpecifySettings.CatchOrSpecifyEnabled = _cbCatchOrDocument.Checked;
            //Settings.CatchOrSpecifySettings.Exclusions = _sceExceptionExclusions.Strings;
            //Settings.CommentsSettings.PublicMembersMustHaveComments = _cbPublicMembers.Checked;
           // Settings.CommentsSettings.Color = _commentColor.Color;
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

        public void InitializeUI()
        {
            //_cbCatchOrDocument.Checked = Settings.CatchOrSpecifySettings.CatchOrSpecifyEnabled;
            //_cbPublicMembers.Checked = Settings.CommentsSettings.PublicMembersMustHaveComments;
            //_sceExceptionExclusions.Strings = Settings.CatchOrSpecifySettings.Exclusions;
            //_commentColor.Color = Settings.CommentsSettings.Color;
            _tbUserDictionary.Lines = Settings.CommentsSettings.UserWords.Split('\n');
            IList<string> dictionaries = loadDictionaries();
            _cbDictionary.DataSource = dictionaries;            
            _cbDictionary.SelectedItem = Settings.CommentsSettings.DictionaryName;
            _mceMatches.Matches = Settings.CommentsSettings.CommentMatch;
            _mceNotMatches.Matches = Settings.CommentsSettings.CommentNotMatch;
            _cbLookAtBase.Checked = Settings.CommentsSettings.SuppressIfBaseHasComment;
        }

        private IList<string> loadDictionaries()
        {
            List<string> list = new List<string>();
            string assemblyDir = Path.GetDirectoryName(new Uri(Assembly.GetExecutingAssembly().CodeBase).LocalPath);
            foreach (string file in Directory.GetFiles(Path.Combine(assemblyDir, "dic")))
            {
                list.Add(Path.GetFileNameWithoutExtension(file));
            }
            return list;
        }

        private void AgentSmithSettingsPage_Load(object sender, EventArgs e)
        {

        }
    }
}