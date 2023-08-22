using System.Collections.Generic;
using System.Web;

namespace Alyas.Feature.SxaSearchWithOrderCloud.Models
{
    public class SearchResultSet
    {
        public long TotalTime { get; set; }

        public long QueryTime { get; set; }

        public string Signature { get; set; }

        public string Index { get; set; }

        public int Count { get; set; }

        public IEnumerable<SearchResult> Results { get; set; }

        public SearchResultSet(
            long totalTime,
            long queryTime,
            string signature,
            string index,
            int count,
            IEnumerable<SearchResult> results)
        {
            this.TotalTime = totalTime;
            this.QueryTime = queryTime;
            this.Signature = HttpUtility.HtmlEncode(signature);
            this.Index = index;
            this.Count = count;
            this.Results = results;
        }

        public SearchResultSet() => this.Results = new List<SearchResult>();
    }
}