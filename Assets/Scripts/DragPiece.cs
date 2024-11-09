using UnityEngine;
using Photon.Pun;

public class DragObject : MonoBehaviourPun
{
    private bool isDragging = false;
    private Vector3 offset;

    void OnMouseDown()
    {
        // Calculate the offset to keep the drag smooth
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0f;
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
            // Update the position locally
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.z = 0f;
            transform.position = mousePosition + offset;

            // Send the position to all clients
            photonView.RPC("UpdatePosition", RpcTarget.AllBuffered, transform.position);
        }
    }

    [PunRPC]
    void UpdatePosition(Vector3 newPosition)
    {
        // Set the new position based on the RPC call
        transform.position = newPosition;
    }
}
