using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderController : MonoBehaviour
{
    private GameObject car;
    private CarController carController;
    [SerializeField] NewTrailController _trailController;
    [SerializeField] private bool _switch = false;
    [SerializeField] private ColliderController cc;

    private void Awake() {
        car = GameObject.FindGameObjectWithTag("Car");
        carController = car.GetComponent<CarController>();
    }

    private void OnTriggerEnter2D(Collider2D other) {

        if(carController.isCarDrifting(out float latVelocity, out bool isDrifting) && other.tag == "Car" && _switch)
        {
            Debug.Log("Tremendo loop");
            _trailController.setPolygon(other.ClosestPoint(transform.position));
            _trailController.ClearPoints();
            //cc.clearSelf();
        }
    }
}
