using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCharacter : MonoBehaviour
{
    [HideInInspector] public int currentRoom = 0;
    [SerializeField] int hp = 999;
    [SerializeField] public GameObject[] itemDrops;
    [SerializeField] public float[] dropChance;

    public void Update() {
        currentRoom = ((int) ((transform.position.x + 40) / 20) + -4 * ((int) ((transform.position.y - 40) / 20)));
    }

    public void TakeDamage(int damage)
    {
        hp -= damage;

        if (hp < 1) {
            Death();
        }
    }

    private void Death() {
        if (dropChance.Length != itemDrops.Length) {
            Debug.Log("dropChance and itemdrops length are different!");
        }
        if (dropChance.Length > 0) {
            float random = Random.Range(0f, 1f);
            float chanceAdding = 0f;
            for (int i = 0; i < dropChance.Length; i++)
            {
                chanceAdding += dropChance[i];
                if (random < chanceAdding) {
                    GameObject item = Instantiate(itemDrops[i]);
                    item.transform.position = transform.position;
                    break;
                }
            }
        }
        Destroy(gameObject);
    }
}
