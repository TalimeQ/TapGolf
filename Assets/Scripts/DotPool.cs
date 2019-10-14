using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Simplified as we pool only trajectory dots

public class DotPool : MonoBehaviour {

    [SerializeField] private GameObject pooledObject;

    private List<GameObject> objectPool =  new List<GameObject>();

    public GameObject GetObjectFromPool()
    {
        foreach(GameObject checkedObject in objectPool)
        {
            bool isObjectAvalibleToTake = !checkedObject.activeInHierarchy;
            if (isObjectAvalibleToTake)
            {
                return checkedObject;
            }
        }
        return CreateObject();
    }

    public void ResetPool()
    {
        foreach(GameObject deactivatedObject in objectPool)
        {
            deactivatedObject.SetActive(false);
        }
    }

    private GameObject CreateObject()
    {
        GameObject createdObject = Instantiate(pooledObject);
        objectPool.Add(createdObject);
        return pooledObject;
    }
}
