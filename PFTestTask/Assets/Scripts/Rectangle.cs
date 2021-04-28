using UnityEngine;

public class Rectangle : MonoBehaviour
{
    [SerializeField] private LayerMask rectangleLayer;
    private Vector3 startPosition;
    private Vector3 firstDragPos;
    private Vector3 currentPoint;
    private Vector3 halfRectangleDimensiongs;
    private float doubleClickTime = 0f;
    private float DoubleClickSpeed = 0.25f;
    private float zDistance;
    private int defaultRectangelLayerNumber = 6;
    private int choosenRectangleLayerNumber = 8;
    private MeshRenderer meshRend;
    private GameManager gameManager;
    [HideInInspector] public Color newColor;

    private void Start()
    {
        meshRend = GetComponent<MeshRenderer>();
        newColor = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f), 1f);
        meshRend.material.color = newColor;
        gameManager = GameManager.Singleton;
        zDistance = gameManager.zDistance;
        halfRectangleDimensiongs = gameManager.halfRectangleDimensiongs;
    }

    private void Update()
    {
        if (doubleClickTime < 1f)
            doubleClickTime += Time.deltaTime;
    }

    private void OnMouseDown()
    {
        //Double click functionality
        if (doubleClickTime <= DoubleClickSpeed)
        {
            Destroy(gameObject);
        }
        doubleClickTime = 0f;

        //start dragging rectangle
        var currentPoint = gameManager.MousePosToWorldPoint(zDistance);
        startPosition = transform.position;
        firstDragPos = currentPoint;
        gameObject.layer = choosenRectangleLayerNumber;
    }

    //dragging rectangle
    private void OnMouseDrag()
    {
        currentPoint = gameManager.MousePosToWorldPoint(zDistance);
        Vector3 direction = currentPoint - firstDragPos;
        Vector3 newPos = startPosition + direction;
        bool canPlaceRectagle = !Physics.CheckBox(newPos, halfRectangleDimensiongs, Quaternion.identity, rectangleLayer);
        if (canPlaceRectagle && gameManager.CheckBordersX(newPos.x) && gameManager.CheckBordersY(newPos.y))
        {
            transform.position = newPos;
        }
    }

    //end dragging rectangle
    private void OnMouseUp()
    {
        startPosition = Vector3.zero;
        gameObject.layer = defaultRectangelLayerNumber;
    }
}
