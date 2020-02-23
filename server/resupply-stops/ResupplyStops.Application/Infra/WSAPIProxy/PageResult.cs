using System;
using System.Collections.Generic;
using System.Text;

namespace ResupplyStops.Application.Infra.WSAPIProxy
{
    public class PageResult<T>
    {
        public int Count { get; set; }
        public string Next { get; set; }
        public string Previus { get; set; }
        public List<T> Result { get; set; }
    }
}
