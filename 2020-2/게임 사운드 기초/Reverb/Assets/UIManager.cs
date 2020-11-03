using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public AudioReverbZone arz;
    public Text modetext;
    public Text status;

    int statusTarget = 0;
    int mode = 0;

    private void Start()
    {
        arz.reverbPreset = (AudioReverbPreset)mode;
    }

    private void Update()
    {
        ChangeMode();
        SetReverb();
        ShowInfo();
    }

    void ChangeMode()
    {
        if(Input.GetKeyDown(KeyCode.Q))
        {
            mode--;
            if (mode < 0)
                mode = 27;
            arz.reverbPreset = (AudioReverbPreset)mode;
            modetext.text = arz.reverbPreset.ToString();
        }
        if(Input.GetKeyDown(KeyCode.E))
        {
            mode++;
            mode %= 28;
            arz.reverbPreset = (AudioReverbPreset)mode;
            modetext.text = arz.reverbPreset.ToString();
        }
        if(mode == (int)AudioReverbPreset.User)
        {
            arz.roomHF = 0;
        }
    }
    void SetReverb()
    {
        if(Input.GetKey(KeyCode.K))
        {
            arz.reverb++;
        }
        if(Input.GetKey(KeyCode.J))
        {
            arz.reverb--;
        }
        if(Input.GetKey(KeyCode.U))
        {
            arz.room -= 15;
        }
        if (Input.GetKey(KeyCode.I))
        {
            arz.room += 15;
        }
        if (Input.GetKey(KeyCode.N))
        {
            arz.decayTime -= 0.01f;
        }
        if (Input.GetKey(KeyCode.M))
        {
            arz.decayTime += 0.01f;
        }
    }

    void ShowInfo()
    {
        status.text = "Room : " + arz.room + "\n";
        status.text += "Decay Time : " + arz.decayTime + "\n";
        status.text += "Reverb : " + arz.reverb + "\n";
    }
}
