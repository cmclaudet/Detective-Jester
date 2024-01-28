using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class SoundClips
{
    public AudioClip audio;
    public string name;
}
public class SoundManager : MonoBehaviour
{
    public List<AudioClip> Clips = new List<AudioClip>();

    private AudioSource audio;
    // Start is called before the first frame update
    void Start()
    {
        audio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void playSound(string clipName)
    {
        AudioClip sc = Clips.Find((x) => x.name == clipName);
        audio.clip = sc;
        audio.Play();
    }

}
