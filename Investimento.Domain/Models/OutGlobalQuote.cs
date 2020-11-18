using Newtonsoft.Json;

namespace Investimento.Domain.Models
{

    public class OutGlobalQuote
    {
        [JsonProperty("Global Quote")]
        public GlobalQuote GlobalQuote { get; set; }
    }

    public class GlobalQuote
    {
        [JsonProperty("01. symbol")]
        public string Symbol { get; set; }

        [JsonProperty("05. price")]
        public double Price { get; set; }
    }

}
