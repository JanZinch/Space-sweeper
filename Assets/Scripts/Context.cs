using CodeBase.ApplicationLibrary.Common;
using CodeBase.ApplicationLibrary.Data;
using UnityEngine;

public class Context : MonoBehaviour
{
    [SerializeField] private DataHelper _dataHelper = null;
    [SerializeField] private DelayedCallback _delayedCallback = null;
    
    public static DataHelper DataHelper => _instance._dataHelper;
    public static DelayedCallback DelayedCallback => _instance._delayedCallback;
        
    private static Context _instance = null;
    
    private void Awake()
    {
        _instance = this;
    }
        
}