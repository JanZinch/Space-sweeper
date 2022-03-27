using UnityEngine;

namespace CodeBase.ApplicationLibrary.Collections
{
    public class GameObjectPool : Pool<GameObject>
    {
        private readonly GameObject _gameObject;
        private readonly Transform _parentTransform;

        public GameObjectPool(GameObject gameObject, Transform parentTransform = null) :
            this(gameObject, 16, int.MaxValue, parentTransform)
        {
        }

        public GameObjectPool(GameObject gameObject, int initialCapacity, Transform parentTransform = null) :
            this(gameObject, initialCapacity, int.MaxValue, parentTransform)
        {
        }

        public GameObjectPool(GameObject gameObject, int initialCapacity, int max, Transform parentTransform = null) :
            base(initialCapacity, max)
        {
            _gameObject = gameObject;
            _parentTransform = parentTransform;
        }

        protected override GameObject NewObject()
        {
            return Object.Instantiate(_gameObject, _parentTransform, true);
        }

        protected virtual void Destroy(GameObject poolObject)
        {
            Object.Destroy(poolObject);
        }
    }
}