using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateGrid : MonoBehaviour
{
    public GameObject SpawnPrefab;
    public int NumberOfCells;

    private void Start()
    {
        int x=0;
        int y = 0;
        int length = NumberOfCells * NumberOfCells;
        for (int i = 0; i < length; i++)
        {
            if (x >= NumberOfCells) return;
            if (y >= NumberOfCells) { x++; y = 0; }
            GameObject cube=Instantiate(SpawnPrefab);
            cube.transform.position = new Vector3(x, 0, y);
            y++;
        }
    }

}
