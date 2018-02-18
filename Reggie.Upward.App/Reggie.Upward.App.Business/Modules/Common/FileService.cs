using Reggie.Utilities.Extensions;
using Reggie.Utilities.Utils.File;
using Reggie.Utilities.Utils.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reggie.Upward.App.Business.Modules.Common
{
    public class FileService : IFileService
    {
        private const string TAG = nameof(FileService);
        public async Task DownloadFile(string url, string savePath)
        {
            var httpResult = await HttpUtil.DownloadFile(url);
            if (httpResult == null) return;

            FileUtil.Create(savePath, httpResult);
        }

        public async Task<byte[]> DownloadFile(string url, string savePath, bool isSave)
        {
            var httpResult = await HttpUtil.DownloadFile(url);
            if (httpResult == null) return null;

            if (isSave)
            {
                FileUtil.Create(savePath, httpResult);
            }
            return httpResult;
        }
    }
}
