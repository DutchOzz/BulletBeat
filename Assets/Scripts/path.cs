using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class path : MonoBehaviour
{
    public GameObject getNextWaypoint(GameObject oldWaypoint) {
        for (int i = 0; i < transform.childCount - 1; i++) {
            if (transform.GetChild(i).gameObject == oldWaypoint) {
                return transform.GetChild(i+1).gameObject;
            }
        }
        return null;
    }

    public GameObject getStartPoint() {
        return transform.GetChild(0).gameObject;
    }
}
