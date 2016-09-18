using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Trigger : MonoBehaviour
{

    public Text name_text;
    public GameObject minigameObject;
    bool playerIsNear;

    void Start()
    {
        name_text = GameObject.Find("SpaceAvailable").GetComponentInChildren<Text>();
        playerIsNear = false;
        name_text.text = "teksti toimii";
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && playerIsNear)
        {
            Minigame minigame = Instantiate(minigameObject).GetComponent<Minigame>();
            minigame.Hack(gameObject.GetComponent<Computer>());
            //gameObject.GetComponent<Computer>().state = ComputerState.Off;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<Animator>().SetBool("hack", true);
            name_text.text = "space available";
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
        }
    }
}
