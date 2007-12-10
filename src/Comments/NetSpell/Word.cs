using System;

namespace AgentSmith.Comments.NetSpell
{
    /// <summary>
    /// The Word class represents a base word in the dictionary.
    /// </summary>
    public class Word : IComparable
    {
        private string _affixKeys = "";
        private int _editDistance = 0;
        private int _height = 0;
        private int _index = 0;
        private string _phoneticCode = "";
        private string _text = "";

        public Word()
        {
        }

        /// <summary>
        ///     Initializes a new instance of the class
        /// </summary>
        /// <param name="text" type="string">
        ///     <para>
        ///         The string for the base word
        ///     </para>
        /// </param>
        /// <param name="affixKeys" type="string">
        ///     <para>
        ///         The affix keys that can be applied to this base word
        ///     </para>
        /// </param>
        /// <param name="phoneticCode" type="string">
        ///     <para>
        ///         The phonetic code for this word
        ///     </para>
        /// </param>
        public Word(string text, string affixKeys, string phoneticCode)
        {
            _text = text;
            _affixKeys = affixKeys;
            _phoneticCode = phoneticCode;
        }

        /// <summary>
        ///     Initializes a new instance of the class
        /// </summary>
        /// <param name="text" type="string">
        ///     <para>
        ///         The string for the base word
        ///     </para>
        /// </param>
        /// <param name="affixKeys" type="string">
        ///     <para>
        ///         The affix keys that can be applied to this base word
        ///     </para>
        /// </param>
        public Word(string text, string affixKeys)
        {
            _text = text;
            _affixKeys = affixKeys;
        }

        /// <summary>
        ///     Initializes a new instance of the class
        /// </summary>
        /// <param name="text" type="string">
        ///     <para>
        ///         The string for the base word
        ///     </para>
        /// </param>
        public Word(string text)
        {
            _text = text;
        }

        /// <summary>
        ///     Initializes a new instance of the class
        /// </summary>
        /// <param name="text" type="string">
        ///     <para>
        ///         The string for the word
        ///     </para>
        /// </param>
        /// <param name="index" type="int">
        ///     <para>
        ///         The position index of this word
        ///     </para>
        /// </param>
        /// <param name="height" type="int">
        ///     <para>
        ///         The line height of this word
        ///     </para>
        /// </param>
        /// <returns>
        ///     A void value...
        /// </returns>
        internal Word(string text, int index, int height)
        {
            _text = text;
            _index = index;
            _height = height;
        }

        /// <summary>
        ///     Initializes a new instance of the class
        /// </summary>
        /// <param name="text" type="string">
        ///     <para>
        ///         The string for the base word
        ///     </para>
        /// </param>
        /// <param name="editDistance" type="int">
        ///     <para>
        ///         The edit distance from the misspelled word
        ///     </para>
        /// </param>
        internal Word(string text, int editDistance)
        {
            _text = text;
            _editDistance = editDistance;
        }

        /// <summary>
        ///     The affix keys that can be applied to this base word
        /// </summary>
        public string AffixKeys
        {
            get { return _affixKeys; }
            set { _affixKeys = value; }
        }

        /// <summary>
        ///     The index position of where this word appears
        /// </summary>
        public int Index
        {
            get { return _index; }
            set { _index = value; }
        }

        /// <summary>
        ///     The phonetic code for this word
        /// </summary>
        public string PhoneticCode
        {
            get { return _phoneticCode; }
            set { _phoneticCode = value; }
        }

        /// <summary>
        ///     The string for the base word
        /// </summary>
        public string Text
        {
            get { return _text; }
            set { _text = value; }
        }

        /// <summary>
        ///     Used for sorting suggestions by its edit distance for 
        ///     the misspelled word
        /// </summary>
        internal int EditDistance
        {
            get { return _editDistance; }
            set { _editDistance = value; }
        }

        /// <summary>
        ///     The line height of this word
        /// </summary>
        internal int Height
        {
            get { return _height; }
            set { _height = value; }
        }

        #region IComparable Members

        /// <summary>
        ///     Sorts a collection of words by <see cref="EditDistance"/>.
        /// </summary>
        /// <remarks>
        ///		The compare sorts in descending order, largest <see cref="EditDistance"/> first.
        /// </remarks>
        public int CompareTo(object obj)
        {
            int result = EditDistance.CompareTo(((Word)obj).EditDistance);
            return result; // * -1; // sorts desc order
        }

        #endregion

        /// <summary>
        ///     Converts the word object to a string
        /// </summary>
        /// <returns>
        ///		Returns the Text Property contents
        /// </returns>
        public override string ToString()
        {
            return _text;
        }
    }
}