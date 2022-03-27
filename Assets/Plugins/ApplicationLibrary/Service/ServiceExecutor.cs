using System;
using System.Collections.Generic;
using CodeBase.ApplicationLibrary.Application;
using CodeBase.ApplicationLibrary.Common;

namespace CodeBase.ApplicationLibrary.Service
{
    public class ServiceExecutor
    {
        public event Action OnComponentInitialized;
        public event Action OnComponentsInitialized;

        private Queue<IAbstractService> Services = new Queue<IAbstractService>();
        private MessengerKeys EventKey = MessengerKeys.NONE;

        public void SetQueue(Queue<IAbstractService> Services, MessengerKeys EventKey)
        {
            this.Services = Services;
            this.EventKey = EventKey;
            Messenger.AddListener(this.EventKey, PopNextService);
            PopNextService();
        }
        
        private void PopNextService()
        {
            if (Services.Count == 0)
            {
                ApplicationUtils.SetInitializedGameSession();
                OnComponentsInitialized?.Invoke();
                Messenger.RemoveListener(EventKey, PopNextService);
                return;
            }
            InitializeService(Services.Dequeue());
        }

        private void InitializeService(IAbstractService Service) => Service.Setup(EventKey, OnComponentInitialized);
    }
}