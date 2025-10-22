using Assets._Project._scripts._core;
using Assets._Project._scripts.Levels;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class RoadGenerator : Singleton<RoadGenerator>
{
    [SerializeField] private float _maxSpeed = 10;
    [SerializeField] private int _maxRoadCount = 5;
    [SerializeField] private bool _endless = true;

    [SerializeField] private Level _level;
    private int _currentSegment;

    private List<LevelSegment> roads = new();
    private float speed;

    void Start()
    {
        
        _level.Initialize();
        ResetLevel();
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
            CreateNextRoad(_endless);
        }
    }

    public void StopMoving()
    {
        speed = 0;
    }

    public void StartMoving()
    {
        speed = _maxSpeed;
    }

    public void StartLevel()
    {
        //UIController.Instance.SwitchTo(UIController.UIMode.GameplayMode);
        StartCoroutine(Run());
    }

    private IEnumerator Run()
    {
        yield return new WaitForSeconds(.7f);

        StartMoving();
        SwipeManager.Instance.enabled = true;
    }

    public void ResetLevel()
    {
        SwipeManager.Instance.enabled = false;
        _currentSegment = 0;
        StopMoving();
        //UIController.Instance.SwitchTo(UIController.UIMode.MenuMode);

        while (roads.Count > 0)
        {
            Destroy(roads[0].gameObject);
            roads.RemoveAt(0);
        }

        for (int i = 0; i < _maxRoadCount; i++)
        {
            CreateNextRoad(_endless);
        }
    }

    private void CreateNextRoad(bool endless)
    {
        Vector3 pos = Vector3.zero;

        LevelSegment nextSegment = _level.GetSegment(_currentSegment, endless);
        if (nextSegment == null)
        {
            Debug.Log("Уровень закончен");
            return;
        }

        if (roads.Count > 0)
        {
            LevelSegment lastSegment = roads[roads.Count - 1];
            float offset = (lastSegment.Length / 2f) + (nextSegment.Length / 2f);
            pos = lastSegment.transform.position + new Vector3(0, 0, offset);
        }

        LevelSegment newSegment = Instantiate(nextSegment, pos, Quaternion.identity);
        newSegment.transform.SetParent(transform);
        roads.Add(newSegment);

        _currentSegment++;
    }
}
