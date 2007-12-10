using System;
using System.Collections.Generic;
using System.Windows.Forms;
using AgentSmith.MemberMatch;

namespace AgentSmith.Options
{
    public partial class MatchOptions : Form
    {
        private readonly Match _match;

        public MatchOptions(Match match)
        {
            _match = match;
            InitializeComponent();

            foreach (DeclarationDescription descr in MatchDeclarationDescription.DeclDescriptions.Values)
            {
                _lbMember.Items.Add(descr);
                if (descr.Declaration == _match.Declaration)
                {
                    _lbMember.SelectedItem = descr;
                }
            }

            Dictionary<AccessLevels, AccessLevelDescription> visibilityDescriptions =
                AccessLevelDescription.Descriptions;
            foreach (AccessLevelDescription descr in visibilityDescriptions.Values)
            {
                if (_match.AccessLevel == AccessLevels.Any)
                {
                    _cbVisibility.Items.Add(descr, descr.AccessLevel == AccessLevels.Any);
                }
                else
                {
                    _cbVisibility.Items.Add(descr,
                                            (descr.AccessLevel & _match.AccessLevel) != 0 &&
                                            descr.AccessLevel != AccessLevels.Any);
                }
            }

            _lbMember.SelectedItem = DeclarationDescription.DeclDescriptions[_match.Declaration];
            _tbInheritedFrom.Text = _match.InheritedFrom;
            _tbMarkedWithAttribute.Text = _match.MarkedWithAttribute;
            _cbReadonly.CheckState = convertBool(_match.IsReadonly);
            _cbStatic.CheckState = convertBool(_match.IsStatic);

        }

        private static CheckState convertBool(FuzzyBool val)
        {
            switch (val)
            {
                case FuzzyBool.True:
                    return CheckState.Checked;
                case FuzzyBool.False:
                    return CheckState.Unchecked;               
                default:
                    return CheckState.Indeterminate;
            }
        }

        private static FuzzyBool convertToBool(CheckState state)
        {
            switch (state)
            {
                case CheckState.Unchecked:
                    return FuzzyBool.False;
                case CheckState.Checked:
                    return FuzzyBool.True;
                default:
                    return FuzzyBool.Maybe;
            }
        }

        private void btnOKClick(object sender, EventArgs e)
        {
            DeclarationDescription decl = (DeclarationDescription) _lbMember.SelectedItem;
            _match.Declaration = decl.Declaration;
            _match.AccessLevel = AccessLevels.None;
            _match.IsReadonly = convertToBool(_cbReadonly.CheckState);
            _match.IsStatic = convertToBool(_cbStatic.CheckState);

            if (decl.HasAccessLevel)
            {
                foreach (AccessLevelDescription descr in _cbVisibility.CheckedItems)
                {
                    _match.AccessLevel |= descr.AccessLevel;
                }
            }

            if (decl.CanInherit)
            {
                _match.InheritedFrom = _tbInheritedFrom.Text.Trim();
            }
            else
            {
                _match.InheritedFrom = null;
            }

            if (decl.CanBeMarkedWithAttribute)
            {
                _match.MarkedWithAttribute = _tbMarkedWithAttribute.Text.Trim();
            }
            else
            {
                _match.MarkedWithAttribute = null;
            }
        }

        private void lbMemberSelectedIndexChanged(object sender, EventArgs e)
        {
            DeclarationDescription description = (DeclarationDescription) _lbMember.SelectedItem;
            _cbVisibility.Enabled = description.HasAccessLevel;
            _tbInheritedFrom.Enabled = description.CanInherit;
            _tbMarkedWithAttribute.Enabled = description.CanBeMarkedWithAttribute;
            _cbReadonly.Enabled = description.CanBeReadonly;
            _cbStatic.Enabled = description.CanBeStatic;
        }
    }
}