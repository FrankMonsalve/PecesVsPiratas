using UnityEngine;

public class DragObject2D : MonoBehaviour
{
    private Vector3 offset; // Desplazamiento relativo al rat�n
    private Vector3 originalPosition; // Posici�n original del objeto
    private Camera mainCamera; // C�mara principal
    private BoxZone2D currentBox; // Caja en la que est� el objeto
    private bool isDragged = false; // Si el objeto est� siendo arrastrado

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
            // Actualiza la posici�n del objeto mientras lo arrastras
            transform.position = GetMouseWorldPosition() + offset;
        }
    }

    void OnMouseUp()
    {
        isDragged = false;

        // Verifica si el objeto est� dentro de alguna caja de arriba (BoxUp)
        BoxZone2D[] allBoxesUp = FindObjectsOfType<BoxZone2D>(); // Buscar todas las cajas de arriba
        foreach (BoxZone2D boxUp in allBoxesUp)
        {
            if (boxUp.CompareTag("boxUp") && boxUp.currentObject == null) // Si la caja est� vac�a
            {
                // Si la caja de arriba est� vac�a, mueve el objeto all�
                MoverABoxUp(boxUp);
                return;
            }
        }

        // Si no se hizo clic en una BoxUp vac�a, verifica si debe regresar a BoxDown
        RegresarABoxDown();
    }

    // M�todo para obtener la posici�n del rat�n en el mundo
    private Vector3 GetMouseWorldPosition()
    {
        Vector3 mouseScreenPosition = Input.mousePosition;
        mouseScreenPosition.z = Mathf.Abs(mainCamera.transform.position.z - transform.position.z);
        return mainCamera.ScreenToWorldPoint(mouseScreenPosition);
    }

    // M�todo para devolver el objeto a la posici�n original
    public void ReturnToOriginalPosition()
    {
        transform.position = originalPosition;
        Debug.Log($"{gameObject.name} regres� a la posici�n original.");
    }

    // M�todo para mover el objeto a una caja de arriba (BoxUp)
    private void MoverABoxUp(BoxZone2D boxUp)
    {
        // Mover el objeto al BoxUp vac�o
        boxUp.currentObject = this.gameObject; // Establece el objeto en la caja de arriba
        transform.SetParent(boxUp.transform);
        transform.localPosition = Vector3.zero; // Coloca el objeto en la posici�n central de la caja de arriba
        Debug.Log($"El objeto {gameObject.name} se movi� a la caja de arriba {boxUp.name}.");
    }

    // M�todo para devolver el objeto a una caja vac�a de abajo (BoxDown)
    private void RegresarABoxDown()
    {
        // Buscar una caja vac�a de abajo (BoxDown)
        BoxZone2D[] allBoxesDown = FindObjectsOfType<BoxZone2D>(); // Buscar todas las cajas de abajo
        foreach (BoxZone2D boxDown in allBoxesDown)
        {
            if (boxDown.CompareTag("boxDown") && boxDown.currentObject == null) // Si la caja de abajo est� vac�a
            {
                // Mueve el objeto a la caja de abajo
                boxDown.currentObject = this.gameObject;
                transform.SetParent(boxDown.transform);
                transform.localPosition = Vector3.zero; // Coloca el objeto en la posici�n central de la caja de abajo
                Debug.Log($"El objeto {gameObject.name} regres� a la caja de abajo {boxDown.name}.");
                return;
            }
        }

        // Si no se encuentra una caja vac�a de abajo, regresa al origen
        ReturnToOriginalPosition();
    }
}
