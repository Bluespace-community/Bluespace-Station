﻿using System;
using Robust.Client.Audio;
using Robust.Shared.Utility;
using System.IO;
using Robust.Client.Graphics;
using Robust.Shared.IoC;

namespace Robust.Client.ResourceManagement
{
    public sealed class AudioResource : BaseResource
    {
        public AudioStream AudioStream { get; private set; } = default!;

        public override void Load(IResourceCache cache, ResPath path)
        {
            if (!cache.ContentFileExists(path))
            {
                throw new FileNotFoundException("Content file does not exist for audio sample.");
            }

            using (var fileStream = cache.ContentFileRead(path))
            {
                if (path.Extension == "ogg")
                {
                    AudioStream = cache.ClydeAudio.LoadAudioOggVorbis(fileStream, path.ToString());
                }
                else if (path.Extension == "wav")
                {
                    AudioStream = cache.ClydeAudio.LoadAudioWav(fileStream, path.ToString());
                }
                else
                {
                    throw new NotSupportedException("Unable to load audio files outside of ogg Vorbis or PCM wav");
                }
            }
        }

        public static implicit operator AudioStream(AudioResource res)
        {
            return res.AudioStream;
        }
    }
}
