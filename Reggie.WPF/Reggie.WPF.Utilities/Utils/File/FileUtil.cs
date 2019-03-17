/****************************************************************
*   作者：yinruimin 5339
*   创建时间：2018/4/26 14:03:38
*   描述说明：File工具
*
* Copyright (c) 2018 yinruimin Corporation. All rights reserved.
*┌──────────────────────────────────┐
*│　此技术信息为本人机密信息，未经本人书面同意禁止向第三方披露．　│
*│　版权所有：yinruimin　　　　　　　　　　　　      │
*└──────────────────────────────────┘
*****************************************************************/
using Reggie.WPF.Utilities.Extensions;
using System;
using System.IO;
using System.Linq;
using System.Security.AccessControl;

namespace Reggie.WPF.Utilities.Utils.File
{
    public class FileUtil
    {
        private const string TAG = nameof(FileUtil);

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
                    Logger.Warn(TAG, ex);
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

        /// <summary>
        /// 创建具有内容的文件
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="content"></param>
        public static void Create(string filePath, byte[] content = null)
        {
            FileStream fs = null;
            try
            {
                fs = System.IO.File.Create(filePath);
                if (content != null)
                {
                    fs.Write(content, 0, content.Count());
                    fs.Flush();
                }
            }
            catch (Exception ex)
            {
                Logger.Error(TAG, ex);
            }
            finally
            {
                fs?.Close();
            }
        }

        /// <summary>
        /// 读取具有内容的文件并关闭
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="content"></param>
        public static bool Read(string filePath,out string content)
        {
            bool result = false;

            try
            {
                content = System.IO.File.ReadAllText(filePath);
                result = true;
            }
            catch (Exception ex)
            {
                Logger.Error(TAG, ex);
                content = string.Empty;
            }
            finally
            {
              
            }

            return result;
        }
    }
}
