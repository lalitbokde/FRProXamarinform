using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace FR
{
    public partial class RecordAudioPage : ContentPage
    {
        public RecordAudioPage()
        {
            InitializeComponent();

            btnStartRecording.Clicked += (sender, args) =>
            {
                DependencyService.Get<ISoundRecorder>().StartRecording();
            };

            btnEndRecording.Clicked += (sender, args) =>
            {
                DependencyService.Get<ISoundRecorder>().EndRecordinging();
            };

            btnStartPlayback.Clicked += (sender, args) =>
            {
                DependencyService.Get<ISoundRecorder>().StartPlayback();
            };

            btnEndPlayback.Clicked += (sender, args) =>
            {
                DependencyService.Get<ISoundRecorder>().EndPlayback();
            };

            btnStartPlaybackUrl.Clicked += (sender, args) =>
            {
                DependencyService.Get<ISoundRecorder>().StartPlaybackByUrl(lblVoiceUrl.Text);
            };
        }
    }
}
