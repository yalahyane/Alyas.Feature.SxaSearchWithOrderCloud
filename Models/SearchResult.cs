using System;
using Sitecore.Links;
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
                var searchItem = searchResult.GetItem();
                this.Id = searchItem.ID.ToString();
                this.Language = searchItem.Language.Name;
                this.Path = searchItem.Paths.FullPath;
                this.Name = searchItem.DisplayName;
                this.Url = LinkManager.GetItemUrl(searchItem);
            }
        }
    }
}