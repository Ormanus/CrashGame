using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public enum GameState
{
    Game,
    Win,
    Lose
}


public class SceneController : MonoBehaviour {
    Animator animator;
    public Text displayCount;


    float timer;
    public static GameState state;

	// Use this for initialization
	void Start () {
        timer = 60;
        state = GameState.Game;
        displayCount = GameObject.Find("DisplayCount").GetComponentInChildren<Text>();
        displayCount.text = "Tässä näkyy aika";

    }

    // Update is called once per frame
    void Update() {
        timer -= Time.deltaTime;

        //Adds timer count to displayCount
        if (timer >= 0)
        {
            animator = GetComponent<Animator>();
            animator.SetFloat("timer", timer);
            displayCount.text =  timer.ToString("0");
        }

        if (timer < 0)
        {
            state = GameState.Win;
            //count bluescreened computers
            GameObject[] computers = GameObject.FindGameObjectsWithTag("Computer");
            int score = 0;
            for(int i = 0; i < computers.Length; i++)
            {
                if(computers[i].GetComponent<Computer>().state == ComputerState.Bluescreen)
                {
                    score++;
                }
            }

            //@TODO: display score
            EndGame(GameState.Win);
        }
	}
    public void EndGame(GameState endType)
    {
        state = endType;
        Invoke("changeScene", 5.0f);
    }

    void changeScene()
    {
        if (state == GameState.Lose)
        {
            SceneManager.LoadScene("LoseScene");
        }
        else
        {
            SceneManager.LoadScene("WinScene");
        }

    }

}
