using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

public class CutsceneSkipper : MonoBehaviour
{
    public PlayableDirector director;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SkipToNext();
        }
    }

    public void SkipToNext()
    {
        var timeline = (TimelineAsset)director.playableAsset;
        double currentTime = director.time;

        TimelineClip nextClip = null;

        foreach (var track in timeline.GetOutputTracks())
        {
            foreach (var clip in track.GetClips())
            {
                if (clip.start > currentTime)
                {
                    if (nextClip == null || clip.start < nextClip.start)
                    {
                        nextClip = clip;
                    }
                }
            }
        }

        if (nextClip != null)
        {
            director.time = nextClip.start;
            director.Evaluate(); // instantly jump
        }
    }
}