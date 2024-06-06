using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorController : MonoBehaviour
{
    private RectTransform cursorRectTransform;

    private void Awake()
    {
        cursorRectTransform = GetComponent<RectTransform>();
        Cursor.visible = false;  // Hide the default cursor
    }

    private void Update()
    {
        Vector2 cursorPosition;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(cursorRectTransform.parent as RectTransform, Input.mousePosition, null, out cursorPosition);
        cursorRectTransform.localPosition = cursorPosition;
    }
}
