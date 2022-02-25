using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LeftWire : MonoBehaviour
{

    [SerializeField]
    private RectTransform mWireBody;
    [SerializeField]
    private LeftWire mSelectedWire;

    [SerializeField]
    private float offset = 15f;


    private Canvas mGameCanvas;

    // Start is called before the first frame update
    void Start()
    {
        mGameCanvas = FindObjectOfType<Canvas>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit2D hit = Physics2D.Raycast(Input.mousePosition, Vector2.right, 1f);
            if (hit.collider != null)
            {
                var left = hit.collider.GetComponentInParent<LeftWire>();
                if (left != null)
                {
                    mSelectedWire = left;
                }
            }
            Debug.Log("in");
        }

        if (Input.GetMouseButtonUp(0))
        {
            if (mSelectedWire != null)
            {
                mWireBody.localRotation = Quaternion.Euler(Vector3.zero);
                mWireBody.sizeDelta = new Vector2(0f, mWireBody.sizeDelta.y);
                mSelectedWire = null;
            }
        }


        if (mSelectedWire !=null)
        {
            float angle = Vector2.SignedAngle(transform.position + Vector3.right - transform.position, Input.mousePosition - transform.position);
            float distance = Vector2.Distance(mWireBody.transform.position, Input.mousePosition) - offset;
            mWireBody.localRotation = Quaternion.Euler(new Vector3(0f, 0f, angle));
            mWireBody.sizeDelta = new Vector2(distance * (1 / mGameCanvas.transform.localScale.x), mWireBody.sizeDelta.y);

            Debug.Log(mWireBody.sizeDelta);
        }
    }
}
