using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectDepth : MonoBehaviour
{
    public float topY;
    public float bottomY;
    public float raycastTolerance;

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

    public void remove(GameObject caller) {
        objectLocations.Remove(caller);
    }

    public List<GameObject> findItemsWithDepth(float depth) {
        List<GameObject> objects = new List<GameObject>();

        foreach (KeyValuePair<GameObject, float> kvp in objectLocations) {
            if (Mathf.Abs(depth - kvp.Value) < raycastTolerance) {
                objects.Add(kvp.Key);
            }
        }
        
        return objects;
    }
}