using System;
using UnityEngine;
using Firebase.Database;

public class SceneHandler : MonoBehaviour
{

    [SerializeField] private StorageData data;

    private TextHandler textHandler;

    DatabaseReference reference;

    private void Awake()
    {
        textHandler = GetComponent<TextHandler>();
    }

    void Start()
    {

        reference = FirebaseDatabase.DefaultInstance.RootReference;

        SaveData();

        if (textHandler != null)
        {
            textHandler.SetTexts();
        }
    }

    public void SaveData(){
        
        string dataJson = JsonUtility.ToJson(data);
        reference.Child("User").Child(DateTime.Now.ToString("yyyyMMddHHmmssffff") + "_" + data.name).SetRawJsonValueAsync(dataJson).ContinueWith(task =>{

            if(task.IsCompleted){
                Debug.Log("added to fb");
            } else {
                Debug.Log("error at fb");
            }
        });

    }

}
