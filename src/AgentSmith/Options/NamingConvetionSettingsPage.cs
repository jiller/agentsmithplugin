using System;
using System.Collections.Generic;
using System.Windows.Forms;
using AgentSmith.NamingConventions;
using JetBrains.ReSharper.OptionPages.CodeStyle;

using JetBrains.UI.Options;

namespace AgentSmith.Options
{
    [OptionsPage(
        Constants.NAMING_CONVENTIONS_SETTINGS_PAGE_ID,
        "Naming Convention Settings",
        "AgentSmith.Options.samplePage.gif",
        ParentId = Constants.AGENT_SMITH_SETTINGS_PAGE_ID,
        Sequence = 0
        )]
    public partial class NamingConventionsSettingsPage : UserControl, IOptionsPage
    {
        private readonly IOptionsDialog _ui;

        public NamingConventionsSettingsPage(IOptionsDialog ui)
        {
            InitializeComponent();
            _ui = ui;
            InitializeUI();
        }

        public string Id
        {
            get { return Constants.NAMING_CONVENTIONS_SETTINGS_PAGE_ID; }
        }

        public CodeStyleSettings Settings
        {
            get
            {
                 return ((CodeStyleSharingPage) _ui.GetPage(Constants.CODE_STYLE_PAGE_ID)).CodeStyleSettings.Get<CodeStyleSettings>();                
            }
        }

        public void InitializeUI()
        {
            bindView();
            _sceExclusions.Strings = Settings.NamingConventionSettings.Exclusions;
        }

        private void bindView()
        {
            _lvRules.Items.Clear();
            foreach (NamingConventionRule rule in  Settings.NamingConventionSettings.Rules)
            {
                ListViewItem item = new ListViewItem(rule.Description);
                item.Tag = rule;
                item.Checked = !rule.IsDisabled;
                _lvRules.Items.Add(item);
            }
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
            Settings.NamingConventionSettings.Exclusions = _sceExclusions.Strings;
            return true;
        }

        public Control Control
        {
            get { return this; }
        }

        private void editRule()
        {
            if (_lvRules.SelectedItems.Count == 1)
            {
                NamingConventionRule rule = (NamingConventionRule)_lvRules.SelectedItems[0].Tag;
                if (new EditRule(rule).ShowDialog() == DialogResult.OK)
                {
                    _lvRules.SelectedItems[0].Text = rule.Description;
                }
            }
        }

        private void lvRules_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete && _lvRules.SelectedItems.Count == 1)
            {
                removeItem(_lvRules.SelectedItems[0]);
            }            
        }

        private void removeItem(ListViewItem item)
        {
            NamingConventionRule rule = (NamingConventionRule)item.Tag;
            List<NamingConventionRule> rules = new List<NamingConventionRule>(Settings.NamingConventionSettings.Rules);
            rules.Remove(rule);
            Settings.NamingConventionSettings.Rules = rules.ToArray();
            _lvRules.Items.Remove(item);
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            List<NamingConventionRule> rules = new List<NamingConventionRule>(Settings.NamingConventionSettings.Rules);
            NamingConventionRule newRule = new NamingConventionRule();
            newRule.Description = "New rule";

            if (new EditRule(newRule).ShowDialog() == DialogResult.OK)
            {
                rules.Add(newRule);
                Settings.NamingConventionSettings.Rules = rules.ToArray();
                ListViewItem item = new ListViewItem(newRule.Description);
                _lvRules.SelectedItems.Clear();
                item.Selected = true;
                item.Checked = !newRule.IsDisabled;
                item.Tag = newRule;
                _lvRules.Items.Add(item);
            }
        }
      
        private void btnUp_Click(object sender, EventArgs e)
        {
            if (_lvRules.SelectedItems.Count == 1 && _lvRules.SelectedItems[0].Index > 0)
            {
                ListViewItem item = _lvRules.SelectedItems[0];
                int newIndex = item.Index - 1;

                move(item, newIndex);
            }
        }

        private void move(ListViewItem item, int newIndex)
        {
            List<NamingConventionRule> rules = new List<NamingConventionRule>(Settings.NamingConventionSettings.Rules);
            rules.Remove((NamingConventionRule)item.Tag);
            rules.Insert(newIndex, (NamingConventionRule)item.Tag);
            Settings.NamingConventionSettings.Rules = rules.ToArray();

            _lvRules.Items.Remove(item);
            _lvRules.Items.Insert(newIndex, item);
        }

        private void btnDown_Click(object sender, EventArgs e)
        {
            if (_lvRules.SelectedItems.Count == 1 && _lvRules.SelectedItems[0].Index < _lvRules.Items.Count - 1)
            {
                ListViewItem item = _lvRules.SelectedItems[0];
                int newIndex = item.Index + 1;

                move(item, newIndex);
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            editRule();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (_lvRules.SelectedItems.Count == 1)
            {
                removeItem(_lvRules.SelectedItems[0]);
            }            
        }

        private void lvRules_ItemChecked(object sender, ItemCheckedEventArgs e)
        {
            ((NamingConventionRule)e.Item.Tag).IsDisabled = !e.Item.Checked;
        }
    }
}