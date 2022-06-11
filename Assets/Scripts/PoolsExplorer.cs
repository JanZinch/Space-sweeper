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
	
	[Space]
	[Header("Effects")]
	[SerializeField] private List<PoolsManager.ObjectPoolData> _effectPools = null;
	
	[Space]
	[Header("Enemies")]
	[SerializeField] private List<PoolsManager.ObjectPoolData> _enemyPools = null;
	
	
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
		/*if (_instance != null)
		{
			throw new Exception("Pool Explorer already exists! Only one Pool Explorer may be created.");
		}
		else {*/

			_instance = this;

			List<PoolsManager.ObjectPoolData> allPools =
				new List<PoolsManager.ObjectPoolData>(_tunnelPartPools.Count + _projectilePools.Count + _effectPools.Count + _enemyPools.Count);
			
			allPools.AddRange(_tunnelPartPools);
			allPools.AddRange(_projectilePools);
			allPools.AddRange(_effectPools);
			allPools.AddRange(_enemyPools);
			
			PoolsManager.Initialize(allPools);
		//}		
	}
	
}

public enum PooledObjectType
{
	NONE = 0,
		
	// tunnel
		
	TUNNEL_CYBER_BRIDGE = 9,
	TUNNEL_CYBER_REPAIRS = 10,
	TUNNEL_HEX_ENEMIES_1 = 11,
	TUNNEL_CYBER_HATCHES = 12,
	TUNNEL_CYBER_GATEWAY_1 = 130,
	TUNNEL_CYBER_GATEWAY_2 = 131,
	TUNNEL_CYBER_PINS_1 = 14,
	TUNNEL_CYBER_PINS_2 = 15,	
	TUNNEL_CYBER_EMPTY = 16,
	TUNNEL_CYBER_BLOWER = 17,
	TUNNEL_HEX_BEAMS_1 = 18,
	TUNNEL_HEX_TRAP = 19,
	TUNNEL_HEX_ENEMIES_2 = 20,
	TUNNEL_CYBER_ARMADA = 21,
	
	// projectiles
		
	FIREBALL = 100,
	BULLET = 101,
	ROCKET = 102,
	LASER = 103,
		
	// effects
	
	FIREBALL_EXPLOSION = 200,
	BULLET_EXPLOSION = 201,
	MINE_EXPLOSION = 202,
	ROCKET_EXPLOSION = 203,
	LASER_MICROEXPLOSIONS = 204,	
	
	// enemies
	
	GAMMAZOID = 300,
	CENTRAL_GAMMAZOID = 301,
	LASER_GAMMAZOID = 302,
	MINE = 303,
	
}