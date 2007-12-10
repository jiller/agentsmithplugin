using System;

namespace AgentSmith.Comments.NetSpell.Affix
{
    /// <summary>
    /// Rule Entry for expanding base words.
    /// </summary>
    public class AffixEntry
    {
        private string _addCharacters = "";
        private int[] _condition = new int[256];
        private int _conditionCount;
        private string _stripCharacters = "";

        /// <summary>
        ///  The characters to add to the string.
        /// </summary>
        public string AddCharacters
        {
            get { return _addCharacters; }
            set { _addCharacters = value; }
        }

        /// <summary>
        ///  The condition to be met in order to add characters.
        /// </summary>
        public int[] Condition
        {
            get { return _condition; }
            set { _condition = value; }
        }

        /// <summary>
        ///  The characters to remove before adding characters.
        /// </summary>
        public string StripCharacters
        {
            get { return _stripCharacters; }
            set { _stripCharacters = value; }
        }

        /// <summary>
        ///  The number of conditions that must be met.
        /// </summary>
        public int ConditionCount
        {
            get { return _conditionCount; }
            set { _conditionCount = value; }
        }
    }
}