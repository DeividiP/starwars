using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace ResupplyStops.Application.Infra.WSAPIProxy
{
    public class PageResult<T>
    {
        public int Count { get; set; }
        public string Next { get; set; }
        public string Previous { get; set; }
        public List<T> Results { get; set; }

        public int? NextPage => ExtractPageNumberFromUrl(Next);

        private int? ExtractPageNumberFromUrl(string url)
        {
            if (string.IsNullOrEmpty(url))
                return null;

            var match = Regex.Match(Next, "\\?page=(\\d+)+");

            if (match.Success)
                return Convert.ToInt32(match.Groups[1].Value);
            else
                return null;
        }
    }
}
