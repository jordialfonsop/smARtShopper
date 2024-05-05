using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinder : MonoBehaviour
{
    [SerializeField] public Vector3 target = new Vector3 (99, 99, 99);

    public Vector3 currentLocation = new Vector3(99,99,99);

    private LineRenderer lineRenderer;
    // Start is called before the first frame update
    void Start()
    {
        lineRenderer = transform.gameObject.GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (currentLocation != new Vector3(99, 99, 99) && target != new Vector3(99,99,99))
        {
            lineRenderer.SetPosition(0, currentLocation);
            lineRenderer.SetPosition(1, new Vector3(0,currentLocation.y,currentLocation.z));
            lineRenderer.SetPosition(2, new Vector3(0, target.y, target.z));
            lineRenderer.SetPosition(3, target);
        }
        else
        {
            lineRenderer.SetPosition(0, new Vector3(0, 0, 0));
            lineRenderer.SetPosition(1, new Vector3(0, 0, 0));
            lineRenderer.SetPosition(2, new Vector3(0, 0, 0));
            lineRenderer.SetPosition(3, new Vector3(0, 0, 0));
        }   
    }
}
