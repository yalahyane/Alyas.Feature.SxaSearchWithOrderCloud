using System;
using Sitecore.XA.Feature.Search.Models;
using Sitecore.XA.Foundation.Search.Models;

namespace Alyas.Feature.SxaSearchWithOrderCloud.Models
{
    public class SearchResult : Result
    {
        public new string Id { get; set; }
        public bool IsProduct { get; set; }

        public SearchResult(ContentPage searchResult)
        {
            this.IsProduct = searchResult.Fields["_datasource"].ToString().Equals("OrderCloud", StringComparison.OrdinalIgnoreCase);
            this.Id = searchResult.ItemId.ToString();
            this.Language = searchResult.Language;
            this.Path = searchResult.Path;
            this.Name = searchResult.Name;
            this.Url = searchResult.Url;
        }
    }
}