using NUnit.Framework;
using Reggie.Utilities.Utils.File;
using Reggie.Utilities.Utils.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Reggie.Utilities.Utils.Http.Tests
{
    [TestFixture()]
    public class HttpUtilTests
    {
        [SetUp]
        public void Init()
        {
            //创建下载目录
            FileUtil.CreateDirectory(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Download"));
        }

        [Test()]
        public void RequestTest()
        {
            var result = HttpUtil.Request("http://langdexuming.xin/api/v1/values");
            Assert.True(result.StatusCode == System.Net.HttpStatusCode.OK);
        }

        [Test()]
        public void DownloadFileTest()
        {
            var downloadUrl = "http://langdexuming.xin/opensource/OpenSourceProject/Tinyhttpd.tar";
            var result = HttpUtil.DownloadFile(downloadUrl);
            Assert.True(result != null && result.Length > 0);

            FileUtil.Create(Path.Combine(AppDomain.CurrentDomain.BaseDirectory,"Download",System.IO.Path.GetFileName(downloadUrl)), result);
        }
    }
}