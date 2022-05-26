using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SabotageMeltReactorMission : MonoBehaviour
{
    [SerializeField]
    private Animator moveWhiteLine;
    [SerializeField]
    private bool isTouch;
    [SerializeField]
    private bool isClear;

    void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            RaycastHit2D hit = Physics2D.Raycast(Input.mousePosition, Vector2.right, 1f);
            if (hit.collider != null)
            {
                TouchOnReactorScreen();
            }
        }
        else if (Input.GetMouseButtonUp(0))
        {
            TouchOffReactorScreen();
        }
    }

    public void TouchOnReactorScreen()
    {
        isTouch = true;
        moveWhiteLine.enabled = true;
        GameSystem.Instance.sabotageCheckCount++;
        var myCharacter = AmongUsRoomPlayer.MyRoomPlayer.myCharacter as InGameCharacterMover;
        myCharacter.CmdSabotageCheckFunc(GameSystem.Instance.sabotageCheckCount);
        isClear = true;
    }

    public void TouchOffReactorScreen()
    {
        isTouch = false;
        moveWhiteLine.enabled = false;
        GameSystem.Instance.sabotageCheckCount--;
    }


}
