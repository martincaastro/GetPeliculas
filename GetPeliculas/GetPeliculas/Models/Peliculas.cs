using GetPeliculas.Models;
using Newtonsoft.Json;

namespace GetPeliculas.Models
{
    public class Peliculas
    {
        [JsonProperty("results")]
        public List<Pelicula> Results { get; set; }
    }
}
