using System;
using System.Collections.Generic;
using CodeBase.ApplicationLibrary.Service;
using Newtonsoft.Json;
using UnityEngine;

namespace CodeBase.ApplicationLibrary.Common
{
    [DisallowMultipleComponent]
    public class SavingHelper : MonoBehaviour, ISavingService
    {
        public void Initialize()
        {
            
        }
        
        /*
        public delegate void OperationCompletedHandler(bool success);
        public delegate void OperationLoadDataCompletedHandler(Dictionary<Type, object[]> CloudData);

        private const string DATA_KEY = "data";
        private const string SAVING_DATA_TIME_KEY = "SavingHelper_save_time";

        private OperationCompletedHandler _completeHandler;
        private OperationCompletedHandler _operationCompletedHandler;
        private OperationLoadDataCompletedHandler _completeDataHandler;

        private string _savingData;

        private bool Available { get; set; }

        private bool Initialized { get; set; }
        private bool InAction { get; set; }


        public override void Setup(MessengerKeys EventKey, Action onComponentInitialized)
        {
            InAction = false;
#if UNITY_ANDROID
            Available = GameServices.IsAvailable();
#elif UNITY_IOS
            Available = true;
#endif
            base.Setup(EventKey, onComponentInitialized);
        }

        private void OnEnable()
        {
            CloudServices.OnSynchronizeComplete += CloudServicesOnOnSynchronizeComplete; 
            CloudServices.OnUserChange += CloudServicesOnOnUserChange; 
            CloudServices.OnSavedDataChange += CloudServicesOnOnSavedDataChange;
        }

        private void OnDisable()
        {
            CloudServices.OnSynchronizeComplete -= CloudServicesOnOnSynchronizeComplete; 
            CloudServices.OnUserChange -= CloudServicesOnOnUserChange; 
            CloudServices.OnSavedDataChange -= CloudServicesOnOnSavedDataChange; 
        }

        private void CloudServicesOnOnSavedDataChange(CloudServicesSavedDataChangeResult result)
        {
        }

        private void CloudServicesOnOnUserChange(CloudServicesUserChangeResult result, Error error)
        {
        }

        private void CloudServicesOnOnSynchronizeComplete(CloudServicesSynchronizeResult result)
        {
        }

        public void LoadCloudData(OperationLoadDataCompletedHandler completedHandler)
        {
            if (!InAction)
            {
                InAction = true;
                _completeDataHandler = completedHandler;
                CloudServices.OnSynchronizeComplete += GetCloudProgress;
                CloudServices.Synchronize();
            }
        }

        private void GetCloudProgress(CloudServicesSynchronizeResult result)
        {
            CloudServices.OnSynchronizeComplete -= GetCloudProgress;
            Dictionary<Type, object[]> readData;
            
            var totalResult = result.Success && !string.IsNullOrEmpty(CloudServices.GetString(DATA_KEY));
            if (totalResult)
            {
                DeserializeData(out readData);
                if (_completeDataHandler != null)
                {
                    _completeDataHandler(readData);
                    _completeDataHandler = null;
                }
            }
            
            if(result.Success && string.IsNullOrEmpty(CloudServices.GetString(DATA_KEY)))
            {
                _completeDataHandler(null);
                _completeDataHandler = null;
            }
            InAction = false;
        }
        
        
        public void SaveGame(OperationCompletedHandler completedHandler)
        {
            if (InAction)
            {
                completedHandler(false);
                return;
            }
            
            CompleteSaveGame(completedHandler);
        }

        private void CompleteSaveGame(OperationCompletedHandler completedHandler)
        {
            if (!InAction)
            {
                InAction = true;
                _completeHandler = completedHandler;
                
                CloudServices.OnSynchronizeComplete += OnGameSaved;
                
                //var data = Context.DataHelper.GetData();
               // CloudServices.SetString(DATA_KEY, SerializeSavingData(data));
                CloudServices.Synchronize();
            }
        }
        
        private void OnGameSaved(CloudServicesSynchronizeResult result)
        {
            CloudServices.OnSynchronizeComplete  -= OnGameSaved;
            
            if (_completeHandler != null)
            {
                _completeHandler(result.Success);
                _completeHandler = null;
            }

            InAction = false;
        }

        private string SerializeSavingData(Dictionary<Type, object[]> data)
        {
            var savingValuesDict = new Dictionary<string, string>();

            foreach (var dataItem in data)
                savingValuesDict[dataItem.Key.FullName] = JsonConvert.SerializeObject(dataItem.Value);

            savingValuesDict[SAVING_DATA_TIME_KEY] = DateTime.UtcNow.Ticks.ToString();
            return JsonConvert.SerializeObject(savingValuesDict);
        }
        
        private void DeserializeData(out Dictionary<Type, object[]> readData)
        {
            var Data = CloudServices.GetString(DATA_KEY);

            var stringDataDict = JsonConvert.DeserializeObject<Dictionary<string, string>>(Data);
            readData = new Dictionary<Type, object[]>();

            foreach (var stringDataEntry in stringDataDict)
            {
                Type type = null;
                foreach (var currentAssembly in AppDomain.CurrentDomain.GetAssemblies())
                {
                    type = currentAssembly.GetType(stringDataEntry.Key, false, true);
                    if (type != null)
                        break;
                }

                if (type != null)
                    readData[type] =
                        (object[]) JsonConvert.DeserializeObject(stringDataEntry.Value, type.MakeArrayType());
            }
        }
    */
    }

    public interface ISavingService
    {
    }
}