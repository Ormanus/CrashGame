using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Trigger : MonoBehaviour
{

    public Text name_text;
    public GameObject minigameObject;
    bool playerIsNear;
    public Minigame minigame;

    void start()
    {
        playerIsNear = false;
        name_text.text = "teksti toimii";
        minigame = minigameObject.GetComponent<Minigame>();
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
            name_text.text = "press space";
            playerIsNear = true;

        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        name_text.text = "";
        playerIsNear = false;
        minigame.stopHack();
    }
}
