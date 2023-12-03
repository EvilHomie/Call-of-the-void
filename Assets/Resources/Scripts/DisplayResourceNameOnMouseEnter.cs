using TMPro;
using UnityEngine;

public class DisplayResourceNameOnMouseEnter : MonoBehaviour
{
    [SerializeField] GameObject displayResNameWindow;
    GameObject displayResName;
    Vector3 offsetToUpAndCenter = new(1, 0, 2);

    private void OnMouseEnter()
    {
        displayResName = Instantiate(displayResNameWindow, transform.position + offsetToUpAndCenter, displayResNameWindow.transform.rotation);
        displayResName.GetComponent<TextMeshPro>().text = GetComponent<ResourceItem>().type.ToString();
    }

    private void OnMouseOver()
    {
        displayResName.transform.position = transform.position + offsetToUpAndCenter;
    }

    private void OnMouseExit()
    {
        Destroy(displayResName);
    }

    private void OnDestroy()
    {
        Destroy(displayResName);
    }
}
