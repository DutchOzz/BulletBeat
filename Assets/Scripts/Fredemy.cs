using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fredemy : Enemy
{
    [SerializeField] GameObject path;
    [SerializeField] float speed;
    private GameObject nextWayPoint;
    private Vector3 movementVector;

    // Start is called before the first frame update
    void Start()
    {
        nextWayPoint = path.GetComponent<path>().getStartPoint();
        transform.position = nextWayPoint.transform.position;
        UpdateMovementVector();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += movementVector * speed * Time.deltaTime;
    }

    public override void UpdateMovementVector() {
        nextWayPoint = path.GetComponent<path>().getNextWaypoint(nextWayPoint);
        if (nextWayPoint == null) {
            Destroy(gameObject);
        } else {
            movementVector = (nextWayPoint.transform.position - transform.position).normalized;
        }
    }
}
