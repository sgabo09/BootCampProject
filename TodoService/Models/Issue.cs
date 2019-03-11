using System;
using System.IO;
using Newtonsoft.Json;

namespace TodoService.Models
{
    public class Issue
    {
        [JsonProperty("id")]
        public int Id { get; set; }
        [JsonProperty("created_at")]
        public DateTime CreationDate { get; set; }
        [JsonProperty("updated_at")]
        public DateTime LastModified { get; set; }
        [JsonProperty("title")]
        public string Name { get; set; }
        [JsonProperty("state")]
        public string Status { get; set; }
        [JsonProperty("body")]
        public string Description { get; set; }
        [JsonProperty("assignee")]
        public Assignee Assignee { get; set; }
    }
}