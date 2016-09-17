using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Trigger : MonoBehaviour
{

    public Text name_text;

    private string testName = "Taman Pitaisi Nakua";

    void start()
    {
        name_text.text = "";
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log(gameObject.name + testName);
        //other.gameObject.SetActive(false);
        name_text.text = "Do YOU see this????";
    }

    void OnTriggerExit2D(Collider2D other)
    {
        name_text.text = "";
    }
}
