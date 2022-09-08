using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Speech.Tts;
using Android.Speech;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SpeechText;
using SpeechText.Droid;
using System.Runtime.Remoting.Contexts;
using System.Threading.Tasks;
using Xamarin.Forms;
using System.Threading;

[assembly: Xamarin.Forms.Dependency(typeof(SpeechService))]
namespace SpeechText.Droid
{
    public class SpeechService : IDictation
    {
        private bool isRecording;
        public static AutoResetEvent autoEvent = new AutoResetEvent(false);
        public static string TextRecorded;
        const int VOICE = 10;

        //public string Dictate()
        //{
        //    var context = Android.App.Application.Context;
        //    // set the isRecording flag to false (not recording)
        //    isRecording = false;


        //    // check to see if we can actually record - if we can, assign the event to the button
        //    string rec = Android.Content.PM.PackageManager.FeatureMicrophone;
        //    var voiceIntent = new Intent(RecognizerIntent.ActionRecognizeSpeech);

        //    if (rec != "android.hardware.microphone")
        //    {
        //        // no microphone, no recording. Disable the button and output an alert
        //        var alert = new AlertDialog.Builder(context);
        //        alert.SetTitle("You don't seem to have a microphone to record with");
        //        alert.SetPositiveButton("OK", (sender, e) =>
        //        {
        //            Toast.MakeText(Android.App.Application.Context, "No microphone present", ToastLength.Short).Show();
        //            return;
        //        });

        //        alert.Show();
        //    }
        //    else
        //    {

        //        // change the text on the button
        //        isRecording = !isRecording;
        //        if (isRecording)
        //        {
        //            // create the intent and start the activity

        //            voiceIntent.PutExtra(RecognizerIntent.ExtraLanguageModel, RecognizerIntent.LanguageModelFreeForm);

        //            // put a message on the modal dialog
        //            voiceIntent.PutExtra(RecognizerIntent.ExtraPrompt, "Speak now!");

        //            // if there is more then 1.5s of silence, consider the speech over
        //            voiceIntent.PutExtra(RecognizerIntent.ExtraSpeechInputCompleteSilenceLengthMillis, 1500);
        //            voiceIntent.PutExtra(RecognizerIntent.ExtraSpeechInputPossiblyCompleteSilenceLengthMillis, 1500);
        //            voiceIntent.PutExtra(RecognizerIntent.ExtraSpeechInputMinimumLengthMillis, 15000);
        //            voiceIntent.PutExtra(RecognizerIntent.ExtraMaxResults, 1);

        //            // you can specify other languages recognised here, for example
        //            // voiceIntent.PutExtra(RecognizerIntent.ExtraLanguage, Java.Util.Locale.German);
        //            // if you wish it to recognise the default Locale language and German
        //            // if you do use another locale, regional dialects may not be recognised very well

        //            voiceIntent.PutExtra(RecognizerIntent.ExtraLanguage, Java.Util.Locale.Default);
        //            //StartActivityForResult(voiceIntent, 10);
        //            context.StartService(voiceIntent);

        //        }

        //    }

        //    //var matches = voiceIntent.GetStringArrayListExtra(RecognizerIntent.ExtraResults);
        //    //if (matches.Count != 0)
        //    //{
        //    //    string textInput = matches[0];

        //    //    // limit the output to 500 characters
        //    //    if (textInput.Length > 500)
        //    //        textInput = textInput.Substring(0, 500);
        //    //    Toast.MakeText(Android.App.Application.Context, textInput, ToastLength.Short).Show();
        //    //    return textInput;
        //    //}
        //    //else
        //    //    Toast.MakeText(Android.App.Application.Context, "No speech was recognised", ToastLength.Short).Show();
        //    //// change the text back on the button
        //    //Toast.MakeText(Android.App.Application.Context, "Start Recording", ToastLength.Short).Show();
        //    return "";
        //}

        public async Task<string> Dictate()
        {
            var voiceIntent = new Intent(RecognizerIntent.ActionRecognizeSpeech);
            voiceIntent.PutExtra(RecognizerIntent.ExtraLanguageModel, RecognizerIntent.LanguageModelFreeForm);
            voiceIntent.PutExtra(RecognizerIntent.ExtraPrompt, "Dicte la cantidad deseada");
            voiceIntent.PutExtra(RecognizerIntent.ExtraSpeechInputCompleteSilenceLengthMillis, 500);
            voiceIntent.PutExtra(RecognizerIntent.ExtraSpeechInputPossiblyCompleteSilenceLengthMillis, 500);
            voiceIntent.PutExtra(RecognizerIntent.ExtraSpeechInputMinimumLengthMillis, 500);
            voiceIntent.PutExtra(RecognizerIntent.ExtraMaxResults, 1);
            voiceIntent.PutExtra(RecognizerIntent.ExtraLanguage, Java.Util.Locale.Default);

            TextRecorded = "";
            autoEvent.Reset();
            ((Activity)Forms.Context).StartActivityForResult(voiceIntent, VOICE);
            await Task.Run(() => { autoEvent.WaitOne(new TimeSpan(0, 2, 0)); });
            return TextRecorded;
        }

    }
}