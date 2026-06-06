using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;
using System.Linq;

public class TimelineReader : MonoBehaviour
{
    public PlayableDirector director;

    void Start()
    {
        var timeline = (TimelineAsset)director.playableAsset;

        foreach (var track in timeline.GetOutputTracks())
        {
            Debug.Log("Track: " + track.name);

            foreach (var clip in track.GetClips())
            {
                Debug.Log($"Clip: {clip.displayName}");
                Debug.Log($"Start: {clip.start}");
                Debug.Log($"Duration: {clip.duration}");
                Debug.Log($"End: {clip.end}");
            }
        }
    }
}
