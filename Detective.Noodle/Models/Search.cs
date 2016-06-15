using Detective.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Detective.Noodle.Models
{
    public class Search
    {
        public string KeyValue
        {
            get;
            set;
        }
        public QueryResult QueryResult
        {
            get;
            set;
        }
    }
}