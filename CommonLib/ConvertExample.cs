using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace CommonLib
{
    public class ConvertExample
    {
        public void Run()
        {
            var username = "fdsfsdf";
            var password = "fsdfsdfds";
            var usernamePassword = username + ":" + password;
            var url = $"http://wwww.test.com/api";
            using (var client = new HttpClient(new HttpClientHandler() { AutomaticDecompression = DecompressionMethods.GZip }))
            {
                client.Timeout = new TimeSpan(0, 0, 5);
                client.DefaultRequestHeaders.Add("Authorization", "Basic " + Convert.ToBase64String(new ASCIIEncoding().GetBytes(usernamePassword)));
                client.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", "application/json");
                Newtonsoft.Json.Linq.JArray userAarray = Newtonsoft.Json.Linq.JArray.Parse(client.GetStringAsync(url).Result) as Newtonsoft.Json.Linq.JArray;
                Newtonsoft.Json.Linq.JObject jObject = userAarray[0] as Newtonsoft.Json.Linq.JObject;
                var a = jObject.Property("value").Value;
                DataSet set = DataTableConvert.ConvertXMLToDataSet(a.ToString());
                DataTable dt = set.Tables[0];
            }
            Console.Write("converttest");
        }
    }
}
