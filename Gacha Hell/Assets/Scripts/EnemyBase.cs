using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Splines;

public class EnemyBase : MonoBehaviour
{
    private GameObject track;
    private SplineContainer splineContainer;
    private SplineAnimate splineAnimate;

    protected virtual float speed { get { return 0.89f; } }
    protected virtual float health { get { return 100f; } }

    // Assigning the track Is in "awake" since it will allow it to automatically 
    // play the animation through "Play on awake" checkbox in spline animate script
    void Awake()
    {
        AssignTrack();
        FollowTrack();
        splineAnimate.MaxSpeed = speed; // Set the speed of the animation
    }

    private void AssignTrack()
    {
        track = GameObject.Find("Track"); // Find the track object in the scene
        if (track != null)
        {
            splineContainer = track.GetComponent<SplineContainer>(); // Get spline container component from track object
            if (splineContainer != null)
            {
                splineAnimate = GetComponent<SplineAnimate>(); // get spline animate script

                if (splineAnimate != null && splineContainer != null)
                {
                    splineAnimate.Container = splineContainer; // Set the track to the spline animate script
                }
            }
            else
            {
                Debug.LogError("SplineContainer component not found on the 'Track' GameObject!");
            }
        }
        else
        {
            Debug.Log("Track not found, please add a track to the scene with the name 'Track'");
        }
    }

    private void FollowTrack()
    {
        // Follow the track
        splineAnimate.Play();
    }
}
