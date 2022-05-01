using System;
using System.Collections;
using UnityEngine;

[AddComponentMenu("Pool/PooledObject")]
public class PooledObject : MonoBehaviour
{
    [SerializeField] private Component _linkedComponent = null;

    public IEnumerator ReturnToPool(float time)
    {
        yield return new WaitForSeconds(time);
        gameObject.SetActive(false);
    }

    public void ReturnToPool()
    {
        gameObject.SetActive(false);
    }

    public TComponent GetLinkedComponent<TComponent>() where TComponent : Component
    {
        return _linkedComponent as TComponent;
    }

}
