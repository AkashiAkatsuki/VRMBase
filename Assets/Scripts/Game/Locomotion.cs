using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Locomotion : MonoBehaviour{

    private Player player;
    private Vector3 lastPosition;
    private Quaternion lastRotation;

    void Start(){
        player = GetComponent<Player>();
    }

    void Update(){
        if(player.anim == null) return;
        var speed = -transform.InverseTransformPoint(lastPosition).z / Time.deltaTime;
        player.anim.SetFloat("Speed", speed);
        lastPosition = transform.position;
    }
    
}
