using UnityEngine;

public class DragObject2D : MonoBehaviour
{
    private Vector3 offset; // Desplazamiento relativo al ratón
    private Vector3 originalPosition; // Posición original del objeto
    private Camera mainCamera; // Cámara principal
    private BoxZone2D currentBox; // Caja en la que está el objeto
    private bool isDragged = false; // Si el objeto está siendo arrastrado

    void Start()
    {
        mainCamera = Camera.main;
        originalPosition = transform.position;
    }

    void OnMouseDown()
    {
        offset = transform.position - GetMouseWorldPosition();
        isDragged = true;
    }

    void OnMouseDrag()
    {
        if (isDragged)
        {
            // Actualiza la posición del objeto mientras lo arrastras
            transform.position = GetMouseWorldPosition() + offset;
        }
    }

    void OnMouseUp()
    {
        isDragged = false;

        // Verifica si el objeto está dentro de alguna caja de arriba (BoxUp)
        BoxZone2D[] allBoxesUp = FindObjectsOfType<BoxZone2D>(); // Buscar todas las cajas de arriba
        foreach (BoxZone2D boxUp in allBoxesUp)
        {
            if (boxUp.CompareTag("boxUp") && boxUp.currentObject == null) // Si la caja está vacía
            {
                // Si la caja de arriba está vacía, mueve el objeto allí
                MoverABoxUp(boxUp);
                return;
            }
        }

        // Si no se hizo clic en una BoxUp vacía, verifica si debe regresar a BoxDown
        RegresarABoxDown();
    }

    // Método para obtener la posición del ratón en el mundo
    private Vector3 GetMouseWorldPosition()
    {
        Vector3 mouseScreenPosition = Input.mousePosition;
        mouseScreenPosition.z = Mathf.Abs(mainCamera.transform.position.z - transform.position.z);
        return mainCamera.ScreenToWorldPoint(mouseScreenPosition);
    }

    // Método para devolver el objeto a la posición original
    public void ReturnToOriginalPosition()
    {
        transform.position = originalPosition;
        Debug.Log($"{gameObject.name} regresó a la posición original.");
    }

    // Método para mover el objeto a una caja de arriba (BoxUp)
    private void MoverABoxUp(BoxZone2D boxUp)
    {
        // Mover el objeto al BoxUp vacío
        boxUp.currentObject = this.gameObject; // Establece el objeto en la caja de arriba
        transform.SetParent(boxUp.transform);
        transform.localPosition = Vector3.zero; // Coloca el objeto en la posición central de la caja de arriba
        Debug.Log($"El objeto {gameObject.name} se movió a la caja de arriba {boxUp.name}.");
    }

    // Método para devolver el objeto a una caja vacía de abajo (BoxDown)
    private void RegresarABoxDown()
    {
        // Buscar una caja vacía de abajo (BoxDown)
        BoxZone2D[] allBoxesDown = FindObjectsOfType<BoxZone2D>(); // Buscar todas las cajas de abajo
        foreach (BoxZone2D boxDown in allBoxesDown)
        {
            if (boxDown.CompareTag("boxDown") && boxDown.currentObject == null) // Si la caja de abajo está vacía
            {
                // Mueve el objeto a la caja de abajo
                boxDown.currentObject = this.gameObject;
                transform.SetParent(boxDown.transform);
                transform.localPosition = Vector3.zero; // Coloca el objeto en la posición central de la caja de abajo
                Debug.Log($"El objeto {gameObject.name} regresó a la caja de abajo {boxDown.name}.");
                return;
            }
        }

        // Si no se encuentra una caja vacía de abajo, regresa al origen
        ReturnToOriginalPosition();
    }
}
