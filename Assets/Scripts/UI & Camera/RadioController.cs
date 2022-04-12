using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using TMPro;

public class RadioController : MonoBehaviour
{
    [SerializeField] private AudioSource source;
    [SerializeField] private AudioSource button;
    [SerializeField] private AudioClip lofi;
    [SerializeField] private AudioClip metal;
    [SerializeField] private AudioClip sunday;
    [SerializeField] private AudioClip mami;
    [SerializeField] private AudioClip tropic;
    [SerializeField] private AudioClip hiphop;



    private AudioClip[] channel = new AudioClip[6];
    private int currentChannel = 0;

    [SerializeField] private TMP_Text channelText;

    // Start is called before the first frame update
    void Start()
    {
        channel[0] = lofi;
        channel[1] = metal;
        channel[2] = sunday;
        channel[3] = mami;
        channel[4] = tropic;
        channel[5] = hiphop;

        source.clip = channel[currentChannel];
        currentChannel = Mathf.Abs(currentChannel - 1) % (channel.Length);
        channelText.text = "Nightdrive";
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Q))
        {
            source.clip = channel[Mathf.Abs(currentChannel - 1) % (channel.Length)];
            currentChannel = Mathf.Abs(currentChannel - 1) % (channel.Length);
            channelText.text = channel[currentChannel].name;
            source.Play();
            button.Play();
        }
        

        if(Input.GetKeyDown(KeyCode.E))
        {
            print(source.clip);
            source.clip = channel[Mathf.Abs(currentChannel + 1) % (channel.Length)];
            currentChannel = Mathf.Abs(currentChannel + 1) % (channel.Length);
            channelText.text = channel[currentChannel].name;
            button.Play();
            source.Play();
        }
    }
}
