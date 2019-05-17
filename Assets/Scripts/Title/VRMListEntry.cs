using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VRMListEntry : MonoBehaviour{

    public Text titleText;
    public Image thumbnailImage;
    private string title;

    public void Initialize(string title, Texture2D thumbnail){
        this.title = title;
        titleText.text = title;
        thumbnailImage.sprite = Sprite.Create(thumbnail, new Rect(0, 0, thumbnail.width, thumbnail.height), Vector2.zero);
    }

    public void OnButton(){
        PlayerPrefs.SetString("PlayerVRM", title);
    }

}
