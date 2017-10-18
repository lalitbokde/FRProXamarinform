using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FR
{
    public interface ISoundRecorder
    {
        void StartRecording();

        void EndRecordinging();

        void StartPlayback();

        void EndPlayback();

        byte[] GetAudio();

        void StartPlaybackByUrl(string voiceUrl);
    }
}
