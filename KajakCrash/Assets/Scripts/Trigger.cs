using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Trigger : MonoBehaviour
{

    public Text name_text;
    bool playerIsNear;

    void start()
    {
        playerIsNear = false;
        name_text.text = "teksti toimii";
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && playerIsNear)
        {
            //Debug.Log("Spaaaace!");
            gameObject.GetComponent<Computer>().state = ComputerState.Off;

        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            name_text.text = "space available";
            playerIsNear = true;
            other.GetComponent<Animator>().SetBool("hack", true);
            Debug.Log("Nud Hagsaan niin maan bendeleesti");
        }

    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            name_text.text = "";
            playerIsNear = false;
            other.GetComponent<Animator>().SetBool("hack", false);
        }
    }
}
