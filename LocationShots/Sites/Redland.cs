using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LocationShots.Sites
{
    public partial class Redland : UserControl
    {
        public string HouseNo { get { return txtHouseNo.Text; } set { txtHouseNo.Text = value; } }
        public string StreetName { get { return txtStreetName.Text; } set { txtStreetName.Text = value; } }

        public Redland()
        {
            InitializeComponent();
        }
    }
}
