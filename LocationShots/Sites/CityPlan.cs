using LocationShots.BLL;
using System.Windows.Forms;

namespace LocationShots.Sites
{
    public partial class CityPlan : UserControl
    {
        public Config.ConfInputs.CityPlan Inputs =>
            new Config.ConfInputs.CityPlan()
            {
                Suburb = Suburb,
                Street = Street,
                StreetNo = StreetNo,
            };

        public string Suburb { get { return txtSuburb.Text; } set { txtSuburb.Text = value; } }
        public string Street { get { return txtStreet.Text; } set { txtStreet.Text = value; } }
        public string StreetNo { get { return txtStreetNo.Text; } set { txtStreetNo.Text = value; } }

        public CityPlan()
        {
            InitializeComponent();
        }
    }
}
