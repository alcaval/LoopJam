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

    Vector3[] positions = new Vector3[1000];

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
            
            int i = _trailRendererLeft.GetPositions(positions);
            
            List<Vector2> lista = new List<Vector2>();
            for(int j = 0; j < positions.Length - 1; j++)
            {
                if(positions[j].x == 0 && positions[j].y == 0) break;
                if(positions[j].x != 0 || positions[j].y != 0) lista.Add(new Vector2(positions[j].x, positions[j].y));
            }

            Vector2[] v = lista.ToArray();

            _edgeCollider.points = v;
        }
        else
        {
            _pointsAux = _points;
            ClearPoints();
        }
    }

    public void ClearPoints()
    {
        positions = new Vector3[1000];
        _points.Clear();
        _edgeCollider.Reset();
        _edgeCollider.isTrigger = true;
        _trailRendererLeft.emitting = false;
        _trailRendererRight.emitting = false;
    }

    public void setPolygon(Vector2 hit)
    {
        List<Vector2> newPoints = new List<Vector2>();
        int j = _edgeCollider.GetPoints(_pointsAux);

        float minDistance = Mathf.Infinity;
        int index = 0;
        for(int i=0; i < 9*_pointsAux.Count/10; ++i)
        {
            Vector2 v = _pointsAux[i];
            if(Vector2.Distance(hit,v) <= minDistance)
            {
                minDistance = Vector2.Distance(hit,v);
                index = i;
            }
            else 
            {
                break;
            }
        }
        _detectionArea.gameObject.SetActive(true);
        _detectionArea.points = _pointsAux.GetRange(index, _pointsAux.Count - index).ToArray();
    }
}
