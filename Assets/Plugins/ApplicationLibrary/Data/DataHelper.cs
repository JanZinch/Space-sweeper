using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using CodeBase.ApplicationLibrary.Common;
using CodeBase.ApplicationLibrary.Service;
using CodeBase.ApplicationLibrary.Utils;
using EasyButtons;
using SimpleSQL;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace CodeBase.ApplicationLibrary.Data
{
    [RequireComponent(typeof(SimpleSQLManager))]
    [DisallowMultipleComponent]
    public class DataHelper : MonoBehaviour, IDataService
    {
        [SerializeField] private SavingHelper _savingHelper;
        
        private readonly IDictionary<Type, IDictionary<object, object>> _data = new Dictionary<Type, IDictionary<object, object>>();
        private readonly MD5CryptoServiceProvider _md5 = new MD5CryptoServiceProvider();
        private IDataContainer Data { get; set; }

        private HashedLong _hashedLongSet;
        private HashedLong _hashedLongGet;
        
        private void Awake()
        {
            var types = GetDatabaseTypes();
            Data = GetNewDataContainer(types.ToArray());

            var loadMethod = GetType().GetMethod("Load", BindingFlags.Instance | BindingFlags.NonPublic);
            foreach (var type in types)
                if (loadMethod != null)
                    loadMethod.MakeGenericMethod(type).Invoke(this, null);
                else
                    throw new NullReferenceException();
            
            InitializeGameComponents();     
            _savingHelper.Initialize();
        }
        
        public void InitializeGameComponents()
        {
            bool firstLaunching = false;
        }
        
        protected virtual IDataContainer GetNewDataContainer(Type[] types)
        {
            return new SqliteDataContainer(GetComponent<SimpleSQLManager>(), types);
        }

        protected virtual List<Type> GetDatabaseTypes()
        {
            return new List<Type>
            {
                typeof(PrefLong),
                typeof(HashedLong),
                typeof(HashedString),
                typeof(HashedDouble),
            };
        }

        public virtual void Clear()
        {
            _data.Clear();
            Data.Clear();
        }

        protected void Load<T>() where T : new()
        {
            var type = typeof(T);
            var items = new Dictionary<object, object>();

            var keyProperty = type.GetProperties().First(p => p.GetCustomAttributes(typeof(PrimaryKeyAttribute), true).Length > 0);

            foreach (var item in Data.GetData<T>())
                items[keyProperty.GetValue(item)] = item;

            _data[type] = items;
        }

        public virtual Dictionary<Type, object[]> GetData()
        {
            var data = new Dictionary<Type, object[]>();

            foreach (var dataEntry in _data)
                data[dataEntry.Key] = dataEntry.Value.Values.ToArray();

            return data;
        }

        public virtual void ApplyData(Dictionary<Type, object[]> data)
        {
            Clear();

            foreach (var dataEntry in data)
            foreach (var item in dataEntry.Value)
                Set(item);
        }

        public void OnApplicationPause(bool pauseStatus)
        {
            if (pauseStatus) Data?.OnPause();
        }

        public void OnDestroy()
        {
            Data?.OnDestroy();
        }

        public IEnumerable<T> Get<T>()
        {
            var type = typeof(T);

            if (!_data.ContainsKey(type))
                _data[type] = new Dictionary<object, object>();

            return _data[type].Values.Cast<T>();
        }

        public T Get<T>(object key)
        {
            var type = typeof(T);

            if (_data.ContainsKey(type))
                if (_data[type].ContainsKey(key))
                    return (T) _data[type][key];

            return default;
        }

        public void Set<T>(T item) where T : new()
        {
            var type = item.GetType();

            if (!_data.ContainsKey(type))
                _data[type] = new Dictionary<object, object>();

            var keyProperty = type.GetProperties().First(p =>
            {
                var customAttributes = p.GetCustomAttributes(typeof(PrimaryKeyAttribute), true);
                return customAttributes.Length > 0;
            });
            var id = keyProperty.GetValue(item);

            if (keyProperty.PropertyType == typeof(int) && (int) id == 0)
            {
                id = (int) (_data[type].Keys.Max() ?? 0) + 1;
                keyProperty.SetValue(item, id);
            }

            _data[type][id] = item;
            Data.Save(item);
        }

        public void Delete<T>(T item) where T : new()
        {
            var type = item.GetType();

            if (!_data.ContainsKey(type))
                _data[type] = new Dictionary<object, object>();

            var keyProperty = type.GetProperties()
                .First(p => p.GetCustomAttributes(typeof(PrimaryKeyAttribute), true).Length > 0);
            var id = keyProperty.GetValue(item);

            _data[type].Remove(id);
            Data.Delete(item);
        }

        // ==== PREF LONG ====

        public long GetPrefLong(string prefName)
        {
            return Get<PrefLong>(prefName)?.Value ?? 0;
        }

        public void SetPrefLong(string prefName, long value)
        {
            var prefLong = Get<PrefLong>(prefName) ?? new PrefLong {PrefName = prefName};
            prefLong.Value = value;
            Set(prefLong);
        }

        public long AppendPrefLong(string prefName, long appendValue)
        {
            var prefLong = Get<PrefLong>(prefName) ?? new PrefLong {PrefName = prefName};
            prefLong.Value += appendValue;
            Set(prefLong);
            return prefLong.Value;
        }


        // ==== HASHED LONG ====

        public long GetLong(string prefName)
        {
             _hashedLongGet = Get<HashedLong>(prefName) ?? new HashedLong(prefName);
             if (_hashedLongGet != null)
            {
                Check(_hashedLongGet);
                return _hashedLongGet.Value;
            }
            return 0;
        }

        public void SetLong(string prefName, long value)
        {
            _hashedLongSet = Get<HashedLong>(prefName) ?? new HashedLong(prefName);
                _hashedLongSet.Value = value;
                _hashedLongSet.Hash = GetHash(_hashedLongSet);
            Set(_hashedLongSet);
        }

        public long AppendLong(string prefName, long value)
        {
            return AppendLong(prefName, value, long.MinValue, long.MaxValue);
        }

        public long AppendLong(string prefName, long value, long min)
        {
            return AppendLong(prefName, value, min, long.MaxValue);
        }

        public long AppendLong(string prefName, long value, long min, long max)
        {
            var hashedLong = Get<HashedLong>(prefName) ?? new HashedLong(prefName);
            Check(hashedLong);
            hashedLong.Value = MathA.Clamp(hashedLong.Value + value, min, max);
            hashedLong.Hash = GetHash(hashedLong);
            Set(hashedLong);
            return hashedLong.Value;
        }


        // ==== HASHED DOUBLE ====

        /*public bool ContainsKey(string KeyName)
        {
            bool isActive = Get<HashedDouble>(KeyName)!=null;
            return isActive;
        }*/

        public double GetDouble(string prefName)
        {
            var hashedDouble = Get<HashedDouble>(prefName) ?? new HashedDouble(prefName);
            Check(hashedDouble);
            return hashedDouble.Value;
        }

        public void SetDouble(string prefName, double value)
        {
            var hashedDouble = Get<HashedDouble>(prefName) ?? new HashedDouble(prefName);
            hashedDouble.Value = value;
            hashedDouble.Hash = GetHash(hashedDouble);
            Set(hashedDouble);
        }

        public double AppendDouble(string prefName, double value)
        {
            return AppendDouble(prefName, value, double.MinValue, double.MaxValue);
        }

        public double AppendDouble(string prefName, double value, double min)
        {
            return AppendDouble(prefName, value, min, double.MaxValue);
        }

        public double AppendDouble(string prefName, double value, double min, double max)
        {
            var hashedDouble = Get<HashedDouble>(prefName) ?? new HashedDouble(prefName);
            Check(hashedDouble);
            hashedDouble.Value = MathA.Clamp(hashedDouble.Value + value, min, max);
            hashedDouble.Hash = GetHash(hashedDouble);
            Set(hashedDouble);
            return hashedDouble.Value;
        }


        // ==== HASHED STRING ====

        public string GetString(string prefName)
        {
            var hashedString = Get<HashedString>(prefName) ?? new HashedString(prefName);
            Check(hashedString);
            return hashedString.Value;
        }

        public void SetString(string prefName, string value)
        {
            var hashedString = Get<HashedString>(prefName) ?? new HashedString(prefName);
            Check(hashedString);
            hashedString.Value = value;
            hashedString.Hash = GetHash(hashedString);
            Set(hashedString);
        }


        // ==== HASHED DATA ====

        private void Check<T>(HashedData<T> hashedData)
        {
            if (hashedData.IsDefault() || GetHash(hashedData).Equals(hashedData.Hash))
                return;

            var prefName = hashedData.PrefName;
            var value = hashedData.Value;
            var hash = hashedData.Hash;
            var hashString = hashedData.GetStringForHash();

            hashedData.Reset();

            if (hashedData is HashedLong)
                Set(hashedData as HashedLong);
            else if (hashedData is HashedString)
                Set(hashedData as HashedString);

            throw new SystemException("Value for '" + prefName + "' corrupted: " + "v=" + value + "; h=" + hash +
                                      "; hs=" + hashString);
        }

        private string GetHash<T>(HashedData<T> hashedData)
        {
            var stringForHash = hashedData.GetStringForHash();

            if (!string.IsNullOrEmpty(stringForHash))
            {
                var ue = new UTF8Encoding();
                var bytes = ue.GetBytes(stringForHash);
                var hashBytes = _md5.ComputeHash(bytes);
                string hashString = "";
                try
                {
                    hashString = hashBytes.Aggregate("", (current, t) => current + Convert.ToString(t, 16).PadLeft(2, '0'));
                }
                catch (StackOverflowException e)
                {
                    Console.WriteLine(e);
                    throw;
                }
                return hashString.PadLeft(32, '0');
            }

            return "";
        }
        
        public DateTime GetDateTime(string prefName)
        {
            var stringTime = GetString(prefName);
            if(string.IsNullOrEmpty(stringTime)) return DateTime.MinValue;
            return DateTime.ParseExact(stringTime, "MM/dd/yyyy HH:mm:ss", CultureInfo.InvariantCulture);
        }
        
        public void SetDateTime(string prefName, DateTime value)
        {
            SetString(prefName, value.ToString(CultureInfo.InvariantCulture));
        }
        
        [Button]
        public void AllClear()
        {
            Clear();
            PlayerPrefs.DeleteAll();
            SceneManager.LoadScene(0);
        }
    }
    
    public interface IDataService
    {
        public string GetString(string key);
        public void SetString(string key, string value);
        
        public long GetLong(string key);
        public void SetLong(string key, long value);
        public long AppendLong(string key, long value);
        
        public double GetDouble(string key);
        public void SetDouble(string key, double value);
        public double AppendDouble(string key, double value);

        public DateTime GetDateTime(string prefName);
        public void SetDateTime(string prefName, DateTime value);
    }
}