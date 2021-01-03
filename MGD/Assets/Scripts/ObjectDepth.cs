using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectDepth : MonoBehaviour
{
    public float topY;
    public float bottomY;

    private IDictionary<GameObject, float> objectLocations;

    private void Start() {
        topY = 0.0f;
        bottomY = -3.7f;
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
}