using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Menuscripts : MonoBehaviour
{
    Animator animator;
    Animator animator2;
    Animator animator3;
    Animator animator4;
    public bool menu1;
    public bool menu2;
    public bool menu3;
    public bool menu4;
    public GameObject menu1gameobject;
    public GameObject menu2gameobject;
    public GameObject menu3gameobject;
    public GameObject menu4gameobject;
    // Use this for initialization
    void Start()
    {
        animator = menu1gameobject.GetComponent<Animator>();
        animator2 = menu2gameobject.GetComponent<Animator>();
        animator3 = menu3gameobject.GetComponent<Animator>();
        animator4 = menu4gameobject.GetComponent<Animator>();
        menu1 = true;
        //audio.Play();
    }


    // Update is called once per frame
    void Update()
    {

        if (menu1 == true)
        {

            //animointi koodi menu 1
            animator.SetBool("menu1animation", true);

            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                animator.SetBool("menu1animation", false);
                //Debug.Log("Activate");
                menu1 = false;
                menu2 = true;
                Debug.Log("menu2");
            }
            if (Input.GetKey(KeyCode.KeypadEnter) || Input.GetKey(KeyCode.Return))

                SceneManager.LoadScene ("level1");

        }

        else if (menu2 == true)
        {
            //animointi koodi menu 2
            animator2.SetBool("menu2animation", true);

            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                animator2.SetBool("menu2animation", false);
                //Debug.Log("Activate");
                menu2 = false;
                menu3 = true;
                Debug.Log("menu3");
            }

            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                animator2.SetBool("menu2animation", false);
                //Debug.Log("Activate");
                menu2 = false;
                menu1 = true;
                Debug.Log("menu1");
            }

        }

        else if (menu3 == true)
        {
            //animointi koodi menu 3
            animator3.SetBool("menu3animation", true);

            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                animator3.SetBool("menu3animation", false);
                //Debug.Log("Activate");
                menu3 = false;
                menu4 = true;
                Debug.Log("menu4");
            }

            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                animator3.SetBool("menu3animation", false);
                //Debug.Log("Activate");
                menu3 = false;
                menu2 = true;
                Debug.Log("menu2");
            }

        }

        else if (menu4 == true)
        {
            //animointi koodi menu 4
            animator4.SetBool("menu4animation", true);

            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                animator4.SetBool("menu4animation", false);
                //Debug.Log("Activate");
                menu4 = false;
                menu3 = true;
                Debug.Log("menu3");
            }

            if (Input.GetKeyDown(KeyCode.KeypadEnter) || Input.GetKey(KeyCode.Return))
            {
                
                Application.Quit();
                Debug.Log("Exit");
            }    
        }

    }
    /*
    public void JeeJeeMenu()
    {
        Debug.Log("JEE JEE");
    }

    public void JooJooMenu()
    {
        Debug.Log("joo joo");
    }
    */
}
