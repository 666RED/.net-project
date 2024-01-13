using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Net_project
{
    public partial class Report : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("Page_Load executed");
            // Populate the card elements with the fetched data
            MostPopularBookTitle.Text = "Hello world";
            MostPopularBookAuthor.Text = "By me";

            LateBorrowerName.Text = "Testing";
            LateDays.Text = "Testing";

            LeastPopularBookTitle.Text = "Testing";
            LeastPopularBookAuthor.Text = "Testing";
        }
        private string FetchMostPopularBookTitle()
        {
            // Code to fetch the most popular book title from the database
            return "The Most Popular Book";
        }
    }
}