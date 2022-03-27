﻿using UnityEngine;

namespace EasyButtons
{
    [CreateAssetMenu(fileName = "ScriptableObjectExample.asset", menuName = "EasyButtons/ScriptableObjectExample")]
    public class ScriptableObjectExample : ScriptableObject
    {
        [Button]
        public void SayHello()
        {
            
        }

        [Button(ButtonMode.DisabledInPlayMode)]
        public void SayHelloEditor()
        {
        }

        [Button(ButtonMode.EnabledInPlayMode)]
        public void SayHelloPlayMode()
        {
            
        }
    }
}
