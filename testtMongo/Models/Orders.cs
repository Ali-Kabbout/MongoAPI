using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace testtMongo.Models
{
    public class Orders
    {
        [BsonId]
        public ObjectId _id { get; set; }
        public List<int> items { set; get; }
    }
}