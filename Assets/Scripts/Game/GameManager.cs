using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MonobitEngine;
using UnityEngine.SceneManagement;
using UniRx.Async;

public class GameManager : MonobitEngine.MonoBehaviour{

    public bool testMode = false;
    public Player mainPlayer;
    public VRMLoader vrmLoader;

    void Start(){
        if(testMode) TestMode().Forget();
        CreateMainPlayer();
    }

    private async UniTask TestMode(){
        MonobitNetwork.playerName = "TestPlayer";
        MonobitNetwork.autoJoinLobby = true;
        MonobitNetwork.ConnectServer("v1.0");
        await UniTask.WaitUntil(() => MonobitNetwork.isConnect);
        MonobitNetwork.CreateRoom("TestRoom");
    }

    private void CreateMainPlayer(){
        var go = MonobitNetwork.Instantiate("Player", Vector3.zero, Quaternion.identity, 0);
        mainPlayer = go.GetComponent<Player>();
        mainPlayer.playerName = PlayerPrefs.GetString("PlayerName", "Player");
        mainPlayer.vrmTitle = PlayerPrefs.GetString("PlayerVRM", "");
        mainPlayer.gameObject.AddComponent<Controller>();
    }
    
}
