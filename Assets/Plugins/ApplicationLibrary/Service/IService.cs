using System;
using CodeBase.ApplicationLibrary.Common;

namespace CodeBase.ApplicationLibrary.Service
{
    public interface IService
    {
        ServiceType ServiceType { get; set; }
        MessengerKeys EventKey { get; set; }
        Action OnComponentInitialized { get; set; }
    }
}