using UnityEngine;

public class SpawnRectangle : MonoBehaviour
{
    [SerializeField] private GameObject rectanglePrefab;
    [SerializeField] private LayerMask rectangleLayers;
    private Vector3 halfRectangleDimensiongs;
    private Camera cam;
    private float zDistance;
    private GameManager gameManager;

    private void Start()
    {
        cam = Camera.main;
        gameManager = GameManager.Singleton;
        halfRectangleDimensiongs = gameManager.halfRectangleDimensiongs;
        zDistance = gameManager.zDistance;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePosition = Input.mousePosition;
            mousePosition.z = zDistance;
            var currentPoint = cam.ScreenToWorldPoint(mousePosition);
            //check if the space is not occupied by another rectangle and spawn rectangle
            bool canPlaceRectagle = !Physics.CheckBox(currentPoint, halfRectangleDimensiongs, Quaternion.identity, rectangleLayers);
            if (canPlaceRectagle && gameManager.CheckBordersX(currentPoint.x) && gameManager.CheckBordersY(currentPoint.y))
            {
                Instantiate(rectanglePrefab, currentPoint, Quaternion.identity);
            }
            else
            {
                return;
            }
        }
    }
}
