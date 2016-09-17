using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Introscript : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	if (Input.GetKeyDown(KeyCode.Return))
        {
            SceneManager.LoadScene("GameScene");
            Debug.Log("Kenttä");
        }

    if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("Mainmenu");
        }

    }
}
