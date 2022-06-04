using System;
using UnityEngine;
using UnityEngine.UI;
using Utils;

namespace UI
{
    public class ChannelSceneUI : MonoBehaviour
    {
        public static ChannelSceneUI Instance { get; private set; } = null;

        [SerializeField] private Slider _healthSlider = null;
     
        
        private void Awake()
        {
            if (Instance != null) Destroy(Instance.gameObject);
            
            Instance = this;

            _healthSlider.maxValue = PlayerUtils.MaxHealth;
            _healthSlider.minValue = 0;
        }
        
        public void UpdatePlayerHealth(int health)
        {
            _healthSlider.value = health;
        }

    }
}