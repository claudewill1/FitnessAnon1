using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net;
using System.Text;
using FitnessApp;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;

namespace FitnessAnon
{

    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]

    class UserLogin : AppCompatActivity
    {
        
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            // Set our view from the "main" layout resource
            SharedPrefManager sp = new SharedPrefManager(this);
            SetContentView(Resource.Layout.UserLogin);
            Button btnRegister = (Button)FindViewById(Resource.Id.Register);
            btnRegister.Click += BtnRegister_Click;
            Button btnLogin = (Button)FindViewById(Resource.Id.Login);
            btnLogin.Click += BtnLogin_Click;

            if(SharedPrefManager.getInstance(Application.Context).LoggedIn)
            {
                GetUserInfo(SharedPrefManager.getInstance(Application.Context).UserID);
                
            }

            
        }

        private void BtnLogin_Click(object sender, EventArgs e)
        {
            try
            {

                EditText txtUsername = (EditText)FindViewById(Resource.Id.username);
                EditText txtPassword = (EditText)FindViewById(Resource.Id.password);

                LoginUser(txtUsername.Text, txtPassword.Text);
                
            }
            catch(Exception ex)
            {
                Toast.MakeText(this, ex.Message, ToastLength.Long).Show();
            }
        }

        private void BtnRegister_Click(object sender, EventArgs e)
        {
            Intent register = new Intent(this, typeof(UserRegister));
            StartActivity(register);
            Finish();

        }


        private void LoginUser(string username, string password)
        {
            try
            {



                WebClient webClient = new WebClient();
                Uri uri = new Uri("http://72.198.243.77:8080/Android/FitnessAppv1/userLogin.php");

                NameValueCollection parmeters = new NameValueCollection();

                parmeters.Add("username", username);
                parmeters.Add("password", password);


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
            
            if(message.Contains("UserID"))
            {
                message.Replace("{", null);
                message.Replace("}", null);
                String[] logInfo = message.Split(",");
                string userID = logInfo[1].Substring(logInfo[1].IndexOf(":") +1);
                string userEmail = logInfo[2].Substring(logInfo[2].IndexOf(":") + 1);

                SharedPrefManager.getInstance(Application.Context).userLogin(userID, userEmail);
                GetUserInfo(userID);
                
            }
            else if (message.Contains("LastName"))
            {
                message.Replace("{", null);
                message.Replace("}", null);
                String[] logInfo = message.Split(",");
                string lastName = logInfo[1].Substring(logInfo[1].IndexOf(":") + 1);
                string firstName = logInfo[2].Substring(logInfo[2].IndexOf(":") + 1);
                string age = logInfo[3].Substring(logInfo[3].IndexOf(":") + 1);
                string height = logInfo[4].Substring(logInfo[4].IndexOf(":") + 1);
                height = height.Substring(0, height.Length - 1);
                
                SharedPrefManager.getInstance(Application.Context).UserProfileSetup(lastName, firstName, age, height);

                Intent Home = new Intent(this, typeof(home));
                StartActivity(Home);
                Finish();
            }
            else if(message.Contains("Invalid username or password"))
            {
                Toast.MakeText(this, "Invalid username or password",ToastLength.Long).Show();
            }
            else if(message.Contains("No user info found"))
            {
                Intent UserSetup = new Intent(this, typeof(userprofilesetup));
                StartActivity(UserSetup);
                Finish();
            }

        }
        private void GetUserInfo(string userid)
        {
            try
            {
                WebClient webClient = new WebClient();
                Uri uri = new Uri("http://72.198.242.232:8080/Android/FitnessAppv1/getUserInfo.php");

                NameValueCollection parmeters = new NameValueCollection();
                parmeters.Add("userid", userid);

                webClient.UploadValuesCompleted += WebClient_UploadValuesCompleted;
                webClient.UploadValuesAsync(uri, parmeters);

            }
            catch (Exception ex)
            {
                Toast.MakeText(this, "Error", ToastLength.Long).Show();
            }
        }

        
    }
}