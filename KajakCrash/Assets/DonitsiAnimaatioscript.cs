using UnityEngine;
using System.Collections;
using System;

public class DonitsiAnimaatioscript : MonoBehaviour {
    Animator animator;

	// Use this for initialization
	void Start () {
        animator = GetComponent<Animator>();
        
	}

    int interval = 1;
    float nextTime = 0;

    // Update is called once per frame
    void Update() {
        if (Time.time >= nextTime)
        {

            System.Random rnd = new System.Random();
            int tila = rnd.Next(1, 5);
            bool syö = (tila == 3);
            bool juo = (tila == 4);
            
            animator.SetBool("jano", juo);
            animator.SetBool("nälkä", syö);

            nextTime += interval;
        }
    }
        
	
	}

