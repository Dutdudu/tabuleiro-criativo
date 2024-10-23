using UnityEngine;

public class DragObject : MonoBehaviour
{
    private bool isDragging = false;
    private Vector3 offset;

    void OnMouseDown()
    {
        // Converter a posição do mouse para coordenadas de mundo
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0f; // Certifique-se de que a posição Z está zerada (importante para 2D)

        // Calcular o deslocamento entre a posição do mouse e a posição do objeto
        offset = transform.position - mousePosition;
        isDragging = true;
    }

    void OnMouseUp()
    {
        isDragging = false;
    }

    void Update()
    {
        if (isDragging)
        {
            // Converter a posição do mouse para coordenadas de mundo
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.z = 0f;

            // Mover o objeto para seguir o mouse, com o deslocamento aplicado
            transform.position = mousePosition + offset;
        }
    }
}