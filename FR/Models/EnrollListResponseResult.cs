using Newtonsoft.Json;
using System.Collections.ObjectModel;

namespace FR
{
    public class EnrollListResponseResult
    {
        [JsonProperty(PropertyName = "total_page")]
        public int TotalPage { get; set; }

        [JsonProperty(PropertyName = "result")]
        public ObservableCollection<Enroll> Enrolls { get; set; }
    }
}
