using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using LinqKit;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace ReactAdminRestServer.Common.Controllers
{
    /// <summary>
    /// Delegate for means to translate a filter to an identity comparison expression.
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <param name="expressionStarter"></param>
    /// <param name="token"></param>
    /// <returns></returns>
    public delegate Expression<Func<TEntity, bool>> IdentityExpressionFn<TEntity>(ExpressionStarter<TEntity> expressionStarter, JToken token);

    /// <summary>
    /// Encapsulation of processing of the request properties by the dialect rules.  Builds properties needed on the EF Core side.
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public class QueryDialectProperties<TEntity>
    {
        public string                             OrderField     { get; set; } = null;
        public string                             OrderDirection { get; set; } = "";

        public int                                StartIndex     { get; set; }
        public int                                EndIndex       { get; set; }

        /// <summary>
        /// Filters that we will apply via dynamic Linq extensions
        /// </summary>
        public List<KeyValuePair<string, string>> Filters        { get; set; } = null;

        /// <summary>
        /// Expression Function that can be used in a Linq Query to identify the record(s) for the entity being queried.
        /// </summary>
        public Expression<Func<TEntity, bool>>    Predicate      { get; set; } = null;

        /// <summary>
        /// Buids
        /// </summary>
        /// <param name="identityExpressionFn"></param>
        /// <param name="pageSize"></param>
        /// <param name="identityPropertyName"></param>
        /// <param name="filter"></param>
        /// <param name="ranges"></param>
        /// <param name="sorts"></param>
        public QueryDialectProperties(
            IdentityExpressionFn<TEntity>      identityExpressionFn,
            int                                pageSize            ,
            string                             identityPropertyName,
            string                             filter            = null, 
            string                             ranges            = null, 
            string                             sorts             = null)
        {
            
            int[]    range = string.IsNullOrEmpty(ranges) ? null : System.Text.Json.JsonSerializer.Deserialize<int   []>(ranges);
            string[] sort  = string.IsNullOrEmpty(sorts ) ? null : System.Text.Json.JsonSerializer.Deserialize<string[]>(sorts );
        
            if (!string.IsNullOrEmpty(filter))
            {
                var jFilter = (JObject)JsonConvert.DeserializeObject(filter);

                Filters = new List<KeyValuePair<string, string>>();
                foreach (var f in jFilter)
                {
                    var prop = typeof(TEntity).GetProperties().FirstOrDefault(x => x.Name.ToLower() == f.Key.ToLower());
                    if (prop != null)
                    {
                        // One of the dialects is a getMany which will send in an array of identifiers.
                        if (f.Value.Type == Newtonsoft.Json.Linq.JTokenType.Array && 
                            prop.Name.ToLower() == identityPropertyName.ToLower() )
                        {
                            // https://github.com/scottksmith95/LINQKit
                            var predicateBuilder = PredicateBuilder.New<TEntity>(true);

                            foreach(var jObject in f.Value)
                            {
                                // the caller supplies the identity expression function.
                                predicateBuilder =  identityExpressionFn(predicateBuilder, jObject);
                            }

                            Predicate = predicateBuilder;
                        }
                        else
                        {
                            // non-array filter.
                            // TODO: use the predicate builder for these too, and eliminate the use of Linq.Dynamic altogether?
                            string key = prop.PropertyType == typeof(string) ? 
                                $"{prop.Name}.Contains(@0)" :
                                $"{prop.Name} = @0";

                            Filters.Add(new KeyValuePair<string, string>(key, f.Value.ToString()));
                        }
                    }
                }
            }
            
            var rangeSpecifiedCorrectly = (range != null && range.Count() == 2);
            StartIndex = rangeSpecifiedCorrectly ? range[0] : 0;
            EndIndex   = rangeSpecifiedCorrectly ? range[1] : pageSize;

            if (sort != null && sort.Count() > 0)
            {
                // In practice you may not have UI's that need to sort by Id.  But the listguesser assumes you do...beware. 
                OrderField = sort[0];

                if (sort.Count() > 1 && !string.IsNullOrEmpty(sort[1]))
                    OrderDirection = sort[1].ToUpper().StartsWith("ASC") ? "" : "desc";
            }
        }
    }
}
