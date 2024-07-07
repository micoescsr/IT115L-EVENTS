using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Widget;
using AndroidX.AppCompat.App;
using System;
using System.Text.Json;
using System.Threading.Tasks;
using System.Net.Http;
using System.Text;
using System.Net;
using System.IO;

namespace App1
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = false)]
    public class ProfileActivity : AppCompatActivity
    {
        EditText passwordUpdate, programUpdate;
        Button submitButton;
        EditText studNumET, fNameET, lNameET, yearLevelET, houseET;

        string ipAdd = "192.168.1.2"; // Replace with your server IP address

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            // Set our view from the "profile_layout" layout resource
            SetContentView(Resource.Layout.profile_layout);

            passwordUpdate = FindViewById<EditText>(Resource.Id.passwordInput);
            programUpdate = FindViewById<EditText>(Resource.Id.programInput);
            studNumET = FindViewById<EditText>(Resource.Id.studNumET);
            fNameET = FindViewById<EditText>(Resource.Id.fNameET);
            lNameET = FindViewById<EditText>(Resource.Id.lNameET);
            yearLevelET = FindViewById<EditText>(Resource.Id.yearLevelET);
            houseET = FindViewById<EditText>(Resource.Id.houseET);

            submitButton = FindViewById<Button>(Resource.Id.submitBtn);

            submitButton.Click += async (sender, e) => await UpdateProfileAsync();

            // Automatically fetch and display profile details when the activity starts
            DisplayProfileAsync();
        }

        private async Task UpdateProfileAsync()
        {
            var studId = studNumET.Text;
            var password = passwordUpdate.Text;
            var firstName = fNameET.Text;
            var lastName = lNameET.Text;
            var yearLevel = yearLevelET.Text;
            var program = programUpdate.Text;
            var house = houseET.Text;

            var client = new HttpClient();
            var url = $"http://{ipAdd}/IT115L/update_profile.php";

            var profileData = new
            {
                Stud_id = studId,
                Password = password,
                First_name = firstName,
                Last_name = lastName,
                Year_lvl = yearLevel,
                Program = program,
                House_name = house
            };

            var json = JsonSerializer.Serialize(profileData);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await client.PostAsync(url, content);

            var responseString = await response.Content.ReadAsStringAsync();
            Toast.MakeText(this, responseString, ToastLength.Long).Show();
        }

        private async void DisplayProfileAsync()
        {
            int studId = 2022151927; // Set the specific student ID here

            string url = $"http://{ipAdd}/IT115L/display_profile.php?stud_id={studId}";
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "GET";

            try
            {
                using (HttpWebResponse response = (HttpWebResponse)await request.GetResponseAsync())
                using (StreamReader reader = new StreamReader(response.GetResponseStream()))
                {
                    string responseString = await reader.ReadToEndAsync();

                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        var profile = JsonSerializer.Deserialize<Profile>(responseString);

                        if (profile != null)
                        {
                            passwordUpdate.Text = profile.Password;
                            fNameET.Text = profile.First_name;
                            lNameET.Text = profile.Last_name;
                            yearLevelET.Text = profile.Year_lvl;
                            programUpdate.Text = profile.Program;
                            houseET.Text = profile.House_name;
                        }
                        else
                        {
                            Toast.MakeText(this, "Profile not found", ToastLength.Long).Show();
                        }
                    }
                    else
                    {
                        Toast.MakeText(this, "Error fetching profile", ToastLength.Long).Show();
                    }
                }
            }
            catch (Exception ex)
            {
                Toast.MakeText(this, $"Error: {ex.Message}", ToastLength.Long).Show();
            }
        }

        public class Profile
        {
            public string Stud_id { get; set; }
            public string Password { get; set; }
            public string First_name { get; set; }
            public string Last_name { get; set; }
            public string Year_lvl { get; set; }
            public string Program { get; set; }
            public string House_name { get; set; }
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);
            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}
