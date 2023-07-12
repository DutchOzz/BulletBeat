using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class RotationTower : Tower
{
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] float rotationSpeed = 1;
    [SerializeField] int amountOfBullets;
    [SerializeField] float bulletSpawnOffset;
    private float angle = 0f;
    private Vector2 direction;

    private void SpawnBullet(float dir_x, float dir_y)
    {
        GameObject bullet = Instantiate(bulletPrefab);
        bullet.transform.position = transform.position + new Vector3(dir_x, dir_y, 0f) * bulletSpawnOffset;
        bullet.GetComponent<projectile>().SetDirection(dir_x, dir_y);
    }

    public override void ReceiveBeat() {
        float radians = (float) (angle / 180 * Math.PI);
        for (int i = 0; i < amountOfBullets; i++)
        {
            SpawnBullet((float) Math.Cos(radians), (float) Math.Sin(radians));
            radians = radians + (float) (2 * Math.PI / amountOfBullets);
        }
    }

    public override void ReceiveHalfBeat() {
        angle = (angle + rotationSpeed) % 360;
        gameObject.transform.eulerAngles = new Vector3(0f, 0f, angle);
    }
}
