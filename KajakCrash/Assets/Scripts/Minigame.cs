
﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Minigame : MonoBehaviour {

    string password;
    string text;
    string[] pool;
    Computer computer;
    Text display;
    
    void Start()
    {
        Debug.Log("Start Minigame");

        display = GetComponentInChildren<Text>();

        pool = new string[14];
        pool[0] = "enter";
        pool[1] = "hax.exe";
        pool[2] = "qwerty";
        pool[3] = ".pexe";
        pool[4] = "CRASH";
        pool[5] = "gamejam";
        pool[6] = "trololoo";
        pool[7] = "your.mom";
        pool[8] = "unix";
        pool[9] = "magic";
        pool[10] = "eat.poo";
        pool[11] = "fear.me";
        pool[12] = "1337";
        pool[13] = "n00b";

        password = "qwertyiuop";
        text = "";
        password = pool[Random.Range(0, pool.Length)]; 
        display.text = password;
    }
    
void Update()
    {
       foreach (char c in Input.inputString)
        {
            Debug.Log("New key: " + c);
            if(text.Length == 0 && c == 32)
            {
                continue;
            }
            if (c == "\b"[0])
            {
                if (text.Length != 0)

                    text = text.Substring(0, text.Length - 1);
            }
            else
            {
                text += c;
                for (int x = 0; x < text.Length; x++)
                {
                    if (text[x] != password[x])
                    {
                        computer.state = ComputerState.Off;
                        //animator.SetInteger("tila", 2);
                        DestroyObject(gameObject);
                        return;
                    }
                }

                if (text.Length == password.Length)
                {
                    computer.state = ComputerState.Bluescreen;
                    //animator.SetInteger("tila", 1);
                    DestroyObject(gameObject);
                    return;
                }
            }
        }
    }

    public void Hack(Computer comp)
    {
        computer = comp;
        text = "";
    }

    public void stopHack()
    {
        if(gameObject != null)
            DestroyObject(gameObject);
    }
}
