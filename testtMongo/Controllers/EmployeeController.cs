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
    public class EmployeeController : ApiController
    {


        static IMongoClient client = new MongoClient(System.Configuration.ConfigurationManager.ConnectionStrings["DefaultMongoConnection"].ConnectionString);
        static IMongoDatabase db = client.GetDatabase(System.Configuration.ConfigurationManager.AppSettings.Get("MongoDbName"));
        static IMongoCollection<BsonDocument> ProfileCollection = db.GetCollection<BsonDocument>(System.Configuration.ConfigurationManager.AppSettings.Get("MongoDbCollectionName"));

        static IMongoDatabase ImportDB; // = client.GetDatabase("productmanagementdb"); //"productmanagementdb"
        static IMongoCollection<BsonDocument> OnixDataCollection; // = db.GetCollection<BsonDocument>("DiliCom"); //db.GetCollection<BsonDocument>(System.Configuration.ConfigurationManager.AppSettings.Get("MongoDbCollectionName"));

        public HttpResponseMessage Get()
        {
            string query = @"
                    select EmployeeId,EmployeeName,Department,
                    convert(varchar(10),DateOfJoining,120) as DateOfJoining,
                    PhotoFileName
                    from
                    dbo.Employee
                    ";
            DataTable table = new DataTable();
            using (var con = new SqlConnection(ConfigurationManager.
                ConnectionStrings["EmployeeAppDB"].ConnectionString))
            using (var cmd = new SqlCommand(query, con))
            using (var da = new SqlDataAdapter(cmd))
            {
                cmd.CommandType = CommandType.Text;
                da.Fill(table);
            }

            return Request.CreateResponse(HttpStatusCode.OK, table);


        }

        [Route("api/Employee/GetBookMongo")]
        [HttpGet]
        public HttpResponseMessage GetBookMongo(string title,string status)
        {
            StringBuilder sb = new StringBuilder();
            try
            {
                FilterDefinition<BsonDocument> filter = "{ status:\"PUBLISH\"}";
                var filterRec = Builders<BsonDocument>.Filter.Eq("status", "PUBLISH");
                var findResult = ProfileCollection.FindSync(filter).ToList();
                foreach (var tt in findResult)
                {
                    var jsonWriterSettings = new JsonWriterSettings { OutputMode = JsonOutputMode.Strict };
                    var ess = tt.ToJson(jsonWriterSettings);
                    sb.Append(ess + ",");
                }
                sb.Length--;

            }
            catch (Exception ex) { }
            var test = "{\"items\":[" + sb.ToString() + "]}";

            return Request.CreateResponse(HttpStatusCode.OK, test);


        }
        [Route("api/Employee/GetMongo")]
        [HttpGet]
        public HttpResponseMessage GetMongo()
        {
            List<books> books = new List<books>();
            StringBuilder sb = new StringBuilder();

            try
            {
                FilterDefinition<BsonDocument> filter = "{ status:\"PUBLISH\"}";


                var filterRec = Builders<BsonDocument>.Filter.Eq("status", "PUBLISH");
                //var filterData = Builders<BsonDocument>.Filter.Eq("Data_source", b["Data_source"]);
                //var filter = Builders<BsonDocument>.Filter.And(filterRec, filterData);
                //var filter = BsonSerializer.Deserialize<BsonDocument>("{status:\"PUBLISH\"}");

                var findResult = ProfileCollection.FindSync(filter).ToList();

                //var jsonWriterSettings = new JsonWriterSettings { OutputMode = JsonOutputMode.Strict };
                //JObject json = JObject.Parse(postBsonDoc.ToJson<MongoDB.Bson.BsonDocument>(jsonWriterSettings));
                foreach (var tt in findResult)
                {
                    //JObject jsonResult = JObject.Parse(tt.ToJson<MongoDB.Bson.BsonDocument>(new MongoDB.Bson.IO.JsonWriterSettings { OutputMode = MongoDB.Bson.IO.JsonOutputMode.Strict }));
                    //JObject json = JObject.Parse(postBsonDoc.ToJson<MongoDB.Bson.BsonDocument>(jsonWriterSettings));


                    //var document = new BsonDocument("_id", ObjectId.GenerateNewId());
                    //var jsonWriterSettingss= new JsonWriterSettings { OutputMode = JsonOutputMode.Strict }; // key part
                    //var test = document.ToJson(tt);

                    //string rc = tt.ToJson<MongoDB.Bson.BsonDocument>();
                    //    var x = Newtonsoft.Json.Linq.JObject.Parse(rc);

                    var jsonWriterSettings = new JsonWriterSettings { OutputMode = JsonOutputMode.Strict };
                    var ess = tt.ToJson(jsonWriterSettings);
                    //Console.WriteLine(document.ToJson(jsonWriterSettings));
                    sb.Append(ess + ",");

                    //    books book = Newtonsoft.Json.JsonConvert.DeserializeObject<books>(ess);
                    //books.Add(book);
                }
                sb.Length--;
                //List<Employee> ep = new List<Employee>();
                //foreach(var tt in findResult)
                //{
                //    Employee edp = new Employee();
                //    edp.EmployeeName = tt["title"].ToString();
                //    edp.Department = tt["status"].ToString();
                //    ep.Add(edp);
                //}
                //string query = @"
                //        select EmployeeId,EmployeeName,Department,
                //        convert(varchar(10),DateOfJoining,120) as DateOfJoining,
                //        PhotoFileName
                //        from
                //        dbo.Employee
                //        ";
                //DataTable table = new DataTable();
                //using (var con = new SqlConnection(ConfigurationManager.
                //    ConnectionStrings["EmployeeAppDB"].ConnectionString))
                //using (var cmd = new SqlCommand(query, con))
                //using (var da = new SqlDataAdapter(cmd))
                //{
                //    cmd.CommandType = CommandType.Text;
                //    da.Fill(table);
                //}
            }
            catch (Exception ex) { }
            var test = "{\"items\":[" + sb.ToString() + "]}";

            return Request.CreateResponse(HttpStatusCode.OK, test);


        }


        public string Post(Employee emp)
        {
            try
            {
                string query = @"
                    insert into dbo.Employee values
                    (
                    '" + emp.EmployeeName + @"'
                    ,'" + emp.Department + @"'
                    ,'" + emp.DateOfJoining + @"'
                    ,'" + emp.PhotoFileName + @"'
                    )
                    ";

                DataTable table = new DataTable();
                using (var con = new SqlConnection(ConfigurationManager.
                    ConnectionStrings["EmployeeAppDB"].ConnectionString))
                using (var cmd = new SqlCommand(query, con))
                using (var da = new SqlDataAdapter(cmd))
                {
                    cmd.CommandType = CommandType.Text;
                    da.Fill(table);
                }

                return "Added Successfully!!";
            }
            catch (Exception)
            {

                return "Failed to Add!!";
            }
        }
    }
}