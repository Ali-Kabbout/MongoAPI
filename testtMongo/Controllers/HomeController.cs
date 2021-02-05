using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;



using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;

namespace testtMongo.Controllers
{
    public class HomeController : Controller
    {
        static IMongoClient client = new MongoClient(System.Configuration.ConfigurationManager.ConnectionStrings["DefaultMongoConnection"].ConnectionString);
        static IMongoDatabase db = client.GetDatabase(System.Configuration.ConfigurationManager.AppSettings.Get("MongoDbName"));
        static IMongoCollection<BsonDocument> ProfileCollection = db.GetCollection<BsonDocument>(System.Configuration.ConfigurationManager.AppSettings.Get("MongoDbCollectionName"));

        static IMongoDatabase ImportDB; // = client.GetDatabase("productmanagementdb"); //"productmanagementdb"
        static IMongoCollection<BsonDocument> OnixDataCollection; // = db.GetCollection<BsonDocument>("DiliCom"); //db.GetCollection<BsonDocument>(System.Configuration.ConfigurationManager.AppSettings.Get("MongoDbCollectionName"));

        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";

            try
            {
                //FilterDefinition<BsonDocument> filter = "{ status:\"PUBLISH\"}";


                //var filterRec = Builders<BsonDocument>.Filter.Eq("status", "PUBLISH");
                ////var filterData = Builders<BsonDocument>.Filter.Eq("Data_source", b["Data_source"]);
                ////var filter = Builders<BsonDocument>.Filter.And(filterRec, filterData);
                ////var filter = BsonSerializer.Deserialize<BsonDocument>("{status:\"PUBLISH\"}");

                //var findResult = ProfileCollection.FindSync(filter).ToList();


            }
            catch (Exception ex)
            { }
            return View();

        }
    }
}
