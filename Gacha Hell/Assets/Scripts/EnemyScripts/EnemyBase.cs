using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Splines;

public class EnemyBase : MonoBehaviour
{
    private GameObject track;
    private SplineContainer splineContainer;
    private SplineAnimate splineAnimate;

    private PlayerVariables playerVariables;

    protected virtual float speed { get { return 0.89f; } }
    protected virtual float maxHealth { get { return 100f; } }
    protected virtual int damage { get { return 10; } }
    protected virtual int money { get { return 10; } }
    public float health;
    

    // Assigning the track Is in "awake" since it will allow it to automatically 
    // play the animation through "Play on awake" checkbox in spline animate script
    void Awake()
    {
        AssignTrack();
        FollowTrack();
        splineAnimate.MaxSpeed = speed; // Set the speed of the animation for enemies
        health = maxHealth;
        ReachedEndOfTrack();
    }

    void Start()
    {
        GetPlayerVariables();
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
            Debug.LogError("Track not found, please add a track to the scene with the name 'Track'");
        }
    }

    private void FollowTrack()
    {
        // Instantly makes enemies follow the track when spawned in scene
        splineAnimate.Play();
    }

    private void GetPlayerVariables()
    {
        playerVariables = GameObject.Find("Castle").GetComponent<PlayerVariables>();
        if (playerVariables == null)
        {
            Debug.LogError("No variables found on the 'Castle' GameObject! Please make a 'Castle' GameObject with the PlayerVariables script attached to it.");
        }
    }

    private void ReachedEndOfTrack()
    {
        splineAnimate.Completed += OnReachedEndOfTrack;
    }
    
    private void OnReachedEndOfTrack()
    {
        // player takes damage when enemy reaches the end of the track
        playerVariables.playerHealth -= damage;
        Destroy(gameObject);
    }

    public void OnTriggerEnter(Collider other)
    {
        health -= other.GetComponent<ProjectileBase>().damage;
        if (health <= 0)
        {
            Destroy(gameObject);
            playerVariables.playerMoney += money;
        }
        Destroy(other.gameObject);
    }

    
}
