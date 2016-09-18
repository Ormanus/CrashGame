using UnityEngine;
using System.Collections;

public class Minigame : MonoBehaviour {

    string password;
    string text;
    string[] pool;

    public bool hacking;

    Computer computer;

    void Start()
    {
        pool = new string[]
        {
            "asd",
            "dsa",
            ".exe",
            "lolollol"
        };
        password = "asd";
        text = "";
    }

    void Update()
    {
        if (!hacking)
        {
            computer.state = ComputerState.Running;
            text = "";
            return;
        }

        foreach (char c in Input.inputString)
        {
            if (c == "\b"[0])
                if (text.Length != 0)
                    text = text.Substring(0, text.Length - 1);
                else
                    text += c;
        }

        for(int x = 0; x < text.Length; x++)
        {
            if(text[x] != password[x])
            {
                computer.state = ComputerState.Off;
                return;
            }
        }

        if(text.Length == password.Length)
        {
            computer.state = ComputerState.Bluescreen;
            return;
        }
    }

    public void Hack(Computer comp)
    {
        computer = comp;
        password = pool[Random.Range(0, pool.Length)];
        text = "";
    }
}
