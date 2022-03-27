using System;
using System.Collections.Generic;
using System.Linq;
using CodeBase.ApplicationLibrary.Service;
using UnityEngine;

namespace CodeBase.ApplicationLibrary.Common
{
    [DisallowMultipleComponent]
    public class MusicPlayer : IAbstractService
    {
        public enum Channel
        {
            Music = 0,
            Ambient = 1
        }

        public const string PrefName = "MusicPlayer_enabled";
        
        private readonly Dictionary<Channel, ChannelData> _chanelDatas = new Dictionary<Channel, ChannelData>();
        private readonly Dictionary<Channel, AudioSource> _sources = new Dictionary<Channel, AudioSource>();

        [SerializeField] private GameObject AudioSourceParent;

        private bool _enabled;

        public override void Setup(MessengerKeys EventKey, Action onComponentInitialized)
        {
            _enabled = PlayerPrefs.GetInt(PrefName, 1) == 1;
            foreach (var channel in Enum.GetValues(typeof(Channel)).Cast<Channel>())
                _sources[channel] = AudioSourceParent.AddComponent(typeof(AudioSource)) as AudioSource;
            base.Setup(EventKey, onComponentInitialized);
        }
        
        public void StopMusic()
        {
            foreach (var source in _sources.Values)
                if (source.isPlaying)
                    source.Stop();
        }

        public void StopChannel(Channel channel)
        {
            if (_sources[channel].isPlaying)
                _sources[channel].Stop();

            _chanelDatas.Remove(channel);
        }

 

        public bool IsEnabled()
        {
            return _enabled;
        }

        private class ChannelData
        {
            public string MusicName { get; set; }

            public float Volume { get; set; }
        }
    }
}