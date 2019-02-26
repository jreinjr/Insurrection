using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// NOW: Stores list of active pops
/// EVENTUALLY: Returns categorized lists
/// </summary>
public class PopCensus : MonoBehaviour
{
    public static Dictionary<GameObject, Pop> gameObjectPopMap;

    private PopSpawner popSpawner;

    private void Awake()
    {
        popSpawner = GetComponent<PopSpawner>();
        gameObjectPopMap = new Dictionary<GameObject, Pop>();
    }

    private void OnEnable()
    {
        popSpawner.OnPopSpawned += PopSpawned;
    }

    private void OnDisable()
    {
        popSpawner.OnPopSpawned -= PopSpawned;
    }

    private void PopSpawned(PopSpawnedEventArgs e)
    {
        var speedList = new float[] { 1f, 2f, 4f };
        int randIx = Mathf.RoundToInt(UnityEngine.Random.Range(0, speedList.Length));
        gameObjectPopMap.Add(e.pawn, new Pop(0f, (Winding)UnityEngine.Random.Range(0, 2), speedList[randIx]));
    }
}
