using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HackerNews.Models
{
    class OutputItem
    {
        [JsonProperty(PropertyName = "title")]
        public string Title { get; set; }
        [JsonProperty(PropertyName = "uri")]
        public string Uri { get; set; }
        [JsonProperty(PropertyName = "author")]
        public string Author { get; set; }
        [JsonProperty(PropertyName = "points")]
        public int Points { get; set; }
        [JsonProperty(PropertyName = "comments")]
        public int Comments { get; set; }
        [JsonProperty(PropertyName = "rank")]
        public int Rank { get; set; }
    }
}
