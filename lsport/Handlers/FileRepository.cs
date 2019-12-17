using lsport.Interfaces;
using lsport.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace lsport.Handlers
{
    public class FileRepository : BaseGiphyRepository
    {
        private readonly IParser<Giphy> _parser;
        private readonly string _path = Path.Combine(Environment.GetFolderPath(
            Environment.SpecialFolder.CommonApplicationData), "data.json");

        public FileRepository(IParser<Giphy> parser)
        {
            _parser = parser;
        }

        private List<Giphy> GetFileContent()
        {
            if (!File.Exists(_path))
            {
                File.Create(_path);
            }
            var json = File.ReadAllText(_path);
            return _parser.ParseString(json);
        }

        private void WriteFileContent(List<Giphy> data)
        {
            var newJson = _parser.ParseToString(data);
            File.WriteAllText(_path, newJson);
        }

        public override ActionResultEnum UpdateInternal(int id, Giphy giphy)
        {

            if (id != giphy.ID)
            {
                return ActionResultEnum.BadRequest;
            }

            var content = GetFileContent();
            var itemFromContent = content.FirstOrDefault(item => item.ID == id);
            if (itemFromContent == null)
            {
                return ActionResultEnum.NotFound;
            }

            itemFromContent.URL = giphy.URL;

            WriteFileContent(content);
            return ActionResultEnum.Success;
        }

        public override async Task<bool> Create(Giphy obj)
        {
            return await Task.Run(() =>
            {
                try
                {
                    var content = GetFileContent();
                    var id = GetNextId(content);
                    obj.ID = id;
                    obj.AddTime = DateTime.Now;
                    obj.UpdateTime = DateTime.Now;

                    content.Add(obj);
                    WriteFileContent(content);
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            });
        }

        private static int GetNextId(IReadOnlyCollection<Giphy> content)
        {
            return content.Count > 0 ? content.Max(x => x.ID) + 1 : 0;
        }

        public override Giphy Retrieve(int id)
        {
            var content = GetFileContent();
            return content.FirstOrDefault(item => item.ID == id);
        }

        public override List<Giphy> RetrieveAll()
        {
            return GetFileContent();
        }

        public override Giphy Delete(int id)
        {
            var content = GetFileContent();
            var ret = content.FirstOrDefault(item => item.ID == id);
            WriteFileContent(content.Where(item => item.ID != id).ToList());
            return ret;
        }
    }
}