using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Alexplay.OilRush.Library.View
{
    public class ResizeProgressBar : MonoBehaviour
    {
        private const float startAngle = 84.8f;
        private const float maxAngle = -26.2f;
        
        private const float startAngleShleif = 0.21f;
        private const float maxAngleShleif = 0.8f;
        
        [SerializeField] private GameObject _arrow;
        [SerializeField] private Image _shleif;
        
        public void SetProgress(float progress)
        {
            float angle = (Math.Abs(startAngle) + Math.Abs(maxAngle)) * progress;
            _arrow.transform.DOLocalRotate(new Vector3(0, 0, startAngle - angle), 0.1f);
            
            
            _shleif.DOFillAmount(startAngleShleif + (maxAngleShleif - startAngleShleif) * progress, 0.1f);
        }
    }
}