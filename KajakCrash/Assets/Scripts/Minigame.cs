
﻿using UnityEngine;
using System.Collections;


public class Minigame : MonoBehaviour {

    
string password;
    
string text;
    
string[] pool;

    
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
        
password = "qwertyiuop";

        text = "";
 
   }

    
void Update()

    {
 
       foreach (char c in Input.inputString)

        {

            if(text.Length == 0 && c == ' ')

            {

                continue;

            }

            if (c == "\b"[0])

                if (text.Length != 0)

                    text = text.Substring(0, text.Length - 1);

                else
                    text += c;
            Debug.Log(text);
        }

        for(int x = 0; x < text.Length; x++)
        {
            if(text[x] != password[x])
            {
                computer.state = ComputerState.Off;
                DestroyObject(gameObject);
                return;
            }
        }

        if(text.Length == password.Length)
        {
            computer.state = ComputerState.Bluescreen;
            DestroyObject(gameObject);
            return;
        }
    }

    public void Hack(Computer comp)
    {
        computer = comp;
        password = pool[Random.Range(0, pool.Length)];
        text = "";
        Debug.Log(password);
    }
}
