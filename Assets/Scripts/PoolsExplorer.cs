using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PoolsExplorer", menuName = "Application/PoolsExplorer", order = 0)]
public class PoolsExplorer : ScriptableObject
{
	[Header("Tunnel parts")]
	[SerializeField] private List<PoolsManager.ObjectPoolData> _tunnelPartPools = null;
	
	[Space]
	[Header("Projectiles")]
	[SerializeField] private List<PoolsManager.ObjectPoolData> _projectilePools = null;
	
	private static PoolsExplorer _instance = null;

	public static List<PooledObjectType> GetTunnelPartsPoolsIds()
	{
		List<PooledObjectType> ids = new List<PooledObjectType>(_instance._tunnelPartPools.Count);

		foreach (PoolsManager.ObjectPoolData data in _instance._tunnelPartPools)
		{
			ids.Add(data.Type);
		}
		return ids;
	}

	public void Initialize()
	{
		if (_instance != null)
		{
			throw new Exception("Pool Explorer already exists! Only one Pool Explorer may be created.");
		}
		else {

			_instance = this;

			List<PoolsManager.ObjectPoolData> allPools =
				new List<PoolsManager.ObjectPoolData>(_tunnelPartPools.Count + _projectilePools.Count);
			
			allPools.AddRange(_tunnelPartPools);
			allPools.AddRange(_projectilePools);
			
			PoolsManager.Initialize(allPools);
		}		
	}
	
}

public enum PooledObjectType
{
	NONE = 0,
		
	// tunnel
		
	TUNNEL_PART_1 = 10,
	TUNNEL_PART_2 = 11,
	TUNNEL_PART_3 = 12,
	TUNNEL_PART_4 = 13,
		
	// projectiles
		
	FIREBALL = 100,
		
	// effects
}