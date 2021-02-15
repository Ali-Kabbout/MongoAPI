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
    public class FinancialController : ApiController
    {


        static IMongoClient client = new MongoClient(System.Configuration.ConfigurationManager.ConnectionStrings["DefaultMongoConnection"].ConnectionString);
        static IMongoDatabase db = client.GetDatabase(System.Configuration.ConfigurationManager.AppSettings.Get("MongoDbName"));
        static IMongoCollection<BsonDocument> FinancialCollection = db.GetCollection<BsonDocument>(System.Configuration.ConfigurationManager.AppSettings.Get("testCollection"));

        static IMongoDatabase ImportDB; // = client.GetDatabase("productmanagementdb"); //"productmanagementdb"

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

        [Route("api/Financial/GetFinancialMongo")]
        [HttpGet]
        public HttpResponseMessage GetFinancialMongo()
        {
            StringBuilder sb = new StringBuilder();
            List<Financial> financials = new List<Financial>();
            try
            {
                FilterDefinition<BsonDocument> filter = "{ }";
                var findResult = FinancialCollection.FindSync(filter).ToList();
                foreach (var financialBson in findResult)
                {
                    Financial financial = BsonSerializer.Deserialize<Financial>(financialBson);
                    financials.Add(financial);
                }
            }
            catch (Exception ex)
            { }
            return Request.CreateResponse(HttpStatusCode.OK, financials);
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