using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Telephony;
using Android.Widget;
using AndroidX.AppCompat.App;
using System;

namespace App1
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {

        Button btn1, btn2, btn3, btn4, btn5, btn6;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);

            btn1 = FindViewById<Button>(Resource.Id.button1);
            btn2 = FindViewById<Button>(Resource.Id.button2);
            btn3 = FindViewById<Button>(Resource.Id.button3);
            btn4 = FindViewById<Button>(Resource.Id.button4);
            btn5 = FindViewById<Button>(Resource.Id.button5);
            btn5 = FindViewById<Button>(Resource.Id.button6);

            btn1.Click += Day1;
            btn2.Click += Day2;
            btn3.Click += Day3;
            btn4.Click += Day4;
            btn5.Click += Day5;
            btn6.Click += Profile;

        }

        public void Day1(object sender, EventArgs e)
        {
            Intent intent = new Intent(this, typeof(Day1Activity));
            StartActivity(intent);  
        }

        public void Day2(object sender, EventArgs e)
        {
            Intent intent = new Intent(this, typeof(Day2Activity));
            StartActivity(intent);
        }

        public void Day3(object sender, EventArgs e)
        {
            Intent intent = new Intent(this, typeof(Day3Activity));
            StartActivity(intent);
        }

        public void Day4(object sender, EventArgs e)
        {
            Intent intent = new Intent(this, typeof(Day4Activity));
            StartActivity(intent);
        }

        public void Day5(object sender, EventArgs e)
        {
            Intent intent = new Intent(this, typeof(Day5Activity));
            StartActivity(intent);
        }

        public void Profile(object sender, EventArgs e)
        {
            Intent intent = new Intent(this, typeof(ProfileActivity));
            StartActivity(intent);
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);


        }
    }
}