using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateGrid : MonoBehaviour
{
    public static CreateGrid Instance;
    private void Awake()
    {
            if(Instance==null)
                Instance = this;
    }
    public GameObject SpawnPrefab;
    public int NumberOfCells;
    public float offsetX;
    public float offsetY;
    public Dictionary<Vector2, Transform> Positions = new Dictionary<Vector2, Transform>();

    private List<Waypoint> waypoitns = new List<Waypoint>();

    private void Start()
    {
        int x = 0;
        int y = 0;
        int tempI=0;
        Vector3 lastPosition=Vector3.zero;
        int length = NumberOfCells * NumberOfCells;
        for (int i = 0; i < length; i++)
        {
            if(tempI>=NumberOfCells)
            {
                tempI=0;
                x++;
                y = 0;
                lastPosition += new Vector3(1 + offsetX, 0, 0);
                lastPosition = new Vector3(lastPosition.x, lastPosition.y,0);
            }
            GameObject cube=Instantiate(SpawnPrefab, transform);
            cube.transform.position = lastPosition;
            AddPlaneToGraph(cube, i);
            Positions.Add(new Vector2(x,y),cube.transform);
            lastPosition = cube.transform.position + new Vector3(0, 0, 1 + offsetY);
            tempI++;
            y++;
        }
        GraphBuilder.CreateGraph(waypoitns, offsetX, offsetY);
    }
    //[0,0]
    //[4,4]
    public bool tryGetPosition(Vector2 cordinate, out Vector3 position)
    {
        position = Vector3.zero;
        if (Positions.TryGetValue(cordinate, out Transform trans)){
            position = trans.position;
            return true;
        }
        return false;
    }
    private void AddPlaneToGraph(GameObject cube, int id)
    {
        cube.AddComponent<Waypoint>();
        cube.GetComponent<Waypoint>().Id = id;
        waypoitns.Add(cube.GetComponent<Waypoint>());
    }
}
