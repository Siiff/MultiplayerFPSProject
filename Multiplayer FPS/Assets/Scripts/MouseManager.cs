using UnityEngine;
using UnityEngine.Events;

public class MouseManager : MonoBehaviour
{
    public EventEnvironment OnClickEnvironment;
    public GameObject directionMarker;
    public LayerMask layerMask;
    public Texture2D cursorTarget;
    public Texture2D cursorArrow;
    public Texture2D cursorInteractable;
    public Texture2D cursorBlocked;
    public Vector2   cursorPos = new Vector2(0, 0);

    private GameObject marker;

    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        Debug.DrawRay(ray.origin, ray.direction * 100, Color.red);

        if (Physics.Raycast(ray, out hit, 100, layerMask))
        {
            switch (hit.collider.tag)
            {
                case "Rock":
                    Cursor.SetCursor(cursorBlocked, new Vector2(16, 16), CursorMode.ForceSoftware);
                    break;

                case "Ground":
                case "Rescue Zone":
                    Cursor.SetCursor(cursorTarget, cursorPos, CursorMode.ForceSoftware);
                    break;
                
                case "Collectable":
                    Cursor.SetCursor(cursorInteractable, cursorPos, CursorMode.ForceSoftware);
                    break;
            }

            if (Input.GetMouseButton(0))
            {
                Debug.Log(hit.collider.tag);
                EventEnvironmentArgs args;

                switch (hit.collider.tag)
                {
                    case "Rock":
                        break;

                    case "Ground":
                        args = new EventEnvironmentArgs();
                        args.destination = hit.point;
                        args.tag = hit.collider.tag;
                        OnClickEnvironment.Invoke(args);

                        if (marker != null) Destroy(marker);
                        marker = Instantiate(directionMarker, hit.point + new Vector3(0, 0.05f, 0), Quaternion.Euler(new Vector3(90, 0, 0)));
                        Destroy(marker, 4);
                        break;

                    case "Collectable":
                        args = new EventEnvironmentArgs();
                        args.destination = hit.point;
                        args.tag = hit.collider.tag;
                        OnClickEnvironment.Invoke(args);
                        break;
                }
            }
        }
    }
}

[System.Serializable]
public class EventEnvironment : UnityEvent<EventEnvironmentArgs>
{ 

}

public class EventEnvironmentArgs
{
    public Vector3 destination;
    public string tag;
}

