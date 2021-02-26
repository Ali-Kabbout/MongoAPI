using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using testtMongo.Models;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Web;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using MongoDB.Bson.IO;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Text;

namespace testtMongo.Controllers
{
    public class SalesController : ApiController
    {

        static IMongoClient client = new MongoClient(System.Configuration.ConfigurationManager.ConnectionStrings["DefaultMongoConnection"].ConnectionString);
        static IMongoDatabase db = client.GetDatabase(System.Configuration.ConfigurationManager.AppSettings.Get("MongoDbName"));
        static IMongoCollection<BsonDocument> SaleCollection = db.GetCollection<BsonDocument>("sales");


        [Route("api/Sales/GetAll")]
        [HttpGet]
        public HttpResponseMessage GetAll()
        {
            List<Sales> sales = new List<Sales>();
            try
            {
                FilterDefinition<BsonDocument> filter = "{ }";
                var findResult = SaleCollection.FindSync(filter).ToList();
                foreach (var sle in findResult)
                {
                    Sales sale = BsonSerializer.Deserialize<Sales>(sle);
                    sales.Add(sale);
                }
            }
            catch (Exception ex) { }

            return Request.CreateResponse(HttpStatusCode.OK, sales);


        }

        [Route("api/Sales/GetInSales")]
        [HttpGet]
        public HttpResponseMessage GetInSales(string storeLocation, string email, DateTime? fromDate, DateTime? toDate, string purchaseMethod)
        {
            List<Sales> sales = new List<Sales>();
            try
            {

                bool isSearchFieldsFilled = false;
                string comma = ",";
                StringBuilder query = new StringBuilder();
                query.Append("{ $and: [ ");
                if (!string.IsNullOrEmpty(storeLocation))
                {
                    query.Append("{\"storeLocation\":/.*" + storeLocation + ".*/}");
                    isSearchFieldsFilled = true;
                }
                if (!string.IsNullOrEmpty(email))
                {
                    string acomma = isSearchFieldsFilled ? comma : string.Empty;
                    query.Append(acomma + "{\"customer.email\":/.*" + email + ".*/}");
                    isSearchFieldsFilled = true;
                }
                if (!string.IsNullOrEmpty(purchaseMethod))
                {
                    string acomma = isSearchFieldsFilled ? comma : string.Empty;
                    query.Append(acomma + "{\"purchaseMethod\":/.*" + purchaseMethod + ".*/}");
                    isSearchFieldsFilled = true;
                }
                if (fromDate != null)
                {
                    string acomma = isSearchFieldsFilled ? comma : string.Empty;
                    query.Append(acomma + "{\"saleDate\":{$gte:new ISODate(\"" + new BsonDateTime(fromDate.Value) + "\")}}");
                    isSearchFieldsFilled = true;
                }
                if (toDate != null)
                {
                    string acomma = isSearchFieldsFilled ? comma : string.Empty;
                    query.Append(acomma + "{\"saleDate\":{$lte:new ISODate(\"" + new BsonDateTime(toDate.Value) + "\")}}");
                    isSearchFieldsFilled = true;
                }

                if (isSearchFieldsFilled)
                    query.Append("] } ");
                else
                {
                    query.Clear();
                    query.Append("{}");

                }
                string ss = query.ToString();
                //db.inventory.find( { $and: [ { price: { $ne: 1.99 } }, { price: { $exists: true } } ] } )

                FilterDefinition<BsonDocument> filter = query.ToString();
                var findResult = SaleCollection.FindSync(filter).ToList();
                foreach (var sle in findResult)
                {
                    Sales sale = BsonSerializer.Deserialize<Sales>(sle);
                    sales.Add(sale);
                }
            }
            catch (Exception ex) { }

            return Request.CreateResponse(HttpStatusCode.OK, sales);


        }
        [Route("api/Sales/SaveInSales")]
        [HttpGet]
        public HttpResponseMessage SaveInSales(string CustomerEmail, string age, string Satisfaction, string gender, string CouponUsed, string PurchaseMethod, DateTime? SaleDate, string StoreLocation)
        {
            try
            {
                var document = new BsonDocument
                  {
                  { "saleDate", new BsonDateTime(SaleDate.Value) },
                                        { "items", new BsonArray {
        new BsonDocument { { "name", "jim" }, { "tags", new BsonArray { "s", "s" } }, { "price", 3 },{ "quantity", 3 } } } },

                  { "storeLocation", StoreLocation },
                                    { "customer", new BsonDocument { { "gender", "M" }, { "age", age }, { "email", CustomerEmail }, { "satisfaction", Satisfaction } } },

                  { "couponUsed", true },

                  { "purchaseMethod", PurchaseMethod }
                                  };
                SaleCollection.InsertOne(document);
            }

            catch (Exception ex)
            { }

            return Request.CreateResponse(HttpStatusCode.OK, "");

        }



        // GET api/<controller>
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<controller>/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<controller>
        public void Post([FromBody]string value)
        {
        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }
    }
}