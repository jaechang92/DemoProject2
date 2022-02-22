using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class IntroCharacter : MonoBehaviour
{
    [SerializeField]
    private Image character;
    [SerializeField]
    private Text nickname;


    public void SetIntroCharacter(string nick, EPlayerColor playerColor)
    {
        if (character == null)
        {
            character = GetComponent<Image>();
        }
        var matInst = Instantiate(character.material);
        character.material = matInst;

        nickname.text = nick;
        character.material.SetColor("_PlayerColor", PlayerColor.GetColor(playerColor));
    }


}
