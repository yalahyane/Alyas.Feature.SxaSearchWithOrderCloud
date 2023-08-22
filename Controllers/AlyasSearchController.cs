using System.Collections.Generic;
using System.Linq;
using System.Web.Http.ModelBinding;
using Alyas.Feature.SxaSearchWithOrderCloud.Models;
using Sitecore.ContentSearch.Data;
using Sitecore.XA.Feature.Search.Attributes;
using Sitecore.XA.Feature.Search.Binder;
using Sitecore.XA.Feature.Search.Controllers;
using Sitecore.XA.Feature.Search.Filters;
using Sitecore.XA.Foundation.Search;
using Sitecore.XA.Foundation.Search.Models;
using Sitecore.XA.Foundation.Search.Models.Binding;
using Sitecore.XA.Foundation.SitecoreExtensions.Extensions;

namespace Alyas.Feature.SxaSearchWithOrderCloud.Controllers
{
    public class AlyasSearchController : SearchController
    {
        [RegisterSearchEvent]
        [CacheWebApi]
        public SearchResultSet Results([ModelBinder(BinderType = typeof(QueryModelBinder))] QueryModel model)
        {
            Timer timer1;
            string index = null;
            int count;
            Timer timer2;
            IEnumerable<SearchResult> list;
            using (timer1 = new Timer())
            {
                SetPageContext(model.ItemID);
                var model1 = new SearchQueryModel
                {
                    Coordinates = model.Coordinates,
                    ItemID = model.ItemID,
                    Languages = model.Languages,
                    Query = GetQueryValue(model),
                    ScopesIDs = model.ScopesIDs,
                    Site = model.Site
                };
                ref var local = ref index;
                var query = this.SearchService.GetQuery(model1, out local);
                count = query.Count();
                var source = GetSearchResults(model.Sortings, model.Offset, model.PageSize, model.Site, query, model.Coordinates);
                using (timer2 = new Timer())
                {
                    list = source.Select(i => new SearchResult(i)).ToList();
                }
                RevertPageContext();
            }
            return new SearchResultSet(timer1.Msec, timer2.Msec, model.Signature, index, count, list);
        }

        private IEnumerable<ContentPage> GetSearchResults(
            IEnumerable<string> sorting,
            int e,
            int p,
            string site,
            IQueryable<ContentPage> query,
            Coordinate center)
        {
            var enumerable = sorting.ToList();
            if (this.SortingService.GetSortingDirection(enumerable) == SortingDirection.Random)
            {
                return from r in query.AsEnumerable().GetRandom(p)
                    select r;
            }
            query = this.SortingService.Order(query, enumerable, center, site);
            query = query.Skip(e);
            query = query.Take(p);

            return query;
        }
    }
}