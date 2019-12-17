using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using lsport.Interfaces;
using lsport.Models;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Configuration;

namespace lsport.Handlers
{
    public class Manager : IManager
    {
        private readonly IConfiguration _config;
        private readonly IDownloader _downloader;
        private readonly IParser<Giphy> _parser;
        private readonly IRepository<Giphy> _repository;
        public Manager(IDownloader downloader, IParser<Giphy> parser, IRepository<Giphy> repository, IConfiguration config)
        {
            _downloader = downloader;
            _parser = parser;
            _repository = repository;
            _config = config;
        }
        public async Task<Giphy> SearchOne(string context)
        {
            var url = new GiphyUrlCreator(_config).CreateSearchUrl(context);
            var res = await _downloader.Download(url);
            return await Task.Run(() => _parser.ParseResult(res));
        }

        public class GiphyUrlCreator
        {
            private readonly IConfiguration _config;
            private static string _key;
            public GiphyUrlCreator(IConfiguration config)
            {
                _config = config;
                _key = _config.GetValue<string>("ApiKey");
            }
            public string CreateSearchUrl(string search)
            {
                var sb = new StringBuilder();
                sb.Append("http://api.giphy.com/v1/gifs/search?q=");
                sb.Append(search);
                sb.Append($"&api_key={_key}");
                sb.Append("&limit=1");
                return sb.ToString();
            }
        }

        public async Task<Giphy> Add(string name)
        {
            var item = await SearchOne(name);
            await _repository.Create(item);
            return item;
        }

        public List<Giphy> GetAll()
        {
            return _repository.RetrieveAll();
        }

        public Giphy Get(int id)
        {
            return _repository.Retrieve(id);
        }

        public Giphy Delete(int id)
        {
            return _repository.Delete(id);
        }

        public async Task<ActionResultEnum> Update(int id, string giphy)
        {
            var item = await SearchOne(giphy);
            return _repository.Update(id, item);
        }
    }
}