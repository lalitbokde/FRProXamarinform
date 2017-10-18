using Xamarin.Forms;
using FR.Droid;
using System;
using System.IO;
using Android.Media;
using System.Threading.Tasks;
using System.Net;
using System.ComponentModel;

[assembly: Dependency(typeof(SoundRecorder_Android))]

namespace FR.Droid
{
    public class SoundRecorder_Android : ISoundRecorder
    {
        static string filename = "test.wav";
        static string documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
        static string filePath = Path.Combine(documentsPath, filename);
        static string voicePath = "";

        MediaRecorder recorder = null;
        MediaPlayer player = null;

        public void StartRecorder()
        {
            try
            {


                if (File.Exists(filePath))
                    File.Delete(filePath);

                if (recorder == null)
                    recorder = new MediaRecorder(); // Initial state.
                else
                    recorder.Reset();

                recorder.SetAudioSource(AudioSource.Mic);
                recorder.SetOutputFormat(OutputFormat.Mpeg4);
                recorder.SetAudioEncoder(AudioEncoder.AmrNb); // Initialized state.
                recorder.SetOutputFile(filePath); // DataSourceConfigured state.
                recorder.Prepare(); // Prepared state
                recorder.Start(); // Recording state.

            }
            catch (Exception ex)
            {
                Console.Out.WriteLine(ex.StackTrace);
            }
        }

        public void StopRecorder()
        {
            if (recorder != null)
            {
                recorder.Stop();
                recorder.Release();
                recorder = null;
            }
        }

        public void StartPlayer()
        {
            try
            {
                if (player == null)
                {
                    player = new MediaPlayer();
                }
                else
                {
                    player.Reset();
                }

                // This method works better than setting the file path in SetDataSource. Don't know why.
                Java.IO.File file = new Java.IO.File(filePath);
                Java.IO.FileInputStream fis = new Java.IO.FileInputStream(file);
                //await player.SetDataSourceAsync(fis.FD);

                player.SetDataSource(filePath);
                player.Prepare();
                player.Start();
            }
            catch (Exception ex)
            {
                Console.Out.WriteLine(ex.StackTrace);
            }
        }

        public void StopPlayer()
        {
            if ((player != null))
            {
                if (player.IsPlaying)
                {
                    player.Stop();
                }
                player.Release();
                player = null;
            }
        }


        void ISoundRecorder.StartRecording()
        {
            StartRecorder();
        }

        void ISoundRecorder.EndRecordinging()
        {
            StopRecorder();
        }

        void ISoundRecorder.StartPlayback()
        {
            StartPlayer();
        }

        void ISoundRecorder.EndPlayback()
        {
            StopPlayer();
        }

        byte[] ISoundRecorder.GetAudio() {
            using (var streamReader = new StreamReader(filePath))
            {
                var bytes = default(byte[]);
                using (var memstream = new MemoryStream())
                {
                    streamReader.BaseStream.CopyTo(memstream);
                    bytes = memstream.ToArray();
                    return bytes;
                }
            }
        }

        public void StartPlaybackByUrl(string voiceUrl)
        {

            string filePath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            string localFilename = "downloaded.mp3";
            voicePath = Path.Combine(filePath, localFilename);

            WebClient webClient = new WebClient();
            webClient.DownloadFileCompleted += new AsyncCompletedEventHandler(DownloadFileCompleted);
            webClient.DownloadFileAsync(new Uri(voiceUrl), voicePath);

        }

        private void DownloadFileCompleted(object sender, AsyncCompletedEventArgs e)
        {
            try
            {
                if (player == null)
                {
                    player = new MediaPlayer();
                }
                else
                {
                    player.Reset();
                }

                player.SetDataSource(voicePath);
                player.Prepare();
                player.Start();
            }
            catch (Exception ex)
            {
                Console.Out.WriteLine(ex.StackTrace);
            }
        }
    }
}