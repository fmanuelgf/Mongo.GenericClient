namespace Mongo.Generics.Models
{
    using System;
    using System.Collections.Generic;

    public class PaginationResult<TModel>
    {
        public int Page { get; set; }

        public int PageSize { get; set; }

        public long TotalItems { get; set; }
    
        public int TotalPages =>
            (int)Math.Ceiling((double)this.TotalItems / (double)this.PageSize);

        public IEnumerable<TModel>? Result { get; set; }
    }
}