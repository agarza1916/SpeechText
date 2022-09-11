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

        public async Task<string> Dictate()
        {
            var voiceIntent = new Intent(RecognizerIntent.ActionRecognizeSpeech);
            voiceIntent.PutExtra(RecognizerIntent.ExtraLanguageModel, RecognizerIntent.LanguageModelFreeForm);
            voiceIntent.PutExtra(RecognizerIntent.ExtraPrompt, "Dicte la cantidad deseada");
            voiceIntent.PutExtra(RecognizerIntent.ExtraSpeechInputCompleteSilenceLengthMillis, 1);
            voiceIntent.PutExtra(RecognizerIntent.ExtraSpeechInputPossiblyCompleteSilenceLengthMillis, 1);
            voiceIntent.PutExtra(RecognizerIntent.ExtraSpeechInputMinimumLengthMillis, 1);
            voiceIntent.PutExtra(RecognizerIntent.ExtraMaxResults, 1);
            voiceIntent.PutExtra(RecognizerIntent.ExtraLanguage, Java.Util.Locale.Default);

            TextRecorded = "";
            autoEvent.Reset();
            ((Activity)Forms.Context).StartActivityForResult(voiceIntent, VOICE);
            await Task.Run(() => { autoEvent.WaitOne(TimeSpan.FromSeconds(5)); });
            return TextRecorded;
        }

    }
}