using UnityEngine;
using System.Collections;

public class TextController : MonoBehaviour {

    public GameObject Player;

    private Vector2 offset;

	// Use this for initialization
	void Start () {
        offset = transform.position - Player.transform.position;
	}
	
	// Update is called once per frame
	void Update () {
        transform.position = Player.transform.position + offset;
	}
}
