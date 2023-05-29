using Newtonsoft.Json;

namespace GetPeliculas.Models
{
    public class Pelicula
    {
        [JsonProperty("id")]
        public int Id;

        [JsonProperty("title")]
        public string Titulo { get; set; }

        [JsonProperty("original_title")]
        public string TituloOriginal { get; set; }

        [JsonProperty("vote_average")]
        public decimal PuntuacionMedia { get; set; }

        [JsonProperty("release_date")]
        public string FechaEstreno { get; set; }

        [JsonProperty("overview")]
        public string Descripcion { get; set; }

        public List<string> ListaPeliculasSimilares { get; set; }
    }
}
