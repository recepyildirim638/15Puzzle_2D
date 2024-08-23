using Game.Manager;
using Game.Model;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MouseHandler : MonoBehaviour
{
    [SerializeField] Camera _cam;
    [SerializeField] LayerMask layerMask;

    private bool _isActive = false;
    private bool _isProgses = false;
    private RaycastHit2D hit;

    Vector2 _lastMousePosition = default;

    Cell _activeCell = null;
    private void Update()
    {
        if (_isProgses)
            return;

        if (Input.GetMouseButtonDown(0))
        {
            _lastMousePosition = Input.mousePosition;
            Vector2 mouseScreenToWorldPoind = _cam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y));

            if (!IsPointerOverUIObject() && _isActive == false)
            {
                RaycastHit2D hit = Physics2D.Raycast(mouseScreenToWorldPoind, -Vector2.up);

                if (hit.collider != null)
                {
                    Cell cell = hit.collider.gameObject.GetComponent<Cell>();

                    if (cell != null)
                    {
                        _activeCell = cell;
                        _isActive = true;
                    }
                }
            }
        }
        if (Input.GetMouseButton(0))
        {
            if (_isActive)
            {
                Vector2 distancePosition = ((Vector2)Input.mousePosition - _lastMousePosition);

                float distance = Vector2.Distance((Vector2)Input.mousePosition, _lastMousePosition);
               

                if(distance > 100f)
                {
                    MOVE_DIRECTION moveDirection = CalculateDirection(distancePosition);

                    if (moveDirection != MOVE_DIRECTION.NONE && _activeCell != null)
                    {
                        _isProgses = true;
                        GameManager.ins.MoveCell(_activeCell, moveDirection, MoveEndCallBack);
                    }
                }
            }

        }
    }

    public void MoveEndCallBack()
    {
        _activeCell = null;
        _isProgses = false;
        _isActive = false;
    }

    public MOVE_DIRECTION CalculateDirection(Vector2 direction)
    {
       
        if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y))
            return direction.x > 0f ? MOVE_DIRECTION.RIGHT : MOVE_DIRECTION.LEFT;
        else if (Mathf.Abs(direction.x) < Mathf.Abs(direction.y))
            return direction.y > 0f ? MOVE_DIRECTION.UP : MOVE_DIRECTION.DOWN;
        else
            return MOVE_DIRECTION.NONE;
    }

    private bool IsPointerOverUIObject()
    {
        PointerEventData eventData = new PointerEventData(EventSystem.current);
        eventData.position = Input.mousePosition;
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventData, results);
        return results.Count > 0;
    }


    public enum MOVE_DIRECTION
    {
        NONE,
        LEFT,
        RIGHT,
        UP,
        DOWN
    }
}
