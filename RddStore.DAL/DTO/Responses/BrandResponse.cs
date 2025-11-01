using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace RddStore.DAL.DTO.Responses
{
    public class BrandResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        [JsonIgnore]
        public string Image { get; set; }
        public string mainImageUrl => $"https://localhost:7042/images/{Image}";
    }
}
