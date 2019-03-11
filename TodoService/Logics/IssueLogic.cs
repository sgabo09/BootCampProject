using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Net;
using Newtonsoft.Json;
using TodoService.Models;

namespace TodoService.Logics
{
    public class IssueLogic
    {
        public List<Issue> GetIssuesFromGitRepository()
        {
            var request = (HttpWebRequest)WebRequest.Create(ConfigurationManager.AppSettings["IssuesGitRepository"]);
            request.UserAgent = ConfigurationManager.AppSettings["GitUserAgent"];
            var response = (HttpWebResponse)request.GetResponse();

            string json;
            using (var stream = new StreamReader(response.GetResponseStream()))
            {
                json = stream.ReadToEnd();
            }

            return JsonConvert.DeserializeObject<List<Issue>>(json);
        }
    }
}