using Newtonsoft.Json;

namespace Investimento.Domain.Models
{

    public class OutGlobalQuote
    {
        /// <summary>
        /// Global Quote
        /// </summary>
        [JsonProperty("Global Quote")]
        public GlobalQuote GlobalQuote { get; set; }

        /// <summary>
        /// Nota de aviso da API
        /// </summary>
        public string Note { get; set; }
    }

    public class GlobalQuote
    {
        /// <summary>
        /// Símbolo único
        /// </summary>
        [JsonProperty("01. symbol")]
        public string Symbol { get; set; }

        /// <summary>
        /// Preço atual da ação
        /// </summary>
        [JsonProperty("05. price")]
        public double Price { get; set; }
    }

}
