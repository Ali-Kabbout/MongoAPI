using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace testtMongo.Models
{
    public class Products
    {
        [BsonId]
        public ObjectId _id { get; set; }

        public string ProductName { set; get; }
    }
}