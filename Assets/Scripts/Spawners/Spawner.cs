using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG_Project
{
    public class Spawner : MonoBehaviour
    {
        public GameObject SpawnObject(GameObject prefab, Vector3 pos)
        {
            return Instantiate(prefab, pos, Quaternion.identity) as GameObject;
        }

        public T SpawnObject<T>(GameObject prefab, Vector3 pos) where T : Component
        {
            var obj = SpawnObject(prefab, pos);

            if (obj != null) return obj.GetComponent<T>();

            return null;
        }

        public virtual void Spawn()
        {

        }
    }
}