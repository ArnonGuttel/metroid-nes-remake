using UnityEngine;

public class BoundaryManager : MonoBehaviour
{

    #region Inspector

    private BoxCollider2D managerBox;
    public Transform Player;
    public GameObject boundary;

    #endregion
    
    #region Fields
    
    // Those variables will be allow the Designer to limit the camera movement in X-axis or Y-axis 

    public bool XMaxEnabled;
    public float XMaxValue;

    public bool XMinEnabled;
    public float XMinValue;
    
    public bool YMaxEnabled;
    public float YMaxValue;

    public bool YMinEnabled;
    public float YMinValue;

    #endregion

    #region MonoBehaviour

    private void Start()
    {
        managerBox = GetComponent<BoxCollider2D>();
    }
    private void Update()
    {
        ManageBoundary();
    }
    
    #endregion

    #region Methods
    
    
    private void ManageBoundary()
        // This method will ensure that at any given time there will be only one active Boundary.
        // The active Boundary will depend on the Player position.
        // We will use the Boundary on the Camera Follows Script.
    {
        if (managerBox.bounds.min.x < Player.position.x && Player.position.x < managerBox.bounds.max.x &&
            managerBox.bounds.min.y < Player.position.y && Player.position.y < managerBox.bounds.max.y)
        {
            boundary.SetActive(true);
        }
        else
        {
            boundary.SetActive(false);
        }
    }

    #endregion

}
