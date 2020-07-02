using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraLooseFollow : MonoBehaviour
{
    public Transform follow;
    [SerializeField]
    private float followDeadZone = 0.25f;

    [SerializeField]
    private float followMaxDistance = 2f;

    [SerializeField]
    private float followMaxSpeed = 2f;

    [SerializeField]
    private float followMinSpeed = 0.5f;

    public float DistanceToPlayer, DistanceToPlayerNormalized;
    
    private float distanceToPlayer
    {
        get
        {
            return Vector3.Distance(transform.position, follow.position);
        }
    }

    private float distanceToPlayerNormalized
    {
        get
        {
            return Mathf.Clamp01((distanceToPlayer - followDeadZone) / (followMaxDistance - followDeadZone));
        }
    }

    private Vector3 directionToPlayer
    {
        get
        {
            return Vector3.Normalize(follow.position- transform.position);
        }
    }

    private float currentSpeed
    {
        get
        {
            return Mathf.Lerp(followMinSpeed, followMaxSpeed, distanceToPlayerNormalized);
        }
    }

    void Update()
    {
        MoveTowardsPlayer();
        DistanceToPlayer = distanceToPlayer;
        DistanceToPlayerNormalized = distanceToPlayerNormalized;
    }

    private void MoveTowardsPlayer()
    {
        transform.position = Vector3.Lerp(
            transform.position,
            transform.position + directionToPlayer * distanceToPlayerNormalized,
            Time.deltaTime * currentSpeed);
    }

}
