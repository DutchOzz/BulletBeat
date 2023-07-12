using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class projectile : MonoBehaviour
{
    Vector3 direction;
    [SerializeField] float speed;
    [SerializeField] int damage = 200;
    [SerializeField] float despawnTime = 5f;
    // Update is called once per frame
    void Update()
    {
        despawnTime -= Time.deltaTime;
        if (despawnTime < 0f) {
            Destroy(gameObject);
        }

        transform.position += direction * speed * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.isTrigger) {
            var otherMove = other.gameObject.GetComponent<PlayerMove>();
            if (otherMove != null) {
                if (otherMove.invulnerable) {
                    return;
                }
            }
        }
        var otherComponent = other.gameObject.GetComponent<Character>();
        if (otherComponent != null) {
            otherComponent.TakeDamage(damage);
            Destroy(gameObject);
        }
    }

    public void SetDirection(float dir_x, float dir_y)
    {
        direction = new Vector3(dir_x, dir_y).normalized;
        // if (dir_x < 0)
        // {
        //     Vector3 scale = transform.localScale;
        //     scale.x = scale.x * -1;
        //     transform.localScale = scale;
        // }
        // Vector2 mathDir = new Vector2(180f, 180f);
        // Double angleRadians = Math.Atan(dir_y/ dir_x);
        // float angle = (float) (angleRadians * 180 / Math.PI);
        // transform.eulerAngles = new Vector3(0f, 0f, angle);
    }
}