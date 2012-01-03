using System;
using System.Collections.Generic;
using System.Xml;

using AgentSmith.Comments;
using AgentSmith.Identifiers;
using AgentSmith.Options;
using AgentSmith.SpellCheck;
using AgentSmith.SpellCheck.NetSpell;
using AgentSmith.Strings;

using JetBrains.Application.Settings;
using JetBrains.ProjectModel;
using JetBrains.ReSharper.Daemon;
using JetBrains.ReSharper.Psi;
using JetBrains.ReSharper.Psi.CSharp;
using JetBrains.ReSharper.Psi.CSharp.Parsing;
using JetBrains.ReSharper.Psi.CSharp.Tree;
using JetBrains.ReSharper.Psi.ExtensionsAPI;
using JetBrains.ReSharper.Psi.Services;
using JetBrains.ReSharper.Psi.Tree;

namespace AgentSmith
{
    /// <summary>
    /// Process stage for analysing comments in a project file.
    /// </summary>
    /// <remarks>
    /// <para>
    /// We scan for all declarations.
    /// </para>
    /// <para>
    /// For each declaration we do several checks:
    /// </para>
    /// <list type="bullet">
    /// <item>
    /// Does the member have an XML documentation comment?
    /// </item>
    /// <item>
    /// Are all the words in the comment correctly spelled?
    /// </item>
    /// <item>
    /// Are there any words that look like identifiers that we should wrap with meta-tags?
    /// </item>
    /// </list>
    /// </remarks>
    internal class AgentSmithDaemonProcess : IDaemonStageProcess
    {
        /// <summary>
        /// Internal storage for the process that this stage is a part of
        /// </summary>
        private readonly IDaemonProcess _daemonProcess;
        private readonly ISolution _solution;

        private readonly IContextBoundSettingsStore _settingsStore;

        /// <summary>
        /// Create a new process within a stage that processes comments in the current file.
        /// </summary>
        /// <param name="daemonProcess">The current instance process that this stage will be a part of</param>
        /// <param name="settingsStore"> </param>
        public AgentSmithDaemonProcess(IDaemonProcess daemonProcess, IContextBoundSettingsStore settingsStore)
        {
            _daemonProcess = daemonProcess;
            _solution = daemonProcess.Solution;

            _settingsStore = settingsStore;


        }

        #region IDaemonStageProcess Members

        /// <summary>
        /// The current instance process that this stage is a part of.
        /// </summary>
        public IDaemonProcess DaemonProcess { get { return _daemonProcess; } }



        private void CheckMember(IClassMemberDeclaration declaration,
                                                List<HighlightingInfo> highlightings, CommentAnalyzer commentAnalyzer, IdentifierSpellCheckAnalyzer identifierAnalyzer)
        {
            if (declaration is IConstructorDeclaration && declaration.IsStatic)
            {
                // TODO: probably need to put this somewhere in settings.
                //Static constructors have no visibility so not clear how to check them.
                return;
            }


            // Documentation doesn't work properly on multiple declarations (as of R# 6.1) so see if we can get it from the parent
            XmlNode docNode = null;
            IDocCommentBlockNode commentBlock;
            IMultipleDeclarationMember multipleDeclarationMember = declaration as IMultipleDeclarationMember;
            if (multipleDeclarationMember != null)
            {
                // get the parent
                IMultipleDeclaration multipleDeclaration = multipleDeclarationMember.MultipleDeclaration;

                // Now ask for the actual comment block
                commentBlock = SharedImplUtil.GetDocCommentBlockNode(multipleDeclaration);

                if (commentBlock != null) docNode = commentBlock.GetXML(null);
            }
            else
            {
                commentBlock = SharedImplUtil.GetDocCommentBlockNode(declaration);

                docNode = declaration.GetXMLDoc(false);

            }

            commentAnalyzer.CheckMemberHasComment(declaration, docNode, highlightings);
            commentAnalyzer.CheckCommentSpelling(declaration, commentBlock, highlightings, true);
            identifierAnalyzer.CheckMemberSpelling(declaration, highlightings, true);
        }

        /// <summary>
        /// Execute this stage of the process.
        /// </summary>
        /// <param name="commiter">The function to call when we've finished the stage to report the results.</param>
        public void Execute(Action<DaemonStageResult> commiter)
        {

            var highlightings = new List<HighlightingInfo>();

            IFile file = _daemonProcess.SourceFile.GetPsiFile(CSharpLanguage.Instance);
            if (file == null)
            {
                return;
            }

            StringSettings stringSettings = _settingsStore.GetKey<StringSettings>(SettingsOptimization.OptimizeDefault);


            file.ProcessChildren<ICSharpLiteralExpression>(literalExpression => this.CheckString(literalExpression, highlightings, stringSettings));

            if (_daemonProcess.FullRehighlightingRequired)
            {
                CommentAnalyzer commentAnalyzer = new CommentAnalyzer(_solution, _settingsStore);
                IdentifierSpellCheckAnalyzer identifierAnalyzer = new IdentifierSpellCheckAnalyzer(_solution, _settingsStore, _daemonProcess.SourceFile);

                file.ProcessChildren<IClassMemberDeclaration>(declaration => this.CheckMember(declaration, highlightings, commentAnalyzer, identifierAnalyzer));
            }


            try
            {
                commiter(new DaemonStageResult(highlightings));
            } catch
            {
                // Do nothing if it doesn't work.
            }
        }


        public void CheckString(ICSharpLiteralExpression literalExpression,
                                                List<HighlightingInfo> highlightings, StringSettings settings)
        {
            //ConstantValue val = literalExpression.ConstantValue;

            // Ignore it unless it's something we're re-evalutating
            if (!_daemonProcess.IsRangeInvalidated(literalExpression.GetDocumentRange())) return;



            // Ignore verbatim strings.
            if (settings.IgnoreVerbatimStrings &&
                LiteralService.Get(CSharpLanguage.Instance).IsVerbatimStringLiteral(literalExpression)) return;

            ITokenNode tokenNode = literalExpression.Literal;
            if (tokenNode == null) return;

            if (tokenNode.GetTokenType() == CSharpTokenType.STRING_LITERAL)
            {
                 ISpellChecker spellChecker = SpellCheckManager.GetSpellChecker(_settingsStore, _solution, settings.DictionaryNames);

                StringSpellChecker.SpellCheck(
                    literalExpression.GetDocumentRange().Document,
                    tokenNode,
                    spellChecker,
                    _solution, highlightings, _settingsStore, settings);
            }
        }

        #endregion
    }
}