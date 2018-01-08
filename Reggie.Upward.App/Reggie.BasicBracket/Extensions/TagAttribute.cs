using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reggie.BasicBracket.Extensions
{
    public class TagAttribute : Attribute
    {
        public string TAG;
        public TagAttribute(string tagName=null)
        {
            if (tagName == null)
            {
                TAG = "";
            }
        }
    }
}
