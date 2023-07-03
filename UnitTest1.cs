using Newtonsoft.Json;
using NUnit.Framework;
using RestSharp;
using System.Diagnostics;


namespace RestSharpAutomationDemo
{ 
    public class tokens
    {
        public string token { get; set; }
    }

    public class Tests
    {
        string myToken;
        [SetUp]
        public void Setup()
        {
            String[] parms = {""};
            ProcessStartInfo startInfo = new ProcessStartInfo(@"C:\Users\Maysoon.Magdy\source\repos\RestSharpAutomationDemo\bin\Debug\net7.0\AAMLiveAES.exe");
            startInfo.WindowStyle = ProcessWindowStyle.Normal;
            Process.Start(startInfo);
            var path = @"C:\Users\Maysoon.Magdy\source\repos\RestSharpAutomationDemo\bin\Debug\net7.0\token.json";
            using (StreamReader r = new StreamReader(path))
            {
                string json = r.ReadToEnd();
                List<tokens> tokens = JsonConvert.DeserializeObject<List<tokens>>(json);
                foreach (var token in tokens)
                {
                   myToken = token.token;
                }
            }
        }

        [Test]
        public void Test1()
        {
            string baseURL = "https://platform-security-qa.aamlive.com/Account/LoginClient?token=" + myToken;
            var restClient = new RestClient();
            var request = new RestRequest(baseURL , Method.Get);
            var content = restClient.Execute(request);
            Console.WriteLine(content);
        }
    }
}