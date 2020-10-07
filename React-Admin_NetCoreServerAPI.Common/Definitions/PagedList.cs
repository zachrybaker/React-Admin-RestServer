using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace ReactAdminRestServer.Common.Definitions
{
   public class PagedList<T> : IPagedList<T>
    {
        public T this[int index] => throw new NotImplementedException();
        public IReadOnlyList<T> List { get; set; }

        
        [JsonIgnore]
        public int TotalCount        { get; set; }
        [JsonIgnore]
        public int StartIndex        { get; set; }
        [JsonIgnore]
        public int EndIndex          { get; set; }

        [JsonIgnore]
        public int PageSize          => (int)(EndIndex - StartIndex + 1);
        [JsonIgnore]
        public int PageIndex         => StartIndex == 0 || PageSize == 0 ? 0 : StartIndex / PageSize;

        public PagedList(IReadOnlyList<T> list, int totalCount, int startIndex, int endIndex)
        {
            List = list;
            TotalCount = totalCount;
            StartIndex = startIndex;
            EndIndex = endIndex;
        }
    }
}
