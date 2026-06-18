using UnityEngine;

public class cursor : MonoBehaviour
{

    private void Awake()
    {
        transform.position = Input.mousePosition;
    }

    private void Update()
    {
        transform.position = Input.mousePosition;
    }
}
