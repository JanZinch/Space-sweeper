using System;
using TMPro;
using UnityEngine;

namespace Alexplay.OilRush.Library.View
{
    public class DoubleCounterText : MonoBehaviour
    {
        [SerializeField] private double _duration = 1f;
        [SerializeField] private double _minChangeSpeed = 20f;
        
        public delegate void CompleteHandler();
        public delegate string CustomFormatter(double value);
        
        private CustomFormatter _customFormatter;
        
        private double _changeSpeed;
        private double _currentValue;
        private double _displayedValue;
        private double _prevDisplayedValue;
        private double _targetValue;
        
        private string _formatString;
        private string _formatStringKey;

        private bool _completeHandled = true;
        private bool _initialized;
        private bool _onCanvas;

        private CompleteHandler _onComplete;
        
        private Type _type;

        private TextMeshPro _textMesh;
        private TextMeshProUGUI _textMeshUI;
        
        private void Awake()
        {
            InitializeIfNeed();
        }

        private void InitializeIfNeed()
        {
            if (!_initialized)
            {
                if (TryGetComponent(out _textMeshUI))
                {
                    _onCanvas = true;
                }
                _initialized = true;
            }
        }

        private void Update()
        {
            if (_displayedValue != _targetValue)
            {
                _currentValue = _displayedValue < _targetValue
                    ? Math.Min(_currentValue + _changeSpeed * Time.deltaTime, _targetValue)
                    : Math.Max(_currentValue - _changeSpeed * Time.deltaTime, _targetValue);

                _displayedValue = _currentValue;

                UpdateText();
            }
        }

        public void ChangeTargetValue(double targetValueChange, bool animated)
        {
            SetTargetValue(_targetValue + targetValueChange, animated);
        }

        public void SetTargetValue(double targetValue, bool animated)
        {
            _completeHandled = false;

            if (animated)
            {
                if (_displayedValue == targetValue)
                {
                    ApplyTargetValueImmediately(targetValue);
                }
                else
                {
                    _targetValue = targetValue;
                    _changeSpeed = Math.Max(_minChangeSpeed, Math.Abs(targetValue - _displayedValue) / _duration);
                }
            }
            else
            {
                ApplyTargetValueImmediately(targetValue);
            }
        }

        private void ApplyTargetValueImmediately(double targetValue)
        {
            _changeSpeed = 0d;
            _currentValue = targetValue;

            _targetValue = targetValue;
            _displayedValue = targetValue;

            UpdateText();
        }

        private void UpdateText()
        {
            InitializeIfNeed();

            switch (_type)
            {
                case Type.BASE:
                    SetText(_displayedValue.ToString());
                    break;
                case Type.FORMATTED_STRING:
                    SetText(string.Format(_formatString, _displayedValue));
                    break;
                case Type.CUSTOM_FORMATTER:
                    SetText(_customFormatter.Invoke(_displayedValue));
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            if (_targetValue == _displayedValue && !_completeHandled)
            {
                _completeHandled = true;
                _onComplete?.Invoke();
            }

            _prevDisplayedValue = _displayedValue;
        }

        private void SetText(string text)
        {
            if (_onCanvas)
            {
                _textMeshUI.text = text;
            }
            else
            {
                _textMesh.text = text;
            }
        }

        public void SetDuration(float duration)
        {
            _duration = duration;
        }

        public void SetFormatString(string formatString)
        {
            _formatString = formatString;
            _type = Type.FORMATTED_STRING;
        }

        public void SetFormatStringKey(string formatStringKey)
        {
            _formatStringKey = formatStringKey;
            _type = Type.FORMATTED_LOCALIZED_STRING;
        }

        public void SetCustomFormatter(CustomFormatter formatter)
        {
            _customFormatter = formatter;
            _type = Type.CUSTOM_FORMATTER;
        }

        public void SetCustomFormatter(string formatStringKey, CustomFormatter formatter)
        {
            _formatStringKey = formatStringKey;
            _customFormatter = formatter;
            _type = Type.LOCALIZED_CUSTOM_FORMATTER;
        }

        public void SetCompleteHandler(CompleteHandler onComplete)
        {
            _onComplete = onComplete;
        }


        private enum Type
        {
            BASE,
            FORMATTED_STRING,
            FORMATTED_LOCALIZED_STRING,
            CUSTOM_FORMATTER,
            LOCALIZED_CUSTOM_FORMATTER
        }
    }
}