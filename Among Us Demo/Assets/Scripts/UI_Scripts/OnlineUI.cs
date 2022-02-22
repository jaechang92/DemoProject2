using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mirror;


public class OnlineUI : MonoBehaviour
{
    [SerializeField]
    private InputField nickNameInputField;
    [SerializeField]
    private GameObject createRoomUI;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnClickCreateRoomButton()
    {
        if (nickNameInputField.text != "")
        {
            PlayerSettins.nickName = nickNameInputField.text;
            createRoomUI.SetActive(true);
            gameObject.SetActive(false);
        }
        else
        {
            nickNameInputField.GetComponent<Animator>().SetTrigger("On");
        }
    }

    public void OnClickEnterGameRoomButton()
    {
        if (nickNameInputField.text != "")
        {
            PlayerSettins.nickName = nickNameInputField.text;
            var manager = AmongUsRoomManager.singleton;
            manager.StartClient();
        }
        else
        {
            nickNameInputField.GetComponent<Animator>().SetTrigger("On");
        }

        
    }
    
}
