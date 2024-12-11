using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.Accessibility;

public class RoadGenerator : MonoBehaviour
{
    [SerializeField] private Transform _createAt;
    [SerializeField] private Transform _destroyer;
    public GameObject RoadPrefab;

    public GameObject[] RoadPrefabs;

    private List<GameObject> roads = new List<GameObject>();
    public float maxSpeed = 10;
    private float speed = 0;
    public int maxRoadCount = 3;

    private void Start()
    {
        ResetRoads();
        StartRoads();
    }

    private void Update()
    {
        if (speed == 0) return;

        foreach (var road in roads)
        {
            road.transform.position -= new Vector3 (0, 0, speed * Time.deltaTime);
        }
        if (roads[0].transform.position.z <= _destroyer.transform.position.z)
        {
            Destroy(roads[0]);
            roads.RemoveAt(0);

            CreateNextRoad();
        }
    }

    private void CreateNextRoad()
    {
        int randIdPrefab = Random.Range(0, RoadPrefabs.Length);

        Vector3 pos = _createAt.position;

        if (roads.Count > 0)
        {
            pos = roads[roads.Count - 1].transform.position + new Vector3(0,0,100);
        }

        GameObject platformFromList = RoadPrefabs[randIdPrefab];

        var platform = Instantiate(platformFromList, pos, Quaternion.identity);
        

        platform.transform.SetParent(transform);
        roads.Add(platform);
    }

    public void StartRoads()
    {
        speed = maxSpeed;
    }

    public void ResetRoads()
    {
        speed = 0;

        while (roads.Count > 0)
        {
            Destroy(roads[0]);
            roads.RemoveAt(0);
        }
        for(int i = 0; i < maxRoadCount; i++)
        {
            CreateNextRoad();
        }
    }
}
