using UnityEngine;
using System.Collections;
using System.Collections.Generic;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class Waypoint : MonoBehaviour {

    [SerializeField]
    public GameObject[] connections;
    public int Count;

    public void AddConnection(GameObject go)
    {
        GameObject[] newList = new GameObject[connections.Length + 1];
        for(int i = 0; i < connections.Length; i++)
        {
            newList[i] = connections[i];
        }
        newList[newList.Length - 1] = go;
        connections = newList;
        Count++;
    }

    public void RemoveConnection(GameObject go)
    {
        bool itemFound = false;
        for (int i = 0; i < connections.Length; i++)
        {
            if(connections[i] == go)
            {
                itemFound = true;
                for (int j = i; j < connections.Length - 1; j++)
                {
                    connections[j] = connections[j + 1];
                }
                break;
            }
        }
        if(itemFound)
        {
            GameObject[] newList = new GameObject[connections.Length - 1];
            for (int i = 0; i < connections.Length - 1; i++ )
            {
                newList[i] = connections[i];
            }
            connections = newList;
            Count--;
        }
    }

	void Start () {
        if(connections == null)
        {
            connections = new GameObject[0];
            Count = 0;
        }
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

                Undo.RecordObject(w1, "Disconnect");
                Undo.RecordObject(w2, "Disconnect");
                EditorUtility.SetDirty(w1);
                EditorUtility.SetDirty(w2);

                w1.RemoveConnection(nodes[k].gameObject);
                w2.RemoveConnection(nodes[i].gameObject);
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

                    for (int j = 0; j < w1.Count; j++)
                    {
                        if (w1.connections[j] == nodes[k])
                        {
                            found = true;
                            break;
                        }
                    }
                    if (!found)
                    {
                        Undo.RecordObject(w1, "Connect");
                        Undo.RecordObject(w2, "Connect");

                        w1.AddConnection(nodes[k].gameObject);
                        w2.AddConnection(nodes[i].gameObject);

                        EditorUtility.SetDirty(w1);
                        EditorUtility.SetDirty(w2);
                    }
                }
            }
        }
    }

    void OnDrawGizmos()
    {
        if(connections == null)
        {
            Start();
        }

        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(transform.position, 0.04f);

        for (int i = 0; i < Count; i++)
        {
            Gizmos.color = Color.white;
            Gizmos.DrawLine(transform.position, connections[i].transform.position);
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(transform.position, 0.05f);
    }

#endif
}
