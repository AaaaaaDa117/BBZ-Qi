﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimTestCircle : MonoBehaviour
{
    Animator anim;

    int count;
    // Start is called before the first frame update
    void Start()
    {
        count = 0;
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        count++;
        if (count == 100)
        {
            anim.SetTrigger("Big");
            count = 0;
        }
    }
}
