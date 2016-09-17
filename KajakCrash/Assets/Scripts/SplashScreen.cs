using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class SplashScreen : MonoBehaviour {

    public GameObject canvas;
    float timer;

	// Use this for initialization
	void Start () {
        timer = 8.0f;
        canvas.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
        timer -= Time.deltaTime;
        if(timer < 0)
        {
            canvas.SetActive(true);
        }
	}
}
