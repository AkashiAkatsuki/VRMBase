using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using MonobitEngine;
using UnityEngine.SceneManagement;
using UniRx.Async;

public class RoomListEntry : MonobitEngine.MonoBehaviour{

    public Text roomNameText;
    public Button enterButton;

    public void Initialize(RoomData roomData){
        roomNameText.text = roomData.name + "(" + roomData.playerCount + "/" + roomData.maxPlayers + ")";
        enterButton.onClick.AddListener(() => OnButton(roomData.name).Forget());
    }

    public async UniTask OnButton(string roomName){
        Debug.Log("Joining Room...");
        var lobby = FindObjectOfType<Lobby>();
        lobby.modal.SetActive(true);

        MonobitNetwork.JoinRoom(roomName);
        await UniTask.WaitUntil(() => MonobitNetwork.inRoom);
        Debug.Log("Success Joining Room!");
        SceneManager.LoadScene("Game");
    }
    
}
