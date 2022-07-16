using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField]
    int numberOfEnemies = 1;
    [SerializeField]
    GameObject enemyPrefab;

    static GameObject staticPrefab;
    static GameObject plane;
    static List<GameObject> enemies = new List<GameObject>();
    static int numberOfCells;
    private void Start()
    {
        staticPrefab = enemyPrefab;
        plane =   GameObject.FindGameObjectWithTag("Plane");
        int numberOfCells = plane.transform.childCount - 1;
        
        
        for (int i = 0; i < numberOfEnemies; i++)
        {
            int random = Random.Range(0, numberOfCells);
            GameObject enemy = Instantiate<GameObject>(enemyPrefab, plane.transform.GetChild(random).position, Quaternion.identity);
            enemies.Add(enemy);        
        }
    }
    
    public static void DestroyEnemy()
    {
        if(enemies.Count > 0)
        {
            enemies.RemoveAt(0);
        }
    }
    public static void SpawnEnemy()
    {
        int random = Random.Range(0, numberOfCells);
        GameObject enemy = Instantiate<GameObject>(staticPrefab, plane.transform.GetChild(random).position, Quaternion.identity);
        enemies.Add(enemy);
    }

}
