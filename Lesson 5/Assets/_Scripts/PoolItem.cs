using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolItem : MonoBehaviour {

	public void ReturnPool()
    {
        gameObject.SetActive(false);
    }
}
