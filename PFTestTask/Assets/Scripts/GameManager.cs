using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Singleton;
    [HideInInspector] public Vector3 halfRectangleDimensiongs = new Vector3(1f, 0.5f, 0.5f);
    [HideInInspector] public float lineZDistance = 9.5f;
    [HideInInspector] public float zDistance = 10f;
    //screen borders values for the center of the rectangle
    [HideInInspector] public float left;
    [HideInInspector] public float rigth;
    [HideInInspector] public float top;
    [HideInInspector] public float bottom;

    private void Awake()
    {
        Singleton = this;
        Vector3 topRightCornerScreen = Camera.main.ViewportToWorldPoint(new Vector3(1f, 1f, 0));
        rigth = topRightCornerScreen.x - halfRectangleDimensiongs.x;
        top = topRightCornerScreen.y - halfRectangleDimensiongs.y;
        Vector3 bottomLeftCornerScreen = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, 0));
        left = bottomLeftCornerScreen.x + halfRectangleDimensiongs.x;
        bottom = bottomLeftCornerScreen.y + halfRectangleDimensiongs.y;
    }

    /// <summary>
    /// convert the cursor position to a point in the world with a given Z component of the vector
    /// </summary>
    /// <returns></returns>
    public Vector3 MousePosToWorldPoint(float zDist)
    {
        Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = zDist;
        return Camera.main.ScreenToWorldPoint(mousePosition);
    }

    /// <summary>
    /// check if the value does not go offscreen
    /// </summary>
    /// <param name="x">x component of vector</param>
    /// <returns></returns>
    public bool CheckBordersX(float x)
    {
        if (x >= left && x <= rigth)
            return true;
        else
            return false;
    }

    /// <summary>
    /// check if the value does not go offscreen
    /// </summary>
    /// <param name="y">y component of vector</param>
    /// <returns></returns>
    public bool CheckBordersY(float y)
    {
        if( y<= top && y>= bottom)
            return true;
        else
            return false;
    }
}
