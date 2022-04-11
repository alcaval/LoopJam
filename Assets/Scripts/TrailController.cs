using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrailController : MonoBehaviour
{
    private CarController carController;
    private TrailRenderer tr;
    Vector3[] positions = new Vector3[1000];

    [SerializeField] EdgeCollider2D ec;
    [SerializeField] private PolygonCollider2D _detectionArea;

    private List<Vector2> puntos;

    private void Awake() {
        carController = GetComponentInParent<CarController>();
        tr = GetComponent<TrailRenderer>();

        tr.emitting = false;
    }

    // Update is called once per frame
    void lateUpdate()
    {
        print("hola?");
        if(carController.isCarDrifting(out float latVelocity, out bool isDrifting))
        {
            tr.time = 2f;
            tr.emitting = true;

            puntos.Add(GameObject.FindGameObjectWithTag("Car").transform.position);
            print(puntos);
            
            // int i = tr.GetPositions(positions);
            
            // List<Vector2> lista = new List<Vector2>();
            // for(int j = 0; j < positions.Length - 1; j++)
            // {
            //     if(positions[j].x == 0 && positions[j].y == 0) break;
            //     if(positions[j].x != 0 || positions[j].y != 0) lista.Add(new Vector2(positions[j].x, positions[j].y));
            // }

            Vector2[] v = puntos.ToArray();

            ec.points = v;
        
        }
        else
        {
            print(puntos);
            tr.time = 2f;
            ClearPoints();
        }
    }

    public void ClearPoints()
    {
        //StartCoroutine(ClearPointsRoutine(boom));
        // if(boom)
        // {
        //     //tr.Clear();  
        //     //_detectionArea.points = ec.points;
        // } 
        
        puntos.Clear();
        //yield return new WaitForSeconds(0.2f);
        ec.Reset();
        ec.isTrigger = true;
        //positions = new Vector3[1000];
        tr.emitting = false;
    }

    public void setPolygon(Vector2 hit)
    {
        List<Vector2> newPoints = new List<Vector2>();
        
        int i = 0;
        bool ahoraSi = false;
        foreach(Vector2 v in ec.points)
        {
            if(ahoraSi)
            {
                newPoints.Add(v);
            }

            if(Vector2.Distance(hit,v) < 0.4) ahoraSi = true;
            i++;
        }

        _detectionArea.points = newPoints.ToArray();
    }
}
