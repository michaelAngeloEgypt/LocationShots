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
    public partial class CityPlan : UserControl
    {
        public Config.ConfInputs.CityPlan Inputs { get; set; }

        public string Suburb { get { return txtSuburb.Text; } set { txtSuburb.Text = value; } }
        public string Street { get { return txtStreet.Text; } set { txtStreet.Text = value; } }
        public string StreetNo { get { return txtStreetNo.Text; } set { txtStreetNo.Text = value; } }

        public CityPlan()
        {
            InitializeComponent();
            Inputs = new Config.ConfInputs.CityPlan()
            {
                Suburb = Suburb,
                Street = Street,
                StreetNo = StreetNo,
            };
        }
    }
}
