using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Trigger : MonoBehaviour
{

    public Text name_text;
    bool playerIsNear;

    public GameObject minigameObject;
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
            //Debug.Log("Spaaaace!");
            //gameObject.GetComponent<Computer>().state = ComputerState.Off;
            //minigame.gameObject.GetComponent<Image>().
            minigame.hacking = true;
            minigame.Hack(gameObject.GetComponent<Computer>());
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        name_text.text = "press space";
        playerIsNear = true;
    }

    void OnTriggerExit2D(Collider2D other)
    {
        name_text.text = "";
        playerIsNear = false;
        minigame.hacking = false;
        minigame.gameObject.SetActive(false);
    }
}
