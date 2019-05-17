using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRM;
using System;
using System.IO;
using System.Linq;
using UniRx.Async;

public class VRMLoader : MonoBehaviour{

    public static string directory;
    public static bool vrmNotFound = false;
    private static Dictionary<string, VRMImporterContext> contextList;
    private static Dictionary<string, GameObject> vrmList = new Dictionary<string, GameObject>();

    [RuntimeInitializeOnLoadMethod()]
    static void Initialize(){
        if(contextList == null) LoadContextList();
        if(contextList.Count == 0) vrmNotFound = true;
    }

    public async UniTask<GameObject> GetVRMModel(string title){
        if (!contextList.ContainsKey(title)){
            Debug.Log("Not found VRM:" + title + ". Default VRM will be used.");
            title = contextList.Keys.ToArray()[0];
        }
        GameObject vrm;
        if (vrmList.TryGetValue(title, out vrm)) {
            vrm = Instantiate(vrm);
            vrm.SetActive(true);
            return vrm;
        }
        await LoadVRMModel(title);
        vrm = Instantiate(vrmList[title]);
        vrm.SetActive(true);
        return vrm;
    }

    private async UniTask LoadVRMModel(string title){
        Debug.Log("Loading VRM:" + title + " ...");
        var context = contextList[title];
        await context.LoadAsyncTask();
        context.ShowMeshes();
        context.Root.SetActive(false);
        vrmList[title] = context.Root;
        Debug.Log("Success loading VRM:" + title + "!");
    }

    public Dictionary<string, Texture2D> GetThumbnailList(){
        return contextList.ToDictionary(kv => kv.Key, kv => kv.Value.ReadMeta(true).Thumbnail);
    }

    private static void LoadContextList(){
        var defaultPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal) + Path.DirectorySeparatorChar + "VRM";
        directory = PlayerPrefs.GetString("VRMDirectory", defaultPath);
        if(!Directory.Exists(directory)){
            Directory.CreateDirectory(directory);
        }
        var fileList = Directory.GetFiles(directory);
        contextList = fileList.Select(path => LoadFile(path))
            .ToArray()
            .ToDictionary(context => context.ReadMeta().Title);
    }

    private static VRMImporterContext LoadFile(string filePath){
        var bytes = File.ReadAllBytes(filePath);
        var context = new VRMImporterContext();
        context.ParseGlb(bytes);
        var meta = context.ReadMeta(false);
        return context;
    }

}