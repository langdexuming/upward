using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Reggie.BasicBracket.Utils.Algorithm
{
    public static class HashUtil
    {
        /// <summary>
        /// 生成哈希值
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static byte[] ComputeHashValue(string source)
        {
            //生成哈希值
            var UE = new UnicodeEncoding();
            var SHhash = new SHA1Managed();

            //加入哈希集合
            var reviewBytes = UE.GetBytes(source);
            var hashValue = SHhash.ComputeHash(reviewBytes);

            return hashValue;
        }

        /// <summary>
        ///     比较哈希值
        /// </summary>
        /// <returns></returns>
        public static bool CompareHash(byte[] _old, byte[] _new)
        {
            var Same = true;

            if (_old.Length != _new.Length)
                Same = false;
            else
                for (var x = 0; x < _old.Length; x++)
                    if (_old[x] != _new[x])
                        Same = false;
            return Same;
        }
    }
}
