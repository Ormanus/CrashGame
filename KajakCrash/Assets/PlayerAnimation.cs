using UnityEngine;
using System.Collections;

public class PlayerAnimation : MonoBehaviour {
    Animator animator;
    

	// Use this for initialization
	void Start () {
        animator = GetComponent<Animator>();
        
	}
	
	// Update is called once per frame
	void Update () {
        bool running = Input.GetKey(KeyCode.UpArrow);
        

        animator.SetBool("run", running);
	
	}
    
    
    
}
