using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System.Linq;

public class GridHandler : MonoBehaviour
{
    public string TagForCollision;
    public static GridHandler Instance;
    public List<PlaneBehaviour> PlaneBehaviours = new List<PlaneBehaviour>();
    public Texture SurpriseTexture;
    public Texture Explosion;
    public Texture MoveTo;
    public Texture AddTimeTexture;
    public Texture RemoveTimeTexture;
    public Texture AddLifeTexture;
    public Texture SpawnEnemyTexture;
    public Texture DestroyEnemyTexture;
    public float TimerFunctionality;
    public PlaneHandler SpawnPrefab;
    public int NumberOfCells;
    public float offsetX;
    public float offsetY;
    public int RotationAngle;
    public float Duration;
    public float WaitTime;
    [Range(0f, 1f)]
    public float Difficulty;
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
        PlaneFunctionality addTimeNormalFunctionality = new PlaneFunctionality(Functionality.AddTime, AddTimeTexture, (PlaneHandler plane) => GameHandler.Instance.changeTimer(TimerFunctionality), 2, false);
        PlaneFunctionality emptyFunctionality = new PlaneFunctionality(Functionality.Empty, null, (PlaneHandler plane) => Debug.Log("Its empty"), 0, false);
        PlaneFunctionality emptyNormalFunctionality = new PlaneFunctionality(Functionality.Empty, null, (PlaneHandler plane) => Debug.Log("Its empty"), 3, false);
        PlaneFunctionality removeTimeNormalFunctionality = new PlaneFunctionality(Functionality.RemoveTime, RemoveTimeTexture, (PlaneHandler plane) => GameHandler.Instance.changeTimer(-TimerFunctionality), 3, false);
        PlaneFunctionality timer1Functionality = new PlaneFunctionality(Functionality.TimerToDeath, Explosion, (PlaneHandler plane) => Explode(plane), 1, true) ;
        PlaneFunctionality timer2Functionality = new PlaneFunctionality(Functionality.TimerToDeath, Explosion, (PlaneHandler plane) => Explode(plane), 2, true);
        PlaneFunctionality timer3Functionality = new PlaneFunctionality(Functionality.TimerToDeath, Explosion, (PlaneHandler plane) => Explode(plane), 3, true);
        PlaneFunctionality deathFunctionality = new PlaneFunctionality(Functionality.Death, Explosion, (PlaneHandler plane) => Explode(plane), 0, true);
        PlaneFunctionality moveToFunctionality = new PlaneFunctionality(Functionality.MoveTo, MoveTo, (PlaneHandler plane) => Debug.Log("Move to"), 0, false);
        PlaneFunctionality addTimeFunctionality = new PlaneFunctionality(Functionality.AddTime, AddTimeTexture, (PlaneHandler plane) => GameHandler.Instance.changeTimer(TimerFunctionality), 0, false);
        PlaneFunctionality removeTimeFunctionality = new PlaneFunctionality(Functionality.RemoveTime, RemoveTimeTexture, (PlaneHandler plane) => GameHandler.Instance.changeTimer(-TimerFunctionality), 0, false);
        PlaneFunctionality addLifeFunctionality = new PlaneFunctionality(Functionality.AddLife, AddLifeTexture, (PlaneHandler plane) => GameHandler.Instance.addLife(), 0, false);
        PlaneFunctionality spawnEnemyFunctionality = new PlaneFunctionality(Functionality.SpawnEnemy, SpawnEnemyTexture, (PlaneHandler plane) => Debug.Log("Spawn enemy"), 0, false);
        PlaneFunctionality destroyEnemyFunctionality = new PlaneFunctionality(Functionality.DestroyEnemy, DestroyEnemyTexture, (PlaneHandler plane) => Debug.Log("Destroy enemy"), 0, false);

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
        var list=RandomUtil.getAvgRandom(length, 0, PlaneBehaviours.Count - 1, Difficulty);
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
            cube.initializePlane(PlaneBehaviours[list[i]], RotationAngle, Duration, WaitTime, TagForCollision);
            cube.transform.position = lastPosition;
            AddPlaneToGraph(cube.gameObject, i);
            Positions.Add(new Vector2(x, y), cube.transform);
            lastPosition = cube.transform.position + new Vector3(0, 0, 1 + offsetY);
            tempI++;
            y++;
        }
        GraphBuilder.CreateGraph(waypoitns, offsetX, offsetY);

    }

    public Dictionary<Vector2, Transform> Positions = new Dictionary<Vector2, Transform>();

    private List<Waypoint> waypoitns = new List<Waypoint>();

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
    public GameObject ObjectForDestroy;
    private void Explode(PlaneHandler plane)
    {      
        GameObject player = plane.CurrentObject;
        if (player != null)
        {
            player.transform.DOKill();
            player.GetComponent<PlayerHandler>().CanMove = false; 
            player.GetComponent<Rigidbody>().useGravity = true;      
            GameHandler.Instance.removeLife();
        }  
        //remove node from graph
        Waypoint waypoint = plane.Cube.GetComponentInParent<Waypoint>();
        GraphBuilder.Graph.RemoveNode(waypoint);
        //remove transform from dictanory
        var myKey = Positions.FirstOrDefault(x => x.Value == waypoint.transform).Key;
        Positions.Remove(myKey);  

        CheckGameOver.GameOver(GraphBuilder.Graph, waypoint);
        ObjectForDestroy = plane.gameObject;
        plane.Platofrm.SetActive(false);
        plane.Faces.ForEach(face => face.gameObject.SetActive(false));
        plane.AnimationHandler.startAnimation(AnimationType.Death, onDone);
    }
    public void onDone()
    {
        Destroy(ObjectForDestroy);
    }
}
