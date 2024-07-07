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
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = false)]
    public class ProfileActivity : AppCompatActivity
    {

        EditText passwordUpdate, programUpdate;
        Button submitButton, returnButton;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.profile_layout);

            passwordUpdate = FindViewById<EditText>(Resource.Id.passwordInput);
            programUpdate = FindViewById<EditText>(Resource.Id.programInput);

            submitButton = FindViewById<Button>(Resource.Id.submitBtn);
            returnButton = FindViewById<Button>(Resource.Id.returnBtn);
        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}