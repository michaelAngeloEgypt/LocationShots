using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using LocationShots.BLL;

namespace LocationShots.Sites
{
    public partial class Redland : UserControl
    {
        public Config.ConfInputs.Redland Inputs => 
         new Config.ConfInputs.Redland()
            {
                //UnitNo = Suburb,
                HouseNo = HouseNo,
                StreetName = StreetName,
            };

        public string HouseNo { get { return txtHouseNo.Text; } set { txtHouseNo.Text = value; } }
        public string StreetName { get { return txtStreetName.Text; } set { txtStreetName.Text = value; } }

        public Redland()
        {
            InitializeComponent();
        }
    }
}
