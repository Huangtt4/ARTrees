using UnityEngine;
using Mapbox.Utils;
using Mapbox.Unity.Map;
using Mapbox.Unity.MeshGeneration.Factories;
using Mapbox.Unity.Utilities;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;

public class AllIndicatorSpawn : MonoBehaviour
{
    [SerializeField]
    AbstractMap _map;

    public static Vector2d currentLoc = new Vector2d();
    public static Vector2d futureLoc = new Vector2d();
    public static Vector2d removedLoc = new Vector2d();

    [SerializeField]
    float _spawnScale = 1.5f;

    [SerializeField]
    GameObject _currentIndicator;
    [SerializeField]
    GameObject _futureIndicator;
    [SerializeField]
    GameObject _removedIndicator;

    [SerializeField]
    GameObject _currentParent;
    [SerializeField]
    GameObject _futureParent;
    [SerializeField]
    GameObject _removedParent;

    GameObject currentSpawn;
    GameObject futureSpawn;
    GameObject removedSpawn;

    private void Start()
    {
        currentLoc.x = 0;
        currentLoc.y = 0;
        futureLoc.x = 0;
        futureLoc.y = 0;
        removedLoc.x = 0;
        removedLoc.x = 0;

        currentSpawn = Instantiate(_currentIndicator, _map.GeoToWorldPosition(currentLoc, true), Quaternion.identity);
        currentSpawn.transform.localPosition=_map.GeoToWorldPosition(currentLoc, true);
        currentSpawn.transform.localScale = new Vector3(_spawnScale, _spawnScale, _spawnScale);
        currentSpawn.transform.parent = _currentParent.transform;

        removedSpawn = Instantiate(_removedIndicator, _map.GeoToWorldPosition(removedLoc, true), Quaternion.identity);
        removedSpawn.transform.localPosition = _map.GeoToWorldPosition(removedLoc, true);
        removedSpawn.transform.localScale = new Vector3(_spawnScale, _spawnScale, _spawnScale);
        removedSpawn.transform.parent = _removedParent.transform;

        futureSpawn = Instantiate(_futureIndicator, _map.GeoToWorldPosition(futureLoc, true), Quaternion.identity);
        futureSpawn.transform.localPosition = _map.GeoToWorldPosition(futureLoc, true);
        futureSpawn.transform.localScale = new Vector3(_spawnScale, _spawnScale, _spawnScale);
        futureSpawn.transform.parent = _futureParent.transform;
    }

    private void Update()
    {
        currentSpawn.transform.localPosition = _map.GeoToWorldPosition(currentLoc, true);
        currentSpawn.transform.localScale = new Vector3(_spawnScale, _spawnScale, _spawnScale);
        currentSpawn.transform.parent = _currentParent.transform;

        removedSpawn.transform.localPosition = _map.GeoToWorldPosition(removedLoc, true);
        removedSpawn.transform.localScale = new Vector3(_spawnScale, _spawnScale, _spawnScale);
        removedSpawn.transform.parent = _removedParent.transform;

        futureSpawn.transform.localPosition = _map.GeoToWorldPosition(futureLoc, true);
        futureSpawn.transform.localScale = new Vector3(_spawnScale, _spawnScale, _spawnScale);
        futureSpawn.transform.parent = _futureParent.transform;

    }

}
