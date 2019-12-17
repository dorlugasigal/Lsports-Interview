using System.Collections.Generic;

namespace lsport.Interfaces
{
    public interface IParser<T>
    {
        public T ParseResult(string data);

        public List<T> ParseString(string data);

        public string ParseToString(List<T> giphy);

    }
}