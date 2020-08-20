namespace Mapbox.Examples
{
	using UnityEngine;
	using UnityEngine.UI;
	using Mapbox.Utils;
	using Mapbox.Unity.Map;
	using Mapbox.Unity.MeshGeneration.Factories;
	using Mapbox.Unity.Utilities;
	using System.Collections.Generic;
	using System.Linq;
	using System.Xml.Serialization;
	using System;

	public class AllTreeSpawn : MonoBehaviour
	{
		[SerializeField]
		AbstractMap _map;

		List<Vector2d> _currentLocations = new List<Vector2d>();
		List<Vector2d> _futureLocations = new List<Vector2d>();
		List<Vector2d> _removedLocations = new List<Vector2d>();

		[SerializeField]
		float _spawnScale = 1f;

		[SerializeField]
		GameObject _currentTreePrefab;
		[SerializeField]
		GameObject _futureTreePrefab;
		[SerializeField]
		GameObject _removedTreePrefab;

		[SerializeField]
		GameObject _currentParent;
		[SerializeField]
		GameObject _futureParent;
		[SerializeField]
		GameObject _removedParent;

		List<GameObject> _spawnedCurrentObjects = new List<GameObject>();
		List<GameObject> _spawnedFutureObjects = new List<GameObject>();
		List<GameObject> _spawnedRemovedObjects = new List<GameObject>();

		double lastLon;
		double lastLat;

		bool ifUpdate = false;

		[SerializeField]
		GameObject loadingInterface;
		[SerializeField]
		Image loadingProgressBar;

		private const double distance = 0.001;



		private void Start()
		{
			LocalDataPreperation.UpdateTreeList();
			List<Trees> currentTrees = LocalDataPreperation.getMyTrees();
			List<Trees> removedTrees = LocalDataPreperation.getRemovedTrees();
			List<Trees> futureTrees = LocalDataPreperation.getFutureTrees();

			PopulateLocations(currentTrees, _currentLocations);
			PopulateTrees(currentTrees, _currentLocations, _currentTreePrefab,_spawnedCurrentObjects, _currentParent);

			PopulateLocations(futureTrees, _futureLocations);
			PopulateTrees(futureTrees, _futureLocations, _futureTreePrefab, _spawnedFutureObjects, _futureParent);

			PopulateLocations(removedTrees, _removedLocations);
			PopulateTrees(removedTrees, _removedLocations, _removedTreePrefab, _spawnedRemovedObjects, _removedParent);


			lastLon = GPSHandeler.Instance.longitude;
			lastLat = GPSHandeler.Instance.latitude;
		}

		private void Update()
		{
			double displaceLon = Math.Abs(GPSHandeler.Instance.longitude - lastLon);
			double displaceLat = Math.Abs(GPSHandeler.Instance.latitude - lastLat);
			if (ifUpdate || displaceLat >= 0.0025 || displaceLon >= 0.0025)
			{
				loadingInterface.SetActive(true);
				LocalDataPreperation.UpdateTreeList();
				List<Trees> currentTrees = LocalDataPreperation.getMyTrees();
				List<Trees> removedTrees = LocalDataPreperation.getRemovedTrees();
				List<Trees> futureTrees = LocalDataPreperation.getFutureTrees();

				DestroyTrees(_spawnedCurrentObjects);
				loadingProgressBar.fillAmount = 0.3f;
				DestroyTrees(_spawnedFutureObjects);
				loadingProgressBar.fillAmount = 0.6f;
				DestroyTrees(_spawnedRemovedObjects);
				loadingProgressBar.fillAmount = 0.7f;

				PopulateLocations(currentTrees, _currentLocations);
				PopulateTrees(currentTrees, _currentLocations, _currentTreePrefab, _spawnedCurrentObjects, _currentParent);
				loadingProgressBar.fillAmount = 0.9f;

				PopulateLocations(futureTrees, _futureLocations);
				PopulateTrees(futureTrees, _futureLocations, _futureTreePrefab, _spawnedFutureObjects, _futureParent);
				loadingProgressBar.fillAmount = 0.98f;

				PopulateLocations(removedTrees, _removedLocations);
				PopulateTrees(removedTrees, _removedLocations, _removedTreePrefab, _spawnedRemovedObjects, _removedParent);
				loadingProgressBar.fillAmount = 1f;


				lastLat = GPSHandeler.Instance.latitude;
				lastLon = GPSHandeler.Instance.longitude;
				ifUpdate = false;
				loadingInterface.SetActive(false);
			}

			UpdatePos(_spawnedCurrentObjects, _currentLocations, _currentParent);
			UpdatePos(_spawnedFutureObjects, _futureLocations, _futureParent);
			UpdatePos(_spawnedRemovedObjects, _removedLocations, _removedParent);
			
		}

		private void PopulateLocations(List<Trees> trees, List<Vector2d> locations)
		{
		
			int size = (trees != null) ? trees.Count : 0;

			locations.Clear();
			for (int i = 0; i < size; i++)
			{
				Vector2d location = new Vector2d();
				location.y = trees[i].CoordinatesX;
				location.x = trees[i].CoordinatesY;
				locations.Add(location);

			}
		}

		public void UpdateTrees()
		{
			ifUpdate = true;

		}


		private void PopulateTrees(List<Trees> trees, List<Vector2d> locations, GameObject prefab, List<GameObject> spawnedObjects, GameObject theParent)
		{
			int size = (locations != null) ? locations.Count : 0;
			for (int i = 0; i < size; i++)
			{
				var instance = Instantiate(prefab, _map.GeoToWorldPosition(locations[i], true), Quaternion.identity);
				Trees treeObject = instance.AddComponent<Trees>();
				treeObject.setAttribute(trees[i]);
				//treeObject.transform.parent = theParent.transform;
				instance.transform.localPosition = _map.GeoToWorldPosition(locations[i], true);
				instance.transform.localScale = new Vector3(_spawnScale, _spawnScale, _spawnScale);
				instance.transform.parent = theParent.transform;
				spawnedObjects.Add(instance);
			}
		}

		private void DestroyTrees(List<GameObject> spawnedObject)
		{
			if (spawnedObject.Count != 0)
			{
				foreach (GameObject i in spawnedObject)
				{
					Debug.Log("Destroying");
					i.Destroy();
					//Destroy(i);
					//spawnedObject.Remove(i);
				}
				spawnedObject.Clear();
				
			}
		}

		private void UpdatePos(List<GameObject> spawnedObjects, List<Vector2d> locations, GameObject theParent)
		{
			int count = (spawnedObjects != null)? spawnedObjects.Count: 0;
			if (theParent != null)
			{
				for (int i = 0; i < count; i++)
				{
					var spawnedObject = spawnedObjects[i];
					var location = locations[i];
					spawnedObject.transform.localPosition = _map.GeoToWorldPosition(location, true);
					spawnedObject.transform.localScale = new Vector3(_spawnScale, _spawnScale, _spawnScale);
					spawnedObject.transform.parent = theParent.transform;
				}
			}
		}

			


	}
}
