using CodeBase.ApplicationLibrary.Common;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Utils;

namespace UI
{
    public class ChannelSceneUI : MonoBehaviour
    {
        public static ChannelSceneUI Instance { get; private set; } = null;

        [SerializeField] private Slider _healthSlider = null;
        [SerializeField] private TextMeshProUGUI _gameEndText = null;
        
        private const float GameEndFadingDuration = 2.0f;
        private bool _starshipIsDead = false;

        private GameEndResult _victory = new GameEndResult("Victory!", new Color32(96, 255, 0, 0));
        private GameEndResult _defeat = new GameEndResult("Game over!", new Color32(255, 0, 60, 0));
        
        private struct GameEndResult
        {
            public string Name { get; }
            public Color Color { get; }

            public GameEndResult(string name, Color color)
            {
                Name = name;
                Color = color;
            }
        }
        
        private void OnEnable()
        {
            Messenger.AddListener(MessengerKeys.ON_PLAYER_STARSHIP_FALL, OnStarshipDeath);
            Messenger.AddListener(MessengerKeys.ON_LEVEL_PASSED, OnLevelPassed);
        }

        private void OnStarshipDeath()
        {
            _starshipIsDead = true;

            _gameEndText.text = _defeat.Name;
            _gameEndText.color = _defeat.Color;
            _gameEndText.DOFade(1.0f, GameEndFadingDuration);
        }
        
        private void OnLevelPassed()
        {
            _gameEndText.text = _victory.Name;
            _gameEndText.color = _victory.Color;
            _gameEndText.DOFade(1.0f, GameEndFadingDuration);
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
            Messenger.RemoveListener(MessengerKeys.ON_PLAYER_STARSHIP_FALL, OnStarshipDeath);
            Messenger.RemoveListener(MessengerKeys.ON_LEVEL_PASSED, OnLevelPassed);
        }

        private void OnDestroy()
        {
            DOTween.Kill(_gameEndText);
        }
    }
}