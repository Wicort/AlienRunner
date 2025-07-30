using NUnit.Framework;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class RoadGenerator : MonoBehaviour
{
    public static RoadGenerator Instance;
    public GameObject RoadPrefab;
    public float maxSpeed = 10;
    public int maxRoadCount = 5;

    private List<GameObject> roads = new();
    private float speed;

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        ResetLevel();
        SwipeManager.Instance.enabled = false;
    }

    void Update()
    {
        if (EventSystem.current.IsPointerOverGameObject()) return;

        if (speed == 0) return;

        foreach (GameObject road in roads)
        {
            road.transform.position -= new Vector3(0, 0, speed * Time.deltaTime);
        }

        if (roads[0].transform.position.z < -15)
        {
            Destroy(roads[0]);
            roads.RemoveAt(0);
            CreateNextRoad();
        }

    }

    public void StartLevel()
    {
        speed = maxSpeed;
        SwipeManager.Instance.enabled = true;
    }

    public void ResetLevel()
    {
        speed = 0;

        while (roads.Count > 0)
        {
            Destroy(roads[0]);
            roads.RemoveAt(0);
        }

        for (int i = 0; i < maxRoadCount; i++)
        {
            CreateNextRoad();
        }
        
    }

    private void CreateNextRoad()
    {
        Vector3 pos = Vector3.zero;

        if (roads.Count > 0)
        {
            pos = roads[roads.Count - 1].transform.position + new Vector3(0, 0, 15);
        }

        GameObject go = Instantiate(RoadPrefab, pos, Quaternion.identity);
        go.transform.SetParent(transform);
        
        roads.Add(go);
    }
}
