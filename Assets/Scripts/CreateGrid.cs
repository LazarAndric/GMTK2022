using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateGrid : MonoBehaviour
{
    public static CreateGrid Instance;
    public List<PlaneBehaviour> PlaneBehaviours = new List<PlaneBehaviour>();
    public Texture SurpriseTexture;
    public Texture Explosion;
    public Texture MoveTo;
    public Texture AddTimeTexture;
    public Texture RemoveTimeTexture;
    public Texture AddLifeTexture;
    public Texture RemoveLifeTexture;
    public Texture SpawnEnemyTexture;
    public Texture DestroyEnemyTexture;
    private void Awake()
    {
        if(Instance==null)
            Instance = this;
        initializePlaneBehaviour();
        initializeGrid();
    }
    public void initializePlaneBehaviour()
    {
        //PlaneType initialization
        PlaneType normalType = new PlaneType(Type.Normal, null);
        PlaneType surpriseType = new PlaneType(Type.Surprise, SurpriseTexture);

        //PlaneFunctionality initialization
        PlaneFunctionality addTimeNormalFunctionality = new PlaneFunctionality(Functionality.AddTime, AddTimeTexture, () => Debug.Log("Add time"), 2, false);
        PlaneFunctionality emptyFunctionality = new PlaneFunctionality(Functionality.Empty, null, () => Debug.Log("Its empty"), 0, false);
        PlaneFunctionality emptyNormalFunctionality = new PlaneFunctionality(Functionality.Empty, null, () => Debug.Log("Its empty"), 3, false);
        PlaneFunctionality removeTimeNormalFunctionality = new PlaneFunctionality(Functionality.RemoveTime, RemoveTimeTexture, () => Debug.Log("Remove time"), 3, false);
        PlaneFunctionality timer1Functionality = new PlaneFunctionality(Functionality.TimerToDeath, Explosion, () => Debug.Log("Exploded"), 1, true);
        PlaneFunctionality timer2Functionality = new PlaneFunctionality(Functionality.TimerToDeath, Explosion, () => Debug.Log("Exploded"), 2, true);
        PlaneFunctionality timer3Functionality = new PlaneFunctionality(Functionality.TimerToDeath, Explosion, () => Debug.Log("Exploded"), 3, true);
        PlaneFunctionality deathFunctionality = new PlaneFunctionality(Functionality.Death, Explosion, () => Debug.Log("Exploded"), 0, true);
        PlaneFunctionality moveToFunctionality = new PlaneFunctionality(Functionality.MoveTo, MoveTo, () => Debug.Log("Move to"), 0, false);
        PlaneFunctionality addTimeFunctionality = new PlaneFunctionality(Functionality.AddTime, AddTimeTexture, () => Debug.Log("Add time"), 0, false);
        PlaneFunctionality removeTimeFunctionality = new PlaneFunctionality(Functionality.RemoveTime, RemoveTimeTexture, () => Debug.Log("Remove time"), 0, false);
        PlaneFunctionality addLifeFunctionality = new PlaneFunctionality(Functionality.AddLife, AddLifeTexture, () => Debug.Log("Add life"), 0, false);
        PlaneFunctionality removeLifeFunctionality = new PlaneFunctionality(Functionality.RemoveLife, RemoveLifeTexture, () => Debug.Log("Remove life"), 0, false);
        PlaneFunctionality spawnEnemyFunctionality = new PlaneFunctionality(Functionality.SpawnEnemy, AddLifeTexture, () => Debug.Log("Spawn enemy"), 0, false);
        PlaneFunctionality destroyEnemyFunctionality = new PlaneFunctionality(Functionality.DestroyEnemy, RemoveLifeTexture, () => Debug.Log("Destroy enemy"), 0, false);

        PlaneBehaviours.Add(new PlaneBehaviour(removeTimeNormalFunctionality, normalType));
        PlaneBehaviours.Add(new PlaneBehaviour(emptyNormalFunctionality, normalType));
        PlaneBehaviours.Add(new PlaneBehaviour(timer1Functionality, normalType));
        PlaneBehaviours.Add(new PlaneBehaviour(timer2Functionality, normalType));
        PlaneBehaviours.Add(new PlaneBehaviour(timer3Functionality, normalType));
        PlaneBehaviours.Add(new PlaneBehaviour(addTimeNormalFunctionality, normalType));

        PlaneBehaviours.Add(new PlaneBehaviour(emptyFunctionality, surpriseType));
        PlaneBehaviours.Add(new PlaneBehaviour(deathFunctionality, surpriseType));
        PlaneBehaviours.Add(new PlaneBehaviour(moveToFunctionality, surpriseType));
        PlaneBehaviours.Add(new PlaneBehaviour(addTimeFunctionality, surpriseType));
        PlaneBehaviours.Add(new PlaneBehaviour(removeTimeFunctionality, surpriseType));
        PlaneBehaviours.Add(new PlaneBehaviour(addLifeFunctionality, surpriseType));
        PlaneBehaviours.Add(new PlaneBehaviour(removeLifeFunctionality, surpriseType));
        PlaneBehaviours.Add(new PlaneBehaviour(spawnEnemyFunctionality, surpriseType));
        PlaneBehaviours.Add(new PlaneBehaviour(destroyEnemyFunctionality, surpriseType));

    }
    public void initializeGrid()
    {
        int x = 0;
        int y = 0;
        int tempI = 0;
        Vector3 lastPosition = Vector3.zero;
        int length = NumberOfCells * NumberOfCells;
        for (int i = 0; i < length; i++)
        {
            if (tempI >= NumberOfCells)
            {
                tempI = 0;
                x++;
                y = 0;
                lastPosition += new Vector3(1 + offsetX, 0, 0);
                lastPosition = new Vector3(lastPosition.x, lastPosition.y, 0);
            }
            PlaneHandler cube = Instantiate(SpawnPrefab, transform);
            cube.initializePlane(PlaneBehaviours[Random.Range(0, PlaneBehaviours.Count)], RotationAngle, Duration, WaitTime);
            cube.transform.position = lastPosition;
            AddPlaneToGraph(cube.gameObject, i);
            Positions.Add(new Vector2(x, y), cube.transform);
            lastPosition = cube.transform.position + new Vector3(0, 0, 1 + offsetY);
            tempI++;
            y++;
        }
        GraphBuilder.CreateGraph(waypoitns, offsetX, offsetY);

    }
    public PlaneHandler SpawnPrefab;
    public int NumberOfCells;
    public float offsetX;
    public float offsetY;
    public int RotationAngle;
    public float Duration;
    public float WaitTime;

    public Dictionary<Vector2, Transform> Positions = new Dictionary<Vector2, Transform>();

    private List<Waypoint> waypoitns = new List<Waypoint>();

    private void Start()
    {
        
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
        Waypoint waypoint = cube.AddComponent<Waypoint>();
        waypoint.Id = id;
        waypoitns.Add(waypoint);
    }
}
