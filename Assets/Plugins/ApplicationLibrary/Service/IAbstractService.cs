using System;
using CodeBase.ApplicationLibrary.Common;
using UnityEngine;

namespace CodeBase.ApplicationLibrary.Service
{
    public abstract class IAbstractService : MonoBehaviour, IService
    {
        [HideInInspector] public ServiceType serviceType = Service.ServiceType.NONE;
        [HideInInspector] public bool IntializeState = false;
        public event Action OnComponentInitialized;
        private Action _onComponentInitialized;

        public virtual void Setup(MessengerKeys EventKey, Action OnComponentInitialized = null)
        {
            this.EventKey = EventKey;
            this.ServiceType = ServiceType;
            this.OnComponentInitialized = OnComponentInitialized;
            Invoke(nameof(OnComplete), 0.15f);
        }
        
        private void OnComplete()
        {
            IntializeState = true;
            OnComponentInitialized?.Invoke();
            Messenger.Broadcast(EventKey);
        }

        public ServiceType ServiceType { get; set; }
        public MessengerKeys EventKey { get; set; }

        Action IService.OnComponentInitialized
        {
            get => _onComponentInitialized;
            set => _onComponentInitialized = value;
        }
    }

    public enum ServiceType
    {
        NONE = 0,
        SERVICE_AUDIO = 1,
        SERVICE_OTHER_MODULES = 2,
    }
}