using UnityEngine;

public class LineHandler : MonoBehaviour
{
    [SerializeField] LayerMask rectangleLayer;
    private bool isDraggingLine;
    private GameObject currentRectangle;
    private GameObject secondRectangle;
    private Line currentRectangleLine;
    private Camera cam;
    private GameManager gameManager;
    private float lineZDistance;

    private void Start()
    {
        cam = Camera.main;
        gameManager = GameManager.Singleton;
        lineZDistance = gameManager.lineZDistance;
    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            isDraggingLine = true;
            OnDragStart();
        }
        if (Input.GetMouseButtonUp(1))
        {
            isDraggingLine = false;
            OnDragEnd();
        }
        if (isDraggingLine)
        {
            OnDragging();
        }
    }

    /// <summary>
    /// if the cursor is over the rectangle, start dragging
    /// </summary>
    private void OnDragStart()
    {
        RaycastHit rayHit;
        if (Physics.Raycast(cam.ScreenPointToRay(Input.mousePosition), out rayHit, Mathf.Infinity, rectangleLayer))
        {
            currentRectangle = rayHit.collider.gameObject;
            currentRectangleLine = currentRectangle.GetComponent<Line>();
        }
    }

    /// <summary>
    /// if the cursor is over the another rectangle - connect both rectangles with a line
    /// </summary>
    private void OnDragEnd()
    {
        RaycastHit rayHit;
        if (Physics.Raycast(cam.ScreenPointToRay(Input.mousePosition), out rayHit, Mathf.Infinity, rectangleLayer))
        {
            secondRectangle = rayHit.collider.gameObject;
            if (currentRectangle != secondRectangle)
            {
                currentRectangleLine.SetupLine(secondRectangle);
            }
            else
            {
                currentRectangleLine.HideLine();
            }
            currentRectangle = secondRectangle = null;
            currentRectangleLine = null;
        }
        else
        {
            currentRectangleLine.HideLine();
            currentRectangle = null;
            currentRectangleLine = null;
        }
    }

    /// <summary>
    /// draw line from current rectangle to cursor while dragging
    /// </summary>
    private void OnDragging()
    {
        var currentLinePoint = gameManager.MousePosToWorldPoint(lineZDistance);
        currentRectangleLine.DraggingLine(currentLinePoint);
    }
}
