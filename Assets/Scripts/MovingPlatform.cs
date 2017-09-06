using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour {

    public Transform movingPlatform;
    public Transform[] positions;

    public bool shouldRotate;

    int nextPositionIndex;
    Transform nextPosition;

    bool moving;
    bool rotating;

    public bool loops;
    private bool backwards;

    public bool smooth;

    public float platformSpeed;
    public float resetTime; // seconds to rotate or to wait when changing directions.

    private float timeSpent; // time remaining to wait.

    private Quaternion initialRotation;

    void OnDrawGizmos()
    {
        Gizmos.color = new Color(254, 234, 0, 0.75f); // yellow

        Transform previousPosition = loops ? positions[positions.Length - 1] : null;
        foreach(Transform position in positions)
        {
            
            Gizmos.DrawSphere(position.position, 0.5f);
            if(previousPosition != null)
            {
                Gizmos.DrawLine(previousPosition.position, position.position);                
            }

            previousPosition = position;
            
        }
    }


    // Use this for initialization
    void Start () {
        ChangeTarget();
	}
	
	void FixedUpdate () {
        if(moving)
        {
            if(!rotating)
            {
                if (smooth)
                {
                    movingPlatform.position = Vector3.Lerp(movingPlatform.position, nextPosition.position, platformSpeed * Time.fixedDeltaTime);
                }
                else
                {
                    movingPlatform.position = Vector3.MoveTowards(movingPlatform.position, nextPosition.position, platformSpeed * Time.fixedDeltaTime);
                }
                // if next movement will reach the destination, then change destination
                if (Vector3.Distance(movingPlatform.position, nextPosition.position) < platformSpeed * Time.fixedDeltaTime)
                {
                    if(shouldRotate)
                    {
                        rotating = true;
                        timeSpent = 0;
                        initialRotation = movingPlatform.rotation;
                    } else
                    {
                        moving = false;
                        Invoke("ChangeTarget", resetTime);
                    }
                    
                }
            } else
            {

                timeSpent += Time.fixedDeltaTime;

                float interpolant = Mathf.InverseLerp(0,resetTime,timeSpent);

                movingPlatform.rotation = Quaternion.Lerp(initialRotation, nextPosition.rotation, interpolant);
                if(timeSpent >= resetTime)
                {
                    rotating = false;
                    ChangeTarget();
                }
            }
        }
        

	}

    void ChangeTarget()
    {
        if(loops)
        {
            nextPositionIndex = (nextPositionIndex + 1) % positions.Length;
        } else
        {
            if(nextPositionIndex == positions.Length - 1)
            {
                backwards = true;
            }

            if(nextPositionIndex == 0)
            {
                backwards = false;
            }

            if(backwards)
            {
                nextPositionIndex -= 1;
            } else
            {
                nextPositionIndex += 1;
            }
        }
        
        nextPosition = positions[nextPositionIndex];
        moving = true;
    }

    
}
