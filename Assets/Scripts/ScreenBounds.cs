using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/*
     SCRIPT IS FROM: https://www.youtube.com/watch?v=1a9ag16PeFw
        - this script is updated to only look at the x position
 */

[RequireComponent(typeof(BoxCollider2D))]
public class ScreenBounds : MonoBehaviour
{
    public Camera mainCamera;
    BoxCollider2D boxCollider;

    public UnityEvent<Collider2D> ExitTriggerFired;

    [SerializeField]
    private float teleportOffset = 0.2f;

    [SerializeField]
    private float cornerOffset = 1;

    private void Awake()
    {
        this.mainCamera.transform.localScale = Vector3.one;
        boxCollider = GetComponent<BoxCollider2D>();
        boxCollider.isTrigger = true;
    }

    private void Start()
    {
        transform.position = Vector3.zero;
        UpdateBoundsSize();
    }

    public void UpdateBoundsSize()
    {
        //orthographicSize = half the size of the height of the screen. That is why we * it by 2
        float ySize = mainCamera.orthographicSize * 2;
        //width of the camera depends on the acpect ration and the height
        Vector2 boxColliderSize = new Vector2(ySize * mainCamera.aspect, ySize);
        boxCollider.size = boxColliderSize;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        ExitTriggerFired?.Invoke(collision);
    }

    public bool AmIOutOfBounds(Vector3 worldPosition)
    {
        return Mathf.Abs(worldPosition.x) > Mathf.Abs(boxCollider.bounds.min.x);
    }

    public Vector2 CalculateWrappedPosition(Vector2 worldPosition)
    {
        bool xBoundResult = Mathf.Abs(worldPosition.x) > (Mathf.Abs(boxCollider.bounds.min.x) - cornerOffset);

        Vector2 signWorldPosition = new Vector2(Mathf.Sign(worldPosition.x), Mathf.Sign(worldPosition.y));

        if (xBoundResult)
        {
            return new Vector2(worldPosition.x * -1, worldPosition.y)
                + new Vector2(teleportOffset * signWorldPosition.x, 0);
        }
        else
        {
            return worldPosition;
        }
    }

}