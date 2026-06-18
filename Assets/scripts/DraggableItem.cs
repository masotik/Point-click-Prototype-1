using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DraggableItem : MonoBehaviour
{
    private bool _dragging;
    [SerializeField] private CraftingManager craftingManager;
    [SerializeField] private ItemData itemData;
    private Vector2 _offset, _originalPosition;

    private void Awake()
    {
        _originalPosition = transform.position;
    }

    private void Update()
    {
        if (!_dragging) return;
        transform.position = GetMousePos() - _offset;
    }

    private void OnMouseDown()
    {
        _dragging = true;
        _offset = GetMousePos() - (Vector2)transform.position;
        craftingManager.OnMouseDownItem(itemData, this);
        Debug.Log("OnMouseDown fired!");
    }

    private void OnMouseUp()
    {
        _dragging = false;
        Debug.Log("OnMouseUp fired!");
    }

    public void SnapToSlot(Vector2 slotPosition)
    {
        transform.position = slotPosition;
    }

    public void ResetPosition()
    {
        transform.position = _originalPosition;
    }

    Vector2 GetMousePos()
    {
        return (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

}
