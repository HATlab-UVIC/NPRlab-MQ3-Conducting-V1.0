using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Conduction : MonoBehaviour
{
    [SerializeField] private Trail trailObject;
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private Instrument[] instruments;
    private List<TrailPoint> trail;
    private bool isGrabbed = false;
    private int numberOfIgnoredFramesUntilNextSwing = 3;
    private int IgnoredFramesUntilNextSwingCounter = 0;

    // temp. Will need to update by actual sheet music nodes with time
    int seminode = 0;

    void Start()
    {
        
    }

    void Update()
    {
        if (trailObject.IsNewTrailAdded())
        {
            trail = trailObject.GetTrail();
            if (trail.Count > 3)
            {
                Vector3 previousSwingVector = trail[2].GetPositionAndRotation().position - trail[1].GetPositionAndRotation().position;
                Vector3 newSwingVector = trail[0].GetPositionAndRotation().position - trail[1].GetPositionAndRotation().position;
                if (Vector3.Angle(previousSwingVector, - newSwingVector) > 90 && newSwingVector.magnitude > 0.01f)
                {
                    if (IgnoredFramesUntilNextSwingCounter == 0)
                    {
                        seminode++;
                        if (seminode == 13)
                        {
                            seminode = 0;
                        }
                        foreach (Instrument instrument in instruments)
                        {
                            instrument.PlayNode(seminode);
                        }
                        IgnoredFramesUntilNextSwingCounter = numberOfIgnoredFramesUntilNextSwing;
                    }
                    else
                    {
                        IgnoredFramesUntilNextSwingCounter--;
                    }
                }
                foreach (Instrument instrument in instruments)
                {
                    if (isGrabbed)
                    {
                        instrument.AddContribution((int) Mathf.Min((newSwingVector.magnitude + previousSwingVector.magnitude) * 300000 * Time.deltaTime, 100));
                    }
                    else
                    {
                        instrument.AddContribution(0);
                    }
                }
            }
        }
        text.text = $"Volume: {instruments[0].GetCurrentVolume():F2}\nContri: {instruments[0].GetCurrentContribution():F2}\nGrabbed?: {isGrabbed}, Semi: {seminode}";
    }

    public void Selected() {
        isGrabbed = true;
    }

    public void Unselected() {
        isGrabbed = false;
    }
}
