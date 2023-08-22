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
            if (searchResult.Fields["_datasource"].ToString().Equals("OrderCloud", StringComparison.OrdinalIgnoreCase))
            {
                this.IsProduct = true;
                this.Id = searchResult.Fields["_name"].ToString();
                this.Language = searchResult.Fields["_language"].ToString();
                this.Name = searchResult.Fields["_displayname"].ToString();
                this.Url = searchResult.Fields["_fullpath"].ToString();
                this.Path = searchResult.Fields["_fullpath"].ToString();
            }
            else
            {
                this.IsProduct = false;
                this.Id = searchResult.ItemId.ToString();
                this.Language = searchResult.Language;
                this.Path = searchResult.Path;
                this.Name = searchResult.Name;
                this.Url = searchResult.Url;
            }
        }
    }
}