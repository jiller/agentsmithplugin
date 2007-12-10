using System;

namespace AgentSmith.Comments.NetSpell
{
    /// <summary>
    /// The suggestion strategy to use when generating suggestions.
    /// </summary>
    public enum Suggestion
    {
        /// <summary>
        /// Combines the phonetic and near miss strategies.
        /// </summary>
        PhoneticNearMiss,
        /// <summary>
        /// The phonetic strategy generates suggestions by word sound.
        /// </summary>
        /// <remarks>
        ///	This technique was developed by the open source project ASpell.net.
        /// </remarks>
        Phonetic,
        /// <summary>
        /// The near miss strategy generates suggestion by replacing, 
        /// removing, adding chars to make words.
        /// </summary>
        /// <remarks>
        /// This technique was developed by the open source spell checker ISpell.
        /// </remarks>
        NearMiss
    }
}