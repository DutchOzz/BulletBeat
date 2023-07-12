using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]

public class Coin : MonoBehaviour
{
    [SerializeField] GameObject PickedUpCoin;

    public void PickUp(GameObject t) {
        GameObject pick = Instantiate(PickedUpCoin);
        pick.transform.position = transform.position;
        pick.GetComponent<PickedUpCoin>().PushBack(t);
        Destroy(gameObject);
    }
}
