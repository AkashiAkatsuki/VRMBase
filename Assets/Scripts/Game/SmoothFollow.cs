using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmoothFollow : MonoBehaviour{

    public GameManager gameManager;
    public Vector3 offset;
    public float posSmooth = 1f;
    public float rotSmooth = 30f;

    void Update(){
        var player = gameManager.mainPlayer;
        if(!player || !player.initialized) return;
        var sight = player.sight;
        var sightOffset = player.sightOffset;
        var target = sight.position + sightOffset + sight.rotation * offset;
        transform.position = Vector3.Lerp(transform.position, target, Time.deltaTime * posSmooth);

        var targetRotation = Quaternion.LookRotation(sight.position + sightOffset - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotSmooth * Time.deltaTime);
    }
    
}
