using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PsyNode : MonoBehaviour
{
    public Transform leyLines;

    public PopCensus popCensus;


    

    void ValidateLeyLineGO(GameObject leyLineGO)
    {
        if (leyLineGO == null)
        {
            throw new System.Exception("leyLineGO is null");
        }
        else if (leyLineGO.GetComponent<BoxCollider>() == null)
        {
            throw new System.Exception("No BoxColliders on leyLineGO");
        }
    }

}
