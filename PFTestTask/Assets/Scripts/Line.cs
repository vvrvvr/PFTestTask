using UnityEngine;

public class Line : MonoBehaviour
{
    [SerializeField] public Transform LineAnchor;
    private LineRenderer lineRend;
    private Transform lineEndPoint;
    private bool isDragging;

    private void Start()
    {
        lineRend = GetComponent<LineRenderer>();
        lineRend.positionCount = 2;
        lineRend.SetPosition(0, LineAnchor.position);
        lineRend.enabled = false;
        lineRend.material.color = GetComponent<Rectangle>().newColor;
    }

    /// <summary>
    /// update line points position while dragging line
    /// </summary>
    /// <param name="endPoint">mouse position</param>
    public void DraggingLine(Vector3 endPoint)
    {
        isDragging = true;
        lineRend.enabled = true;
        lineRend.SetPosition(0, LineAnchor.position);
        lineRend.SetPosition(1, endPoint);
    }

    public void HideLine()
    {
        isDragging = true;
        lineRend.enabled = false;
    }

    public void SetupLine(GameObject rectangle)
    {
        var EndPointObjectLine = rectangle.GetComponent<Line>();
        lineEndPoint = EndPointObjectLine.LineAnchor;
        lineRend.SetPosition(1, lineEndPoint.position);
        isDragging = false;
    }

    private void Update()
    {
        if (!isDragging)
        {
            if (lineEndPoint == null)
            {
                lineRend.enabled = false;
                return;
            }
            if (lineRend.enabled == true)
            {
                lineRend.SetPosition(0, LineAnchor.position);
                lineRend.SetPosition(1, lineEndPoint.position);
            }
        }
    }
}
