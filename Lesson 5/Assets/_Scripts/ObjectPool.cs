using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour {

    public int count;
    public GameObject prefab;
    private List<GameObject> objects = new List<GameObject>();

    public void Initialization()
    {
        for (int i = 0; i < count; i++)
        {
            GameObject temp = Instantiate(prefab, transform.position, Quaternion.identity,transform) as GameObject;
            objects.Add(temp);
            temp.SetActive(false);
        }
    }

    public GameObject GetObject(Vector3 pos, Quaternion rotation){

        GameObject temp = null;
        for (int i = 0; i < objects.Count; i++)
        {
            if (!objects[i].gameObject.activeSelf)
            {
                temp = objects[i];
                break;
            }
        }

        if (temp == null) return null;

        temp.transform.position = pos;
        temp.transform.rotation = rotation;
        temp.SetActive(true);
        return temp;
    }
}
