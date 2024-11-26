using UnityEngine;

public class DragObject2D : MonoBehaviour
{
    private Vector3 offset; // Desplazamiento relativo al mouse
    private Vector3 originalPosition; // Posición original del objeto
    private Camera mainCamera; // Referencia a la cámara principal

    void Start()
    {
        mainCamera = Camera.main;
        originalPosition = transform.position;
    }

    void OnMouseDown()
    {
        offset = transform.position - GetMouseWorldPosition();
    }

    void OnMouseDrag()
    {
        transform.position = GetMouseWorldPosition() + offset;
    }

    void OnMouseUp()
    {
        // Verificar si el objeto está dentro de alguna caja
        BoxZone2D[] allBoxes = FindObjectsOfType<BoxZone2D>(); // Busca todas las cajas en la escena
        bool objectInBox = false;

        foreach (BoxZone2D box in allBoxes)
        {
            if (box.currentObject == this.gameObject) // Si este objeto está en una caja
            {
                objectInBox = true;
                Debug.Log($"{gameObject.name} se quedó en la caja.");
                break;
            }
        }

        if (!objectInBox)
        {
            Debug.Log($"{gameObject.name} no se puede quedar en ninguna caja.");
            ReturnToOriginalPosition();
        }
    }

    private Vector3 GetMouseWorldPosition()
    {
        Vector3 mouseScreenPosition = Input.mousePosition;
        mouseScreenPosition.z = Mathf.Abs(mainCamera.transform.position.z - transform.position.z);
        return mainCamera.ScreenToWorldPoint(mouseScreenPosition);
    }

    public void ReturnToOriginalPosition()
    {
        transform.position = originalPosition;
        Debug.Log($"{gameObject.name} regresó a la posición original.");
    }
}
