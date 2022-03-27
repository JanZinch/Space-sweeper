using System;

using UnityEngine;

namespace Alexplay.OilRush.Library.View
{
    public class CountdownTimer : MonoBehaviour
    {
        private const long UPDATE_INTERVAL = TimeSpan.TicksPerMillisecond * 100;
        private const long MIN_REWIND_SPEED = 10 * TimeSpan.TicksPerMinute;
        private const long MAX_REWIND_DURATION = 5L * TimeSpan.TicksPerSecond;
        private int _timeInMinutes = 0;
        private long _durationTicks;
        private bool _inProgress;
        private long _rewindSpeed;
        private long _startTimeTicks;
        private bool _stopped;
        private bool _textChecked;
        [Tooltip("d:h:m:s")] 
        [SerializeField] private string _timeFormatDays;
        [Tooltip("h:m:s")]
        [SerializeField] private string _timeFormatHours;
        [Tooltip("m:s")] 
        [SerializeField] private string _timeFormatMinutes;
        private long _timeLeft;
        public long TimeLeft => _timeLeft;
        private long _updatedTimeLeft = long.MinValue;
        public long DisplayTimeScale { get; set; } = 1;
        public string TimeFormatDays
        {
            get => _timeFormatDays;
            set => _timeFormatDays = value;
        }
        public TextAdapter Text { get; private set; }
        public event Action OnCountdownFinish;
         public event Action OnValueChanged;
        public void ResetOnCountdownFinish()
        {
            OnCountdownFinish = null;
        }

        private void InitializeTextAdapterIfNeed()
        {
            var _adapter = GetComponent<TextAdapter>();
            if (_adapter == null)
                gameObject.AddComponent<TextAdapter>();
        }

        private void Awake()
        {
            InitializeTextAdapterIfNeed();
            Text = GetComponent<TextAdapter>();
            if (string.IsNullOrEmpty(_timeFormatDays))
                _timeFormatDays = "{0:0} {1:00}:{2:00}:{3:00}";
            if (string.IsNullOrEmpty(_timeFormatHours))
                _timeFormatHours = "{0:00}:{1:00}:{2:00}";
            if (string.IsNullOrEmpty(_timeFormatMinutes))
                _timeFormatMinutes = "{0:00}:{1:00}";
            _textChecked = true;
        }
        public void ResetTimeLeft()
        {
            _timeLeft = 0;
        }

        public long GetStartTimeTicks()
        {
            return _startTimeTicks;
        }
        
        public long GetDurationTicks()
        {
            return _durationTicks;
        }
        
        public int GetMinutes()
        {
            return _timeInMinutes;
        }

        public int GetChangedMinutes(int minutes = 0)
        {
            var _timeInMinutes = (_timeLeft * DisplayTimeScale);
            var new_time = _timeInMinutes - TimeSpan.TicksPerMinute * minutes;
            return (int)(new_time/TimeSpan.TicksPerMinute);
        }

        public void StartCountdown(long startTimeTicks, long durationTicks ,bool animated = false)
        {
            _durationTicks = durationTicks;
            _inProgress = true;
            _stopped = false;
            _rewindSpeed = 0;
            if (animated)
            {
                _rewindSpeed = (startTimeTicks - _startTimeTicks) / Math.Min(MAX_REWIND_DURATION, Math.Abs(startTimeTicks - _startTimeTicks) /MIN_REWIND_SPEED);  ;
                _startTimeTicks = startTimeTicks;
            }
            else
            {
                _startTimeTicks = startTimeTicks;
            }
            Update();
        }
        
        public void StartCountdown()
        {
            Update();
        }
        public void ChangeStartTime(long startTime)
        {
            var change = startTime - _startTimeTicks;
            _rewindSpeed = change / Math.Min(MAX_REWIND_DURATION, Math.Abs(change) /MIN_REWIND_SPEED);
            _startTimeTicks = startTime;
        }
        public void ChangeTime(long changeTime)
        {
            var change = changeTime + _startTimeTicks;
            _rewindSpeed = changeTime / Math.Min(MAX_REWIND_DURATION, Math.Abs(changeTime) /MIN_REWIND_SPEED);
            _startTimeTicks = change;
        }
        public void StopCountdown()
        {
            _stopped = true;
        }
        public bool IsActive()
        {
            return _inProgress && !_stopped;
        }
        private void Update()
        {
            if (_inProgress && !_stopped && _textChecked)
            {
                if (_rewindSpeed != 0)
                {
                    if (_updatedTimeLeft < 0)
                    {
                        _rewindSpeed = 0;
                        return;
                    }

                    var realTimeLeft = _durationTicks - (DateTime.UtcNow.Ticks - _startTimeTicks);
                    _timeLeft = (long) (_updatedTimeLeft + (double) Time.deltaTime * _rewindSpeed);
                    if (_rewindSpeed < 0 && _timeLeft <= realTimeLeft)
                    {
                        _timeLeft = realTimeLeft;
                        _rewindSpeed = 0;
                    }
                    else if (_rewindSpeed > 0 && _timeLeft >= realTimeLeft)
                    {
                        _timeLeft = realTimeLeft;
                        _rewindSpeed = 0;
                    }
                }
                else
                {
                    _timeLeft = _durationTicks - (DateTime.UtcNow.Ticks - _startTimeTicks);
                }

                if (_timeLeft > 0)
                {
                    OnValueChanged?.Invoke();
                    _timeInMinutes = (int) (_timeLeft * DisplayTimeScale / TimeSpan.TicksPerMinute);

                    if (Math.Abs(_timeLeft - _updatedTimeLeft) >= UPDATE_INTERVAL)
                    {
                        var displayTimeLeft = _timeLeft * DisplayTimeScale;
                        if (displayTimeLeft > TimeSpan.TicksPerDay)
                            Text.SetText(_timeFormatDays,
                                displayTimeLeft / TimeSpan.TicksPerDay,
                                displayTimeLeft % TimeSpan.TicksPerDay / TimeSpan.TicksPerHour,
                                displayTimeLeft % TimeSpan.TicksPerHour / TimeSpan.TicksPerMinute,
                                displayTimeLeft % TimeSpan.TicksPerMinute / TimeSpan.TicksPerSecond);
                        else if (displayTimeLeft > TimeSpan.TicksPerHour)
                            Text.SetText(_timeFormatHours,
                                displayTimeLeft / TimeSpan.TicksPerHour,
                                displayTimeLeft % TimeSpan.TicksPerHour / TimeSpan.TicksPerMinute,
                                displayTimeLeft % TimeSpan.TicksPerMinute / TimeSpan.TicksPerSecond);
                        else
                            Text.SetText(_timeFormatMinutes,
                                displayTimeLeft / TimeSpan.TicksPerMinute,
                                displayTimeLeft % TimeSpan.TicksPerMinute / TimeSpan.TicksPerSecond);
                        _updatedTimeLeft = _timeLeft;
                    }
                }
                else
                {
                    Text.SetText(_timeFormatMinutes, 0, 0);
                    OnCountdownFinish?.Invoke();
                    _inProgress = false;
                }
            }
        }
    }
}
