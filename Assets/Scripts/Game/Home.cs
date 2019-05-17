using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Home : MonoBehaviour{
    
    public GameObject modal;
    public GameObject vrmNotFoundDialog;

    void Start(){
        if (VRMLoader.vrmNotFound){
            modal.SetActive(true);
            vrmNotFoundDialog.SetActive(true);
        }
    }

}
