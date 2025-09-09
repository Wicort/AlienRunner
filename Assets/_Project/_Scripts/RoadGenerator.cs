using Assets._Project._scripts.Levels;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class RoadGenerator : MonoBehaviour
{
    public static RoadGenerator Instance;
    public float maxSpeed = 10;
    public int maxRoadCount = 5;
    public GameObject menuUI;

    [SerializeField] private Level _level;
    private int _currentSegment;

    private List<LevelSegment> roads = new();
    private float speed;

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        _currentSegment = 1;
        _level.Initialize();
        ResetLevel();
        SwipeManager.Instance.enabled = false;
    }

    void Update()
    {
        if (EventSystem.current.IsPointerOverGameObject()) return;

        if (speed == 0) return;

        foreach (LevelSegment road in roads)
        {
            road.transform.position -= new Vector3(0, 0, speed * Time.deltaTime);
        }

        if (roads[0].transform.position.z < -1 * roads[0].Length)
        {
            Destroy(roads[0].gameObject);
            roads.RemoveAt(0);
            CreateNextRoad();
        }

    }

    public void StartLevel()
    {
        speed = maxSpeed;
        menuUI.SetActive(false);
        SwipeManager.Instance.enabled = true;
    }

    public void ResetLevel()
    {
        speed = 0;
        menuUI.SetActive(true);

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

        LevelSegment nextSegment = _level.GetSegment(_currentSegment);

        if (roads.Count > 0)
        {
            pos = roads[roads.Count - 1].transform.position + new Vector3(0, 0, nextSegment.Length);
        }

        if (nextSegment != null)
        {
            LevelSegment segment = Instantiate(nextSegment, pos, Quaternion.identity);
            segment.transform.SetParent(transform);
        
            roads.Add(segment);

            _currentSegment++;
        } else
        {
            Debug.Log("Уровень зщакончен");
        }
    }
}
