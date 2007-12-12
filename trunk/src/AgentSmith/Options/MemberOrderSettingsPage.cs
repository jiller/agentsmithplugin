using System;
using System.Collections.Generic;
using System.Windows.Forms;
using AgentSmith.NamingConventions;
using JetBrains.ReSharper.OptionPages.CodeStyle;
using JetBrains.ReSharper.OptionsUI;

namespace AgentSmith.Options
{
    [OptionsPage(
        Constants.MemberOrderSettingsPageId,
        "Class Member Order Settings",
        "AgentSmith.Options.samplePage.gif",
        ParentId = Constants.AgentSmithSettingsPageId,
        InsertionPosition = PageInsertionPosition.LAST
        )]
    public partial class MemberOrderSettingsPage : UserControl, IOptionsPage
    {
        private IOptionsUI _ui;

        public MemberOrderSettingsPage(IOptionsUI ui)
        {
            InitializeComponent();
            _ui = ui;
        }

        public CodeStyleSettings Settings
        {
            get
            {
                CodeStylePage page1 = (CodeStylePage) _ui.GetPage(Constants.CodeStylePageId);
                return page1.CodeStyleSettings.Get<CodeStyleSettings>();
            }
        }

        public void InitializeUI()
        {
            _cbMemberOrderEnabled.Checked = Settings.MemberOrderSettings.MemberOrderValidationEnabled;
            _rbVisibilityType.Checked = Settings.MemberOrderSettings.OrdingType == OrdingType.ByVisibilityFirst;
            _rbTypeVisibility.Checked = Settings.MemberOrderSettings.OrdingType == OrdingType.ByMemberTypeFirst;
            
            List<object> memberTypes = new List<object>();
            foreach (Declaration decl in Settings.MemberOrderSettings.DeclarationOrder)
            {
                memberTypes.Add(DeclarationDescription.DeclDescriptions[decl]);                
            }
            _lbType.Items = memberTypes.ToArray();

            List<object> accessLevels = new List<object>();
            foreach (AccessLevels accessLevel in Settings.MemberOrderSettings.VisibilityOrder)
            {
                accessLevels.Add(AccessLevelDescription.Descriptions[accessLevel]);
            }
            _lbVisibility.Items = accessLevels.ToArray();
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
            Settings.MemberOrderSettings.MemberOrderValidationEnabled = _cbMemberOrderEnabled.Checked;
            if (_rbVisibilityType.Checked)
            {
                Settings.MemberOrderSettings.OrdingType = OrdingType.ByVisibilityFirst;
            }
            if (_rbTypeVisibility.Checked)
            {
                Settings.MemberOrderSettings.OrdingType = OrdingType.ByMemberTypeFirst;
            }
            
            List<Declaration> decls = new List<Declaration>();
            foreach (DeclarationDescription decl in _lbType.Items)
            {
                decls.Add(decl.Declaration);
            }
            Settings.MemberOrderSettings.DeclarationOrder = decls.ToArray();

            List<AccessLevels> accessLevels = new List<AccessLevels>();
            foreach (AccessLevelDescription descr in _lbVisibility.Items)
            {
                accessLevels.Add(descr.AccessLevel);
            }
            Settings.MemberOrderSettings.VisibilityOrder = accessLevels.ToArray();
            
            return true;
        }

        public Control Control
        {
            get { return this; }
        }        
    }
}