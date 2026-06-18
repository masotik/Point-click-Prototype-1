using UnityEngine;

public class nameTag : MonoBehaviour
{
    Vector2 resolution, resolutionWorldUnits = new Vector2(17.8f, 10);
    void Start()
    {
        resolution = new Vector2 (Screen.width, Screen.height);
    }

    // Update is called once per frame
    void LateUpdate()
    {
        FollowMouse();

    }

    public void FollowMouse()
    {
        transform.position = Input.mousePosition / resolution * resolutionWorldUnits;
    }
}
