﻿using UnityEngine;
using System.Collections.Generic;
using System.Collections.ObjectModel;

public static class TubesContainer
{
    private static ReadOnlyDictionary<int, List<TubeData>> _levelMaps =
        new ReadOnlyDictionary<int, List<TubeData>>(new Dictionary<int, List<TubeData>>()
        {
            {0, new List<TubeData>()
            {
                new TubeData(PooledObjectType.TUNNEL_CYBER_EMPTY, Quaternion.identity),
                new TubeData(PooledObjectType.TUNNEL_HEX_ENEMIES_1, Quaternion.identity),
                new TubeData(PooledObjectType.TUNNEL_CYBER_PINS_1, Quaternion.Euler(0.0f, 0.0f, 22.5f)),
                new TubeData(PooledObjectType.TUNNEL_CYBER_PINS_2, Quaternion.identity),
                new TubeData(PooledObjectType.TUNNEL_CYBER_PINS_2, Quaternion.Euler(0.0f, 0.0f, 22.5f)),
                new TubeData(PooledObjectType.TUNNEL_CYBER_EMPTY, Quaternion.identity),
                new TubeData(PooledObjectType.TUNNEL_CYBER_REPAIRS, Quaternion.identity),
                new TubeData(PooledObjectType.TUNNEL_HEX_ENEMIES_1, Quaternion.identity),
                new TubeData(PooledObjectType.TUNNEL_CYBER_EMPTY, Quaternion.identity),
                new TubeData(PooledObjectType.TUNNEL_CYBER_HATCHES, Quaternion.Euler(0.0f, 0.0f, -22.5f)),
                new TubeData(PooledObjectType.TUNNEL_CYBER_EMPTY, Quaternion.identity),
                new TubeData(PooledObjectType.TUNNEL_CYBER_PINS_1, Quaternion.Euler(0.0f, 0.0f, 22.5f)),
                new TubeData(PooledObjectType.TUNNEL_CYBER_EMPTY, Quaternion.identity),
                new TubeData(PooledObjectType.TUNNEL_HEX_ENEMIES_1, Quaternion.identity),
                new TubeData(PooledObjectType.TUNNEL_HEX_ENEMIES_1, Quaternion.Euler(0.0f, 0.0f, 22.5f)),
                new TubeData(PooledObjectType.TUNNEL_CYBER_EMPTY, Quaternion.identity),
                new TubeData(PooledObjectType.TUNNEL_CYBER_EMPTY, Quaternion.identity),
                new TubeData(PooledObjectType.TUNNEL_CYBER_EMPTY, Quaternion.identity),
                new TubeData(PooledObjectType.TUNNEL_CYBER_EMPTY, Quaternion.identity),
                new TubeData(PooledObjectType.TUNNEL_CYBER_EMPTY, Quaternion.identity),
                new TubeData(PooledObjectType.TUNNEL_CYBER_EMPTY, Quaternion.identity),
                new TubeData(PooledObjectType.TUNNEL_CYBER_EMPTY, Quaternion.identity),
                new TubeData(PooledObjectType.TUNNEL_CYBER_EMPTY, Quaternion.identity),
                
                
                
                new TubeData(PooledObjectType.TUNNEL_CYBER_EMPTY, Quaternion.identity),
                new TubeData(PooledObjectType.TUNNEL_CYBER_EMPTY, Quaternion.identity),
                new TubeData(PooledObjectType.TUNNEL_HEX_ENEMIES_1, Quaternion.identity),
                new TubeData(PooledObjectType.TUNNEL_CYBER_EMPTY, Quaternion.identity),
             
                new TubeData(PooledObjectType.TUNNEL_CYBER_EMPTY, Quaternion.identity),
                
                
                
            }},
            
             {1, new List<TubeData>()
             {
                 new TubeData(PooledObjectType.TUNNEL_CYBER_EMPTY, Quaternion.identity),
                 new TubeData(PooledObjectType.TUNNEL_CYBER_GATEWAY_1, Quaternion.identity),
                 new TubeData(PooledObjectType.TUNNEL_CYBER_GATEWAY_1, Quaternion.Euler(0.0f, 0.0f, 22.5f)),
                 new TubeData(PooledObjectType.TUNNEL_CYBER_GATEWAY_1, Quaternion.identity),
                 new TubeData(PooledObjectType.TUNNEL_CYBER_REPAIRS, Quaternion.identity),
                 new TubeData(PooledObjectType.TUNNEL_CYBER_REPAIRS, Quaternion.identity),
                 new TubeData(PooledObjectType.TUNNEL_CYBER_BLOWER, Quaternion.Euler(0.0f, 0.0f, 22.5f)),
                 new TubeData(PooledObjectType.TUNNEL_CYBER_EMPTY, Quaternion.identity),
                 new TubeData(PooledObjectType.TUNNEL_CYBER_BLOWER, Quaternion.identity),
                
                
            }},
             
             {2, new List<TubeData>()
             {
                 ///*
                 
                 new TubeData(PooledObjectType.TUNNEL_CYBER_EMPTY, Quaternion.identity),
                 new TubeData(PooledObjectType.TUNNEL_CYBER_GATEWAY_1, Quaternion.identity),
                 
                 new TubeData(PooledObjectType.TUNNEL_HEX_ENEMIES_2, Quaternion.identity),
                 new TubeData(PooledObjectType.TUNNEL_HEX_ENEMIES_1, Quaternion.identity),
                 new TubeData(PooledObjectType.TUNNEL_CYBER_PINS_1, Quaternion.Euler(0.0f, 0.0f, 22.5f)),
                 new TubeData(PooledObjectType.TUNNEL_CYBER_PINS_2, Quaternion.identity),
                 new TubeData(PooledObjectType.TUNNEL_CYBER_PINS_2, Quaternion.Euler(0.0f, 0.0f, 22.5f)),
                 new TubeData(PooledObjectType.TUNNEL_CYBER_EMPTY, Quaternion.identity),
                 new TubeData(PooledObjectType.TUNNEL_CYBER_REPAIRS, Quaternion.identity),
                 new TubeData(PooledObjectType.TUNNEL_HEX_ENEMIES_1, Quaternion.identity),
                 new TubeData(PooledObjectType.TUNNEL_CYBER_EMPTY, Quaternion.identity),
                 new TubeData(PooledObjectType.TUNNEL_CYBER_HATCHES, Quaternion.Euler(0.0f, 0.0f, -22.5f)),
                 new TubeData(PooledObjectType.TUNNEL_CYBER_PINS_1, Quaternion.Euler(0.0f, 0.0f, -70.5f)),
                 new TubeData(PooledObjectType.TUNNEL_CYBER_BLOWER, Quaternion.Euler(0.0f, 0.0f, 22.5f)),
                 new TubeData(PooledObjectType.TUNNEL_HEX_ENEMIES_2, Quaternion.identity),
                 new TubeData(PooledObjectType.TUNNEL_CYBER_GATEWAY_2, Quaternion.identity),
                 
                 new TubeData(PooledObjectType.TUNNEL_CYBER_BRIDGE, Quaternion.Euler(0.0f, 0.0f, 90.0f)),
                 new TubeData(PooledObjectType.TUNNEL_CYBER_PINS_1, Quaternion.Euler(0.0f, 0.0f, 30.0f)),
                 new TubeData(PooledObjectType.TUNNEL_HEX_ENEMIES_2, Quaternion.identity),
                 new TubeData(PooledObjectType.TUNNEL_CYBER_PINS_1, Quaternion.Euler(0.0f, 0.0f, 0.0f)),
                 new TubeData(PooledObjectType.TUNNEL_CYBER_EMPTY, Quaternion.identity),
                 new TubeData(PooledObjectType.TUNNEL_CYBER_GATEWAY_1, Quaternion.identity),
                 
                 //*/
                 
                 new TubeData(PooledObjectType.TUNNEL_CYBER_EMPTY, Quaternion.identity),
                 new TubeData(PooledObjectType.TUNNEL_HEX_TRAP, Quaternion.identity),
                 new TubeData(PooledObjectType.TUNNEL_HEX_TRAP, Quaternion.Euler(0.0f, 0.0f, 135.0f)),
                 new TubeData(PooledObjectType.TUNNEL_CYBER_EMPTY, Quaternion.identity),
                 new TubeData(PooledObjectType.TUNNEL_CYBER_ARMADA, Quaternion.identity),                                   // arm
                 
                 new TubeData(PooledObjectType.TUNNEL_CYBER_EMPTY, Quaternion.identity),
                 new TubeData(PooledObjectType.TUNNEL_CYBER_BRIDGE, Quaternion.Euler(0.0f, 0.0f, 160.0f)),
                 
                 new TubeData(PooledObjectType.TUNNEL_CYBER_EMPTY, Quaternion.identity),
                 new TubeData(PooledObjectType.TUNNEL_CYBER_HATCHES, Quaternion.Euler(0.0f, 0.0f, -30.0f)),
                 new TubeData(PooledObjectType.TUNNEL_CYBER_EMPTY, Quaternion.identity),
                 new TubeData(PooledObjectType.TUNNEL_CYBER_HATCHES, Quaternion.Euler(0.0f, 0.0f, 60.0f)),
                 
                 new TubeData(PooledObjectType.TUNNEL_CYBER_EMPTY, Quaternion.identity),
                 new TubeData(PooledObjectType.TUNNEL_CYBER_EMPTY, Quaternion.identity),
                 new TubeData(PooledObjectType.TUNNEL_CYBER_REPAIRS, Quaternion.identity),
                 new TubeData(PooledObjectType.TUNNEL_CYBER_EMPTY, Quaternion.identity),
                 new TubeData(PooledObjectType.TUNNEL_CYBER_PINS_1, Quaternion.identity),
                 new TubeData(PooledObjectType.TUNNEL_CYBER_PINS_1, Quaternion.Euler(0.0f, 0.0f, 60.0f)),
                 new TubeData(PooledObjectType.TUNNEL_CYBER_PINS_1, Quaternion.Euler(0.0f, 0.0f, -90.0f)),
                 
                 new TubeData(PooledObjectType.TUNNEL_CYBER_EMPTY, Quaternion.identity),
                 new TubeData(PooledObjectType.TUNNEL_CYBER_GATEWAY_2, Quaternion.identity),
                 
                 new TubeData(PooledObjectType.TUNNEL_CYBER_EMPTY, Quaternion.identity),
                 new TubeData(PooledObjectType.TUNNEL_HEX_ENEMIES_1, Quaternion.identity),
                 
                 new TubeData(PooledObjectType.TUNNEL_CYBER_BLOWER, Quaternion.Euler(0.0f, 0.0f, 30.0f)),
                 new TubeData(PooledObjectType.TUNNEL_CYBER_BLOWER, Quaternion.Euler(0.0f, 0.0f, 40.0f)),
                 new TubeData(PooledObjectType.TUNNEL_CYBER_BLOWER, Quaternion.Euler(0.0f, 0.0f, 50.0f)),
                 
                 // end
                 
                 
                 
                 new TubeData(PooledObjectType.TUNNEL_CYBER_PINS_1, Quaternion.Euler(0.0f, 0.0f, 22.5f)),
                 new TubeData(PooledObjectType.TUNNEL_CYBER_EMPTY, Quaternion.identity),
                 new TubeData(PooledObjectType.TUNNEL_HEX_ENEMIES_1, Quaternion.identity),
                 new TubeData(PooledObjectType.TUNNEL_HEX_ENEMIES_1, Quaternion.Euler(0.0f, 0.0f, 22.5f)),
                 new TubeData(PooledObjectType.TUNNEL_CYBER_EMPTY, Quaternion.identity),
                 new TubeData(PooledObjectType.TUNNEL_CYBER_GATEWAY_1, Quaternion.identity),
                 new TubeData(PooledObjectType.TUNNEL_CYBER_BLOWER, Quaternion.Euler(0.0f, 0.0f, 22.5f)),
                 new TubeData(PooledObjectType.TUNNEL_CYBER_EMPTY, Quaternion.identity),
                 new TubeData(PooledObjectType.TUNNEL_CYBER_BLOWER, Quaternion.identity),
                 new TubeData(PooledObjectType.TUNNEL_CYBER_GATEWAY_1, Quaternion.Euler(0.0f, 0.0f, 22.5f)),
                 new TubeData(PooledObjectType.TUNNEL_CYBER_GATEWAY_1, Quaternion.identity),
                 new TubeData(PooledObjectType.TUNNEL_HEX_ENEMIES_1, Quaternion.identity),
                 new TubeData(PooledObjectType.TUNNEL_HEX_ENEMIES_1, Quaternion.Euler(0.0f, 0.0f, 22.5f)),
                 new TubeData(PooledObjectType.TUNNEL_HEX_ENEMIES_1, Quaternion.identity),
                 new TubeData(PooledObjectType.TUNNEL_CYBER_BLOWER, Quaternion.Euler(0.0f, 0.0f, 22.5f)),
                 new TubeData(PooledObjectType.TUNNEL_CYBER_EMPTY, Quaternion.identity),
                 new TubeData(PooledObjectType.TUNNEL_CYBER_BLOWER, Quaternion.identity),
                 new TubeData(PooledObjectType.TUNNEL_CYBER_EMPTY, Quaternion.identity),
                 new TubeData(PooledObjectType.TUNNEL_CYBER_HATCHES, Quaternion.Euler(0.0f, 0.0f, -22.5f)),
                 new TubeData(PooledObjectType.TUNNEL_CYBER_PINS_1, Quaternion.Euler(0.0f, 0.0f, 22.5f)),
                 new TubeData(PooledObjectType.TUNNEL_CYBER_EMPTY, Quaternion.identity),
                 new TubeData(PooledObjectType.TUNNEL_HEX_ENEMIES_1, Quaternion.identity),
                 new TubeData(PooledObjectType.TUNNEL_HEX_ENEMIES_1, Quaternion.Euler(0.0f, 0.0f, 22.5f)),
                 new TubeData(PooledObjectType.TUNNEL_CYBER_EMPTY, Quaternion.identity),
                
             }},
             
             {3, new List<TubeData>()
             {
                 new TubeData(PooledObjectType.TUNNEL_CYBER_EMPTY, Quaternion.identity),
                 new TubeData(PooledObjectType.TUNNEL_CYBER_EMPTY, Quaternion.identity),
                 new TubeData(PooledObjectType.TUNNEL_HEX_TRAP, Quaternion.identity),
                 new TubeData(PooledObjectType.TUNNEL_CYBER_GATEWAY_2, Quaternion.identity),
                 new TubeData(PooledObjectType.TUNNEL_HEX_BEAMS_1, Quaternion.identity),
                 new TubeData(PooledObjectType.TUNNEL_CYBER_GATEWAY_2, Quaternion.identity),
                 new TubeData(PooledObjectType.TUNNEL_HEX_ENEMIES_1, Quaternion.identity),
                 
                 //new TubeData(PooledObjectType.TUNNEL_HEX_ENEMIES_1, Quaternion.Euler(0.0f, 0.0f, 22.5f)),
                 //new TubeData(PooledObjectType.TUNNEL_HEX_ENEMIES_1, Quaternion.Euler(0.0f, 0.0f, -22.5f)),
                 new TubeData(PooledObjectType.TUNNEL_CYBER_EMPTY, Quaternion.identity),
                 new TubeData(PooledObjectType.TUNNEL_CYBER_EMPTY, Quaternion.identity),
                 new TubeData(PooledObjectType.TUNNEL_CYBER_EMPTY, Quaternion.identity),
                 new TubeData(PooledObjectType.TUNNEL_CYBER_EMPTY, Quaternion.identity),
                 new TubeData(PooledObjectType.TUNNEL_CYBER_EMPTY, Quaternion.identity),
                 new TubeData(PooledObjectType.TUNNEL_CYBER_EMPTY, Quaternion.identity),
                 new TubeData(PooledObjectType.TUNNEL_CYBER_EMPTY, Quaternion.identity),
                 new TubeData(PooledObjectType.TUNNEL_CYBER_EMPTY, Quaternion.identity),
                 new TubeData(PooledObjectType.TUNNEL_CYBER_EMPTY, Quaternion.identity),
                 new TubeData(PooledObjectType.TUNNEL_CYBER_EMPTY, Quaternion.identity),
                 new TubeData(PooledObjectType.TUNNEL_CYBER_EMPTY, Quaternion.identity),
             }},
             
             
             {4, new List<TubeData>()
             {
                 new TubeData(PooledObjectType.TUNNEL_CYBER_EMPTY, Quaternion.identity),
                 new TubeData(PooledObjectType.TUNNEL_CYBER_EMPTY, Quaternion.identity),
                 new TubeData(PooledObjectType.TUNNEL_HEX_TRAP, Quaternion.identity),
                 new TubeData(PooledObjectType.TUNNEL_HEX_TRAP, Quaternion.Euler(0.0f, 0.0f, 135.0f)),
                 new TubeData(PooledObjectType.TUNNEL_CYBER_EMPTY, Quaternion.identity),
                 new TubeData(PooledObjectType.TUNNEL_CYBER_ARMADA, Quaternion.identity),
                 
                 new TubeData(PooledObjectType.TUNNEL_CYBER_EMPTY, Quaternion.identity),
                 new TubeData(PooledObjectType.TUNNEL_CYBER_EMPTY, Quaternion.identity),
                 new TubeData(PooledObjectType.TUNNEL_CYBER_EMPTY, Quaternion.identity),
                 new TubeData(PooledObjectType.TUNNEL_CYBER_EMPTY, Quaternion.identity),
                 new TubeData(PooledObjectType.TUNNEL_CYBER_EMPTY, Quaternion.identity),
                 new TubeData(PooledObjectType.TUNNEL_CYBER_EMPTY, Quaternion.identity),
                 new TubeData(PooledObjectType.TUNNEL_CYBER_EMPTY, Quaternion.identity),
                 new TubeData(PooledObjectType.TUNNEL_CYBER_EMPTY, Quaternion.identity),
                 new TubeData(PooledObjectType.TUNNEL_CYBER_EMPTY, Quaternion.identity),
                 new TubeData(PooledObjectType.TUNNEL_CYBER_EMPTY, Quaternion.identity),
                 new TubeData(PooledObjectType.TUNNEL_CYBER_EMPTY, Quaternion.identity),
                 
                 new TubeData(PooledObjectType.TUNNEL_CYBER_EMPTY, Quaternion.identity),
                 new TubeData(PooledObjectType.TUNNEL_CYBER_EMPTY, Quaternion.identity),
                 new TubeData(PooledObjectType.TUNNEL_CYBER_EMPTY, Quaternion.identity),
                 new TubeData(PooledObjectType.TUNNEL_CYBER_EMPTY, Quaternion.identity),
                 new TubeData(PooledObjectType.TUNNEL_CYBER_EMPTY, Quaternion.identity),
                 new TubeData(PooledObjectType.TUNNEL_CYBER_EMPTY, Quaternion.identity),
                 new TubeData(PooledObjectType.TUNNEL_CYBER_EMPTY, Quaternion.identity),
                 new TubeData(PooledObjectType.TUNNEL_CYBER_EMPTY, Quaternion.identity),
                 new TubeData(PooledObjectType.TUNNEL_CYBER_EMPTY, Quaternion.identity),
                 new TubeData(PooledObjectType.TUNNEL_CYBER_EMPTY, Quaternion.identity),
                 new TubeData(PooledObjectType.TUNNEL_CYBER_EMPTY, Quaternion.identity),
                 new TubeData(PooledObjectType.TUNNEL_CYBER_EMPTY, Quaternion.identity),
                 new TubeData(PooledObjectType.TUNNEL_CYBER_EMPTY, Quaternion.identity),
                 new TubeData(PooledObjectType.TUNNEL_CYBER_EMPTY, Quaternion.identity),
                 new TubeData(PooledObjectType.TUNNEL_CYBER_EMPTY, Quaternion.identity),
                 new TubeData(PooledObjectType.TUNNEL_CYBER_EMPTY, Quaternion.identity),
                 new TubeData(PooledObjectType.TUNNEL_CYBER_EMPTY, Quaternion.identity),
                 new TubeData(PooledObjectType.TUNNEL_CYBER_EMPTY, Quaternion.identity),
                 new TubeData(PooledObjectType.TUNNEL_CYBER_EMPTY, Quaternion.identity),
                 new TubeData(PooledObjectType.TUNNEL_CYBER_EMPTY, Quaternion.identity),
                 new TubeData(PooledObjectType.TUNNEL_CYBER_EMPTY, Quaternion.identity),
                 new TubeData(PooledObjectType.TUNNEL_CYBER_EMPTY, Quaternion.identity),
                 new TubeData(PooledObjectType.TUNNEL_CYBER_EMPTY, Quaternion.identity),
                 new TubeData(PooledObjectType.TUNNEL_CYBER_EMPTY, Quaternion.identity),
                 new TubeData(PooledObjectType.TUNNEL_CYBER_EMPTY, Quaternion.identity),
                 new TubeData(PooledObjectType.TUNNEL_CYBER_EMPTY, Quaternion.identity),
                 new TubeData(PooledObjectType.TUNNEL_CYBER_EMPTY, Quaternion.identity),
                 new TubeData(PooledObjectType.TUNNEL_CYBER_EMPTY, Quaternion.identity),
                 new TubeData(PooledObjectType.TUNNEL_CYBER_EMPTY, Quaternion.identity),
                 new TubeData(PooledObjectType.TUNNEL_CYBER_EMPTY, Quaternion.identity),
                 new TubeData(PooledObjectType.TUNNEL_CYBER_EMPTY, Quaternion.identity),
                 new TubeData(PooledObjectType.TUNNEL_CYBER_EMPTY, Quaternion.identity),
                 new TubeData(PooledObjectType.TUNNEL_CYBER_EMPTY, Quaternion.identity),
                 new TubeData(PooledObjectType.TUNNEL_CYBER_EMPTY, Quaternion.identity),
                 new TubeData(PooledObjectType.TUNNEL_CYBER_EMPTY, Quaternion.identity),
                 new TubeData(PooledObjectType.TUNNEL_CYBER_EMPTY, Quaternion.identity),
                 new TubeData(PooledObjectType.TUNNEL_CYBER_EMPTY, Quaternion.identity),
                 new TubeData(PooledObjectType.TUNNEL_CYBER_EMPTY, Quaternion.identity),
                 new TubeData(PooledObjectType.TUNNEL_CYBER_EMPTY, Quaternion.identity),
                 new TubeData(PooledObjectType.TUNNEL_CYBER_EMPTY, Quaternion.identity),
                 new TubeData(PooledObjectType.TUNNEL_CYBER_EMPTY, Quaternion.identity),
                 new TubeData(PooledObjectType.TUNNEL_CYBER_EMPTY, Quaternion.identity),
                 new TubeData(PooledObjectType.TUNNEL_CYBER_EMPTY, Quaternion.identity),
                 new TubeData(PooledObjectType.TUNNEL_CYBER_EMPTY, Quaternion.identity),
                 new TubeData(PooledObjectType.TUNNEL_CYBER_EMPTY, Quaternion.identity),
                 new TubeData(PooledObjectType.TUNNEL_CYBER_EMPTY, Quaternion.identity),
                 new TubeData(PooledObjectType.TUNNEL_CYBER_EMPTY, Quaternion.identity),
                 new TubeData(PooledObjectType.TUNNEL_CYBER_EMPTY, Quaternion.identity),
                 new TubeData(PooledObjectType.TUNNEL_CYBER_EMPTY, Quaternion.identity),
                 new TubeData(PooledObjectType.TUNNEL_CYBER_EMPTY, Quaternion.identity),
                 new TubeData(PooledObjectType.TUNNEL_CYBER_EMPTY, Quaternion.identity),
                 new TubeData(PooledObjectType.TUNNEL_CYBER_EMPTY, Quaternion.identity),
                 new TubeData(PooledObjectType.TUNNEL_CYBER_EMPTY, Quaternion.identity),
                 new TubeData(PooledObjectType.TUNNEL_CYBER_EMPTY, Quaternion.identity),
                 new TubeData(PooledObjectType.TUNNEL_CYBER_EMPTY, Quaternion.identity),
                 new TubeData(PooledObjectType.TUNNEL_CYBER_EMPTY, Quaternion.identity),
                 new TubeData(PooledObjectType.TUNNEL_CYBER_EMPTY, Quaternion.identity),
                 new TubeData(PooledObjectType.TUNNEL_CYBER_EMPTY, Quaternion.identity),
                 new TubeData(PooledObjectType.TUNNEL_CYBER_EMPTY, Quaternion.identity),
                 new TubeData(PooledObjectType.TUNNEL_CYBER_EMPTY, Quaternion.identity),
                 new TubeData(PooledObjectType.TUNNEL_CYBER_EMPTY, Quaternion.identity),
                 new TubeData(PooledObjectType.TUNNEL_CYBER_EMPTY, Quaternion.identity),
                 new TubeData(PooledObjectType.TUNNEL_CYBER_EMPTY, Quaternion.identity),
                 new TubeData(PooledObjectType.TUNNEL_CYBER_EMPTY, Quaternion.identity),
                 new TubeData(PooledObjectType.TUNNEL_CYBER_EMPTY, Quaternion.identity),
                 new TubeData(PooledObjectType.TUNNEL_CYBER_EMPTY, Quaternion.identity),
             }},
            
        });

    public static List<TubeData> GetLevelMap(int level)
    {
        return _levelMaps[level];
    }
}

public struct TubeData
{
    private PooledObjectType _tubeType;
    private Quaternion _rotation;

    public PooledObjectType TubeType => _tubeType;
    public Quaternion Rotation => _rotation;
        
    public TubeData(PooledObjectType tubeType, Quaternion rotation)
    {
        _tubeType = tubeType;
        _rotation = rotation;
    }
}