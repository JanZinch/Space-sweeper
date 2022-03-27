using System;
using DG.Tweening;
using UnityEngine;

namespace CodeBase.ApplicationLibrary.Camera
{
    public class CameraResizer : MonoBehaviour
    {
        public event Action OnCameraResized;
        
        private float _baseWidth = 9;

        private void Awake() => SetupCamera();

        private void SetupCamera()
        {
            if ((float) Screen.height / Screen.width > 1.4f)
            {
                var ortoSize = _baseWidth * 0.5f / Screen.width * Screen.height;
                if(ortoSize < 9) ortoSize = _baseWidth;
                GetComponent<UnityEngine.Camera>().DOOrthoSize(ortoSize, 0).OnComplete(() => OnCameraResized?.Invoke());
            }
        }
    }
}
