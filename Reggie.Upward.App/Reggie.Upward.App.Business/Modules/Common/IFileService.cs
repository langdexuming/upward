using System.Threading.Tasks;

namespace Reggie.Upward.App.Business.Modules.Common
{
    public interface IFileService
    {
        Task DownloadFile(string url, string savePath);

        Task<byte[]> DownloadFile(string url, string savePath, bool isSave);
    }
}
