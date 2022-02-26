using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LeftWire : MonoBehaviour
{

    [SerializeField]
    private RectTransform mWireBody;
    [SerializeField]
    public LeftWire mSelectedWire;
    [SerializeField]
    public bool isConnect = false;
    [SerializeField]
    public GameObject target;

    [SerializeField]
    private float offset = 15f;

    [SerializeField]
    public List<Image> changeColorList;

    private Canvas mGameCanvas;

    private void InitSetting()
    {
        mWireBody.localRotation = Quaternion.Euler(Vector3.zero);
        mWireBody.sizeDelta = new Vector2(0f, mWireBody.sizeDelta.y);
        mSelectedWire = null;
    }
    // Start is called before the first frame update
    void Start()
    {
        mGameCanvas = FindObjectOfType<Canvas>();
        InitSetting();
    }

    void Update()
    {
        
        if (Input.GetMouseButtonUp(0))
        {
            if (mSelectedWire != null && !isConnect)
            {
                mWireBody.localRotation = Quaternion.Euler(Vector3.zero);
                mWireBody.sizeDelta = new Vector2(0f, mWireBody.sizeDelta.y);
                mSelectedWire = null;
            }
        }


        if (mSelectedWire !=null && !isConnect)
        {
            float angle = Vector2.SignedAngle(transform.position + Vector3.right - transform.position, Input.mousePosition - transform.position);
            float distance = Vector2.Distance(mWireBody.transform.position, Input.mousePosition) - offset;
            mWireBody.localRotation = Quaternion.Euler(new Vector3(0f, 0f, angle));
            mWireBody.sizeDelta = new Vector2(distance * (1 / mGameCanvas.transform.localScale.x), mWireBody.sizeDelta.y);
        }

        if (target != null && isConnect)
        {
            float angle = Vector2.SignedAngle(transform.position + Vector3.right - transform.position, target.transform.position - transform.position);
            float distance = Vector2.Distance(mWireBody.transform.position, target.transform.position) - offset;
            mWireBody.localRotation = Quaternion.Euler(new Vector3(0f, 0f, angle));
            mWireBody.sizeDelta = new Vector2(distance * (1 / mGameCanvas.transform.localScale.x), mWireBody.sizeDelta.y);
        }


    }
}
