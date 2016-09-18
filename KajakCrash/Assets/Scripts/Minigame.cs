﻿
﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Minigame : MonoBehaviour {

    string password;
    string text;
    string[] pool;
    Computer computer;
    Text display;
    PlayAudio audio;
    
    void Start()
    {
        Debug.Log("Start Minigame");

        display = GetComponentInChildren<Text>();

        pool = new string[8];
        pool[0] = "asd";
        pool[1] = ".lololllo";
        pool[2] = "qwetryiu";
        pool[3] = ".exe";
        pool[4] = "CRASH";
        pool[5] = "GaMeJaM";
        pool[6] = "TrololoO";
        pool[7] = "Your mom.";
        
        password = "qwertyiuop";
        text = "";
        password = pool[Random.Range(0, pool.Length)]; 
        display.text = password;

        audio = GameObject.Find("EffectSource").GetComponent<PlayAudio>();
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
                        audio.PlaySound(1);
                        DestroyObject(gameObject);
                        return;
                    }
                }

                if (text.Length == password.Length)
                {
                    computer.state = ComputerState.Bluescreen;
                    audio.PlaySound(0);
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
