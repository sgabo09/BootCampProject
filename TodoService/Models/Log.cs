using System;
using System.ComponentModel.DataAnnotations;

namespace TodoService.Models
{
    public class Log
    {
        [Key]
        public Guid Id { get; set; }
        public string Method { get; set; }
        public string Uri { get; set; }
        public DateTime RequestTime { get; set; }
        public string Header { get; set; }
        public string Body { get; set; }
        public string Query { get; set; }
    }
}