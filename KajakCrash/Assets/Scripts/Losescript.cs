﻿using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Losescript : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            SceneManager.LoadScene("Mainmenu");
            Debug.Log("hävisit");
        }

    }
}
