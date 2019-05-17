using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour{

    public float dashSpeed = 5;
    public float rotSpeed = 60;

    void Update(){
        var dirX = Input.GetAxis("Horizontal");
        var dirY = Input.GetAxis("Vertical");
        if(dirY < 0) dirY /= 2;
        transform.position += transform.forward * dirY * dashSpeed * Time.deltaTime;
        transform.Rotate(0, dirX * rotSpeed * Time.deltaTime, 0);
    }
    
}