using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerSetting : MonoBehaviour{

    public VRMLoader vrmLoader;
    public Transform vrmListViewer;
    public Transform vrmListEntryPrefab;
    public ToggleGroup toggleGroup;
    public InputField playerNameInput;

    void Start(){
        InitVRMList();
        playerNameInput.text = PlayerPrefs.GetString("PlayerName", "Player");
    }

    public void SetPlayerName(string playerName){
        PlayerPrefs.SetString("PlayerName", playerName);
    }

    private void InitVRMList(){
        foreach(var kv in vrmLoader.GetThumbnailList()){
            var title = kv.Key;
            var thumbnail = kv.Value;
            var go = Instantiate(vrmListEntryPrefab, vrmListViewer);
            go.GetComponent<VRMListEntry>().Initialize(title, thumbnail);
            var toggle = go.GetComponent<Toggle>();
            toggle.group = toggleGroup;
            if(title == PlayerPrefs.GetString("PlayerVRM", title)) toggle.isOn = true;
        }
    }

}