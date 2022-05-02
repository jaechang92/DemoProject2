using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//[System.Serializable]
//struct MyDictionary
//{
//    public List<Transform> tr;
//    public List<int> idx;
//    public void Add(Transform _tr, int _idx)
//    {
//        tr.Add(_tr);
//        idx.Add(_idx);
//    }
//}

public class EnergyConversionMission : MonoBehaviour
{
    
    //[SerializeField]
    //private MyDictionary test;
    [SerializeField]
    private Dictionary<Transform,int> kvp = new Dictionary<Transform, int>();
    [SerializeField]
    private List<Transform> switchs;
    [SerializeField]
    private List<VibrationGageUI> gages;
    [SerializeField]
    private GameObject select;
    [SerializeField]
    private Transform topPosition;
    [SerializeField]
    private Transform bottomPosition;
    [SerializeField]
    private Vector3 switchPosition;

    [SerializeField]
    private float switchAmount;
    [SerializeField]
    private float switchAmountOther;
    void Start()
    {
        for (int i = 0; i < switchs.Count; i++)
        {
            kvp.Add(switchs[i], i);
        }
        foreach (var item in gages)
        {
            item.AmountCenterValue = 0.5f;
            item.GageRange = 0.01f;
            item.VibrationTIme = 0.0625f;
        }
    }
    [SerializeField]
    float deltaAmount;
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit2D hit = Physics2D.Raycast(Input.mousePosition,Vector2.right, 1);
            if (hit.collider != null)
            {
                select = hit.transform.gameObject;
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            select = null;
        }

        if (select != null)
        {
            int selectIdx;
            kvp.TryGetValue(select.transform, out selectIdx);
            float originAmount = gages[selectIdx].AmountCenterValue;
            switchPosition = new Vector3(select.transform.position.x, Input.mousePosition.y, select.transform.position.z);
            // 리미트 -80 ~ -200
            switchPosition.y = Mathf.Clamp(switchPosition.y, bottomPosition.position.y + (topPosition.position.y - bottomPosition.position.y)*0.1f, topPosition.position.y);
            select.transform.position = switchPosition;
            switchAmount = (switchPosition.y - bottomPosition.position.y) / (topPosition.position.y - bottomPosition.position.y);
            // 변화값 델타
            deltaAmount = switchAmount - originAmount;
            // 전체 파이는 0.5 * 8 = 4
            // 현재 스위치의 어마운트를 빼준다 4-switchAmount
            // 나머지를 균등분배해준다
            // 균등분배 할때는 가중치를 둔다 많은건 많이 적은건 적게
            // (4 - switchAmount) / 7 * 원본 * 2
            switchAmountOther = (4 - switchAmount) / 7;
            //---------------------------------------------------------------
            gages[selectIdx].AmountCenterValue = switchAmount;
            CalculAmountEnergy(selectIdx, deltaAmount);
            deltaAmount = 0;

            //for (int i = 0; i < gages.Count; i++)
            //{
            //    if (selectIdx == i)
            //    {
            //        gages[i].AmountCenterValue = switchAmount;
            //    }
            //    else
            //    {

            //        //gages[i].AmountCenterValue -= deltaAmount / 7 * gages[i].calibratorGauges.fillAmount * 2;
            //        gages[i].AmountCenterValue -= deltaAmount;
            //    }
            //}

            //foreach (var item in gages)
            //{
            //    if (item != select)
            //    {
            //        item.AmountCenterValue -= deltaAmount / 7 * item.calibratorGauges.fillAmount * 2;
            //        //item.GageRange = new Vector2(switchAmountOther - 0.01f, switchAmountOther + 0.01f);
            //    }
            //}
            //select.transform.position.Set(select.transform.position.x, Input.mousePosition.y, select.transform.position.z);
        }
    }

    //private int GetSelectIdx(Transform obj)
    //{
    //    int i = 0;
    //    foreach (var item in switchs)
    //    {
    //        if (obj == item)
    //        {
    //            return i;
    //        }
    //        i++;
    //    }
    //}


    private float returnEnergy = 0;
    private Queue<int> gagesIdx = new Queue<int>();
    private void CalculAmountEnergy(int selectIdx, float deltaEnergy)
    {
        returnEnergy = 0;

        float temp = 0;
        
        // 뺴줘야되는 경우와 더해줘야 되는 2가지 케이스

        if (deltaEnergy > 0)
        {
            // 에너지가 양수여서 빼줘야 하는 경우
            for (int i = 0; i < gages.Count; i++)
            {
                if (selectIdx == i)
                {
                    continue;
                }
                if (gages[i].AmountCenterValue <= 0.1f)
                {
                    continue;
                }
                gagesIdx.Enqueue(i);
                temp += gages[i].AmountCenterValue;
            }

            while (gagesIdx.Count > 0)
            {
                int gageIndex = gagesIdx.Dequeue();
                gages[gageIndex].AmountCenterValue -= deltaEnergy * gages[gageIndex].AmountCenterValue / temp;
                if (gages[gageIndex].AmountCenterValue < 0.1f)
                {
                    returnEnergy += 0.1f - gages[gageIndex].AmountCenterValue;
                    gages[gageIndex].AmountCenterValue = 0.1f;
                }
            }
            if (returnEnergy > 0)
            {
                CalculAmountEnergy(selectIdx, returnEnergy);
            }
        }
        else if(deltaEnergy < 0)
        {
            for (int i = 0; i < gages.Count; i++)
            {
                if (selectIdx == i)
                {
                    continue;
                }
                if (gages[i].AmountCenterValue >= 1.0f)
                {
                    continue;
                }
                gagesIdx.Enqueue(i);
                temp += gages[i].AmountCenterValue;
            }

            while (gagesIdx.Count > 0)
            {
                int gageIndex = gagesIdx.Dequeue();
                gages[gageIndex].AmountCenterValue -= deltaEnergy * gages[gageIndex].AmountCenterValue / temp;
                if (gages[gageIndex].AmountCenterValue > 1.0f)
                {
                    returnEnergy += 1.0f - gages[gageIndex].AmountCenterValue;
                    gages[gageIndex].AmountCenterValue = 1.0f;
                }
            }

            if (returnEnergy < 0)
            {
                CalculAmountEnergy(selectIdx, returnEnergy);
            }
        }


        
    }


}
