using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {

    float direction = 0;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void FixedUpdate()
    {
        Rigidbody2D rb = gameObject.GetComponent<Rigidbody2D>();
        rb.velocity = Vector2.zero;

        float speed = 5;
        float angularSpeed = 0.05f;

        if (Input.GetAxisRaw("Horizontal") > 0.5f)
        {
            direction -= angularSpeed;
        }
        if (Input.GetAxisRaw("Horizontal") < -0.5f)
        {
            direction += angularSpeed;
        }
        if (Input.GetAxisRaw("Vertical") > 0.5f)
        {
            rb.velocity += new Vector2(Mathf.Cos(direction), Mathf.Sin(direction)) * speed;
        }
        if (Input.GetAxisRaw("Vertical") < -0.5f)
        {
            rb.velocity -= new Vector2(Mathf.Cos(direction), Mathf.Sin(direction)) * speed;
        }

        transform.eulerAngles = new Vector3(0, 0, direction / Mathf.PI * 180);
    }
}
