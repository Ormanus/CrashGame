using UnityEngine;
using System.Collections;
using System.Collections.Generic;

class searchNode
{
    public searchNode previous;
    public Waypoint node;
    public float distance;
    public float total;
    public Vector2 pos;
    public searchNode(Vector2 position, searchNode prev, float prevDist, Waypoint _node = null)
    {
        node = _node;
        previous = prev;
        pos = position;
        if(prev != null)
        {
            Vector2 delta = previous.pos - pos;
            distance = delta.magnitude;
            total = prevDist + distance;
        }
        else
        {
            distance = 0;
            total = 0;
        }
    }
}

class ClosedList
{
    List<searchNode> list;
    public ClosedList()
    {
        list = new List<searchNode>();
    }

    public searchNode Contains(Waypoint wp)
    {
        for(int i = 0; i < list.Count; i++)
        {
            if(list[i].node == wp)
            {
                return list[i];
            }
        }
        return null;
    }

    public void Add(searchNode node)
    {
        list.Add(node);
    }

    public void Remove(searchNode node)
    {
        list.Remove(node);
    }
}

class OpenList
{
    List<searchNode> list;
    public int Count;
    public OpenList()
    {
        list = new List<searchNode>();
        Count = 0;
    }
    public searchNode GetNode()
    {
        if (Count == 0)
            return null;

        searchNode lowest = list[0];
        for(int i = 1; i < list.Count; i++)
        {
            if(list[i].total < lowest.total)
            {
                lowest = list[i];
            }
        }
        return lowest;
    }
    public void Add(searchNode node)
    {
        for(int i = 0; i < list.Count; i++)
        {
            if (list[i].node == node.node)
            {
                if (list[i].total > node.total)
                    list[i].total = node.total;
                return;
            }
        }
        list.Add(node);
        Count++;
    }

    public void Remove(searchNode node)
    {
        list.Remove(node);
        Count--;
    }

    public searchNode Contains(GameObject go)
    {
        for(int i = 0; i < Count; i++)
        {
            if(list[i].node.gameObject == go)
            {
                return list[i];
            }
        }
        return null;
    }

    public searchNode this[int i]
    {
        get { return list[i]; }
        set { list[i] = value; }
    }
}

public class EnemyAI : MonoBehaviour {

    List<Vector2> path;
    Vector2 target;
    float speed;
    List<GameObject> computers;
    float updateTimer = 0;
    float fixTimer = 0;

    bool hasTarget;

    GameObject[] nodes;

	// Use this for initialization
	void Start () {
        path = null;
        nodes = GameObject.FindGameObjectsWithTag("Waypoint");
        computers = new List<GameObject>();
        speed = 5;
        GameObject[] comp = GameObject.FindGameObjectsWithTag("computer");
        for(int i = 0; i < comp.Length; i++)
        {
            computers.Add(comp[i]);
        }
        WaypointTarget();
	}
	
	// Update is called once per frame
	void Update () {
        updateTimer += Time.deltaTime;

        if (updateTimer > 1.0f)
        {
            updateTimer -= 1.0f;
            if(!hasTarget)
            {
                Debug.Log("searching for targets..");
                WaypointTarget();
                if (!hasTarget)
                    Debug.Log("target == null");
            }
        }
	}

    void FixedUpdate()
    {
        if (hasTarget)
        {
            Vector2 delta = target - (Vector2)transform.position;
            if (delta.magnitude < 0.1f)
            {
                if(path.Count == 1)
                {
                    //enemy at the last node (computer)
                    GetComponent<Rigidbody2D>().velocity = new Vector3(0, 0, 0);
                    fixTimer += Time.fixedDeltaTime;
                    if (fixTimer > 2.0f)
                    {
                        Transform comp = GetNearestComputer();
                        comp.GetComponent<Computer>().state = ComputerState.Running;
                        fixTimer = 0.0f;
                        WaypointTarget();
                    }
                }
                else
                {
                    path.RemoveAt(0);
                    target = path[0];
                }
            }
            else
            {
                //move
                float direction = Mathf.Atan2(delta.y, delta.x) + Mathf.PI * 2;

                GetComponent<Rigidbody2D>().velocity = new Vector3(Mathf.Cos(direction) * speed, Mathf.Sin(direction) * speed);
            }
        }
        if(path == null)
        {
            WaypointTarget();
        }
        else if(path.Count > 0)
        {
            target = path[0];
        }
    }

    Transform GetNearestComputer()
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
        return nearest;
    }

    void WaypointTarget()
    {
        OpenList openList = new OpenList();
        ClosedList closedList = new ClosedList();

        //add non-running computers to the list
        for(int i = 0; i < computers.Count; i++)
        {
            if(computers[i].GetComponent<Computer>().state != ComputerState.Running)
            {
                searchNode n = new searchNode(computers[i].transform.position, null, 0);
                Transform tr = getNearestNode(computers[i].transform.position);
                Vector2 pos = tr.position;
                searchNode n2 = new searchNode(pos, n, (pos - n.pos).magnitude, tr.gameObject.GetComponent<Waypoint>());

                openList.Add(n2);
            }
        }

        if(openList.Count == 0)
        {
            return;
        }

        //get goal node
        GameObject goal = getNearestNode(transform.position).gameObject;

        searchNode first = null;

        int count = 0;
        while(openList.Count > 0)
        {



            if(count++ > 10000)
            {
                Debug.Log("ERROR CODE 1: Pathfindging failed. Please consult Olli for further information.");
                hasTarget = false;
                return;
            }

            searchNode current = openList.GetNode();
            openList.Remove(current);
            closedList.Add(current);

            if(current.node.gameObject == goal)
            {
                first = current;
                break;
            }

            for(int i = 0; i < current.node.Count; i++)
            {
                GameObject next = current.node.connections[i];
                float g = (current.pos - (Vector2)next.transform.position).magnitude;
                searchNode nextNode = openList.Contains(next);
                if(nextNode != null)
                {
                    if(nextNode.total < current.total + g)
                    {
                        continue;
                    }
                }
                else
                {
                    nextNode = closedList.Contains(next.GetComponent<Waypoint>());
                    if (nextNode != null)
                    {
                        if (nextNode.total < current.total + g)
                        {
                            continue;
                        }
                        else
                        {
                            closedList.Remove(nextNode);
                            //openList.Add(nextNode);
                        }
                    }
                    else
                    {
                        nextNode = new searchNode(next.transform.position, current, current.total, next.GetComponent<Waypoint>());
                        openList.Add(nextNode);
                    }
                }
            }
        }
        if (first == null)
        {
            Debug.Log("ERROR CODE 2; Pathfindging failed. Please consult Olli for further information.");
            hasTarget = false;
            return;
        }

        path = new List<Vector2>();

        //generate path
        while (first.previous != null)
        {
            Debug.DrawLine(first.pos, first.previous.pos, Color.green, 0.8f);

            path.Add(first.pos);
            first = first.previous;
        }
        hasTarget = true;
        return;
    }

    Transform getNearestNode(Vector2 pos)
    {
        if(nodes.Length == 0)
        {
            Debug.Log("ERROR: No waypoints found!");
            return null;
        }

        Transform nearest = nodes[0].transform;
        Vector2 delta = pos - (Vector2)nearest.transform.position;
        float distance = delta.magnitude;
        for (int i = 1; i < nodes.Length; i++)
        {
            delta = pos - (Vector2)nodes[i].transform.position;
            if(delta.magnitude < distance)
            {
                distance = delta.magnitude;
                nearest = nodes[i].transform;
            }
        }
        return nearest;
    }
}
