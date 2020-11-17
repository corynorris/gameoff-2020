using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    [SerializeField] GameObject bounds;
    private List<Transform> boundList;
    private List<Transform> sortedBoundList;
    private Camera camera;

    private float height;
    private float width;
    private float moveSpeed;
    private float zoomSpeed;

    private bool movingUp;
    private bool movingRight;
    private bool movingBottom;
    private bool movingLeft;

    private bool horizontalMax;
    private bool verticalMax;
    private float minCameraSize;

    private bool zoomOutFull;
    private bool zoomInFull;

    private enum Direction {Top, Right, Bottom, Left }

    // Start is called before the first frame update
    void Start()
    {
        camera = GetComponent<Camera>();
        height = Camera.main.orthographicSize * 2.0f;
        width = height * Screen.width / Screen.height;
        Debug.Log(width + " " + height);
        moveSpeed = (Camera.main.orthographicSize) / 55;
        zoomSpeed = 0.5f;
        movingUp = false;
        movingRight = false;
        movingBottom = false;
        movingLeft = false;
        horizontalMax = false;
        verticalMax = false;
        minCameraSize = 5f;

        zoomOutFull = false;
        zoomInFull = false;
        sortBounds();       
        
        foreach(Transform bound in boundList)
        {
            Debug.Log(bound.position);
        }
    }

    public void moveCamera(float xpos, float ypos)
    {
        transform.position = new Vector3(transform.position.x + xpos, transform.position.y + ypos, transform.position.z);
    }

    public void zoomCamera(bool zoomIn)
    {
        if (zoomIn)
        {
            if (camera.orthographicSize > minCameraSize)
            {
                camera.orthographicSize = camera.orthographicSize - zoomSpeed;
                height = Camera.main.orthographicSize * 2.0f;
                width = height * Screen.width / Screen.height;
                horizontalMax = false;
                verticalMax = false;
                moveSpeed = (Camera.main.orthographicSize) / 55;
            }
            else
            {
                zoomInFull = false;
            }
        }
        else
        {
            if (!horizontalMax && !verticalMax)
            {
                camera.orthographicSize = camera.orthographicSize + zoomSpeed;
                height = Camera.main.orthographicSize * 2.0f;
                width = height * Screen.width / Screen.height;
                moveSpeed = Camera.main.orthographicSize / 55;
            }
            else
            {
                zoomOutFull = false;
            }
        }
    }

    public void zoomOutMax()
    {
        this.zoomOutFull = true;
    }

    public void zoomInMax()
    {
        this.zoomInFull = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (zoomOutFull)
        {
            zoomCamera(false);

        } else if (zoomInFull) {
            zoomCamera(true);
        }
        else {
            if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
            {
                movingUp = true;
            }
            if (Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.UpArrow))
            {
                movingUp = false;
            }
            if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
            {
                movingRight = true;
            }
            if (Input.GetKeyUp(KeyCode.D) || Input.GetKeyUp(KeyCode.RightArrow))
            {
                movingRight = false;
            }
            if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
            {
                movingBottom = true;
            }
            if (Input.GetKeyUp(KeyCode.S) || Input.GetKeyUp(KeyCode.DownArrow))
            {
                movingBottom = false;
            }
            if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
            {
                movingLeft = true;
            }
            if (Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.LeftArrow))
            {
                movingLeft = false;
            }


            if (Input.mouseScrollDelta.y > 0f) // forward
            {
                zoomCamera(false);
            }
            else if (Input.mouseScrollDelta.y < 0f) // backwards
            {
                zoomCamera(true);
            }

            if (Input.GetKeyDown(KeyCode.Equals)) 
            {
                zoomInFull = true;
            }
            else if (Input.GetKeyDown(KeyCode.Minus))
            {
                zoomOutFull = true;
            }
        }
    }

    void FixedUpdate()
    {

        if (movingUp && !verticalMax)
        {
            moveCamera(0, moveSpeed);
        }else if (movingBottom && !verticalMax)
        {
            moveCamera(0, -moveSpeed);
        }
        if (movingRight && !horizontalMax)
        {
            moveCamera(moveSpeed,0);
        }
        else if (movingLeft && !horizontalMax)
        {
            moveCamera(-moveSpeed, 0);
        }
    }

    void LateUpdate()
    {        
        transform.position = new Vector3(boundXaxis(transform.position.x), boundYaxis(transform.position.y), transform.position.z);
    }

    private void sortBounds()
    {
        boundList = new List<Transform>(bounds.GetComponentsInChildren<Transform>());
        boundList.RemoveAt(0);
        sortedBoundList = new List<Transform>();
        Transform topBound = boundList[0];
        foreach (Transform bound in boundList)
        {
            if (bound.position.y > topBound.position.y)
            {
                topBound = bound;
            }
        }
        sortedBoundList.Add(topBound);

        Transform rightBound = boundList[0];
        foreach (Transform bound in boundList)
        {
            if (bound.position.x > rightBound.position.x)
            {
                rightBound = bound;
            }
        }
        sortedBoundList.Add(rightBound);

        Transform bottomBound = boundList[0];
        foreach (Transform bound in boundList)
        {
            if (bound.position.y < bottomBound.position.y)
            {
                bottomBound = bound;
            }
        }
        sortedBoundList.Add(bottomBound);

        Transform leftBound = boundList[0];
        foreach (Transform bound in boundList)
        {
            if (bound.position.x < leftBound.position.x)
            {
                leftBound = bound;
            }
        }
        sortedBoundList.Add(leftBound);
    }

    private float boundXaxis(float xpos)
    {
        if (transform.position.x - width / 2 < sortedBoundList[(int)Direction.Left].transform.position.x && !horizontalMax)
        {
            if (transform.position.x + width / 2 > sortedBoundList[(int)Direction.Right].transform.position.x)
            {
                xpos = sortedBoundList[(int)Direction.Left].transform.position.x + ((sortedBoundList[(int)Direction.Right].transform.position.x - sortedBoundList[(int)Direction.Left].transform.position.x) / 2);
                horizontalMax = true;
            }
            else
            {

                xpos = sortedBoundList[(int)Direction.Left].transform.position.x + width / 2;
                if (xpos + width / 2 > sortedBoundList[(int)Direction.Right].transform.position.x)
                {
                    horizontalMax = true;
                }
            }
        }
        else if (transform.position.x + width / 2 > sortedBoundList[(int)Direction.Right].transform.position.x && !horizontalMax)
        {
            xpos = sortedBoundList[(int)Direction.Right].transform.position.x - width / 2;
        }
        return xpos;
    }

    private float boundYaxis(float ypos)
    {
        if (transform.position.y - height / 2 < sortedBoundList[(int)Direction.Bottom].transform.position.y && !verticalMax)
        {
            if (transform.position.y + height / 2 > sortedBoundList[(int)Direction.Top].transform.position.y)
            {
                ypos = sortedBoundList[(int)Direction.Bottom].transform.position.y + ((sortedBoundList[(int)Direction.Top].transform.position.y - sortedBoundList[(int)Direction.Bottom].transform.position.y) / 2);
                verticalMax = true;
            }
            else
            {
                ypos = sortedBoundList[(int)Direction.Bottom].transform.position.y + height / 2;
                if (ypos + height / 2 > sortedBoundList[(int)Direction.Top].transform.position.y)
                {
                    verticalMax = true;
                }
            }
        }
        else if (transform.position.y + height / 2 > sortedBoundList[(int)Direction.Top].transform.position.y && !verticalMax)
        {
            ypos = sortedBoundList[(int)Direction.Top].transform.position.y - height / 2;
        }
        return ypos;
    }
}
