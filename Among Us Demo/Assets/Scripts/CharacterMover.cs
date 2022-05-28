using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mirror;

public class CharacterMover : NetworkBehaviour
{

    protected Animator animator;
    [SerializeField]
    private bool isMoveable;

    public bool IsMoveable
    {
        get 
        {
            return IsMoveable;
        }
        set
        {
            if (!value)
            {
                animator.SetBool("isMove", false);
            }
            isMoveable = value;
        }
    }
    [SyncVar(hook = nameof(SetPlayerMoveSpeed_Hook))]
    public float moveSpeed;
    public void SetPlayerMoveSpeed_Hook(float _, float value)
    {
        moveSpeed = value;
    }

    [SerializeField]
    private float characterSize = 0.7f;
    [SerializeField]
    private float camSize = 2.5f;

    protected SpriteRenderer spriteRenderer;

    [SyncVar(hook = nameof(SetPlayerColor_Hook))]
    public EPlayerColor playerColor;

    public void SetPlayerColor_Hook(EPlayerColor oldColor, EPlayerColor newColor)
    {
        if (spriteRenderer == null)
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
        }
        spriteRenderer.material.SetColor("_PlayerColor", PlayerColor.GetColor(newColor));
    }

    [SyncVar(hook = nameof(SetNickname_Hook))]
    public string nickname;
    [SerializeField]
    protected Text nicknameText;

    public void SetNickname_Hook(string _, string value)
    {
        nicknameText.text = value;
    }


    private void init()
    {
        animator = GetComponent<Animator>();
        if (spriteRenderer == null)
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
        }
        var inst = Instantiate(spriteRenderer.material);
        spriteRenderer.material = inst;
        spriteRenderer.material.SetColor("_PlayerColor", PlayerColor.GetColor(playerColor));
        var manager = NetworkManager.singleton as AmongUsRoomManager;
        
        if (NetworkManager.networkSceneName != manager.GameplayScene)
        {
            manager.gameRuleData = FindObjectOfType<GameRuleStore>().GetGameRuleData();
        }
        moveSpeed = manager.gameRuleData.moveSpeed;
    }

    public virtual void Start()
    {
        init();

        if (hasAuthority)
        {
            Camera cam = Camera.main;
            cam.transform.SetParent(transform);
            cam.transform.localPosition = new Vector3(0.0f, 0.0f, -10.0f);
            cam.orthographicSize = camSize;
        }

    }

    public virtual void Update()
    {
        Move();
    }
    public void Move()
    {
        bool isMove = false;
        if (hasAuthority && isMoveable)
        {
            if (PlayerSettins.controlType == EControlType.Mouse)
            {
                // 마우스 이동
                if (Input.GetMouseButton(0))
                {
                    Vector3 dir = (Input.mousePosition - new Vector3(Screen.width * 0.5f, Screen.height * 0.5f, 0f)).normalized;
                    transform.position += dir * moveSpeed * Time.deltaTime;

                    if (dir.x < 0.0f)
                    {
                        transform.localScale = new Vector3(-characterSize, characterSize, 1);
                        
                    }
                    else if (dir.x > 0.0f)
                    {
                        transform.localScale = new Vector3(characterSize, characterSize, 1);
                    }

                    isMove = dir.magnitude != 0.0f;
                }
            }
            else
            {
                // 키보드 마우스 이동
                Vector3 dir = Vector3.ClampMagnitude(new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0.0f), 1.0f);
                    if (dir.x < 0.0f)
                    {
                        transform.localScale = new Vector3(-characterSize, characterSize, 1);
                        
                    }
                    else if (dir.x > 0.0f)
                    {
                        transform.localScale = new Vector3(characterSize, characterSize, 1);
                    }

                transform.position += dir * moveSpeed * Time.deltaTime;
                isMove = dir.magnitude != 0.0f;
            }
            animator.SetBool("isMove", isMove);
        }

        if (transform.localScale.x < 0)
        {
            nicknameText.transform.localScale = new Vector3(-1f, 1f, 1f);
        }
        else if (transform.localScale.x > 0)
        {
            nicknameText.transform.localScale = new Vector3(1f, 1f, 1f);
        }

    }

    
}
