using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Side
{
    N,
    E,
    S,
    W
}

public class Leyline : MonoBehaviour
{
    public int attunement;
    public Side side;
    public List<GameObject> attunedPops;
    public MeshRenderer rend; 

    void OnTriggerEnter(Collider other)
    {
        if (other.tag != "Pop") return;

        if (attunedPops.Contains(other.gameObject))
        {
            Debug.Log("Already have this GO on my list");
            return;
        }

        var popData = PopCensus.gameObjectPopMap[other.gameObject];
        if (popData == null)
        {
            throw new System.Exception("Pop not found in census or data is null");
        }

        if (popData.attuned)
        {
            attunedPops.Add(other.gameObject);
            attunement++;
            UpdateLeylineMesh();
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (attunedPops.Contains(other.gameObject) == false)
        {
            return;
        }

        var popData = PopCensus.gameObjectPopMap[other.gameObject];
        if (popData == null)
        {
            throw new System.Exception("Pop not found in census or data is null");
        }

        if (popData.attuned)
        {
            attunedPops.Remove(other.gameObject);
            attunement--;
            UpdateLeylineMesh();
        }
    }

    void UpdateLeylineMesh()
    {
        var c = rend.material.color;
        float newA = Mathf.Clamp(attunement * 0.05f, 0f, 1f);
        rend.material.SetColor("_Color", new Color(c.r, c.g, c.b, newA));
    }
}
