using System;
using CodeBase.ApplicationLibrary.Common;
using UnityEngine;
using UnityEngine.UI;
using Utils;

namespace UI
{
    public class ChannelSceneUI : MonoBehaviour
    {
        public static ChannelSceneUI Instance { get; private set; } = null;

        [SerializeField] private Slider _healthSlider = null;

        private bool _starshipIsDead = false;


        private void OnEnable()
        {
            Messenger.AddListener(MessengerKeys.ON_PLAYER_STARSHIP_FALL, OnStarshipIsDead);
        }

        private void OnStarshipIsDead()
        {
            _starshipIsDead = true;
        }

        private void Awake()
        {
            if (Instance != null) Destroy(Instance.gameObject);
            
            Instance = this;

            _healthSlider.maxValue = PlayerUtils.MaxHealth;
            _healthSlider.minValue = 0;
            _healthSlider.value = _healthSlider.maxValue;
        }
        
        public void UpdatePlayerHealth(int health)
        {
            if (_starshipIsDead) return;
            
            _healthSlider.value = health;
        }

        private void OnDisable()
        {
            Messenger.RemoveListener(MessengerKeys.ON_PLAYER_STARSHIP_FALL, OnStarshipIsDead);
        }
        
    }
}