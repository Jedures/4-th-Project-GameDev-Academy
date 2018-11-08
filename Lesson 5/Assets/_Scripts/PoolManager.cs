using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour {

    public static PoolManager _instance = null;

    public ObjectPool[] pools;

    private void Awake()
    {
        if (_instance == null) _instance = this;
        else Destroy(this);
    }

    private void Start()
    {
        Initialization();
    }

    public void Initialization()
    {
        for (int i = 0; i < pools.Length; i++) pools[i].Initialization();
    }

    public ObjectPool GetPool(int i)
    {
        if (i >= pools.Length || i < 0) return null;
        else return pools[i];
    }


}
