using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Learning.Code.Base
{
    class JsonParseExample : ITask
    {
        public string Name
        {
            get
            {
                return GetType().Name;
            }
        }

        public void Run()
        {
            string value = "[{\"dataBaseName\":\"fdsfsdfsdfsdf\",\"userId\":\"lhrtestfordbaapi\",\"password\":\"123456\",\"isreadonly\":0},{\"dataBaseName\":\"dsfsfsd\",\"userId\":\"fdfdfd\",\"password\":\"123456\",\"isreadonly\":0}]";
            var requestconent = JToken.Parse(value);
            JArray projectarra = JArray.Parse(requestconent.ToString());
            foreach(var a in projectarra.Children())
            {
                Console.WriteLine(a.Children<JProperty>().FirstOrDefault(x => x.Name == "dataBaseName").Value.ToString());
            }
            Console.ReadLine();
            
            /*
            var str = "{\"server\":\"10.100.100.26\",\"port\":3000,\"referIP\":\"\",\"referPort\":\"\",\"dataBaseType\":2,\"env\":0,\"dblist\":[{\"dataBaseName\":\"fsdfsdfds\",\"userId\":\"lhrtestfordbaapi\",\"password\":\"123456\",\"isreadonly\":0}]}";
            var requestconent = JToken.Parse(str);
            requestconent.Children<JProperty>().FirstOrDefault(x => x.Name == "server").Value.ToString();
            int.Parse(requestconent.Children<JProperty>().FirstOrDefault(x => x.Name == "port").Value.ToString());
            requestconent.Children<JProperty>().FirstOrDefault(x => x.Name == "referIP").Value.ToString();
            if (requestconent.Children<JProperty>().FirstOrDefault(x => x.Name == "referPort").Value != null)
            {
                int a = 0;
                int.TryParse(requestconent.Children<JProperty>().FirstOrDefault(x => x.Name == "referPort").Value.ToString(), out a);
            }
            
            requestconent.Children<JProperty>().FirstOrDefault(x => x.Name == "dataBaseType").Value.ToString();
            requestconent.Children<JProperty>().FirstOrDefault(x => x.Name == "env").Value.ToString();
            */


            //var trendsArray = twitterObject.Children<JProperty>().FirstOrDefault(x => x.Name == "result").Value;


            //foreach (var item in trendsArray.Children())
            //{
            //    var itemProperties = item.Children<JProperty>();
            //    //you could do a foreach or a linq here depending on what you need to do exactly with the value
            //    var myElement = itemProperties.FirstOrDefault(x => x.Name == "lastModifiedDate");
            //    var myElementValue = myElement.Value; ////This is a JValue type
            //    Console.WriteLine(myElementValue);
            //}
        }
    }
}
