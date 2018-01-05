using System;
using Android.App;
using Android.Content;
using Android.OS;

namespace MonoAndroidApplication1
{
    //[Activity(MainLauncher = true, Theme = "@style/Theme.Splash", NoHistory = true)]
    [Activity(Theme = "@style/Theme.Splash", NoHistory = true, MainLauncher = true)]
    public class SplashActivity : Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Start our real activity
            StartActivity(typeof(ApnaPages.HomeActivity));
        }
    }
}