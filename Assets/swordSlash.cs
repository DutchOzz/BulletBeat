using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class swordSlash : MonoBehaviour
{

    Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponentInChildren<Animator>();
        animator.SetTrigger("Slash");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.L)) {
            animator.SetTrigger("Slash");
        }
    }
}