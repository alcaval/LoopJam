/*
    Template adaptado de https://github.com/justinwasilenko/Unity-Style-Guide#classorganization
    Hay mas regiones pero por tal de que sea legible de primeras he puesto solo unas pocas
    y algun ejemplo.
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LoopJam
{
    /// <summary>  
	/// Brief summary of what the class does
	/// </summary>
    public class TurretPool : MonoBehaviour
    {
        public static TurretPool SharedInstance;
        public List<GameObject> pooledObjects;
        public GameObject objectToPool;
        public int amountToPool;

        void Awake()
        {
            SharedInstance = this;
        }

        void Start()
        {
            pooledObjects = new List<GameObject>();
            GameObject tmp;
            for (int i = 0; i < amountToPool; i++)
            {
                tmp = Instantiate(objectToPool, this.transform);
                tmp.SetActive(false);
                pooledObjects.Add(tmp);
            }
        }


        public GameObject GetPooledObject()
        {
            for (int i = 0; i < amountToPool; i++)
            {
                if (!pooledObjects[i].activeInHierarchy)
                {
                    return pooledObjects[i];
                }
            }
            return null;
        }

        public List<GameObject> GetActiveObjects()
        {
            List<GameObject> active = new List<GameObject>();
            for(int i=0; i<amountToPool; ++i)
            {
                if (pooledObjects[i].activeInHierarchy)
                {
                    active.Add(pooledObjects[i]);
                }
            }

            return active;
        }
    }
}
