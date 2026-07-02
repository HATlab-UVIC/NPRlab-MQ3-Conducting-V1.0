using System.Collections.Generic;
using UnityEngine;

public class Trail : MonoBehaviour
{
    [SerializeField] private int updateFrequencyTriggerNumber = 10;
    [SerializeField] private int trailLength = 100;
    private List<TrailPoint> trailPoints = new List<TrailPoint>();
    private int updateFrequencyAddCounter;
    private LineRenderer lineRenderer;
    private bool newTrailAdded = false;

    void Start() {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.startWidth = 0.02f;
        lineRenderer.endWidth = 0.01f;
        lineRenderer.startColor = Color.red;
        lineRenderer.endColor = Color.white;
    }

    void FixedUpdate() {
        if (trailPoints.Count < 100)
        {
            if (updateFrequencyAddCounter < updateFrequencyTriggerNumber)
            {
                updateFrequencyAddCounter++;
            }
            else
            {
                updateFrequencyAddCounter = 0;
                TrailPoint trailPoint = new TrailPoint();
                trailPoint.Initialize();
                trailPoint.SetPositionAndRotation(transform.position, transform.rotation);
                trailPoints.Insert(0, trailPoint);
            }
        }
        else
        {
            if (updateFrequencyAddCounter < updateFrequencyTriggerNumber)
            {
                updateFrequencyAddCounter++;
            }
            else
            {
                updateFrequencyAddCounter = 0;
                TrailPoint trailPoint = new TrailPoint();
                trailPoint.Initialize();
                trailPoint.SetPositionAndRotation(transform.position, transform.rotation);
                trailPoints.Insert(0, trailPoint);
                trailPoints.RemoveAt(trailPoints.Count - 1);
            }
        }
        newTrailAdded = true;
        RenderTrail();
    }

    private void RenderTrail() {
        lineRenderer.positionCount = trailPoints.Count;
        for (int i = 0; i < trailPoints.Count; i++)
        {
            Vector3 position = trailPoints[i].GetPositionAndRotation().position;
            lineRenderer.SetPosition(i, position);
        }
    }

    public List<TrailPoint> GetTrail() {
        return trailPoints;
    }

    public bool IsNewTrailAdded() {
        return newTrailAdded;
    }

    public void SetIsNewTrailAdded(bool val) {
        newTrailAdded = val;
    }
}
