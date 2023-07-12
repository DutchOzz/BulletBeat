using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighLightedPlacePoint : MonoBehaviour
{
    [SerializeField] GameObject NormalPoint;
    [SerializeField] GameObject[] Towers;
    //aiming tower
    //rotating tower
    [SerializeField] int[] towerCosts; 
    //aiming tower
    //rotating tower
    [HideInInspector] public GameObject beatManager;

    public void UnLight() {
        GameObject normal = Instantiate(NormalPoint);
        normal.transform.position = transform.position;
        normal.GetComponent<PlacePoint>().BeatManager = beatManager;
        Destroy(gameObject);
    }

    public int PlaceTower(int towerType) {
        GameObject tower = Instantiate(Towers[towerType]);
        tower.transform.position = transform.position;
        beatManager.GetComponent<BeatManager>().addTower(tower);
        Destroy(gameObject);
        return towerCosts[towerType];
    }
}
