using UnityEngine;
using UnityEngine.UI;

namespace Alexplay.OilRush.Library.View
{
    [RequireComponent(typeof(Button))]
    public abstract class ToggleButton : MonoBehaviour
    {
        public delegate void CheckChangedHandler(bool isChecked);

        private bool _isChecked;

        public Button button;

        public bool IsChecked
        {
            get => _isChecked;
            set
            {
                _isChecked = value;
                UpdateState();
                OnCheckChanged?.Invoke(_isChecked);
            }
        }

        public event CheckChangedHandler OnCheckChanged;

        protected abstract void UpdateState();

        private void Awake()
        {
            button = GetComponent<Button>();
        }

        private void Start()
        {
            button.onClick.AddListener(() => IsChecked = !_isChecked);
        }

        private void OnDestroy()
        {
            button.onClick.RemoveAllListeners();
        }
    }
}