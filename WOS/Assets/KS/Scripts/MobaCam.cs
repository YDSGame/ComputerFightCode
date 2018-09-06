using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobaCam : MonoBehaviour
{

    [SerializeField] bool rotationMode;
    public float scrollSpeed; //화면 이동 속도
    public float scrollWheelSpeed; //화면 줌 속도
    public float topBarrier; //마우스로 받은 정보를 스크린높이의 어느정도(퍼센트)에 왔을때 움직이도록 할것인가.
    public float botBarrier;
    public float leftBarrier;
    public float rightBarrier;

  //제한
    public float yTopPositionLock;
    public float yBottomPositonLock;
    public float XRightositionLock;
    public float xLeftPosiotionLock;
    public float ZoomInLock;
    public float ZoomOutLock;
   
    private void Update()
    {       
        if (Input.mousePosition.y >= Screen.height * topBarrier && transform.position.x > -yTopPositionLock)
        {
            if (rotationMode)
            {
                transform.position = transform.position + transform.forward * scrollSpeed * Time.deltaTime;
            }
        }
        if (Input.mousePosition.y <= Screen.height * botBarrier && transform.position.x < yBottomPositonLock)
        {
            if (rotationMode)
            {
                transform.position = transform.position - transform.forward * scrollSpeed * Time.deltaTime;
            }
        }
        if (Input.mousePosition.x >= Screen.width * rightBarrier && transform.position.z < XRightositionLock)
        {
            if (rotationMode)
            {
                transform.position = transform.position + transform.right * scrollSpeed * Time.deltaTime;
            }
        }
        if (Input.mousePosition.x <= Screen.width * leftBarrier && transform.position.z > -xLeftPosiotionLock)
        {
            if (rotationMode)
            {
                transform.position = transform.position - transform.right * scrollSpeed * Time.deltaTime;
            }
        }
        if (Input.GetAxis("Mouse ScrollWheel") < 0 && Camera.main.transform.localPosition.y < ZoomOutLock) //ZoomOut
        {
            //Camera.main.orthographicSize = Camera.main.orthographicSize + scrollWheelSpeed*Time.deltaTime;
            
            Camera.main.transform.localPosition = new Vector3(0f,Camera.main.transform.localPosition.y, Camera.main.transform.localPosition.z) - new Vector3(0f,-1,0.3f) * scrollWheelSpeed *Time.deltaTime;
            //Camera.main.transform.localPosition = Vector3.Lerp(new Vector3(0f, Camera.main.transform.localPosition.y, Camera.main.transform.localPosition.z), new Vector3(0f, Camera.main.transform.localPosition.y, Camera.main.transform.localPosition.z) - new Vector3(0f, -1, 0.3f) * scrollWheelSpeed,Time.deltaTime);
        }
        if (Input.GetAxis("Mouse ScrollWheel") > 0 && Camera.main.transform.localPosition.y > -ZoomInLock ) //ZoomIn
        {
            //print(Camera.main.transform.forward); //포워드 X의 값이 있어 X가 영향을 받는다. 이를 움직이지 못하도록 해야 중심을 벗어나지 않고 줌인 아웃이 될 수 있다. 그래서 포워드 대신 값을 새로 지정한다.
            //Camera.main.orthographicSize = Camera.main.orthographicSize - scrollWheelSpeed * Time.deltaTime;
            Camera.main.transform.localPosition = new Vector3(0f, Camera.main.transform.localPosition.y, Camera.main.transform.localPosition.z) + new Vector3(0f, -1, 0.3f) * scrollWheelSpeed * Time.deltaTime;
        }
    }
    
}