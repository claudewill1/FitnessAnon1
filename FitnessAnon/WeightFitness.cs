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
    [Activity(Label = "WeightFitness", MainLauncher = false)]

    public class WeightFitness : Activity
    {
        
        EditText txtWeight;
        TextView lblBMI;
        double height;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            SetContentView(Resource.Layout.weight_bmi);
            
            height = Convert.ToDouble(SharedPrefManager.getInstance(Application.Context).Height);
            txtWeight = (EditText)FindViewById(Resource.Id.txtWeight);
            lblBMI = (TextView)FindViewById(Resource.Id.lblBMI);
            Button submitBtn = (Button)FindViewById(Resource.Id.Submit);
            submitBtn.Click += SubmitBtn_Click;
            

            
        }

        private void SubmitBtn_Click(object sender, EventArgs e)
        {
            double bmi;
            double weight = Convert.ToDouble(txtWeight.Text);
            


           bmi = (weight / (Math.Pow(height, 2)) * 703);
            lblBMI.Text = bmi.ToString("N1");

            
        }
    }
}