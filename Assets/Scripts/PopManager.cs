using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopManager : MonoBehaviour
{
    private PopSpawner popSpawner;
    private PopCensus popCensus;

    private void Awake()
    {
        popSpawner = GetComponent<PopSpawner>();
        if (popSpawner == null)
            throw new System.Exception("PopSpawner component not found");

        popCensus = GetComponent<PopCensus>();
        if (popCensus == null)
            throw new System.Exception("popCensus component not found");
    }


    // Start is called before the first frame update
    void Start()
    {
        popSpawner.SpawnPops();
    }

    public void Test(GameObject g)
    {

    }

    public void AttunePop()
    {
        Debug.Log("Attempting to attune");
        var hitPop = GetClickedPop();
        if (hitPop == null) return;

        var popData = PopCensus.gameObjectPopMap[hitPop];
        if (popData == null) return;
        Debug.Log("Attuning pop!");
        popData.attuned = true;
    }

    GameObject GetClickedPop()
    {

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        LayerMask mask = LayerMask.GetMask("Level", "Zone");
        mask = ~mask;

        if (Physics.Raycast(ray, out RaycastHit hitInfo, 100f, mask))
        {
            Debug.Log("Hit " + LayerMask.LayerToName(hitInfo.collider.gameObject.layer));
            if (hitInfo.collider.tag != "Pop")
            {   
                return null;
            }
            Debug.Log("Hit a pop");
            return hitInfo.collider.gameObject;
        }
    
        return null;
    }
}
