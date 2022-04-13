using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectionAreaController : MonoBehaviour
{
    [SerializeField] private CMScreenshake cmscreenshake;
    [SerializeField] private PolygonCollider2D poly;

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.tag == "Enemy")
        {
            cmscreenshake.ShakeCamera(8, 1f);
            other.gameObject.transform.parent.gameObject.SetActive(false);
            poly.pathCount = 0;
        }
        
        if(other.tag == "Car")
        {
            poly.pathCount = 0;
        }
    }

    private void Update() {
        
    }


}
