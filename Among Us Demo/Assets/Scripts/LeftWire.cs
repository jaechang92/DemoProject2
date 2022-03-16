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
    [SerializeField]
    private Canvas mGameCanvas;

    public bool isDragged = false;


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
        if (isDragged && !isConnect)
        {
            SetTarget(Input.mousePosition, -40f);
        }
    }

    public void SetTarget(Vector3 targetPosition, float offset)
    {
        float angle = Vector2.SignedAngle(transform.position + Vector3.right - transform.position, targetPosition - transform.position);
        float distance = Vector2.Distance(mWireBody.transform.position, targetPosition) + offset;
        mWireBody.localRotation = Quaternion.Euler(new Vector3(0f, 0f, angle));
        mWireBody.sizeDelta = new Vector2(distance , mWireBody.sizeDelta.y);
        Debug.Log(1 / mGameCanvas.transform.localScale.x);
        Debug.Log(mGameCanvas.transform.localScale.x);
        Debug.Log(mWireBody.sizeDelta);
        Debug.Log(distance);
    }

    public void ResetTarget()
    {
        mWireBody.localRotation = Quaternion.Euler(Vector3.zero);
        mWireBody.sizeDelta = new Vector2(0f, mWireBody.sizeDelta.y);
    }


}
