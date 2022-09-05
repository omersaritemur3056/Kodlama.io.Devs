using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.ElasticSearch.Models
{
    public class ElasticSearchResult : IElasticSearchResult
    {
        public ElasticSearchResult(bool success, string message) : this(success)
        {
            Message = message;
        }

        public ElasticSearchResult(bool success)
        {
            Success = success;
        }

        public bool Success { get; set; }

        public string Message { get; set; }
    }
}
