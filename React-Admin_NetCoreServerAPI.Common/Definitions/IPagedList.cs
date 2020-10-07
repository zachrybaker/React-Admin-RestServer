using System.Collections.Generic;

namespace ReactAdminRestServer.Common.Definitions
{
    public interface IPagedList<T>  
    {
        public int       TotalCount { get; set; }
        public int       StartIndex { get; set; }
        public int       EndIndex   { get; set; }

        public int       PageSize  => (int)(EndIndex - StartIndex + 1);
        public int       PageIndex => StartIndex == 0 || PageSize == 0 ? 0 : (int)StartIndex / PageSize;

        IReadOnlyList<T> List     { get; set; }
    }
}
