using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRM;
using MonobitEngine;
using UniRx.Async;

public class Player : MonobitEngine.MonoBehaviour{

    public string playerName;
    public string vrmTitle;
    [System.NonSerialized] public bool initialized = false;
    [System.NonSerialized] public Transform sight;
    [System.NonSerialized] public Vector3 sightOffset;
    [System.NonSerialized] public Animator anim;
    public RuntimeAnimatorController defaultAnimator;

    void Start(){
        Initialize().Forget();
    }

    public void OnMonobitSerializeView(MonobitStream stream, MonobitMessageInfo info){
        if(stream.isWriting){
            stream.Enqueue(playerName);
            stream.Enqueue(vrmTitle);
        } else {
            playerName = (string)stream.Dequeue();
            vrmTitle = (string)stream.Dequeue();
        }
    }
    
    private async UniTask Initialize(){
        Debug.Log("Waiting sync VRMTitle...");
        await UniTask.WaitWhile(() => vrmTitle == "" || vrmTitle == null);
        Debug.Log("VRMTitle synced");

        Debug.Log("Initializing player:" + playerName + " ...");
        var loader = FindObjectOfType<VRMLoader>();
        var vrm = await loader.GetVRMModel(vrmTitle);
        vrm.transform.parent = transform;
        vrm.transform.position = transform.position;

        var vfp = vrm.GetComponent<VRMFirstPerson>();
        sight = vfp.FirstPersonBone;
        sightOffset = vfp.FirstPersonOffset;

        anim = vrm.GetComponent<Animator>();
        anim.runtimeAnimatorController = defaultAnimator;

        initialized = true;
        Debug.Log("Success initializing player:" + playerName + "!");
    }

}
