using UnityEngine;

public class TrailPoint : MonoBehaviour
{
    private float timeSinceInitialized = 0;
    private bool initialized = false;
    private Vector3 position = Vector3.zero;
    private Quaternion rotation = Quaternion.identity;

    private void Start() {
        timeSinceInitialized = 0;
    }

    private void FixedUpdate() {
        if (initialized)
        {
            timeSinceInitialized += Time.fixedDeltaTime;
        }
    }

    public void Initialize() {
        initialized = true;
        timeSinceInitialized = 0;
    }

    public void Stop() {
        initialized = false;
    }

    public float GetTimeSinceInitialized() {
        return timeSinceInitialized;
    }

    public void SetPositionAndRotation(Vector3 position, Quaternion quaternion) {
        this.position = position;
        this.rotation = quaternion;
    }

    public (Vector3 position, Quaternion quaternion) GetPositionAndRotation() {
        return (this.position, this.rotation);
    }
}
