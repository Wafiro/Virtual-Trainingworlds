using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

public class CameraMovement : MonoBehaviour
{

    public PlayableDirector director;
    public List<TimelineAsset> timelines;

    public void Play()
    {
        director.Play();
    }


    public void PlayTimeline(int index)
    {
        TimelineAsset selected;

        if (timelines.Count <= index)
        {
            selected = timelines[timelines.Count - 1];
        }
        else
        {
            selected = timelines[index];
        }
        
        Debug.Log("here");
        director.Play(selected);
    }

}
