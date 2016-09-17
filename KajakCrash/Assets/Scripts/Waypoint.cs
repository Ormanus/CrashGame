﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class Waypoint : MonoBehaviour {

    public List<Transform> connections;

	void Start () {
        connections = new List<Transform>();
	}

#if UNITY_EDITOR

    [MenuItem("My Commands/Disconnect %k")]
    static void Disconnect()
    {
        Debug.Log("Ctrl+K");

        List<Transform> nodes = new List<Transform>();

        for (int i = 0; i < Selection.transforms.Length; i++)
        {
            if (Selection.transforms[i].gameObject.tag == "Waypoint")
            {
                nodes.Add(Selection.transforms[i]);
            }
        }

        for(int i = 0; i < nodes.Count; i++)
        {
            for (int k = i + 1; k < nodes.Count; k++)
            {
                Waypoint w1 = nodes[i].GetComponent<Waypoint>();
                Waypoint w2 = nodes[k].GetComponent<Waypoint>();

                w1.connections.Remove(nodes[k].transform);
                w2.connections.Remove(nodes[i].transform);
            }
        }
    }

    [MenuItem("My Commands/Connect %l")]
    static void Connect()
    {
        Debug.Log("Ctrl+L");

        List<Transform> nodes = new List<Transform>();

        for (int i = 0; i < Selection.transforms.Length; i++)
        {
            if(Selection.transforms[i].gameObject.tag == "Waypoint")
            {
                nodes.Add(Selection.transforms[i]);
            }
        }
        for (int i = 0; i < nodes.Count; i++)
        {
            for (int k = i + 1; k < nodes.Count; k++)
            {
                if (nodes[i] != nodes[k])
                {
                    bool found = false;

                    Waypoint w1 = nodes[i].GetComponent<Waypoint>();
                    Waypoint w2 = nodes[k].GetComponent<Waypoint>();

                    for (int j = 0; j < w1.connections.Count; j++)
                    {
                        if (w1.connections[j] == nodes[k])
                        {
                            found = true;
                            break;
                        }
                    }
                    if (!found)
                    {
                        w1.connections.Add(nodes[k]);
                        w2.connections.Add(nodes[i]);
                    }
                }
            }
        }
    }

    void OnDrawGizmos()
    {
        if(connections == null)
        {
            connections = new List<Transform>();
        }

        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(transform.position, 0.04f);

        for (int i = 0; i < connections.Count; i++)
        {
            Gizmos.color = Color.white;
            Gizmos.DrawLine(transform.position, connections[i].position);
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(transform.position, 0.05f);
    }

#endif
}
