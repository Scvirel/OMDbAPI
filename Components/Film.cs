using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OMDbAPI.Components
{
    [JsonObject(MemberSerialization = MemberSerialization.OptIn)]
    internal  class Film
    {
        [JsonProperty(PropertyName = "Title")]
        public string Title { get; set; }

        [JsonProperty(PropertyName = "Year")]
        public string Year { get; set; }

        [JsonProperty(PropertyName = "ImdbID")]
        public string ImdbID { get; set; }

        [JsonProperty(PropertyName = "Type")]
        public string Type { get; set; }

        [JsonProperty(PropertyName = "Poster")]
        public string Poster { get; set; }
    }
}
