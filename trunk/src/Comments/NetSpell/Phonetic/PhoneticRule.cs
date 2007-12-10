/* Copyright (c) 2003, Paul Welter
*  All rights reserved.
*/

using System;

namespace AgentSmith.Comments.NetSpell.Phonetic
{
    /// <summary>
    ///		This class hold the settings for a phonetic rule
    /// </summary>
    public class PhoneticRule
    {
        private bool _beginningOnly;
        private readonly int[] _condition = new int[256];
        private int _conditionCount = 0;
        private int _consumeCount;
        private bool _endOnly;
        private int _priority;
        private bool _replaceMode = false;
        private string _replaceString;

        /// <summary>
        ///     True if this rule should be applied to the beginning only
        /// </summary>
        public bool BeginningOnly
        {
            get {return _beginningOnly;}
            set {_beginningOnly = value;}
        }


        /// <summary>
        ///     The ASCII condition array.
        /// </summary>
        public int[] Condition
        {
            get {return _condition;}
        }

        /// <summary>
        ///     The number of conditions
        /// </summary>
        public int ConditionCount
        {
            get {return _conditionCount;}
            set {_conditionCount = value;}
        }

        /// <summary>
        ///     The number of chars to consume with this rule
        /// </summary>
        public int ConsumeCount
        {
            get {return _consumeCount;}
            set {_consumeCount = value;}
        }

        /// <summary>
        ///     True if this rule should be applied to the end only
        /// </summary>
        public bool EndOnly
        {
            get {return _endOnly;}
            set {_endOnly = value;}
        }

        /// <summary>
        ///     The priority of this rule
        /// </summary>
        public int Priority
        {
            get {return _priority;}
            set {_priority = value;}
        }

        /// <summary>
        ///     True if this rule should run in replace mode
        /// </summary>
        public bool ReplaceMode
        {
            get {return _replaceMode;}
            set {_replaceMode = value;}
        }

        /// <summary>
        ///     The string to use when replacing
        /// </summary>
        public string ReplaceString
        {
            get {return _replaceString;}
            set {_replaceString = value;}
        }

    }
}