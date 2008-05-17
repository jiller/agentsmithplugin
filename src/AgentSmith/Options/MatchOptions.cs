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
            if (string.IsNullOrEmpty(_tbInheritedFrom.Text))
            {
                _tbInheritedFrom.Text = _match.IsOfType;
            }
            _tbMarkedWithAttribute.Text = _match.MarkedWithAttribute;
            _cbReadonly.CheckState = convertBool(_match.IsReadOnly);
            _cbStatic.CheckState = convertBool(_match.IsStatic);

            _cbIn.Checked = (_match.ParamDirection & ParamDirection.In) != 0;
            _cbOut.Checked = (_match.ParamDirection & ParamDirection.Out) != 0;
            _cbRef.Checked = (_match.ParamDirection & ParamDirection.Ref) != 0;
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

        private void btnOK_Click(object sender, EventArgs e)
        {
            DeclarationDescription decl = (DeclarationDescription) _lbMember.SelectedItem;
            _match.AccessLevel = AccessLevels.Any;
            _match.Declaration = decl.Declaration;
            _match.MarkedWithAttribute = null;
            _match.InheritedFrom = null;
            _match.IsOfType = null;
            _match.IsReadOnly = FuzzyBool.Maybe;
            _match.IsStatic = FuzzyBool.Maybe;
            _match.ParamDirection = ParamDirection.Any;

            if (decl.HasAccessLevel)
            {
                _match.AccessLevel = AccessLevels.None;
                foreach (AccessLevelDescription descr in _cbVisibility.CheckedItems)
                {
                    _match.AccessLevel |= descr.AccessLevel;
                }
            }

            if (decl.CanInherit)
            {
                _match.InheritedFrom = _tbInheritedFrom.Text.Trim();
            }

            if (decl.OwnsType)
            {
                _match.IsOfType = _tbInheritedFrom.Text.Trim();
            }
            
            if (decl.CanBeMarkedWithAttribute)
            {
                _match.MarkedWithAttribute = _tbMarkedWithAttribute.Text.Trim();
            }
            
            if (decl.CanBeReadonly)
            {
                _match.IsReadOnly = convertToBool(_cbReadonly.CheckState);
            }

            if (decl.CanBeStatic)
            {
                _match.IsStatic = convertToBool(_cbStatic.CheckState);
            }

            if (decl.Declaration == Declaration.Parameter)
            {
                _match.ParamDirection = 0;
                if (_cbIn.Checked)
                {
                    _match.ParamDirection |= ParamDirection.In;
                }
                if (_cbOut.Checked)
                {
                    _match.ParamDirection |= ParamDirection.Out;
                }
                if (_cbRef.Checked)
                {
                    _match.ParamDirection |= ParamDirection.Ref;
                }
            }
        }

        private void lbMember_SelectedIndexChanged(object sender, EventArgs e)
        {
            DeclarationDescription description = (DeclarationDescription) _lbMember.SelectedItem;
            _cbVisibility.Enabled = description.HasAccessLevel;
            _tbInheritedFrom.Enabled = description.CanInherit || description.OwnsType;
            _tbMarkedWithAttribute.Enabled = description.CanBeMarkedWithAttribute;
            _cbReadonly.Enabled = description.CanBeReadonly;
            _cbStatic.Enabled = description.CanBeStatic;

            _cbIn.Enabled = _cbOut.Enabled = _cbRef.Enabled = description.Declaration == Declaration.Parameter;
        }

        private void cbVisibility_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            AccessLevelDescription descr = (AccessLevelDescription)_cbVisibility.Items[e.Index];
            if (e.NewValue == CheckState.Checked)
            {
                if (descr.AccessLevel == AccessLevels.Any)
                {
                    for (int i = 0; i < _cbVisibility.Items.Count; i++)
                    {
                        if (i != e.Index)
                        {
                            _cbVisibility.SetItemChecked(i, false);
                        }
                    }
                }
                else
                {
                    _cbVisibility.SetItemChecked(0, false);
                }
            }
        }
    }
}