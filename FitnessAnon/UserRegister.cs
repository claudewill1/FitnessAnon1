using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Widget;
using System;
using System.Text;
using System.Net;
using System.Collections.Specialized;
using Android.Content;

namespace FitnessAnon
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = false)]
    public class UserRegister : AppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);
            Button btnSubmit = (Button)FindViewById(Resource.Id.btnRegister);
            btnSubmit.Click += BtnSubmit_Click;
        }

        private void BtnSubmit_Click(object sender, System.EventArgs e)
        {
            EditText txtUsername = (EditText)FindViewById(Resource.Id.txtUsername);
            EditText txtEmail = (EditText)FindViewById(Resource.Id.txtEmail);
            EditText txtPassword = (EditText)FindViewById(Resource.Id.txtPassword);
            EditText txtPasswordCheck = (EditText)FindViewById(Resource.Id.txtPasswordCheck);

            if (txtPassword.Text == txtPasswordCheck.Text)
            {

                RegisterUser(txtUsername.Text, txtEmail.Text, txtPassword.Text);
            }
            else
            {
                Toast.MakeText(this, "Passwords Don't Match", ToastLength.Long).Show();
            }



        }


         private void RegisterUser(string username, string email, string password)
        {
            try
            {



                WebClient webClient = new WebClient();
                Uri uri = new Uri("http://72.198.242.232:8080/Android/FitnessAppv1/registerUser.php");

                NameValueCollection parmeters = new NameValueCollection();

                parmeters.Add("username", username);
                parmeters.Add("email", email);
                parmeters.Add("password", password);


                webClient.UploadValuesCompleted += WebClient_UploadValuesCompleted;
                webClient.UploadValuesAsync(uri, parmeters);
                                
            }
            catch(Exception e)
            {
                Toast.MakeText(this, e.Message, ToastLength.Long).Show();
            }
        }

        private void WebClient_UploadValuesCompleted(object sender, UploadValuesCompletedEventArgs e)
        {
            string message = Encoding.UTF8.GetString(e.Result);
            Toast.MakeText(this, message, ToastLength.Long).Show();

            if (message.Contains("User registered successfully"))
            {
                Intent UserLogin = new Intent(this, typeof(UserLogin));
                StartActivity(UserLogin);
                Finish();
            }

        }
    }
}