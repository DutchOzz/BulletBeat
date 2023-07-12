using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlaceTowers : MonoBehaviour
{
    [SerializeField] int startingCurrency;
    [SerializeField] float detectionRange;
    [SerializeField] GameObject uiManager;
    private GameObject previousClosestTower;
    private int currency;

    // Start is called before the first frame update
    void Start()
    {
        previousClosestTower = null;
        currency = startingCurrency;
        uiManager.GetComponent<uiManager>().UpdateScoreCounter(currency);
    }



    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            PlaceTower(0);
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            PlaceTower(1);
        }
        
        GameObject closestTower = GetClosestTower();
        if (previousClosestTower == closestTower) {
            return;
        }
        if (previousClosestTower != null) {
            previousClosestTower.GetComponent<HighLightedPlacePoint>().UnLight();
        } 
        if (closestTower != null) {
            previousClosestTower = closestTower.GetComponent<PlacePoint>().HighLight();
        } else {
            previousClosestTower = null;
        }
    }

    private GameObject GetClosestTower()
    {
        Collider2D[] hit = Physics2D.OverlapCircleAll(transform.position, detectionRange);
        List<Collider2D> towers = new List<Collider2D>();
        foreach (Collider2D c in hit) {
            if (c.tag == "PlacePoint") {
                towers.Add(c);
            }
        }
        if (towers.Count != 0) {
            Collider2D[] towerArr = towers.ToArray();
            Collider2D bestTarget = null;
            float closestDistanceSqr = Mathf.Infinity;
            Vector3 currentPosition = transform.position;
            foreach(Collider2D potentialTarget in towerArr)
            {
                Vector3 directionToTarget = potentialTarget.gameObject.transform.position - currentPosition;
                float dSqrToTarget = directionToTarget.sqrMagnitude;
                if(dSqrToTarget < closestDistanceSqr)
                {
                    closestDistanceSqr = dSqrToTarget;
                    bestTarget = potentialTarget;
                }
            }
            return bestTarget.gameObject;
        }
        return null;
    }

    private void PlaceTower(int index) {
        if (previousClosestTower != null) {
            int cost = previousClosestTower.GetComponent<HighLightedPlacePoint>().PlaceTower(index);
            currency = currency - cost;
            uiManager.GetComponent<uiManager>().UpdateScoreCounter(currency);
            previousClosestTower = null;
        }
    }
    
    public void PickUpCoin() {
        currency = currency + 1;
        uiManager.GetComponent<uiManager>().UpdateScoreCounter(currency);
    }
}
