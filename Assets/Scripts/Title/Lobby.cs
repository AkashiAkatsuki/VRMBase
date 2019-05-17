using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using MonobitEngine;
using UnityEngine.SceneManagement;
using UniRx.Async;

public class Lobby : MonobitEngine.MonoBehaviour{
    
    public GameObject roomListEntryPrefab;
    public Transform roomListView;
    public string gameVersion;
    public string newRoomName { get; set; }
    public InputField roomNameInput;
    public GameObject modal;

    void Start(){
        ConnectServer().Forget();
        ReloadRoomList();
    }

    public void CreateRoom(){
        PrivateCreateRoom().Forget();
    }

    public void ReloadRoomList(){
        foreach(GameObject entry in roomListView) GameObject.Destroy(entry);
        var roomList = MonobitNetwork.GetRoomData();
        foreach(var roomData in roomList){
            var go = Instantiate(roomListEntryPrefab, roomListView);
            go.GetComponent<RoomListEntry>().Initialize(roomData);
        }
    }

    private async UniTask ConnectServer(){
        Debug.Log("Connecting to server...");
        modal.SetActive(true);
        
        var playerName = PlayerPrefs.GetString("PlayerName", "Player");
        MonobitNetwork.playerName = playerName;
        MonobitNetwork.autoJoinLobby = true;
        MonobitNetwork.ConnectServer(gameVersion);

        await UniTask.WaitUntil(() => MonobitNetwork.isConnect);
        modal.SetActive(false);
        Debug.Log("Success connecting to server!");
    }

    private async UniTask PrivateCreateRoom(){
        Debug.Log("Creating room...");
        if(newRoomName == "" || newRoomName == null) newRoomName = "New Room";
        MonobitNetwork.CreateRoom(newRoomName);
        modal.SetActive(true);
        await UniTask.WaitUntil(() => MonobitNetwork.inRoom);
        SceneManager.LoadScene("Game");
        Debug.Log("Success creating room!");
    }

}