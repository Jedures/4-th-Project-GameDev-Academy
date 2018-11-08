using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateObjects : MonoBehaviour {

    public GameObject prefab;
    private Vector3 spawnCoords = Vector3.zero;
    private GameObject player;
    public GameObject player1;
    public GameObject player2;
    
    private const float planesLength = 10f;
    private bool isStart = false;
    private List<GameObject> planes = new List<GameObject>();
    private List<GameObject> enemies = new List<GameObject>();
    private List<GameObject> coins = new List<GameObject>();

    public void Start()
    {
        player = Instantiate(player1, new Vector3(spawnCoords.x, 0.5f, spawnCoords.z), Quaternion.identity); 
        GameObject go1 = PoolManager._instance.GetPool(Random.Range(0, 3)).GetObject(spawnCoords, Quaternion.identity);
        planes.Add(go1);
        spawnCoords.x += 10;
        GameObject go2 = PoolManager._instance.GetPool(Random.Range(0, 3)).GetObject(spawnCoords, Quaternion.identity);
        planes.Add(go2);
    }

    public void StartGame (int i) {
        if (i == 1)  player = Instantiate(player1, new Vector3(spawnCoords.x, 0.5f, spawnCoords.z), Quaternion.identity); 
        else player = Instantiate(player2, new Vector3(spawnCoords.x, 0.5f, spawnCoords.z), Quaternion.identity); 
        isStart = true;
    }
	
	
	void Update () {

        if(isStart)PlaneController();
          
    }

    private void PlaneController()
    {
        if ((player.transform.position.x + planesLength) > spawnCoords.x)
        {
            spawnCoords.x += planesLength;
            GameObject go = PoolManager._instance.GetPool(Random.Range(0, 3)).GetObject(spawnCoords, Quaternion.identity);
            GameObject enemyTemp = PoolManager._instance.GetPool(3).GetObject( new Vector3(spawnCoords.x, 0, spawnCoords.z), Quaternion.identity);
            planes.Add(go);
            enemies.Add(enemyTemp);
            CoinsController();
        }

        if (planes[0].transform.position.x < (player.transform.position.x - planesLength))
        {
            planes[0].GetComponent<PoolItem>().ReturnPool();
            planes.RemoveAt(0);
        }

        if (enemies.Count > 0 && Vector3.Distance(enemies[0].transform.position, player.transform.position)> 20)
        {
            enemies[0].GetComponent<PoolItem>().ReturnPool();
            enemies.RemoveAt(0);
        }

        if (coins.Count > 0 && Vector3.Distance(coins[0].transform.position, player.transform.position) > 20)
        {
            coins[0].GetComponent<PoolItem>().ReturnPool();
            coins.RemoveAt(0);
        }

    }
    

    private void CoinsController()
    {
        for (int i = 0; i<Random.Range(0,5); i++)
        {
            GameObject Temp = PoolManager._instance.GetPool(4).GetObject(new Vector3(spawnCoords.x + Random.Range(-5, 5), 0.5f, spawnCoords.z + Random.Range(-5, 5)), new Quaternion(90,0,0,0));
            coins.Add(Temp);
        }
    }

	
}
