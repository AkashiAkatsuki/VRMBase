using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx.Async;

public class PlayerName : MonoBehaviour{

    void Start(){
        SetText().Forget();
        transform.localScale = new Vector3 (-1, 1, 1);
    }

    void Update(){
        transform.LookAt(Camera.main.transform);
    }

    private async UniTask SetText(){
        var text = GetComponent<TextMesh>();
        var player = transform.parent.GetComponent<Player>();
        await UniTask.WaitUntil(() => player.initialized);
        text.text = player.playerName;
    }
    
}
