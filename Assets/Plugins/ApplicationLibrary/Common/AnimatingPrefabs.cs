using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.ApplicationLibrary.Common
{
    [CreateAssetMenu(fileName = "AnimatingPrefabs", menuName = "Application/AnimatingPrefabsList")]
    public class AnimatingPrefabs : ScriptableObject
    {
        [SerializeField] private List<AnimatingObject> _objects;
        [SerializeField] private Image _prefab;
        
        public GameObject GetPrefab(AnimateObjectType key)
        {
            _prefab.sprite = GetSprite(key);
            return _prefab.gameObject;
        }

        public Sprite GetSprite(AnimateObjectType key)
        {
            return _objects.Find(x => x.Name == key).Image;
        }

        [System.Serializable]
        private struct AnimatingObject
        {
            public Sprite Image;
            public AnimateObjectType Name;
        }
        
    }
}
