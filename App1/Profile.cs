using System;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Widget;
using AndroidX.AppCompat.App;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using System.IO;
using System.Net;
using Android.Runtime;
using System.Text;
using System.Text.Json.Serialization;
using Android.Telephony;
using static Xamarin.Essentials.Platform;


namespace App1
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = false)]
    public class ProfileActivity : AppCompatActivity
    {
        EditText passwordUpdate, programUpdate;
        EditText studNumET, fNameET, lNameET, yearLevelET, houseET;
        Button submitButton, returnBtn;

        string ipAdd = "192.168.1.2"; // Replace with your server IP address
        string studId;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            SetContentView(Resource.Layout.profile_layout);

            passwordUpdate = FindViewById<EditText>(Resource.Id.passwordInput);
            programUpdate = FindViewById<EditText>(Resource.Id.programInput);
            studNumET = FindViewById<EditText>(Resource.Id.studNumET);
            fNameET = FindViewById<EditText>(Resource.Id.fNameET);
            lNameET = FindViewById<EditText>(Resource.Id.lNameET);
            yearLevelET = FindViewById<EditText>(Resource.Id.yearLevelET);
            houseET = FindViewById<EditText>(Resource.Id.houseET);
            returnBtn = FindViewById<Button>(Resource.Id.returnBtn);

            submitButton = FindViewById<Button>(Resource.Id.submitBtn);
            returnBtn.Click += ReturnHome;
            submitButton.Click += async (sender, e) => await UpdateProfileAsync();

            // Get the studId from the intent
            studId = Intent.GetStringExtra("stud_id");

            // Automatically fetch and display profile details when the activity starts
            if (!string.IsNullOrEmpty(studId))
            {
                DisplayProfileAsync(studId);
            }
            else
            {
                Toast.MakeText(this, "No student ID provided", ToastLength.Long).Show();
            }
        }

        public void ReturnHome (object sender, EventArgs e)
        {
            Android.Content.Intent intent = new Android.Content.Intent(this, typeof(MainActivity));
            StartActivity(intent);
        }

        private async Task UpdateProfileAsync() // updated - july 8
        {
            var password = passwordUpdate.Text;
            var program = programUpdate.Text;

            var url = $"http://{ipAdd}/IT115L/update_profile.php";

            var profileData = new
            {
                Stud_id = studId,
                Password = password,
                Program = program
            };

            try
            {
                var json = JsonSerializer.Serialize(profileData);
                var request = (HttpWebRequest)WebRequest.Create(url);
                request.Method = "POST";
                request.ContentType = "application/json";

                using (var streamWriter = new StreamWriter(request.GetRequestStream()))
                {
                    streamWriter.Write(json);
                    streamWriter.Flush();
                    streamWriter.Close();
                }

                using (var response = (HttpWebResponse)await request.GetResponseAsync())
                using (var streamReader = new StreamReader(response.GetResponseStream()))
                {
                    var responseString = await streamReader.ReadToEndAsync();

                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        Toast.MakeText(this, "Profile updated successfully", ToastLength.Long).Show();
                    }
                    else
                    {
                        Toast.MakeText(this, "Failed to update profile", ToastLength.Long).Show();
                    }
                }
            }
            catch (Exception ex)
            {
                Toast.MakeText(this, $"Error: {ex.Message}", ToastLength.Long).Show();
            }
        }

        private async void DisplayProfileAsync(string studId)
        {
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
                        var options = new JsonSerializerOptions
                        {
                            PropertyNameCaseInsensitive = true
                        };
                        var profile = JsonSerializer.Deserialize<Profile>(responseString, options);

                        if (profile != null)
                        {
                            RunOnUiThread(() =>
                            {
                                studNumET.Text = profile.Stud_id;
                                passwordUpdate.Text = profile.Password;
                                fNameET.Text = profile.First_name;
                                lNameET.Text = profile.Last_name;
                                yearLevelET.Text = profile.Year_lvl;
                                programUpdate.Text = profile.Program;
                                houseET.Text = profile.House_name;
                            });
                        }
                        else
                        {
                            Toast.MakeText(this, "No profile data found", ToastLength.Long).Show();
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
            [JsonPropertyName("STUD_ID")]
            public string Stud_id { get; set; }
            [JsonPropertyName("PASSWORD")]
            public string Password { get; set; }
            [JsonPropertyName("FIRST_NAME")]
            public string First_name { get; set; }
            [JsonPropertyName("LAST_NAME")]
            public string Last_name { get; set; }
            [JsonPropertyName("YEAR_LVL")]
            public string Year_lvl { get; set; }
            [JsonPropertyName("PROGRAM")]
            public string Program { get; set; }
            [JsonPropertyName("HOUSE_NAME")]
            public string House_name { get; set; }
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);
            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}
