using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Trigger : MonoBehaviour
{
    Animator animator;
    public Text name_text;
    public GameObject minigameObject;
    bool playerIsNear;
    Computer computer;
    public static Minigame minigame;

    PlayAudio audio;

    void Start()
    {
        audio = GameObject.Find("EffectSource").GetComponent<PlayAudio>();
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
            name_text.text = "";
            audio.PlaySound(2);
            //gameObject.GetComponent<Computer>().state = ComputerState.Off;
        }
        if (computer.state== ComputerState.Running)
        {
            animator = gameObject.GetComponentInChildren<Animator>();
            animator.SetInteger("tila", 0);
           // animator = gameObject.GetComponentInChildren
            
        }
        if (computer.state == ComputerState.Off)
        { 
            animator = gameObject.GetComponentInChildren<Animator>();
            animator.SetInteger("tila", 1);
        }

        if (computer.state == ComputerState.Bluescreen)
        {
            animator = gameObject.GetComponentInChildren<Animator>();
            animator.SetInteger("tila", 2);
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
            other.GetComponent<Animator>().SetBool("hack", false);
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
