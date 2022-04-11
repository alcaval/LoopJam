using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewTrailController : MonoBehaviour
{
    private CarController _carController;
    [SerializeField] private TrailRenderer _trailRendererLeft;
    [SerializeField] private TrailRenderer _trailRendererRight;
    [SerializeField] private EdgeCollider2D _edgeCollider;
    [SerializeField] private PolygonCollider2D _detectionArea;

    private List<Vector2> _points = new List<Vector2>();
    private List<Vector2> _pointsAux = new List<Vector2>();

    private void Awake() {
        _carController = GetComponent<CarController>();

        _trailRendererLeft.emitting = false;
        _trailRendererRight.emitting = false;
    }

    private void Update()
    {
        if(_carController.isCarDrifting(out float latVelocity, out bool isDrifting))
        {
            _trailRendererLeft.emitting = true;
            _trailRendererRight.emitting = true;

            _points.Add(GameObject.FindGameObjectWithTag("Player").transform.position);

            _edgeCollider.points = _points.ToArray();
        }
        else{
            _pointsAux = _points;
            ClearPoints();
        }
    }

    public void ClearPoints()
    {
        _points.Clear();
        _edgeCollider.Reset();
        _edgeCollider.isTrigger = true;
        _trailRendererLeft.emitting = false;
        _trailRendererRight.emitting = false;
    }

    public void setPolygon(Vector2 hit)
    {
        List<Vector2> newPoints = new List<Vector2>();
        
        int i = 0;
        bool ahoraSi = false;
        foreach(Vector2 v in _pointsAux)
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
