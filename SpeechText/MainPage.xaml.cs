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
            Index = 0;
            carousel.ItemsSource = Items;
        }

        public int Index { get; set; }

        public List<item> Items = new List<item>
            {
                new item{ Name = "Media tina alta", Image = "c_MTA.png" },
                new item{ Name = "Media tina baja", Image = "c_MTB.png" },
                new item { Name = "TARIMA", Image = "c_TARIMA.png" },
                new item { Name = "Dolly plastico negro", Image = "c_DLP.png" }
            };


        private async void btnGrabar_Clicked(object sender, EventArgs e)
        {
            string text = await DependencyService.Get<IDictation>().Dictate();
            txtEditor.Text = text;
        }

        private async void txtPhisical_Focused(object sender, FocusEventArgs e)
        {
            try
            {
                var send = (sender as Entry);
                string text = await DependencyService.Get<IDictation>().Dictate();
                
                if (text == "uno" || text == "una")
                {
                    text = "1";
                }

                if (!int.TryParse(text, out int convert))
                {
                    await App.Current.MainPage.DisplayAlert("cuidado", "el texto dictado no se reconocio como un numero, por favor vuelva a intentar ", "ok");
                    return;
                }
                send.Text = text;
                send.Unfocus();
                await Task.Delay(300);
                Index++;
                if (Index > 3)
                {
                    Index = 0;
                }
                carousel.Position = Index;
            }
            catch (Exception)
            {
                await App.Current.MainPage.DisplayAlert("cuidado", "el texto dictado no se reconocio como un numero, por favor vuelva a intentar ", "ok");
            }
        }

        private void carousel_PositionChanged(object sender, PositionChangedEventArgs e)
        {
            Index = carousel.Position;
        }
    }

    public class item
    {
        public string Name { get; set; }
        public string Image { get; set; }
    }
}
