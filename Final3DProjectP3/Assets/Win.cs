using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static UnityEditor.Timeline.TimelinePlaybackControls;

public class Win : MonoBehaviour
{
    public TextMeshProUGUI WinText;

    public void WinBox()
    {
        WinText.gameObject.SetActive(true);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
