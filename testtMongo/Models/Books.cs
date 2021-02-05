using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace testtMongo.Models
{
    public class books
    {
        public string _id { get; set; }
        public string title { get; set; }
        public string isbn { get; set; }
        public string pageCount { get; set; }
        public IDictionary<string, object> publishedDate { get; set; }
        public string thumbnailUrl { get; set; }
        public string shortDescription { get; set; }
        public string longDescription { get; set; }
        public string status { get; set; }
        public List<string> authors { get; set; }
        public List<string> categories { get; set; }
    }
}