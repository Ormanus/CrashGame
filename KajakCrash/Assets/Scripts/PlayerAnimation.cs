using UnityEngine;
using System.Collections;

public class PlayerAnimation : MonoBehaviour {
    Animator animator;
    bool hacking;
    bool running;

    // Use this for initialization
    void Start() {
        animator = GetComponent<Animator>();
        
    }

    // Update is called once per frame
    void Update() {
        running = Input.GetKey(KeyCode.UpArrow);

        animator.SetBool("run", running);
    }

    void onTriggerEnter2D(Collider2D other )
    {
        hacking = true;
        animator.SetBool("hack", hacking);
    }
    void onTriggerExit2D(Collider2D other)
    {
        hacking = false;
        animator.SetBool("hack", hacking);

    }
    
    
}
