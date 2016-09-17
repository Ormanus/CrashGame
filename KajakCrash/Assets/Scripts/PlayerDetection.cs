using UnityEngine;
using System.Collections;

public class PlayerDetection : MonoBehaviour {

    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Colliding to " + other.name);
        Vector2 delta = other.gameObject.transform.position - transform.position;    
        float distance = delta.magnitude;
        if (other.tag == "Player")
        {
            Debug.Log("Raycasting...");
            RaycastHit2D hit = Physics2D.Raycast(transform.position, delta.normalized, distance, 128);
            Debug.DrawRay(transform.position, delta, Color.blue, 1.0f);
            if (hit.collider == null)
            {
                Debug.Log("Game over!");
                //TheEnd = 3
            }
       }       
    }
}
