using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            //set view to water_intake.xml
            SetContentView(Resource.Layout.WaterIntake);

            // Create your application here
        }
    }
}