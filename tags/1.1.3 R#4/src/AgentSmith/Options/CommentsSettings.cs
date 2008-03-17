using System;
using AgentSmith.MemberMatch;

namespace AgentSmith.Options
{
    [Serializable]
    public class CommentsSettings
    {
        private Match[] _commentMatch;
        private Match[] _commentNotMatch;
        private string _dictionaryName = "en-US";
        private bool _suppressIfBaseHasComment;
        
        public string DictionaryName
        {
            get { return _dictionaryName; }
            set { _dictionaryName = value; }
        }

        public Match[] CommentMatch
        {
            get { return _commentMatch; }
            set { _commentMatch = value; }
        }

        public Match[] CommentNotMatch
        {
            get { return _commentNotMatch; }
            set { _commentNotMatch = value; }
        }

        public bool SuppressIfBaseHasComment
        {
            get { return _suppressIfBaseHasComment; }
            set { _suppressIfBaseHasComment = value; }
        }
    }
}