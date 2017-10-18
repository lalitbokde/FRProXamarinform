using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace FR
{
    public partial class SimpleAudioPage : ContentPage
    {
        public SimpleAudioPage()
        {
            InitializeComponent();

            btnPlayWavFile.Clicked += (sender, e) => {
                DependencyService.Get<IAudio>().PlayWavFile("ding_persevy.wav"); ;
            };

            btnPlayMP3File.Clicked += (sender, e) => {
                DependencyService.Get<IAudio>().PlayMp3File("test.mp3");
            };
        }
    }
}
