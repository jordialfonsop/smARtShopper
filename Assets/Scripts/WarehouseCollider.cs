using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static TaskManager;

public class WarehouseCollider : MonoBehaviour
{
    [SerializeField] public Vector3 location;
    [SerializeField] public Pathfinder pathfinder;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "MainCamera") {

            pathfinder.currentLocation = location;

        }
    }
}
