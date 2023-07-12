using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Tower : MonoBehaviour
{
    public abstract void ReceiveBeat();

    public abstract void ReceiveHalfBeat();
}
