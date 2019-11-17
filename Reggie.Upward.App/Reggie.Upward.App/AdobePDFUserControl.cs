using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Reggie.Upward.App
{
    public partial class AdobePDFUserControl : UserControl
    {
        public AdobePDFUserControl()
        {
            InitializeComponent();

            this.axAcroPDF1.LoadFile("d:/044001800111_82056336.pdf");
        }
    }
}
