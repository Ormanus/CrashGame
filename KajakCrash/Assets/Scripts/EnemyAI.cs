using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyAI : MonoBehaviour {

    Transform target;
    float speed;
    List<GameObject> computers;
    float timer = 0;

	// Use this for initialization
	void Start () {
        computers = new List<GameObject>();
        speed = 5;
        GameObject[] comp = GameObject.FindGameObjectsWithTag("Computer");
        for(int i = 0; i < comp.Length; i++)
        {
            computers.Add(comp[i]);
        }
        updateTarget();
	}
	
	// Update is called once per frame
	void Update () {
        timer += Time.deltaTime;

        if(timer > 1.0f)
        {
            timer -= 1.0f;
            updateTarget();
            Debug.Log("update");
            if(target == null)
                Debug.Log("null");
        }
	}

    void FixedUpdate()
    {
        if (target != null)
        {
            Vector2 delta = target.position - transform.position;
            if (delta.magnitude < 1.0f)
            {
                GetComponent<Rigidbody2D>().velocity = new Vector3(0, 0, 0);
                target.GetComponent<Computer>().state = ComputerState.Running;
                updateTarget();
            }
            else
            {
                //move
                float direction = Mathf.Atan2(delta.y, delta.x) + Mathf.PI * 2;

                GetComponent<Rigidbody2D>().velocity = new Vector3(Mathf.Cos(direction) * speed, Mathf.Sin(direction) * speed);
            }
        }
    }

    void updateTarget()
    {
        Transform nearest = null;
        Vector2 delta;
        float distance = float.MaxValue;
        for (int i = 0; i < computers.Count; i++)
        {
            delta = transform.position - computers[i].transform.position;
            if (nearest == null && computers[i].GetComponent<Computer>().state != ComputerState.Running)
            {
                nearest = computers[i].transform;
                distance = delta.magnitude;
            }
            else
            {
                
                if (delta.magnitude < distance && computers[i].GetComponent<Computer>().state != ComputerState.Running)
                {
                    distance = delta.magnitude;
                    nearest = computers[i].transform;
                }
            }
        }
        if(nearest != null)
            target = nearest;
    }
}
