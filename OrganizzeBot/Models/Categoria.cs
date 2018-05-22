using Newtonsoft.Json;
using System;

namespace OrganizzeBot.Models
{

    public class Categoria
    {
        [JsonProperty("id")]
        public int Id { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("color")]
        public string Color { get; set; }
        [JsonProperty("parent_id")]
        public int Parent_id { get; set; }
    }

}