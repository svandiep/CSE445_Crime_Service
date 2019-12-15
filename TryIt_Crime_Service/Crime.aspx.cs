using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

// Try it page for testing Crime Web Service

namespace TryIt_Crime_Service
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        public string City;
        public string Zip;
        
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            City = GetCity();
            CityLabel.Text = City.ToString();
        }

        private string GetCity()
        {
            string city = CityBox.Text;
            string state = StateBox.Text;
            var client = new WebClient();//create new webclient
            var response = client.DownloadString("http://webstrar10.fulton.asu.edu/page4/Service1.svc/crimeCity?city=" + city + "&state=" + state);
            return response;//return string from webservice
        }

        protected void ZipButton_Click(object sender, EventArgs e)
        {
            Zip = GetZip();
            ZipLabel.Text = Zip.ToString();

        }

        private string GetZip()
        {
            int zip = Convert.ToInt32(ZipBox.Text);
            var client = new WebClient();//create new webclient
            var response = client.DownloadString("http://webstrar10.fulton.asu.edu/page4/Service1.svc/crimeZip?zipcode=" + zip);
            return response;//return string from webservice
        }

        protected void HomeButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("http://webstrar10.fulton.asu.edu/page0/");
        }
    }
}