using System;

namespace AgentSmith.Options
{
    [Serializable]
    public class CatchOrSpecifySettings
    {
        private bool _enableCatchOrSpecify;
        private string[] _exclusions = new string[] { "System.SystemException", "System.Reflection.TargetInvocationException" };

        public bool CatchOrSpecifyEnabled
        {
            get { return _enableCatchOrSpecify; }
            set { _enableCatchOrSpecify = value; }
        }

        public string[] Exclusions
        {
            get { return _exclusions; }
            set { _exclusions = value == null ? new string[] { } : value; }
        }
    }
}