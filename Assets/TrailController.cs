using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrailController : MonoBehaviour
{
    private CarController carController;
    private TrailRenderer tr;

    private void Awake() {
        carController = GetComponentInParent<CarController>();
        tr = GetComponent<TrailRenderer>();

        tr.emitting = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(carController.isCarDrifting(out float latVelocity, out bool isDrifting))
        {
            tr.emitting = true;
        }
        else
        {
            tr.emitting = false;
        }
    }
}
