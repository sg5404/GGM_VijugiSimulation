using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorImage : MonoBehaviour
{
    [SerializeField]
    private Texture2D cursorImage;

    void Awake()
    {
        Cursor.SetCursor(cursorImage, Vector2.zero, CursorMode.Auto);
    }
}
