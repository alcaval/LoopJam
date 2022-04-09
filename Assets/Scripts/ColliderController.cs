using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderController : MonoBehaviour
{
    private GameObject car;
    private CarController carController;

    private void Awake() {
        car = GameObject.FindGameObjectWithTag("Car");
        carController = car.GetComponent<CarController>();
    }

    private void OnTriggerEnter2D(Collider2D other) {
        Debug.Log("ei");
    }
}
