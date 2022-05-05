using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mirror;


public class CreateRoomUI : MonoBehaviour
{
    [SerializeField]
    private List<Image> crewImages;
    [SerializeField]
    private List<Button> imposterCountButtons;
    [SerializeField]
    private List<Button> maxPlayerCountButtons;

    private CreateGameRoomData roomData;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < crewImages.Count; i++)
        {
            Material materialInstance = Instantiate(crewImages[i].material);
            crewImages[i].material = materialInstance;
        }
        roomData = new CreateGameRoomData() { imposterCount = 1, maxPlayerCount = 10 };
        UpdateCrewImage();
    }
    
    public void UpdateMaxPlayerCount(int count)
    {
        roomData.maxPlayerCount = count;

        for (int i = 0; i < maxPlayerCountButtons.Count; i++)
        {
            if (i == count - 4)
            {
                maxPlayerCountButtons[i].image.color = new Color(1f, 1f, 1f, 1f);
            }
            else
            {
                maxPlayerCountButtons[i].image.color = new Color(1f, 1f, 1f, 0f);
            }
        }

        
        UpdateCrewImage();
    }

    public void UpdateImposterCount(int count)
    {
        roomData.imposterCount = count;

        for (int i = 0; i < imposterCountButtons.Count; i++)
        {
            if (i == count-1)
            {
                imposterCountButtons[i].image.color = new Color(1f, 1f, 1f, 1f);
            }
            else
            {
                imposterCountButtons[i].image.color = new Color(1f, 1f, 1f, 0f);
            }
        }

        int limitMaxPlayerCount = count == 1 ? 4 : count == 2 ? 7 : 9;
        if (roomData.maxPlayerCount < limitMaxPlayerCount)
        {
            UpdateMaxPlayerCount(limitMaxPlayerCount);
        }
        else
        {
            UpdateMaxPlayerCount(roomData.maxPlayerCount);
        }

        for (int i = 0; i < maxPlayerCountButtons.Count; i++)
        {
            var text = maxPlayerCountButtons[i].GetComponentInChildren<Text>();
            if (i < limitMaxPlayerCount - 4)
            {
                maxPlayerCountButtons[i].interactable = false;
                text.color = Color.gray;
            }
            else
            {
                maxPlayerCountButtons[i].interactable = true;
                text.color = Color.white;
            }
        }
    }

    private void UpdateCrewImage()
    {
        for (int i = 0; i < crewImages.Count; i++)
        {
            crewImages[i].material.SetColor("_PlayerColor", Color.white);
        }
        int imposterCount = roomData.imposterCount;
        int idx = 0;
        while (imposterCount != 0)
        {
            if (idx >= roomData.maxPlayerCount)
            {
                idx = 0;
            }

            if (crewImages[idx].material.GetColor("_PlayerColor") != Color.red && Random.Range(0,5) == 0)
            {
                crewImages[idx].material.SetColor("_PlayerColor", Color.red);
                imposterCount--;
            }
            idx++;
        }

        for (int i = 0; i < crewImages.Count; i++)
        {
            if (i < roomData.maxPlayerCount)
            {
                crewImages[i].gameObject.SetActive(true);
            }
            else
            {
                crewImages[i].gameObject.SetActive(false);
            }

        }

    }

    public void CreateRoom()
    {
        var manager = NetworkManager.singleton as AmongUsRoomManager;
        manager.minPlayerCount = roomData.imposterCount == 1 ? 4 : roomData.imposterCount == 2 ? 7 : 9;
        manager.imposterCount = roomData.imposterCount;
        manager.maxConnections = roomData.maxPlayerCount;


        manager.StartHost();
        manager.nowScene = manager.RoomScene;
    }
}

public class CreateGameRoomData
{
    public int imposterCount;
    public int maxPlayerCount;
}
