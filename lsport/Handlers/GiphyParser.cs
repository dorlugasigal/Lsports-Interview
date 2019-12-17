using System.Collections.Generic;
using lsport.Interfaces;
using lsport.Models;
using Newtonsoft.Json;

namespace lsport.Handlers
{
    public class GiphyParser : IParser<Giphy>
    {
        public Giphy ParseResult(string data)
        {
            var converted = JsonConvert.DeserializeObject(data);

            return new Giphy()
            {
                URL = converted.ToString().Split("url")[1].Split("\"")[2]
            };
        }

        public List<Giphy> ParseString(string data)
        {
            var converted = JsonConvert.DeserializeObject<List<Giphy>>(data);
            return converted ??= new List<Giphy>();
        }

        public string ParseToString(List<Giphy> giphy)
        {
            return JsonConvert.SerializeObject(giphy);
        }
    }
}