using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Assets._Project._scripts.Obstacles
{
    public enum PlaceType 
    {
        LEFT = 0,
        MIDDLE = 1,
        RIGHT = 2,
        LEFTMIDDLE = 3,
        RIGHTMIDDLE = 4,
        LEFTRIGHT = 5,
        ALL = 6,
    }

    public class RandomPrefabPlacer : MonoBehaviour
    {
        [SerializeField] private GameObject[] _obstacles;
        private static float _offset = 2f;
        private static float _roadPosition = -5.5f;
        private static Vector3 _leftPosition = new Vector3(-1 * _offset, 0, _roadPosition);
        private static Vector3 _rightPosition = new Vector3(_offset, 0, _roadPosition);
        private static Vector3 _middlePosition = new Vector3(0, 0, _roadPosition);

        private static readonly Dictionary<PlaceType, Vector3[]> _placementPoints = new()
        {
            { PlaceType.LEFT,        new[] { _leftPosition } },
            { PlaceType.MIDDLE,      new[] { _middlePosition } },
            { PlaceType.RIGHT,       new[] { _rightPosition } },
            { PlaceType.LEFTMIDDLE,  new[] { _leftPosition, _middlePosition } },
            { PlaceType.RIGHTMIDDLE, new[] { _rightPosition, _middlePosition } },
            { PlaceType.LEFTRIGHT,   new[] { _leftPosition, _rightPosition } },
            { PlaceType.ALL,         new[] { _leftPosition, _middlePosition, _rightPosition } },
        };

        private void Start()
        {
            PlaceType placeType = GetRandomPlaceType();
            PlacePrefab(placeType);
        }

        private PlaceType GetRandomPlaceType()
        {
            int ind = Random.Range(0, Enum.GetValues(typeof(PlaceType)).Length);

            return (PlaceType)ind;
        }

        private void PlacePrefab(PlaceType placeType)
        {
            if (!_placementPoints.TryGetValue(placeType, out Vector3[] points))
            {
                Debug.LogWarning($"Неизвестный PlaceType: {placeType}");
                return;
            }

            foreach (Vector3 point in points)
            {

                GameObject obstacle = Instantiate(GetRandomObstacle(), transform.position + point, Quaternion.identity);
                obstacle.transform.parent = transform;
            }
        }

        private GameObject GetRandomObstacle()
        {
            return _obstacles[Random.Range(0, _obstacles.Length)];
        }
    }
}
