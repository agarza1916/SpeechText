using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.OS;
using Android.Widget;
using Android.Speech;
using Android.Speech.Tts;
using Android.Content;
using System.Runtime.Remoting.Contexts;

namespace SpeechText.Droid
{
    [Activity(Label = "SpeechText", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        private readonly int VOICE = 10;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
            LoadApplication(new App());
        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

        protected override void OnActivityResult(int requestCode, Result resultVal, Intent data)
        {
            //base.OnActivityResult(requestCode, resultVal, data);
            if (requestCode == VOICE)
            {
                if (resultVal == Result.Ok)
                {
                    var matches = data.GetStringArrayListExtra(RecognizerIntent.ExtraResults);
                    if (matches.Count != 0)
                    {
                        string textInput = matches[0];

                        // limit the output to 500 characters
                        //if (textInput.Length > 500)
                        //    textInput = textInput.Substring(0, 500);
                        SpeechService.TextRecorded = textInput;
                        // change the text back on the button
                        SpeechService.autoEvent.Set();
                    }
                    else
                        Toast.MakeText(Android.App.Application.Context, "No speech was recognised", ToastLength.Short).Show();
                }
            }
        }

    }
}