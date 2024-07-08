using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
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
            btn6 = FindViewById<Button>(Resource.Id.button6);


            btn1.Click += OnButtonClick;
            btn2.Click += OnButtonClick;
            btn3.Click += OnButtonClick;
            btn4.Click += OnButtonClick;
            btn5.Click += OnButtonClick;
            btn6.Click += OnButtonClick;

        }

        private void OnButtonClick(object sender, EventArgs e)
        {
            var button = sender as Button;
            Intent intent = null;

            switch (button.Id)
            {
                case Resource.Id.button1:
                    intent = new Intent(this, typeof(Day1Activity));
                    break;
                case Resource.Id.button2:
                    intent = new Intent(this, typeof(Day2Activity));
                    break;
                case Resource.Id.button3:
                    intent = new Intent(this, typeof(Day3Activity));
                    break;
                case Resource.Id.button4:
                    intent = new Intent(this, typeof(Day4Activity));
                    break;
                case Resource.Id.button5:
                    intent = new Intent(this, typeof(Day5Activity));
                    break;
                case Resource.Id.button6:
                    intent = new Intent(this, typeof(ProfileActivity));
                    intent.PutExtra("stud_id", "2022152824");
                    break;
            }

            if (intent != null)
            {
                StartActivity(intent);
            }
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);


        }
    }
}