using System.Threading.Tasks;

namespace lsport.Interfaces
{
    public interface IDownloader
    {
        public Task<string> Download(string url);
    }
}