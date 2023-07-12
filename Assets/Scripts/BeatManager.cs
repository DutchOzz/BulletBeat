using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeatManager : MonoBehaviour
{
    [SerializeField] float tempo;
    [SerializeField] List<GameObject> towers;
    private float beatTimer;
    private bool halfBeat;

    // Start is called before the first frame update
    void Start()
    {
        beatTimer = tempo/2;
        halfBeat = true;
    }

    // Update is called once per frame
    void Update()
    {
        beatTimer = beatTimer - Time.deltaTime;
        if (beatTimer < 0) {
            if (halfBeat) {
                foreach (GameObject tower in towers)  {
                    tower.GetComponent<Tower>().ReceiveHalfBeat();
                }
            } 
            else {
                foreach (GameObject tower in towers)  {
                    tower.GetComponent<Tower>().ReceiveBeat();
                }
            }
            halfBeat = !halfBeat;
            beatTimer = tempo/2;
        }
    }

    public void addTower(GameObject tower) {
        towers.Add(tower);
    }
}
