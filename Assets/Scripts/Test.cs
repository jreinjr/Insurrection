using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    public LayerMask layerMask;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ShootRay();
        }
    }

    void ShootRay()
    {
        Debug.Log("Shooting ray");
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit, 100f, layerMask))
        {
            Debug.Log("First hit: ");
            Debug.Log("Name: " + hit.collider.name);
            Debug.Log("Layer: " + LayerMask.LayerToName(hit.collider.gameObject.layer));
        }

        var allHits = Physics.RaycastAll(ray, 100f, layerMask);
        foreach (var h in allHits)
        {
            Debug.Log("Next allhit: ");
            Debug.Log("Name: " + h.collider.name);
            Debug.Log("Layer: " + LayerMask.LayerToName(h.collider.gameObject.layer));
        }
    }
}


