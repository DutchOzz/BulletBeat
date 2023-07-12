using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Rigidbody2D))]

public class PlayerMove : MonoBehaviour
{
    Rigidbody2D rgbd2d;
    [HideInInspector] public Vector3 movementVector;
    [HideInInspector] public float lastHorizontalVector;
    [HideInInspector] public float lastVerticalVector;
    [HideInInspector] public bool invulnerable = false;
    
    [SerializeField] float speed = 3f;
    [SerializeField] Vector3 dodgeMomentum;
    [SerializeField] Vector2 dodgeInvulnerablilty;
    [SerializeField] float dodgeSpeed = 3f;
    [SerializeField] float dodgeCooldown = 2f;
    private float tempDodgeCooldown;
    private bool dodgeRolling;
    private float dodgeX = 0;
    private Vector2 dodgeVector;

    //Animator animator;

    // Start is called before the first frame update
    private void Awake()
    {
        rgbd2d = GetComponent<Rigidbody2D>();
        movementVector = new Vector3();
        tempDodgeCooldown = dodgeCooldown;
        //animator = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    private void Update()
    {
        Collider2D[] hit = Physics2D.OverlapCircleAll(transform.position, 0.3f);
        foreach (Collider2D c in hit)
        {
            if (c.tag != "Untagged") {
                HitTagged(c);
            }
        }

        if (dodgeRolling) {
            DodgeRoll();
        } else {
            tempDodgeCooldown -= Time.deltaTime;

            movementVector.x = 0;
            movementVector.y = 0;

            if (Input.GetKey(KeyCode.D))
            {
                movementVector.x = 1;
            }
            if (Input.GetKey(KeyCode.A))
            {
                movementVector.x = -1;
            }
            if (Input.GetKey(KeyCode.W))
            {
                movementVector.y = 1;
            }
            if (Input.GetKey(KeyCode.S))
            {
                movementVector.y = -1;
            }
            if (Input.GetKey(KeyCode.Space))
            {
                if (tempDodgeCooldown < 0) {
                    dodgeRolling = true;
                    dodgeX = 0f;
                    dodgeVector = movementVector.normalized;
                    tempDodgeCooldown = dodgeCooldown;
                }
            }
            // if (Input.GetKey(KeyCode.L) && !dodgeRolling) {
            //     swordSlash.SetActive(true);
            //     swordSlash.GetComponent<swordSlash>().SetDirection(movementVector);
            // }


            // animator.SetFloat("Horizontal", movementVector.x);
            // animator.SetFloat("Vertical", movementVector.y);


            // if (movementVector.x == 0 && movementVector.y == 0)
            // {
            //     animator.SetFloat("Speed", 0);
            // }
            // else { animator.SetFloat("Speed", 1); }

            if (movementVector.x != 0)
            {
                lastHorizontalVector = movementVector.x;
            }
            if (movementVector.y != 0)
            {
                if (movementVector.x == 0) {
                    lastHorizontalVector = 0;
                }
                lastVerticalVector = movementVector.y;
            }
            movementVector = movementVector * speed;
            rgbd2d.velocity = movementVector;
        }
    }

    private void DodgeRoll()
    {
        float dodgeFormulaRes = dodgeMomentum.x*dodgeX*dodgeX + dodgeMomentum.y*dodgeX + dodgeMomentum.z;

        if (!invulnerable && dodgeX > dodgeInvulnerablilty.x && dodgeX < dodgeInvulnerablilty.y) {
            invulnerable = true;
        } else if (invulnerable && dodgeX > dodgeInvulnerablilty.y) {
            invulnerable = false;
        }

        if (dodgeFormulaRes < 1) {
            dodgeRolling = false;
            invulnerable = false;
        }

        rgbd2d.velocity = dodgeVector * dodgeSpeed * dodgeFormulaRes;
        dodgeX += Time.deltaTime;
    }

    private void HitTagged(Collider2D c) {
        if (c.tag == "Projectile") {
            if (!invulnerable) {
                Destroy(c);
            }
            return;
        } else if (c.tag == "Enemy") {
            return;
        } else if (c.tag == "waypointCollider") {
            return;
        }else if (c.tag == "PlacePoint") {
            return;
        } else if (c.tag == "coin") {
            c.GetComponent<Coin>().PickUp(gameObject);
            return;
        } else if (c.tag == "PickedUpCoin") {
            if (!c.GetComponent<PickedUpCoin>().invincible) {
                Destroy(c.gameObject);
                gameObject.GetComponent<PlaceTowers>().PickUpCoin();
            }
            return;
        }
        else {
            Debug.Log(c.tag + " not a recognised tag");
        }
    }
}