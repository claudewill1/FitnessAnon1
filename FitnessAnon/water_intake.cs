using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FitnessApp;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace FitnessAnon
{
    [Activity(Label = "water_intake")]
    public class water_intake : Activity
    {
        double glass = 0.0;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            //set view to water_intake.xml
            SetContentView(Resource.Layout.WaterIntake);
            TextView txtWaterGlass = (TextView)FindViewById(Resource.Id.txtGlassOfWater);
            txtWaterGlass.Text = SharedPrefManager.getInstance(Application.Context).GetWaterIntake(DateTime.Now).ToString();
            // Creating button Click event to decrease amount of water intake for day by 0.5 glasses with a glass being 8oz
            Button btnDecreaseIntake = (Button)FindViewById(Resource.Id.btnDecrease);
            btnDecreaseIntake.Click += BtnDecreaseIntake_Click;
            //increase water intake
            Button btnIncreaseIntake = (Button)FindViewById(Resource.Id.btnIncrease);
            btnIncreaseIntake.Click += BtnIncreaseIntake_Click;
            //Button to take user back to home
            Button btnHome = (Button)FindViewById(Resource.Id.btn_home);
            btnHome.Click += BtnHome_Click;
        }
        private void BtnHome_Click(object sender, EventArgs e)
        {
            try
            {
                Intent home = new Intent(this, typeof(home));
                this.StartActivity(home);
            }
            catch (Exception ex)
            {
                Toast.MakeText(this, ex.Message, ToastLength.Long).Show();
            }
        }
        private void BtnDecreaseIntake_Click(object sender,EventArgs e)
        {
            TextView txtWaterGlass = (TextView)FindViewById(Resource.Id.txtGlassOfWater);
            //holds initial value of water intake for day
            
            

            if(txtWaterGlass.Text.Length > 0 || txtWaterGlass.Text != "0.0")
            {
                glass = Convert.ToDouble(txtWaterGlass.Text);
                glass -= 0.5;
                if(glass >= 0)
                {
                    txtWaterGlass.Text = glass.ToString();
                }
                else
                {
                    Toast.MakeText(this, "Water Intake for the day cannot go below 0", ToastLength.Long).Show();
                }

            }
            else
            {
                txtWaterGlass.Text = "0.0";
            }
            SharedPrefManager.getInstance(Application.Context).SetWaterIntake(glass, DateTime.Now);
        }
        private void BtnIncreaseIntake_Click(object sender, EventArgs e)
        {
            try
            {

            
            TextView txtWaterGlass = (TextView)FindViewById(Resource.Id.txtGlassOfWater);
            // increase water intake by 0.5 glass (8oz)
            if(txtWaterGlass.Text.Length > 0)
            {
                glass = Convert.ToDouble(txtWaterGlass.Text);
                glass += 0.5;

                   
               txtWaterGlass.Text = glass.ToString();





            }
            else
            {
                txtWaterGlass.Text = "0.5";
            }
            }
            catch (Exception ex)
            {
                Toast.MakeText(this, ex.Message, ToastLength.Long).Show();
            }
            SharedPrefManager.getInstance(Application.Context).SetWaterIntake(glass, DateTime.Now);
        }

    }
}