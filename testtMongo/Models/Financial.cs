using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace testtMongo.Models
{
    public class Financial
    {
        [BsonId]
        public ObjectId _id { get; set; }
        public string Year { get; set; }
        public string Industry_aggregation_NZSIOC { get; set; }
        public string Industry_code_NZSIOC { get; set; }
        public string Industry_name_NZSIOC { get; set; }
        public string Units { get; set; }
        public string Variable_code { get; set; }
        public string Variable_name { get; set; }
        public string Variable_category { get; set; }
        public string Value { get; set; }
        public string Industry_code_ANZSIC06 { get; set; }
    }
}