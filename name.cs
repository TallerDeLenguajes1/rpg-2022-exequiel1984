using System.Text.Json.Serialization;
    
namespace rpg_2022_exequiel1984
{
    public class ApiOwner
    {
        [JsonPropertyName("author")]
        public string Author { get; set; }

        [JsonPropertyName("cafecito")]
        public string Cafecito { get; set; }

        [JsonPropertyName("instagram")]
        public string Instagram { get; set; }

        [JsonPropertyName("github")]
        public string Github { get; set; }

        [JsonPropertyName("linkedin")]
        public string Linkedin { get; set; }

        [JsonPropertyName("twitter")]
        public string Twitter { get; set; }
    }

    public class Body
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("genre")]
        public string Genre { get; set; }
    }

    public class NombreYGeneroAleatorio
    {
        [JsonPropertyName("api_owner")]
        public ApiOwner ApiOwner { get; set; }

        [JsonPropertyName("body")]
        public Body Body { get; set; }
    }        
}
    
