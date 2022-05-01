using System;
using System.Collections.Generic;
using CodeBase.ApplicationLibrary.Collections;
using UnityEngine;

static class PoolsManager
{
	private static List<ObjectPoolData> _pools = null;
	private static GameObject _root = null;
	private const string RootName = "Pool";
	
	[Serializable]
	public class ObjectPoolData
	{
		[SerializeField] private PooledObjectType _type;
		public PooledObjectType Type => _type;
		public PooledObject Prefab;
		public int Count;
		private ObjectPool _pool;
		
		public void SetPool(ObjectPool pool)
		{
			_pool = pool;
		}
		
		public ObjectPool GetPool()
		{
			return _pool;
		}
	}

	public static void Initialize(List<ObjectPoolData> newPools)
	{
		_pools = newPools;
		_root = new GameObject();
		_root.name = RootName;
		for (int i = 0; i < _pools.Count; i++)
		{
			if (_pools[i].Prefab != null)
			{
				_pools[i].SetPool(new ObjectPool(_pools[i].Count, _pools[i].Prefab, _root.transform));
			}
		}
	}


	public static PooledObject GetPooledObject(PooledObjectType type, Vector3 position, Quaternion rotation)
	{
		PooledObject result = null;
		if (_pools != null)
		{
			for (int i = 0; i < _pools.Count; i++)
			{
				if (_pools[i].Type == type)
				{
					result = _pools[i].GetPool().GetObject();
					result.transform.position = position;
					result.transform.rotation = rotation;
					result.gameObject.SetActive(true);
					return result;
				}
			}
		}
		return result;
	}
	

}

