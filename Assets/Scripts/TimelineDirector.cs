using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class TimelineDirector : MonoBehaviour
{
    public PlayableDirector timelineDirector;
    // Start is called before the first frame update
    void Start()
    {
        timelineDirector = GetComponent<PlayableDirector>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
