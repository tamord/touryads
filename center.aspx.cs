using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TouryAds
{
    public partial class center : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                bool query;

                query = init_query(sender, e);

                if (query == false)
                {
                    init_countries();
                    init_location();
                    init_related();
                }


            }
        }

        public bool init_query(object sender, EventArgs e)
        {

            bool query_exist = false;


            if (Request.QueryString["createad"] != null)
            {
                init_countries();
                init_location();
                init_related();
                create_ad_click(sender, e);
                query_exist = true;
            }

            if (Request.QueryString["affiliates"] != null)
            {
                init_countries();
                init_location();
                init_related();

                aff_click(sender, e);
                query_exist = true;
            }

            if (Request.QueryString["publishers"] != null)
            {
                init_countries();
                init_location();
                init_related();
                monetize_click(sender, e);
                query_exist = true;
            }

            if (Request.QueryString["bloggers"] != null)
            {
                init_countries();
                init_location();
                init_related();
                monetize2_click(sender, e);
                query_exist = true;
            }

            if (Request.QueryString["city"] != null)
            {
                string city = Request.QueryString["city"];
                string cityval = city2val(city);
                string countryval = find_country_by_city_val(cityval);

                init_countries();                
                change_country(countryval);                
                change_city(cityval);

                DropDownCities.Focus();

                //change_selected_city(cityval);                


                //city_change(sender, e);
                query_exist = true;
            }


            return query_exist;
        }


        public string city2val(string city)
        {

            TouryAds.CountriesDataContext db = new TouryAds.CountriesDataContext();

            var the_city = (from t in db.Table_TouryAds_Cities
                            where (t.city.Equals(city))
                            select t).FirstOrDefault();

            if (the_city == null)
            {
                return "144";
            }

            string val = the_city.id.ToString();

            return val;
        }


        public void init_countries()
        {
            if (DropDownCountries.Items.Count == 1)
            {

                DropDownCountries.Items.Clear();
                ListItem item3 = new ListItem("Select Country");
                DropDownCountries.Items.Add(item3);

                TouryAds.CountriesDataContext db4 = new TouryAds.CountriesDataContext();                

                var countries = (from t in db4.Table_TouryAds_Countries
                                 orderby t.country
                                 select t.country);

                foreach (var country in countries)
                {
                    ListItem item2 = new ListItem(country);
                    DropDownCountries.Items.Add(item2);
                }
            }
        }
        public void init_loc()
        {
            TouryAds.CountriesDataContext db = new TouryAds.CountriesDataContext();            

            string country = DropDownCountries.SelectedItem.Text;
            string city = DropDownCities.SelectedItem.Text;

            if (country.Equals("Select Country"))
            {
                Label411.Text = "Welcome to Toury Deals, where you can find travel deals, travel forums, and travel blogs. Use this information wisely and you can find great travel deals!";
                location_image.ImageUrl = "https://www.tourist-ads.com/maintravel.jpg";
                //init_related();
                return;
            }


            var the_country = (from t in db.Table_TouryAds_Countries
                               where (t.country.Equals(country))
                               select t).FirstOrDefault();

            if (the_country == null)
            {
                wrong19.Visible = true;
                return;
            }


            if (city.Equals("Select City"))
            {
                string gray_circle = the_country.graylogo;

                Label411.Text = "Welcome to Toury Deals CAT, where you can find CAT travel deals, CAT travel forums, and CAT travel blogs. Use this information wisely and you can find great travel deals!";
                Label411.Text = Label411.Text.Replace("CAT", country);

                string gray_circle5 = the_country.graylogo;
                location_image.ImageUrl = gray_circle5;
                return;
            }




               var the_city = (from t in db.Table_TouryAds_Cities
                            where (t.city.Equals(city))
                            select t).FirstOrDefault();

            if (the_city == null)
            {
                wrong19.Visible = true;
                return;
            }

            string dest = city + ", " + country;

            Label411.Text = "Welcome to Toury Deals CAT, where you can find CIT travel deals, CIT travel forums, and CIT travel blogs. Use this information wisely and you can find great travel deals!";
            Label411.Text = Label411.Text.Replace("CAT", dest);
            Label411.Text = Label411.Text.Replace("CIT", city);

            Label413.Text = "Create an Ad and Promote your local tourist attraction!";
            Label414.Text = "Monetize your local travel traffic from your website or your blog!";
            Label416.Text = "Become our Affiliate and Earn Great Commissions by inviting local publishers!";
            Label417.Text = "Create an Ad, Promote your local Tourist Attraction!";

            Label421.Text = "Create an Ad and promote your local tourist attraction! We will optimize your ad to show on your destination - It can be  your city, state or country or any other destination you will choose!";
            Label423.Text = "Monetize your local travel traffic from your website or blog and be rewarded. We will show relevant ads based on your chosen destination, It can be: Your Website City,  State or  Country!";
            Label425.Text = "Monetize your local travel traffic from your blog and be rewarded. We will show relevant ads based on your chosen destination, It can be: Your Blog's City,  State or  Country!";
            Label427.Text = "Become our Affiliate and Earn Great Commissions by inviting local travel businesses, publishers and bloggers, we will pay weekly!";

            Label422.Text = "Monetize your local Travel Traffic";
            Label424.Text = "Monetize your local Travel Traffic";

            Label413.Text = Label413.Text.Replace("local", dest);
            Label414.Text = Label414.Text.Replace("local", dest);
            Label416.Text = Label416.Text.Replace("local", dest);
            Label417.Text = Label417.Text.Replace("local", dest);
            Label421.Text = Label421.Text.Replace("local", dest);
            Label423.Text = Label423.Text.Replace("local", dest);
            Label425.Text = Label425.Text.Replace("local", dest);

            Label422.Text = Label422.Text.Replace("local", dest);
            Label424.Text = Label424.Text.Replace("local", dest);
            Label427.Text = Label427.Text.Replace("local", dest);

            string gray_circle6 = the_city.graylogo;
            location_image.ImageUrl = gray_circle6;
            //DropDownVertical.Focus();

            //init_related();

        }


        public void init_location()
        {
            wrong19.Visible = false;
            TouryAds.CountriesDataContext db = new TouryAds.CountriesDataContext();            

            if (DropDownCountries.Items.Count == 0)
            {
                ListItem item = new ListItem("Select Country", "0");
                DropDownCountries.Items.Add(item);


                var the_ver = (from t in db.Table_TouryAds_Countries
                               orderby t.country ascending
                               select t);

                if (the_ver == null)
                {
                    wrong19.Visible = true;
                    return;
                }

                foreach (var cat in the_ver)
                {
                    ListItem item2 = new ListItem(cat.country, cat.id.ToString());
                    DropDownCountries.Items.Add(item2);

                }

            }

            if (DropDownCities.Items.Count == 0)
            {
                ListItem item = new ListItem("Select City", "0");
                DropDownCities.Items.Add(item);
            }

            string country = DropDownCountries.SelectedItem.Text;

            if (!(country.Equals("Select Country")))
            {

                DropDownCities.Items.Clear();
                ListItem item = new ListItem("Select City", "0");
                DropDownCities.Items.Add(item);


                var the_cities = (from t in db.Table_TouryAds_Cities
                                  where t.country.Equals(country)
                                  orderby t.city ascending
                                  select t);

                if (the_cities != null)
                {


                    foreach (var cit in the_cities)
                    {
                        ListItem item2 = new ListItem(cit.city, cit.id.ToString());
                        DropDownCities.Items.Add(item2);
                    }

                }

            }

            init_loc();
        }


        protected void country_change(object sender, EventArgs e)
        {
            string country = DropDownCountries.SelectedItem.Text;

            DropDownCities.Items.Clear();

            init_location();

            TouryAds.CountriesDataContext db = new TouryAds.CountriesDataContext();           


            var res = (from t in db.Table_TouryAds_Cities
                       where ((t.country.Equals(country)))
                       orderby t.rank descending
                       select t).FirstOrDefault();

            string val;

            bool sel = false;

            if (res == null)
            {
                val = "144";  // new york city
            }

            else if (country.Equals("Select Country"))
            {
                val = "144";   // new york city

            }

            else
            {
                val = res.id.ToString();
            }


            change_city(val);

            //init_related();            

            string country_flag = extract_flag(country);

            if (country_flag.Equals(""))
            {
                location_image.ImageUrl = "https://www.tourist-ads.com/maintravel.jpg";
            }

            else
            {
                location_image.ImageUrl = country_flag;
            }

            //feedbutton1.Focus();
            init_related();

            my_focus();
            
            //DropDownCountries.Focus();
        }

        public void my_focus()
        {
            if (MultiView1.ActiveViewIndex == 0)
            {
                createad.Focus();
            }
            else if (MultiView1.ActiveViewIndex == 1)
            {
                advertisers2.Focus();
            }
            else if(MultiView1.ActiveViewIndex == 2)
            {
                publishers3.Focus();
            }

            else if (MultiView1.ActiveViewIndex == 3)
            {
                blogers3.Focus();
            }

            else if (MultiView1.ActiveViewIndex == 4)
            {
                affiliates4.Focus();
            }            


        }

        public string find_country(string val)
        {
            TouryAds.CountriesDataContext db = new TouryAds.CountriesDataContext();            

            var city = (from t in db.Table_TouryAds_Cities
                        where t.id.ToString().Equals(val)
                        select t).FirstOrDefault();

            if (city == null)
                return "";

            return city.country;


        }

        public string find_country_val(string val)
        {
            TouryAds.CountriesDataContext db = new TouryAds.CountriesDataContext();

            var country = (from t in db.Table_TouryAds_Countries
                           where t.id.ToString().Equals(val)
                           select t).FirstOrDefault();

            if (country == null)
                return "42";

            return country.id.ToString();


        }

        public string find_country_by_city_val(string val)
        {
            TouryAds.CountriesDataContext db = new TouryAds.CountriesDataContext();

            var city = (from t in db.Table_TouryAds_Cities
                        where t.id.ToString().Equals(val)
                        select t).FirstOrDefault();

            if (city == null)
                return "42";

            var country = find_country(city.id.ToString());

            var mycountry = (from t in db.Table_TouryAds_Countries
                        where t.country.Equals(country)
                        select t).FirstOrDefault();

            if (mycountry == null)
                return "42";

            return mycountry.id.ToString();


        }


        

        public string find_country_by_val(string val)
        {
            TouryAds.CountriesDataContext db = new TouryAds.CountriesDataContext();

            var country = (from t in db.Table_TouryAds_Countries
                        where t.id.ToString().Equals(val)
                        select t).FirstOrDefault();

            if (country == null)
                return "";

            return country.country;


        }

        public void change_country(string val)
        {

            if (val.Equals("0"))
            {
                return;
            }

            string country = find_country_by_val(val);

            TouryAds.CountriesDataContext db = new TouryAds.CountriesDataContext();

            if (DropDownCountries.SelectedItem.Text.Equals("Select Country"))
            {
                foreach (ListItem li in DropDownCountries.Items)
                {
                    if (li.Text.Equals(country))
                    {
                        DropDownCountries.SelectedValue = li.Value;
                    }

                }

                var the_cities2 = (from t in db.Table_TouryAds_Cities
                                   where t.country.Equals(country)
                                   orderby t.city ascending
                                   select t);

                if (the_cities2 != null)
                {
                    DropDownCities.Items.Clear();

                    foreach (var cit in the_cities2)
                    {
                        ListItem item2 = new ListItem(cit.city, cit.id.ToString());
                        DropDownCities.Items.Add(item2);
                    }

                }

            }

            /*

            string the_city = (from t in db.Table_TouryAds_Cities
                               where (t.id.ToString().Equals(val))
                               orderby t.city ascending
                               select t.city).FirstOrDefault();

            if (the_city == null)
            {
                return;
            }


            foreach (ListItem li in DropDownCities.Items)
            {
                if (li.Text.Equals(the_city))
                {
                    DropDownCities.SelectedValue = li.Value;
                }

            }

            */
            //city_change(this, null);
            

        }

        public void change_selected_city(string cityval)
        {
            TouryAds.CountriesDataContext db = new TouryAds.CountriesDataContext();

            var the_city = (from t in db.Table_TouryAds_Cities
                               where (t.id.ToString().Equals(cityval))
                               orderby t.city ascending
                               select t).FirstOrDefault();

            if (the_city == null)
            {
                return;
            }

            
            if (the_city != null)
            {                

                foreach (ListItem li in DropDownCities.Items)
                {
                    if (li.Text.Equals(the_city.city))
                    {
                        DropDownCities.SelectedValue = li.Value;
                    }

                }
            }

        }


        public void change_city(string val)
        {

            if (val.Equals("0"))
            {
                return;
            }
            

            string country = find_country(val);

            TouryAds.CountriesDataContext db = new TouryAds.CountriesDataContext();            

            if (DropDownCountries.SelectedItem.Text.Equals("Select Country"))
            {
                foreach (ListItem li in DropDownCountries.Items)
                {
                    if (li.Text.Equals(country))
                    {
                        DropDownCountries.SelectedValue = li.Value;
                    }

                }

                var the_cities2 = (from t in db.Table_TouryAds_Cities
                                   where t.country.Equals(country)
                                   orderby t.city ascending
                                   select t);

                if (the_cities2 != null)
                {


                    foreach (var cit in the_cities2)
                    {
                        ListItem item2 = new ListItem(cit.city, cit.id.ToString());
                        DropDownCities.Items.Add(item2);
                    }

                }

            }

            string the_city = (from t in db.Table_TouryAds_Cities
                               where (t.id.ToString().Equals(val))
                               orderby t.city ascending
                               select t.city).FirstOrDefault();

            if (the_city == null)
            {
                return;
            }


            foreach (ListItem li in DropDownCities.Items)
            {
                if (li.Text.Equals(the_city))
                {
                    DropDownCities.SelectedValue = li.Value;
                }

            }

            city_change(this, null);


        }


        protected void city_change(object sender, EventArgs e)
        {
            init_loc();
            init_related();
            my_focus();
        }

        public string extract_flag(string country)
        {
            TouryAds.CountriesDataContext db = new TouryAds.CountriesDataContext();            

            var res = (from t in db.Table_TouryAds_Countries
                       where ((t.country.Equals(country)))
                       select t).FirstOrDefault();

            if (res == null)
                return "";

            return res.graylogo;
        }

        public void reset_related()
        {

            RelDest1.ImageUrl = "";
            destlink1.Text = "";
            HiddenDest1.Value = "0";
            RelDest1.BorderWidth = 0;
            RelDest1.Visible = false;


            RelDest2.ImageUrl = "";
            destlink2.Text = "";
            HiddenDest2.Value = "0";
            RelDest2.BorderWidth = 0;
            RelDest2.Visible = false;

            RelDest3.ImageUrl = "";
            destlink3.Text = "";
            HiddenDest3.Value = "0";
            RelDest3.BorderWidth = 0;
            RelDest3.Visible = false;

            RelDest4.ImageUrl = "";
            destlink4.Text = "";
            HiddenDest4.Value = "0";
            RelDest4.BorderWidth = 0;
            RelDest4.Visible = false;

            RelDest5.ImageUrl = "";
            destlink5.Text = "";
            HiddenDest5.Value = "0";
            RelDest5.BorderWidth = 0;
            RelDest5.Visible = false;


        }
        public void init_related()
        {
            TouryAds.CountriesDataContext db = new TouryAds.CountriesDataContext();            

            string dest = DropDownCountries.SelectedItem.Text;

            if (dest.Equals("Select Country"))
            {
                //dest = "United States";                
            }

            var the_cities = (from t in db.Table_TouryAds_Cities
                              where ((t.country.Equals(dest)) || (dest.Equals("Select Country")))
                              orderby t.rank descending
                              select t).Take(5);


            /*var the_cities = (from t in db.Table_MyTouristbook_Cities
                              where t.country.Equals(dest)
                              orderby t.rank descending
                              select t).Take(5);
                              */

            int counter = 1;

            reset_related();

            foreach (var city in the_cities)
            {
                //affsbook.Table_Affsbook_Affiliate aff = get_affiliate((int)tab2.authoraid);

                if (counter == 1)
                {
                    RelDest3.ImageUrl = city.icon;
                    RelDest3.Width = 175;
                    RelDest3.Height = 150;
                    RelDest3.BorderWidth = 2;
                    destlink3.Text = city.city;
                    HiddenDest3.Value = city.id.ToString();
                    RelDest3.Visible = true;
                }

                if (counter == 2)
                {
                    RelDest2.ImageUrl = city.icon;
                    RelDest2.Width = 175;
                    RelDest2.Height = 150;
                    RelDest2.BorderWidth = 2;
                    destlink2.Text = city.city;
                    HiddenDest2.Value = city.id.ToString();
                    RelDest2.Visible = true;
                }

                if (counter == 3)
                {
                    RelDest4.ImageUrl = city.icon;
                    RelDest4.Width = 175;
                    RelDest4.Height = 150;
                    RelDest4.BorderWidth = 2;
                    destlink4.Text = city.city;
                    HiddenDest4.Value = city.id.ToString();
                    RelDest4.Visible = true;
                }

                if (counter == 4)
                {
                    RelDest1.ImageUrl = city.icon;
                    RelDest1.Width = 175;
                    RelDest1.Height = 150;
                    RelDest1.BorderWidth = 2;
                    destlink1.Text = city.city;
                    HiddenDest1.Value = city.id.ToString();
                    RelDest1.Visible = true;
                }

                if (counter == 5)
                {
                    RelDest5.ImageUrl = city.icon;
                    RelDest5.Width = 175;
                    RelDest5.Height = 150;
                    RelDest5.BorderWidth = 2;
                    destlink5.Text = city.city;
                    HiddenDest5.Value = city.id.ToString();
                    RelDest5.Visible = true;
                }

                counter++;
            }


        }

        protected void RDest1_Click(object sender, ImageClickEventArgs e)
        {
            string val = HiddenDest1.Value;

            change_city(val);
        }

        protected void RDest2_Click(object sender, ImageClickEventArgs e)
        {
            string val = HiddenDest2.Value;

            change_city(val);
        }

        protected void RDest3_Click(object sender, ImageClickEventArgs e)
        {
            string val = HiddenDest3.Value;

            change_city(val);
        }

        protected void RDest4_Click(object sender, ImageClickEventArgs e)
        {
            string val = HiddenDest4.Value;

            change_city(val);
        }

        protected void RDest5_Click(object sender, ImageClickEventArgs e)
        {
            string val = HiddenDest5.Value;

            change_city(val);
        }

        protected void create_ad_click(object sender, EventArgs e)
        {
            MultiView1.ActiveViewIndex = 1;
            advertisers2.Focus();
        }

        protected void monetize_click(object sender, EventArgs e)
        {
            MultiView1.ActiveViewIndex = 2;
            publishers3.Focus();
            //blogers3.Focus();
        }

        protected void monetize2_click(object sender, EventArgs e)
        {
            MultiView1.ActiveViewIndex = 3;
            blogers3.Focus();
            //blogers3.Focus();
        }

        protected void aff_click(object sender, EventArgs e)
        {
            MultiView1.ActiveViewIndex = 4;
            affiliates4.Focus();
        }
    }
}