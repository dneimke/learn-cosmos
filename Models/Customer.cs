using Newtonsoft.Json;

namespace LearnCosmos.Models
{
    public class Customer
    {
        [JsonProperty("id")]
        public string Id { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("address")]
        public Address Address { get; set; }
        [JsonProperty("kids")]
        public string[] Kids { get; set; }
    }

    public class Address
    {
        [JsonProperty("addressType")]
        public string AddressType { get; set; }
        [JsonProperty("city")]
        public string City { get; set; }
        [JsonProperty("state")]
        public string State { get; set; }
        [JsonProperty("zipCode")]
        public string ZipCode { get; set; }
    }
}
