/* Copyright (c) 2003, Paul Welter
*  All rights reserved.
*/

using System;
using System.Collections.Generic;

namespace AgentSmith.Comments.NetSpell.Affix
{
    /// <summary>
    /// Rule for expanding base words.
    /// </summary>
    public class AffixRule
    {
        private List<AffixEntry> _affixEntries = new List<AffixEntry>();
        private bool _allowCombine = false;
        private string _name = "";

        /// <summary>
        /// Allow combining prefix and suffix.
        /// </summary>
        public bool AllowCombine
        {
            get { return _allowCombine; }
            set { _allowCombine = value; }
        }

        /// <summary>
        /// Collection of text entries that make up this rule.
        /// </summary>
        public List<AffixEntry> AffixEntries
        {
            get { return _affixEntries; }
            set { _affixEntries = value; }
        }

        /// <summary>
        /// Name of the Affix rule.
        /// </summary>
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }
    }
}