using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace SpeechText
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private async void btnGrabar_Clicked(object sender, EventArgs e)
        {
            string text = await DependencyService.Get<IDictation>().Dictate();
            txtEditor.Text = text;
        }
    }
}
