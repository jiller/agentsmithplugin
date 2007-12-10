using System;

using JetBrains.ReSharper.TextControl.Markup;

// Registers highlighting type
[assembly : RegisterHighlighter(Highlighting.SUGGESTION_ID, Highlighting.SUGGESTION_GUID,
    EffectType = EffectType.WAVE,
    EffectColor = "Green",
    ErrorStripeColorName = "Green",
    ErrorStripeKind = ErrorStripeKind.WARNING,
    Layer=HighlighterLayer.ERROR,
    VSPriority=VSPriority.ERRORS
    )]

//[assembly: RegisterHighlighter(Highlighting.SUGGESTION_ID, Highlighting.SUGGESTION_GUID, EffectType = EffectType.WAVE, EffectColor = "Green", Layer = 0xfa0, VSPriority = 0x351)]


internal class Highlighting
{
    public const string SUGGESTION_ID = "Agent Smith Suggestion";
    public const string SUGGESTION_GUID = "{7922EB14-3619-4d77-8A00-755D86E75008}";
}