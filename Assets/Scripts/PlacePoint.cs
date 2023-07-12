using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacePoint : MonoBehaviour
{
    [SerializeField] GameObject HighLighted;
    public GameObject BeatManager;

    public GameObject HighLight() {
        GameObject high = Instantiate(HighLighted);
        high.transform.position = transform.position;
        high.GetComponent<HighLightedPlacePoint>().beatManager = BeatManager;
        Destroy(gameObject);
        return high;
    }
}
