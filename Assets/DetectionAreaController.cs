using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectionAreaController : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.tag == "Enemy")
        {
            print("sjsjs");
            Destroy(other.gameObject);
        }
    }

    private void OnTriggerStay2D(Collider2D other) {
        if(other.tag == "Enemy")
        {
            print("ojojo");
            Destroy(other.gameObject);
        }
    }
}
