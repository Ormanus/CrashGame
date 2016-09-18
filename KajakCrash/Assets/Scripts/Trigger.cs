using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Trigger : MonoBehaviour
{

    public Text name_text;
    public GameObject minigameObject;
    bool playerIsNear;
    Computer computer;
    public static Minigame minigame;

    void Start()
    {
        playerIsNear = false;
        name_text = GameObject.Find("SpaceCanvas").GetComponentInChildren<Text>();
        computer = gameObject.GetComponent<Computer>();
        //name_text.text = "teksti toimii";
        Debug.Log("text found = " + name_text);
        minigame = null;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && playerIsNear && minigame == null)
        {
            playerIsNear = false;
            minigame = Instantiate(minigameObject).GetComponent<Minigame>();
            minigame.Hack(computer);
            //gameObject.GetComponent<Computer>().state = ComputerState.Off;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && computer.state == ComputerState.Running)
        {
            other.GetComponent<Animator>().SetBool("hack", true);
            name_text.text = "press space";
            playerIsNear = true;

        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            name_text.text = "";
            playerIsNear = false;
            if (minigame != null)
            {
                minigame.stopHack();
                minigame = null;
            }
        }
    }
}
