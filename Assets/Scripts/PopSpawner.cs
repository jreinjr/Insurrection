using System;
using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class PopSpawner : MonoBehaviour
{
    public GameObject popSpawnZoneGO;
    public GameObject popPrefab;
    public int numPops;

    public delegate void PopSpawnedAction(PopSpawnedEventArgs e);
    public event PopSpawnedAction OnPopSpawned;


    /// <summary>
    /// Instantiates a new spawner for the given zones and prefab type
    /// </summary>
    /// <param name="spawnZoneGO"></param>
    /// <param name="prefab"></param>
    /// <param name="num"></param>
    public PopSpawner(GameObject spawnZoneGO, GameObject prefab, int num)
    {
        popSpawnZoneGO = spawnZoneGO;
        popPrefab = prefab;
        numPops = num;
    }


    /// <summary>
    /// Spawn pops at random points inside valid zones
    /// </summary>
    public void SpawnPops()
    {
        ValidateSpawnZoneGO(popSpawnZoneGO);
        var bounds = GetBoundsFromSpawnZoneGO(popSpawnZoneGO);
        ValidateSpawnBounds(bounds);
        var points = GetValidSpawnPoints(numPops, bounds);
        SpawnPopsAtPoints(popPrefab, points);
    }


    /// <summary>
    /// Spawns a pop at the given point and calls the 'OnPopSpawned' event
    /// </summary>
    /// <param name="popPrefab"></param>
    /// <param name="point"></param>
    public void SpawnPopAtPoint(GameObject popPrefab, Vector3 point)
    {
        var newPop = Instantiate(popPrefab);
        newPop.transform.position = point;
        OnPopSpawned?.Invoke(new PopSpawnedEventArgs(newPop));
    }


    /// <summary>
    /// Check that the spawnZone game object is set up
    /// and throw excpetions if it isn't.
    /// 
    /// SpawnZone gameobject should have at least 1 box collider.
    /// </summary>
    /// <param name="spawnZoneGO"></param>
    void ValidateSpawnZoneGO(GameObject spawnZoneGO)
    {
        if (spawnZoneGO == null)
        {
            throw new System.Exception("spawnZoneGO is null");
        }
        else if(spawnZoneGO.GetComponent<BoxCollider>() == null)
        {
            throw new System.Exception("No BoxColliders on spawnZoneGO");
        }
    }


    /// <summary>
    /// Get a list of bounds from provided gameobject
    /// ASSERT: GO has been validated above
    /// </summary>
    /// <param name="spawnZoneGO"></param>
    /// <returns></returns>
    List<Bounds> GetBoundsFromSpawnZoneGO(GameObject spawnZoneGO)
    {
        Component[] spawnZones = spawnZoneGO.GetComponents(typeof(BoxCollider));
        var result = new List<Bounds>();

        foreach (BoxCollider zone in spawnZones)
        {
            var newBounds = new Bounds(zone.center, zone.size);
            result.Add(newBounds);
        }

        return result;
    }


    private void ValidateSpawnBounds(List<Bounds> bounds)
    {
        if(bounds.Count == 0)
        {
            throw new System.Exception("Bounds list is empty");
        }
        /*
        foreach (var b in bounds.Where(b => (
            b.size.x == 0 || b.size.y == 0 || b.size.z == 0
            )))
        {
            throw new System.Exception("At least one bounds has a size 0");
        }
        */
    }


    /// <summary>
    /// Returns a list of valid pop spawn points.
    /// </summary>
    /// <param name="numPoints">Number of points to generate.</param>
    /// <param name="spawnZones">Valid spawn zones</param>
    /// <returns></returns>
    IEnumerable<Vector3> GetValidSpawnPoints(int numPoints, List<Bounds> spawnZones)
    {
        var result = new List<Vector3>();

        for (int i = 0; i < numPoints; i++)
        {
            // Pick random zone from list.
            // TODO: Ensure we aren't picking outside valid indices
            int zoneIndex = UnityEngine.Random.Range(0, spawnZones.Count);
            var zone = spawnZones[zoneIndex];
            // Pick a random point within this zone and add it to our result
            var newPoint = new Vector3(
                UnityEngine.Random.Range(zone.min.x, zone.max.x),
                UnityEngine.Random.Range(zone.min.y, zone.max.y),
                UnityEngine.Random.Range(zone.min.z, zone.max.z)
                );

            // Offset new point by spawnGO transform center
            newPoint += popSpawnZoneGO.transform.position;

            yield return newPoint;
        }
    }

    /// <summary>
    /// Spawn a pop at each provided point
    /// </summary>
    /// <param name="popPrefab"></param>
    /// <param name="points"></param>
    private void SpawnPopsAtPoints(GameObject popPrefab, IEnumerable<Vector3> points)
    {
        foreach (var spawnPoint in points)
        {
            SpawnPopAtPoint(popPrefab, spawnPoint);
        }
    }
}


public class PopSpawnedEventArgs
{
    public GameObject pawn;

    public PopSpawnedEventArgs(GameObject pawn)
    {
        this.pawn = pawn;
    }
}