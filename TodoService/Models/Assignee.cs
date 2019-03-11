using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace TodoService.Models
{
    public class Assignee
    {
        [JsonProperty("login")]
        public string Responsible { get; set; }
    }
}