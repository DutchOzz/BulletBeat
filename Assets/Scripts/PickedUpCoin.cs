using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickedUpCoin : MonoBehaviour
{
    Rigidbody2D rgbd2d;
    [SerializeField] private float pushBackVelocity;
    [SerializeField] private float acceleration;
    [SerializeField] private float deceleration;
    [SerializeField] private float followPlayerMultiplier;
    [SerializeField] private float maxSpeed;
    [SerializeField] private float invincibilityTime;
    [HideInInspector] public bool invincible = true;
    private float pushBackMultiplier;
    private bool pushedBack = false;
    private Vector2 pushBackVector;
    private GameObject target;
    
    // Start is called before the first frame update
    void Awake()
    {
        rgbd2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (pushedBack) {
            invincibilityTime -= Time.deltaTime;
            if (invincibilityTime < 0) {
                invincible = false;
            }

            Vector2 targetVector = (target.transform.position - transform.position).normalized;
            rgbd2d.velocity = pushBackVector * pushBackMultiplier + targetVector * followPlayerMultiplier;

            pushBackMultiplier = pushBackMultiplier * deceleration;
            if (followPlayerMultiplier < maxSpeed) {
                followPlayerMultiplier = followPlayerMultiplier * acceleration;
            }
        }
    }
    
    public void PushBack(GameObject t) {
        target = t;
        pushBackMultiplier = 1;
        pushBackVector = (target.transform.position - transform.position).normalized * -1 * pushBackVelocity;
        pushedBack = true;
    }
}
