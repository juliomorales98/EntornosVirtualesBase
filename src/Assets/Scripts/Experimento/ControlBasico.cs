using Photon.Pun;
using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class ControlBasico : MonoBehaviour
{ 

    private float adelante;
    private float lado;
    private float ratonx;
    private float ratony;

    public enum RotationAxes { MouseXAndY = 0, MouseX = 1, MouseY = 2 }
    public RotationAxes axes = RotationAxes.MouseXAndY;
    public float sensitivityX = 15F;
    public float sensitivityY = 15F;

    public float minimumX = -360F;
    public float maximumX = 360F;

    public float minimumY = -60F;
    public float maximumY = 60F;

    float rotationY = 0F;
    


    private PhotonView PV;

    private Camera miCamara;



    void Start()
    {
        
        PV = GetComponent<PhotonView>();
        miCamara = transform.GetChild(0).GetComponent<Camera>();

    }


    // Update is called once per frame
    void Update()
    {
        
        if (PV.IsMine)
        {
            miCamara.enabled = false;
            miCamara.enabled = true;

            adelante = Input.GetAxis("Vertical");




            //de-commenting
            lado = Input.GetAxis("Horizontal");
            ratonx = Input.mousePosition.x;
            ratony = Input.mousePosition.y;


            if (lado != 0)
            {
                
            }
            else
            {
                float temp = 0;
               
            }

            //si presionan adelante puede caminar
            if (adelante > 0)
            {
               


                if (lado > 0.1 || lado < 0.1)
                {
                    
                }
            }
            else if (adelante == 0)
            {
              
            }
            else
            {
               

            }
            if (axes == RotationAxes.MouseXAndY)
            {
                float rotationX = transform.localEulerAngles.y + Input.GetAxis("Mouse X") * sensitivityX;

                rotationY += Input.GetAxis("Mouse Y") * sensitivityY;
                rotationY = Mathf.Clamp(rotationY, minimumY, maximumY);

                transform.localEulerAngles = new Vector3(-rotationY, rotationX, 0);
                transform.localEulerAngles = new Vector3(0, rotationX, 0);

                //			head.Rotate(0, Input.GetAxis("Mouse X") * sensitivityX, 0);
                transform.LookAt(Input.mousePosition);
                transform.LookAt(Input.mousePosition);
            }
            else if (axes == RotationAxes.MouseX)
            {
                transform.Rotate(0, Input.GetAxis("Mouse X") * sensitivityX, 0);

                //			head.Rotate(0, Input.GetAxis("Mouse X") * sensitivityX, 0);
            }


            if (ratony > (Screen.height - 100))
            {
               
            }
            else if (ratony < 100)
            {
                
            }
            else if (ratonx < 250)
            {
               
            }
            else if (ratonx > Screen.width - 250)
            {
               
            }
            else
            {
                
            }
        }

    }
    
}