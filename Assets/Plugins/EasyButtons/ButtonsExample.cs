﻿using UnityEngine;

namespace EasyButtons
{
    public class ButtonsExample : MonoBehaviour
    {
        // Example use of the ButtonAttribute
        [Button]
        public void SayMyName()
        {
           
        }

        // Example use of the ButtonAttribute that is not shown in play mode
        [Button(ButtonMode.DisabledInPlayMode)]
        protected void SayHelloEditor()
        {
            
        }

        // Example use of the ButtonAttribute that is only shown in play mode
        [Button(ButtonMode.EnabledInPlayMode)]
        private void SayHelloInRuntime()
        {
          
        }

        // Example use of the ButtonAttribute with custom name
        [Button("Special Name", ButtonSpacing.Before)]
        private void TestButtonName()
        {
           
        }

        // Example use of the ButtonAttribute with custom name and button mode
        [Button("Special Name Editor Only", ButtonMode.DisabledInPlayMode)]
        private void TestButtonNameEditorOnly()
        {
            
        }
        
        // Example use of the ButtonAttribute with static method
        [Button]
        private static void TestStaticMethod()
        {
            
        }
        
        // Example use of the ButtonAttribute with ButtonSpacing, and mix two spacing together.
        [Button("Space Before and After", ButtonSpacing.Before | ButtonSpacing.After)]
        private void TestButtonSpaceBoth() {
        }
        
        // Placeholder to show the last button have space after it.
        [Button("Another Button")]
        private void TestButtonEndSpace() { ;
        }
    }
}
