using UnityEngine;

public class CameraDrag : MonoBehaviour
{
    public enum RotationAxes { MouseXAndY = 0, MouseX = 1, MouseY = 2 }
    public RotationAxes axes = RotationAxes.MouseXAndY;
    public float sensitivityX = 15F;
    public float sensitivityY = 15F;

    public float minimumX = -360F;
    public float maximumX = 360F;

    public float minimumY = -60F;
    public float maximumY = 60F;

    float rotationY = 0F;
    private Vector3 lastPosition;
    float threshold = 0.5f;
    private void reset()
    {
        rotationY = 0F;
    }

    void Update()
    {
        if (!Input.GetMouseButton(0)) return;

        //Reseteamos la rotación de la camara si se ha desplazado y no está rotando
        Vector3 movement = (this.transform.position - lastPosition);
        if (movement.x > threshold || movement.y > threshold || movement.z > threshold)
        {
            reset();
        }

        if (axes == RotationAxes.MouseXAndY)
        {
            float rotationX = transform.localEulerAngles.y + -Input.GetAxis("Mouse X") * sensitivityX;

            rotationY += -Input.GetAxis("Mouse Y") * sensitivityY;
            rotationY = Mathf.Clamp(rotationY, minimumY, maximumY);

            transform.localEulerAngles = new Vector3(-rotationY, rotationX, 0);
        }
        else if (axes == RotationAxes.MouseX)
        {
            transform.Rotate(0, -Input.GetAxis("Mouse X") * sensitivityX, 0);
        }
        else
        {
            rotationY += -Input.GetAxis("Mouse Y") * sensitivityY;
            rotationY = Mathf.Clamp(rotationY, minimumY, maximumY);

            transform.localEulerAngles = new Vector3(-rotationY, transform.localEulerAngles.y, 0);
        }
        lastPosition = transform.position;
    }

}

