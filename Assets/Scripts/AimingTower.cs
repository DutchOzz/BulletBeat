using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class AimingTower : Tower
{
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] int amountOfBullets;
    [SerializeField] float detectionRange;
    [SerializeField] float bulletSpawnOffset;
    private Vector2 direction;


    Transform GetClosestEnemy()
    {
        Collider2D[] hit = Physics2D.OverlapCircleAll(transform.position, detectionRange);
        List<Transform> enemies = new List<Transform>();
        foreach (Collider2D c in hit) {
            EnemyCharacter enemy = c.GetComponent<EnemyCharacter>();
            Character player = c.GetComponent<Character>();
            if (enemy != null) {
                enemies.Add(enemy.transform);
            } else if (player != null) {
                enemies.Add(player.transform);
            }
        }
        if (enemies.Count != 0) {
            Transform[] enem = enemies.ToArray();
            Transform bestTarget = null;
            float closestDistanceSqr = Mathf.Infinity;
            Vector3 currentPosition = transform.position;
            foreach(Transform potentialTarget in enemies)
            {
                Vector3 directionToTarget = potentialTarget.position - currentPosition;
                float dSqrToTarget = directionToTarget.sqrMagnitude;
                if(dSqrToTarget < closestDistanceSqr)
                {
                    closestDistanceSqr = dSqrToTarget;
                    bestTarget = potentialTarget;
                }
            }
        
            return bestTarget;
        }
        return null;
    }

    private void SpawnBullet(float dir_x, float dir_y)
    {
        GameObject bullet = Instantiate(bulletPrefab);
        bullet.transform.position = transform.position + new Vector3(dir_x, dir_y, 0f) * bulletSpawnOffset;
        bullet.GetComponent<projectile>().SetDirection(dir_x, dir_y);
    }

    public override void ReceiveBeat() {
        Transform closestEnemy = GetClosestEnemy();
        if (closestEnemy != null) {
            float radians = (float) (Math.Atan((transform.position.y - closestEnemy.position.y) / (transform.position.x - closestEnemy.position.x)) + Math.PI);
            gameObject.transform.eulerAngles = new Vector3(0f, 0f, (float) (radians * 180 / Math.PI));

            for (int i = 0; i < amountOfBullets; i++)
            {
                SpawnBullet((float) Math.Cos(radians), (float) Math.Sin(radians));
                radians = radians + (float) (2 * Math.PI / amountOfBullets);
            }
        }
    }

    public override void ReceiveHalfBeat() {

    }
}
