using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectDepth : MonoBehaviour
{
    public float topY = 0.0f;
    public float bottomY = -3.7f;

    private IDictionary<GameObject, float> objectLocations;

    private void Start() {
        objectLocations = new Dictionary<GameObject, float>();
    }

    public float updateY(GameObject caller, float depth) {
        
        if (objectLocations.ContainsKey(caller)) {
            objectLocations[caller] = depth;
        }
        else {
            objectLocations.Add(caller, depth);
        }

        return depthToY(depth);
    }

    public float getDepth(GameObject caller) {
        return objectLocations[caller];
    }

    public float depthToY(float depth) {
        return depth * (topY - bottomY) + bottomY;
    }

    public float yToDepth(float y) {
        return (y - bottomY) / (topY - bottomY);
    }

    public int depthToLayerOrder(float depth) {
        return 100 - Mathf.RoundToInt(depth * 100);
    }
}