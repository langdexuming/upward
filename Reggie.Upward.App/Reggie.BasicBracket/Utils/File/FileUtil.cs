using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace Reggie.BasicBracket.Utils.File
{
    public class FileUtil
    {
        /// <summary>
        /// 创建目录
        /// </summary>
        /// <param name="directoryPath"></param>
        public static void CreateDirectory(string directoryPath)
        {
            if (string.IsNullOrEmpty(directoryPath)) throw new ArgumentNullException(nameof(directoryPath));

            //创建必要的文件夹
            var path = directoryPath;
            if (!Directory.Exists(path))
            {
                try
                {
                    Directory.CreateDirectory(path);
                }
                catch (ArgumentException ex)
                {
                    ////-创建文件的权限不足
                    //throw new UnauthorizedAccessException(ex.Message, ex);
                }
                catch (Exception ex)
                {

                }
            }
        }

        /// <summary>
        /// 拥有程序目录权限（完全控制）
        /// </summary>
        /// <param name="directoryPath"></param>
        public static void SetAccessControl(string directoryPath)
        {
            if (string.IsNullOrEmpty(directoryPath)) throw new ArgumentNullException(nameof(directoryPath));
            DirectoryInfo info = null;
            try
            {
                info = new DirectoryInfo(directoryPath)
                {
                    Attributes = FileAttributes.Normal & FileAttributes.Directory
                };
            }
            catch (ArgumentException ex) //权限不足时，会引发该异常
            {
                throw new UnauthorizedAccessException(ex.Message, ex);
            }

            var security = info.GetAccessControl();
            security.AddAccessRule(new FileSystemAccessRule("Everyone", FileSystemRights.FullControl,
                AccessControlType.Allow));
            security.AddAccessRule(new FileSystemAccessRule("Users", FileSystemRights.FullControl,
                AccessControlType.Allow));
            info.SetAccessControl(security); //权限不足
        }
    }
}
