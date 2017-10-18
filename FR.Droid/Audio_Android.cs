using System;
using Xamarin.Forms;
using FR.Droid;
using Android.Media;

[assembly: Dependency(typeof(Audio_Android))]

namespace FR.Droid
{
    public class Audio_Android : IAudio
    {
        public Audio_Android() { }

        private MediaPlayer _mediaPlayer;

        public bool PlayMp3File(string fileName)
        {
            _mediaPlayer = MediaPlayer.Create(global::Android.App.Application.Context, Resource.Raw.test);
            _mediaPlayer.Start();

            return true;
        }

        public bool PlayWavFile(string fileName)
        {
            _mediaPlayer = MediaPlayer.Create(global::Android.App.Application.Context, Resource.Raw.aaa);
            _mediaPlayer.Start();

            return true;
        }
    }
}