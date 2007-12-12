using System;
using System.ComponentModel;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using AgentSmith.NamingConventions;

namespace AgentSmith.Options
{
    public partial class EditRule : Form
    {
        private readonly NamingConventionRule _rule;

        public EditRule()
        {
            InitializeComponent();
        }

        public EditRule(NamingConventionRule rule) : this()
        {
            _rule = rule;
            _tbDescription.Text = rule.Description;
            _sceMustHavePrefix.Strings = rule.MustHavePrefixes;
            _sceMustNotHavePrefix.Strings = rule.MustNotHavePrefixes;
            _sceMustHaveSuffix.Strings = rule.MustHaveSuffixes;
            _sceMustNotHaveSuffix.Strings = rule.MustNotHaveSuffixes;
            _cbStyle.DataSource = RuleKindDescription.GetDescriptions();
            foreach (RuleKindDescription descr in _cbStyle.Items)
            {
                if (descr.Rule == rule.Rule)
                {
                    _cbStyle.SelectedItem = descr;
                    break;
                }
            }
            _tbRegex.Text = rule.Regex;
            _cbDisabled.Checked = _rule.IsDisabled;
            _mceMatches.Matches = rule.Matches;
            _mceNotMatches.Matches = rule.NotMatches;           
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            _rule.Description = _tbDescription.Text;
            _rule.Matches = _mceMatches.Matches;
            _rule.NotMatches = _mceNotMatches.Matches;
            _rule.MustHavePrefixes = _sceMustHavePrefix.Strings;
            _rule.MustNotHavePrefixes = _sceMustNotHavePrefix.Strings;
            _rule.MustHaveSuffixes = _sceMustHaveSuffix.Strings;
            _rule.MustNotHaveSuffixes = _sceMustNotHaveSuffix.Strings;
            _rule.Rule = ((RuleKindDescription) _cbStyle.SelectedItem).Rule;
            _rule.Regex = ((RuleKindDescription) _cbStyle.SelectedItem).HasRegex ? _tbRegex.Text : null;
            _rule.IsDisabled = _cbDisabled.Checked;
        }

        /*private void bindMatches(ListView view, Match[] matches)
        {
            if (matches == null) return;

            foreach (Match match in matches)
            {
                ListViewItem lvi = new ListViewItem(match.ToString());
                lvi.ToolTipText = match.ToString();
                lvi.Tag = match;
                view.Items.Add(lvi);
            }
        }*/

        private void cbStyle_SelectedValueChanged(object sender, EventArgs e)
        {
            _tbRegex.Enabled = ((RuleKindDescription) _cbStyle.SelectedItem).HasRegex;
        }

        private void editRule_Validating(object sender, CancelEventArgs e)
        {
            if (((RuleKindDescription) _cbStyle.SelectedItem).HasRegex)
            {
                try
                {
                    new Regex(_tbRegex.Text);
                }
                catch (ArgumentException)
                {
                    e.Cancel = true;
                }
            }
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }        
    }
}