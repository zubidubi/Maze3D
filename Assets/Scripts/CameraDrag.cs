using UnityEngine;

public class CameraDrag : MonoBehaviour
{
    public enum RotationAxes { MouseXAndY = 0, MouseX = 1, MouseY = 2 }
    public RotationAxes axes = RotationAxes.MouseXAndY;
    public float sensitivityX;
    public float sensitivityY;

    public float minimumX;
    public float maximumX;

    public float minimumY;
    public float maximumY;

    float rotationY = 0F;
    //private Vector3 lastPosition;
    float threshold = 0.5f;
    /*private void reset()
    {
        rotationY = 0F;
    }*/

    void Update()
    {
        //if (!Input.GetMouseButton(0)) return;

        //Reseteamos la rotación de la camara si se ha desplazado y no está rotando
        //Vector3 movement = (this.transform.parent.transform.position - lastPosition);
        /*if (movement.x > threshold || movement.y > threshold || movement.z > threshold)
        {
            reset();
        }
        */
        if (axes == RotationAxes.MouseXAndY)
        {
            float rotationX = transform.parent.transform.localEulerAngles.y + Input.GetAxis("Mouse X") * sensitivityX;

            rotationY += Input.GetAxis("Mouse Y") * sensitivityY;
            rotationY = Mathf.Clamp(rotationY, minimumY, maximumY);

            transform.parent.transform.localEulerAngles = new Vector3(-rotationY, rotationX, 0);
        }
        else if (axes == RotationAxes.MouseX)
        {
            transform.parent.transform.Rotate(0, Input.GetAxis("Mouse X") * sensitivityX, 0);
        }
        else
        {
            rotationY += Input.GetAxis("Mouse Y") * sensitivityY;
            rotationY = Mathf.Clamp(rotationY, minimumY, maximumY);

            transform.parent.transform.localEulerAngles = new Vector3(-rotationY, transform.parent.transform.localEulerAngles.y, 0);
        }
        //lastPosition = transform.parent.transform.position;
    }

}

