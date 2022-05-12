using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Garbage_leverController : ClearChecker
{
    [SerializeField]
    private RectTransform garbage_leverBars;

    [SerializeField]
    private RectTransform garbage_leverHandle;

    [SerializeField]
    private bool isSelected = false;
    private float originSizeY;

    [SerializeField]
    private Collider2D garbage_Door;

    [SerializeField]
    private List<Rigidbody2D> leafs_rb;
    [SerializeField]
    private float forceValue;
    [SerializeField]
    private CameraShake shakeParent;
    void Start()
    {

        originSizeY = garbage_leverBars.sizeDelta.y;
        if (shakeParent == null)
        {
            shakeParent = GetComponentInParent<CameraShake>();
        }
    }

    void Update()
    {
        if (isClear) return;
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit2D hit = Physics2D.Raycast(Input.mousePosition, Vector2.right, 1f);
            if (hit.collider != null && hit.collider.CompareTag("ClickObject"))
            {
                isSelected = true;
                garbage_Door.isTrigger = true;

                foreach (var leaf in leafs_rb)
                {
                    leaf.AddRelativeForce(Vector2.up * forceValue);
                    leaf.AddForce(Vector2.up * forceValue);
                    shakeParent.shakeDuration = 2.0f;
                }
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            isSelected = false;
            garbage_leverBars.sizeDelta = new Vector2(garbage_leverBars.sizeDelta.x, originSizeY);
            garbage_leverBars.localRotation = Quaternion.Euler(new Vector3(0, 0, 180));
            garbage_Door.isTrigger = false;
        }


        if (isSelected)
        {
            float angle = Vector2.SignedAngle(transform.position + Vector3.right - transform.position, Input.mousePosition - transform.position);
            Debug.Log(angle);

            

            float dist = garbage_leverBars.transform.position.y > Input.mousePosition.y ? garbage_leverBars.transform.position.y - Input.mousePosition.y : Input.mousePosition.y - garbage_leverBars.transform.position.y;
            //Vector2.Distance(garbage_leverBars.transform.position, Input.mousePosition)-27;
            dist -= 27;

            dist = Mathf.Clamp(dist, 0, originSizeY);
            angle = angle > 0 ? 180 : 0;
            Debug.Log(dist);
            garbage_leverBars.localRotation = Quaternion.Euler(new Vector3(0, 0, angle));
            garbage_leverBars.sizeDelta = new Vector2(garbage_leverBars.sizeDelta.x, dist);
        }

        CheckClear();
    }


    private void CheckClear()
    {
        foreach (var leaf in leafs_rb)
        {
            if (garbage_Door.transform.position.y + garbage_Door.offset.y < leaf.transform.position.y)
            {
                return;
            }
        }
        isClear = true;
        InGameUIManager.Instance.CloseTaskUI(shakeParent.gameObject, 1.0f);
    }

}
