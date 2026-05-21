using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace PlayerScripts.Shooting
{
    /// <summary>
    /// Component used to pool one type of object. Pooling a different object requires another instance of BulletPooling.
    /// </summary>
    public class BulletPooling : MonoBehaviour
    {
        public List<Bullet> pooledObjects;
        public GameObject objectToPool;
        public int amountToPool;

        private void Start()
        {
            // Create a list of bullets
            pooledObjects = new List<Bullet>();
            
            // Create n bullets
            for (var i = 0; i < amountToPool; i++)
            {
                CreateBullet();
            }
        }

        // Return inactive Bullets
        public Bullet GetPooledObject()
        {
            // Go through the whole list
            foreach (var t in pooledObjects)
            {
                // If the object is inactive, it means it is currently unused, so it should be shot as the next bullet
                if (!t.gameObject.activeInHierarchy)
                {
                    return t;
                }
            }
            
            // If there are no free bullets, create a new one and return it
            CreateBullet();
            return pooledObjects.Last();
        }

        private void CreateBullet()
        {
            // Create instance
            var tmp = Instantiate(objectToPool);
            // Disable it
            tmp.SetActive(false);
            // Add it to the list
            pooledObjects.Add(tmp.GetComponent<Bullet>());
        }
    }
}
