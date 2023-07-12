using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wayPoint : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D c) {
        if (c.tag == "waypointCollider") {
            c.transform.parent.gameObject.GetComponent<Enemy>().UpdateMovementVector();
        }
    }
}
