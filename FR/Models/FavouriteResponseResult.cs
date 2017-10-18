using Newtonsoft.Json;

namespace FR
{
    public class FavouriteResponseResult
    {
        [JsonProperty(PropertyName = "success")]
        public bool Success { get; set; }

        [JsonProperty(PropertyName = "result")]
        public Favourite Result { get; set; }
    }
}
