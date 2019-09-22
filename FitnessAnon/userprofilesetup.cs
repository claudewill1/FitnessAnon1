using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using FitnessApp;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System.Collections.Specialized;
//Written by Claude J Will
//September 7, 2019
namespace FitnessAnon
{
    [Activity(Label = "userprofilesetup")]
    public class userprofilesetup : Activity
    {
        
        EditText txtFirstname;
        EditText txtLastname;
        EditText txtAge;
        EditText txtHeight;
        
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.userSetup);
            
            
            
            Button submit = (Button)FindViewById(Resource.Id.btnSubmit);
            submit.Click += Submit_Click;

            // Create your application here
        }

        private void Submit_Click(object sender, EventArgs e)
        {
            try
            {
                string userID = SharedPrefManager.getInstance(Application.Context).UserID;
                txtFirstname = (EditText)FindViewById(Resource.Id.firstName);
                txtLastname = (EditText)FindViewById(Resource.Id.lastname);
                txtAge = (EditText)FindViewById(Resource.Id.age);
                txtHeight = (EditText)FindViewById(Resource.Id.height);

                CreateUserProfile(userID,txtFirstname.Text, txtLastname.Text, txtAge.Text, txtHeight.Text);

            }
            catch (Exception ex)
            {
                Toast.MakeText(this, ex.Message, ToastLength.Long).Show();
            }
            
        }
        private void CreateUserProfile(string userid, string firstname, string lastname, string age, string heightInInches)
        {
            try
            {

                

                WebClient webClient = new WebClient();
                Uri uri = new Uri("http://72.198.242.232:8080/Android/FitnessAppv1/modifyUser.php");

                NameValueCollection parmeters = new NameValueCollection();
                parmeters.Add("userid", userid);
                parmeters.Add("firstname", firstname);
                parmeters.Add("lastname", lastname);
                parmeters.Add("age", age);
                parmeters.Add("height", heightInInches);


                webClient.UploadValuesCompleted += WebClient_UploadValuesCompleted;
                webClient.UploadValuesAsync(uri, parmeters);

                
            }
            catch (Exception ex)
            {
                Toast.MakeText(this, ex.Message, ToastLength.Long).Show();
            }
        }

        private void WebClient_UploadValuesCompleted(object sender, UploadValuesCompletedEventArgs e)
        {
            string message = Encoding.UTF8.GetString(e.Result);
            Toast.MakeText(this, message, ToastLength.Long).Show();
            if(message.Contains("Successfully changed information."))
            {
                SharedPrefManager.getInstance(Application.Context).UserProfileSetup(txtLastname.Text,txtFirstname.Text, txtAge.Text, txtHeight.Text);
                Intent home = new Intent(this, typeof(home));
                StartActivity(home);
                Finish();
            }
        }
    }
}