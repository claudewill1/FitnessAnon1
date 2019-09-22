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
    [Activity(Label = "Fitness Anonymous", MainLauncher = false)]
    public class home : Activity
    {
        
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            SharedPrefManager sp = new SharedPrefManager(this);
            SetContentView(Resource.Layout.Home);
            TextView textView = (TextView)FindViewById(Resource.Id.textView1);
            //textView.Append(" " + sp.GetString("firstname", null).Replace("\"",""));
            Button weightBmi = (Button)FindViewById(Resource.Id.weight_bmi);
            weightBmi.Click += WeightBmi_Click;
            Button waterIntake = (Button)FindViewById(Resource.Id.water_intake);
            waterIntake.Click += WaterIntake_Click;
        }

        private void WeightBmi_Click(object sender, EventArgs e)
        {
            // opens weight_bmi.cs activity
            Intent weight_bmi = new Intent(this, typeof(WeightFitness));
            this.StartActivity(weight_bmi);
            Finish();
        }
        private void WaterIntake_Click(object sender, EventArgs e)
        {
            // opens water_intake activity
            Intent water_intake = new Intent(this, typeof(water_intake));
            this.StartActivity(water_intake);
            Finish();
        }
    }
}