using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VentSystem : MonoBehaviour
{
    public static VentSystem Instance;

    [SerializeField]
    private List<Transform> ventCafeteriaAdminHallway;
    [SerializeField]
    private List<Transform> ventMedbayElectricalSecurity;
    [SerializeField]
    private List<Transform> ventReactorUpperEngine;
    [SerializeField]
    private List<Transform> ventReactorLowerEngine;
    [SerializeField]
    private List<Transform> ventNavigationWeapons;
    [SerializeField]            
    private List<Transform> ventNavigationShields;

    [SerializeField]
    private List<Button> arrows;
    [SerializeField]
    private bool used = false;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }


    }

    void Start()
    {
        
    }

    void Update()
    {
        
    }


    public void SetVent(int idx, GameObject obj)
    {
        
        if (!used)
        {
            // 사용중이 아닐때는 사용하는 함수 동작
            switch (idx)
            {
                case 0:
                    FindAnsDirection(ventCafeteriaAdminHallway, obj);
                    break;
                case 1:
                    FindAnsDirection(ventMedbayElectricalSecurity, obj);
                    break;
                case 2:
                    FindAnsDirection(ventReactorUpperEngine, obj);
                    break;
                case 3:
                    FindAnsDirection(ventReactorLowerEngine, obj);
                    break;
                case 4:
                    FindAnsDirection(ventNavigationWeapons, obj);
                    break;
                case 5:
                    FindAnsDirection(ventNavigationShields, obj);
                    break;
                default:
                    break;
            }

            ActionVent(true);
        }
        else
        {
            // 사용중일때는 사용중지 함수 동작
            ActionVent(false);
        }
        used = !used;
    }


    private void ActionVent(bool isUse)
    {
        // 임포스터 캐릭터 밴트 액션
        // 밴트 액션

        if (isUse == true)
        {
            AmongUsRoomPlayer.MyRoomPlayer.myCharacter.IsMoveable = false;
            AmongUsRoomPlayer.MyRoomPlayer.myCharacter.GetComponent<InGameCharacterMover>().SetVisibility(false);
        }

        if (isUse == false)
        {
            AmongUsRoomPlayer.MyRoomPlayer.myCharacter.IsMoveable = true;
            AmongUsRoomPlayer.MyRoomPlayer.myCharacter.GetComponent<InGameCharacterMover>().SetVisibility(true);
            SetActiveFalseArrows();
        }
    }


    private void FindAnsDirection(List<Transform> m_list,GameObject obj)
    {
        for (int i = 0; i < m_list.Count; i++)
        {
            if (m_list[i].gameObject != obj)
            {
                for (int j = 0; j < arrows.Count; j++)
                {
                    if (arrows[j].gameObject.activeSelf == false)
                    {
                        arrows[j].gameObject.SetActive(true);
                        arrows[j].transform.right = AToBDirection(obj.transform.position, m_list[i].position, arrows[j].gameObject);
                        int temp = i;
                        arrows[j].onClick.AddListener(() => 
                        { 
                            MoveToTarget(m_list[temp].transform.position);
                            SetActiveFalseArrows();
                            FindAnsDirection(m_list, m_list[temp].gameObject);
                        });
                        //arrows[j].onClick.AddListener(() =>
                        //{
                        //    AmongUsRoomPlayer.MyRoomPlayer.myCharacter.transform.position = m_list[i].position;
                        //});
                        Vector3 screenPos = Camera.main.WorldToScreenPoint(obj.transform.position + arrows[j].transform.right.normalized * 0.5f);
                        arrows[j].transform.position = screenPos;
                        break;
                    }
                }
            }
        }
    }

    private void MoveToTarget(Vector3 target)
    {
        AmongUsRoomPlayer.MyRoomPlayer.myCharacter.transform.position = target;
    }

    private Vector3 AToBDirection(Vector3 A_Point, Vector3 B_Point, GameObject arrow)
    {
        return new Vector3(B_Point.x - A_Point.x, B_Point.y - A_Point.y);
    }

    private void SetActiveFalseArrows()
    {
        for (int i = 0; i < arrows.Count; i++)
        {
            arrows[i].onClick.RemoveAllListeners();
            arrows[i].gameObject.SetActive(false);
        }
    }
}
