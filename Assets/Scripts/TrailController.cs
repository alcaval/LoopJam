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

    private void Awake() {
        carController = GetComponentInParent<CarController>();
        tr = GetComponent<TrailRenderer>();

        tr.emitting = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(carController.isCarDrifting(out float latVelocity, out bool isDrifting))
        {
            tr.emitting = true;
            
            int i = tr.GetPositions(positions);
            
            List<Vector2> lista = new List<Vector2>();
            for(int j = 0; j < positions.Length - 1; j++)
            {
                if(positions[j].x != 0 || positions[j].y != 0) lista.Add(new Vector2(positions[j].x, positions[j].y));
            }

            Vector2[] v = lista.ToArray();

            ec.points = v;
        
        }
        else
        {
            ClearPoints(false);
        }
    }

    public void ClearPoints(bool boom)
    {
        StartCoroutine(ClearPointsRoutine(boom));
        positions = new Vector3[1000];
        tr.emitting = false;
    }

    IEnumerator ClearPointsRoutine(bool boom)
    {
        if(boom)
        {
            tr.Clear();  
            //_detectionArea.points = ec.points;
        } 
        yield return new WaitForSeconds(0.2f);
        ec.Reset();
        ec.isTrigger = true;
    }

    public void setPolygon(Vector2 hit)
    {
        List<Vector2> newPoints = new List<Vector2>();
        
        foreach(Vector2 v in ec.points)
        {
            if(Vector2.Distance(hit,v) > 0.2)
            {
                print("y tal");
                newPoints.Add(v);
            }
        }

        _detectionArea.points = newPoints.ToArray();
    }
}
