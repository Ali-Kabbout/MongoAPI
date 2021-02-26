using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace testtMongo.Models
{
    public class Items
    {
        public string name { get; set; }
        public List<string> tags { get; set; }
        public decimal price { get; set; }
        public int quantity { get; set; }
    }
    public class Customer
    {
        public string gender { get; set; }
        public int age { get; set; }
        public string email { get; set; }
        public int satisfaction { get; set; }
    }
    public class Sales
    {
        [BsonId]
        public ObjectId _id { get; set; }
        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime saleDate { get; set; }
        public List<Items> items { get; set; }
        public Customer customer { get; set; }
        public bool couponUsed { get; set; }
        public string purchaseMethod { get; set; }
        public string storeLocation { get; set; }
        public DateTime deleted_at { set; get; }

    }
}