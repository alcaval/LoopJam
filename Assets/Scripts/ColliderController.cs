using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderController : MonoBehaviour
{
    private GameObject car;
    private CarController carController;
    [SerializeField] TrailController trailController;
    [SerializeField] private bool _switch = false;
    [SerializeField] private ColliderController cc;

    private void Awake() {
        car = GameObject.FindGameObjectWithTag("Car");
        carController = car.GetComponent<CarController>();
    }

    private void OnTriggerEnter2D(Collider2D other) {
        //ContactPoint2D[] contacts;
        //int ncontacts = other.GetContacts(contacts)[0];
        if(carController.isCarDrifting(out float latVelocity, out bool isDrifting) && other.tag == "Car" && _switch)
        {
            Debug.Log("Tremendo loop");
            trailController.ClearPoints(true);
            RaycastHit hit;
            if (Physics.Raycast(transform.position, transform.forward, out hit))
            {
                Debug.Log("Point of contact: " + hit.point);
                trailController.setPolygon(hit.point);
            }
            cc.clearSelf();
        }
    }

    

    public void clearSelf()
    {
        trailController.ClearPoints(true);
    }
}
