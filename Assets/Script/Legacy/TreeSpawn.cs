using UnityEngine;
using Mapbox.Utils;
using Mapbox.Unity.Map;
using Mapbox.Unity.MeshGeneration.Factories;
using Mapbox.Unity.Utilities;
using System.Collections.Generic;
using Mapbox.Unity.Location;
using UnityEngine.UIElements;

public class TreeSpawn : MonoBehaviour
{
	[SerializeField]
	AbstractMap _map;

	[SerializeField]
	[Geocode]
	private List<string> _locationStrings = new List<string>();
	Vector2d[] _locations;

	[SerializeField]
	float _spawnScale = 1f;

	[SerializeField]
	GameObject _markerPrefab;

	List<GameObject> _spawnedObjects = new List<GameObject>();


	private float time = 50.0f;

	[SerializeField]
	GameObject treeParent;

	private double lastLon;
	private double lastLa;



	void Start()
	{
		UpdateObjects();
		
	}

	private void Update()
	{


		if (time == 50)
		{
			lastLa = GPSHandeler.Instance.latitude;
			lastLon = GPSHandeler.Instance.longitude;
		}

		if (time >= 60f && time != -1)
		{
			destroyTrees();
			UpdateObjects();
			time = -1;
		}
		time += Time.deltaTime;

		if (GPSHandeler.Instance.latitude - lastLa >= 0.0005 || GPSHandeler.Instance.longitude - lastLon >= 0.0005)
		{
			destroyTrees();
			UpdateObjects();
			lastLa = GPSHandeler.Instance.latitude;
			lastLon = GPSHandeler.Instance.longitude;
		}

		int count = _spawnedObjects.Count;
		for (int i = 0; i < count; i++)
		{
			var spawnedObject = _spawnedObjects[i];
			var location = _locations[i];
			spawnedObject.transform.localPosition = _map.GeoToWorldPosition(location, true);
			spawnedObject.transform.localScale = new Vector3(_spawnScale, _spawnScale, _spawnScale);
		}
	}

	private void PrepareJSON()
	{
		_locationStrings = new List<string>();
		//TreeParser.readJson("good");

		int size = (TreeParser.getMyTrees() != null)? TreeParser.getMyTrees().Count : 0;
		for (int i = 0; i < size; i++)
		{
			string location = TreeParser.getMyTrees()[i].CoordinatesY + "," + TreeParser.getMyTrees()[i].CoordinatesX;
			_locationStrings.Add(location);
			//Debug.Log(location);

		}
	}

	private void UpdateObjects()
	{
		
		PrepareJSON();
		_locations = new Vector2d[_locationStrings.Count];
		_spawnedObjects = new List<GameObject>();
		//Info UIInfo = gameObject.GetComponent<Info>();
		for (int i = 0; i < _locationStrings.Count; i++)
		{
			var locationString = _locationStrings[i];
			_locations[i] = Conversions.StringToLatLon(locationString);
			var instance = Instantiate(_markerPrefab, _map.GeoToWorldPosition(_locations[i], true), Quaternion.identity);
			Trees treeObject = instance.AddComponent<Trees>();
			treeObject.setAttribute(TreeParser.getMyTrees()[i]);
			treeObject.transform.parent = (Transform)treeParent.transform;
			//treeObject.info = UIInfo;
			instance.transform.localPosition = _map.GeoToWorldPosition(_locations[i], true);
			instance.transform.localScale = new Vector3(_spawnScale, _spawnScale, _spawnScale);
			_spawnedObjects.Add(instance);


		}
	}

	private void destroyTrees()
	{
		if (_spawnedObjects.Count != 0)
		{
			foreach (GameObject i in _spawnedObjects)
			{
				Destroy(i);
			}
		}
	}
}
