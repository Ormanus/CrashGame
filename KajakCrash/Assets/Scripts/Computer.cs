using UnityEngine;
using System.Collections;

public enum ComputerState
{
    Running,
    Off,
    Bluescreen
};

public class Computer : MonoBehaviour {

    public ComputerState state;

	// Use this for initialization
	void Start () {
        state = ComputerState.Bluescreen;
	}
	
	// Update is called once per frame
	void Update () {
	    
	}
}
