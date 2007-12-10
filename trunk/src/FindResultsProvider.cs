using System;
using JetBrains.ReSharper.CodeView.Search;

namespace AgentSmith
{
    public class FindResultsProvider// : IFindResultsItemsProvider
    {
        SearchResultsConsumer _exceptions = new SearchResultsConsumer();
        SearchResultsConsumer _namingConventions = new SearchResultsConsumer();
        SearchResultsConsumer _commentsConventions = new SearchResultsConsumer();
        SearchResultsConsumer _structureConventions = new SearchResultsConsumer();
        
        /*public FindResultsViewSection[] Sections()
        {
            FindResultsViewSection exceptionSection = new FindResultsViewSection("Exceptions not conforming to Throw or Specify.", FindResultsItemsDisplayMode.Tree, _exceptions);
            FindResultsViewSection namingConventionSection = new FindResultsViewSection("Naming convention violations.", FindResultsItemsDisplayMode.Tree, _namingConventions);
            FindResultsViewSection commentsConventionSection = new FindResultsViewSection("Comment convention violations.", FindResultsItemsDisplayMode.Tree, _commentsConventions);
            FindResultsViewSection structureConventions = new FindResultsViewSection("Member order violation.", FindResultsItemsDisplayMode.Tree, _commentsConventions);

            return new FindResultsViewSection[] { exceptionSection, namingConventionSection, commentsConventionSection, structureConventions };
        }*/

        public void RerunSearch()
        {            
        }

        public string Title
        {
            get { return "Agent Smith Code Style Violations"; }
        }

        public int InitialSection
        {
            get { return 0; }
        }

        public SearchResultsConsumer Exceptions
        {
            get { return  _exceptions; }
        }

        public SearchResultsConsumer NamingConventions
        {
            get { return _namingConventions; }
        }

        public SearchResultsConsumer Comments
        {
            get { return _commentsConventions; }
        }

        public SearchResultsConsumer Structure
        {
            get { return _structureConventions; }
        }        
    }
}