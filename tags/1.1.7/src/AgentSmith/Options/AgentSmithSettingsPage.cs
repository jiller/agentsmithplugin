using System;
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
                CodeStyleSharingPage page = (CodeStyleSharingPage) _optionsDialog.GetPage(Constants.CODE_STYLE_PAGE_ID);
                return page.CodeStyleSettings.Get<CodeStyleSettings>();
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
            _mceMatches.Matches = Settings.CommentsSettings.CommentMatch;
            _mceNotMatches.Matches = Settings.CommentsSettings.CommentNotMatch;
            _cbLookAtBase.Checked = Settings.CommentsSettings.SuppressIfBaseHasComment;
        }
    }
}