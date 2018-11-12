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
    public partial class CityPlan : UserControl
    {
        public string Suburb { get { return txtSuburb.Text; } set { txtSuburb.Text = value; } }
        public string Street { get { return txtStreet.Text; } set { txtStreet.Text = value; } }
        public string StreetNo { get { return txtStreetNo.Text; } set { txtStreetNo.Text = value; } }

        public CityPlan()
        {
            InitializeComponent();
        }
    }
}
