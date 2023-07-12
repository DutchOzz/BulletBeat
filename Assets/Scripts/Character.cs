using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public int maxHp = 1000;
    public int currentHp = 1000;
    [HideInInspector] public bool dead = false;
    [HideInInspector] public bool invincible = false;
    [SerializeField] StatusBar hpBar;

    public void TakeDamage(int damage){
        if (invincible) {
            return;
        }
        currentHp -= damage;
        if (currentHp <= 0) {
            dead = true;
            Debug.Log("Character is dead");
        }
        hpBar.SetState(currentHp, maxHp);
    }

    public void Heal(int amount) {
        if (currentHp <= 0) {
            currentHp += amount;
            if (currentHp >= maxHp) {
                currentHp = maxHp;
            }   
            hpBar.SetState(currentHp, maxHp);
        }
    }
}
