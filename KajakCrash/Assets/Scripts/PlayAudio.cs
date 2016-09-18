using UnityEngine;
using System.Collections;

public class PlayAudio : MonoBehaviour {

    public AudioClip BluescreenSound;
    public AudioClip ErrorSound;
    public AudioClip MinigameSound;

    AudioSource AS;

    public void PlaySound(int index)
    {
        switch(index)
        {
            case 0: AS.PlayOneShot(BluescreenSound); break;
            case 1: AS.PlayOneShot(ErrorSound); break;
            case 2: AS.PlayOneShot(MinigameSound); break;
        }
    }

	// Use this for initialization
	void Start () {
        AS = gameObject.GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
