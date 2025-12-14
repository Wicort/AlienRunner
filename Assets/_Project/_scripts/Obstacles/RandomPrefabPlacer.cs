using System;
using System.Collections.Generic;
using UnityEngine;

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
    }

    public class RandomPrefabPlacer : MonoBehaviour
    {
        [SerializeField] private GameObject _obstaclePrefab;
        private static float _offset = 2f;
        private static float _roadPosition = -5.5f;

        private static readonly Dictionary<PlaceType, Vector3[]> _placementPoints = new()
        {
            { PlaceType.LEFT,        new[] { new Vector3(-1 * _offset, 0, _roadPosition) } },
            { PlaceType.MIDDLE,      new[] { new Vector3(0, 0, _roadPosition) } },
            { PlaceType.RIGHT,       new[] { new Vector3(_offset, 0, _roadPosition) } },
            { PlaceType.LEFTMIDDLE,  new[] { new Vector3(-1 * _offset, 0, _roadPosition), new Vector3(0, 0, _roadPosition) } },
            { PlaceType.RIGHTMIDDLE, new[] { new Vector3(0, 0, _roadPosition), new Vector3(_offset, 0, _roadPosition) } },
            { PlaceType.LEFTRIGHT,   new[] { new Vector3(-1 * _offset, 0, _roadPosition), new Vector3(_offset, 0, _roadPosition) } },
        };

        private void Start()
        {
            PlaceType placeType = GetRandomPlaceType();
            PlacePrefab(placeType);
        }

        private PlaceType GetRandomPlaceType()
        {
            int ind = UnityEngine.Random.Range(0, Enum.GetValues(typeof(PlaceType)).Length);

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

                GameObject obstacle = Instantiate(_obstaclePrefab, transform.position + point, Quaternion.identity);
                obstacle.transform.parent = transform;
            }
        }
    }
}
