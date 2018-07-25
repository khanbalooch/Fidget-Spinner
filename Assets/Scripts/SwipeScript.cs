using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class SwipeScript : MonoBehaviour
{
    public float maxTime;  // set maximum user swipe time.......
    public float minSwipeDistance; // set minimum user swipe Distance......
    public int maxSwipe;  // Set maximum user swipes..........

    public GameObject fidgetSpinner;  // set fidget refference........ 
    public GameObject swipePanel;
    private static int maxUserSwipe;

    float startTime;  // store start time when user press on screen........
    float endTime;    // store end time when user leave from screen........ 
    float speed;
    Vector3 startPos;  // store starting position of user touch......
    Vector3 endPos;    // store ending position of user touch........

    float swipeDistance;  // store user calculated swipe distance.......
    float swipeTime;      // store user swipe time.........

    bool isValidSwipe, isValidMove;
    Vector2 moveDirection;
    float speedMove;
    // Use this for initialization
    void Start()
    {
        maxUserSwipe = maxSwipe;
        isValidSwipe = isValidMove = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount > 0)
        {

            Touch touch = Input.GetTouch(0);   // Getting First user touch......

            if (swipePanel.name.Equals("SwipePanel"))
            {
                isValidSwipe = checkIfClickedOutside(swipePanel, touch.position);
            }

            /*if (touch.phase == TouchPhase.Moved)
            {
                //Debug.Log("TP: " + touch.position);
                isValidMove = checkIfClickedOutside(swipePanel, touch.position);

                if (isValidMove)
                {
                    moveDirection = touch.deltaPosition.normalized;   // Get Unit Vector to check Move Direction.........
                    //Debug.Log("Dir: " + direction);
                    speedMove = touch.deltaPosition.magnitude / touch.deltaTime;

                    if (moveDirection.x < 0)
                    {
                        //Debug.Log("Left Move: " + speedMove);
                        fidgetSpinner.GetComponent<Transform>().Rotate(Vector3.forward, Time.deltaTime * speedMove * 0.05f);
                    }
                    else if (moveDirection.x > 0)
                    {
                        //fidgetSpinner.GetComponent<RotateFidgetScript>().RotateClockWise(speedMove);
                        fidgetSpinner.GetComponent<Transform>().Rotate(Vector3.back, Time.deltaTime * speedMove * 0.5f);
                        //Debug.Log("Right Move: " + speedMove);
                    }
                }
            }*/

            if (touch.phase == TouchPhase.Began)
            {
                startTime = Time.time;
                startPos = touch.position;
                isValidMove = false;
            }
            else if (touch.phase == TouchPhase.Ended)
            {
                endTime = Time.time;
                endPos = touch.position;

                swipeDistance = (endPos - startPos).magnitude;    // Means SQRT(X^2 + Y^2).....
                swipeTime = endTime - startTime;
                //speed = touch.deltaPosition.magnitude / touch.deltaTime;
                if (swipeTime < maxTime && swipeDistance > minSwipeDistance && maxUserSwipe > 0 && isValidSwipe)
                {
                    maxUserSwipe--;
             //       Debug.Log("MaxSwipe: " + maxUserSwipe);
                    user_Swipe();
                    
                }

                /*if (isValidMove)
                {
                    if (moveDirection.x > 0)
                    {
                        fidgetSpinner.GetComponent<RotateFidgetScript>().RotateClockWise(speedMove);
                    }
                    else if (moveDirection.x < 0)
                    {
                        fidgetSpinner.GetComponent<RotateFidgetScript>().RotateAntiClockWise(speedMove);
                    }
                }*/
            }
        }
    }

    void user_Swipe()
    {
        Vector2 distance = endPos - startPos;
        speed = distance.magnitude / swipeTime;
        //Debug.Log("Speed1: " + speed);
        if (Mathf.Abs(distance.x) > Mathf.Abs(distance.y))
        {
            //Debug.Log("Horizontal Swipe: " + speed);

            if (distance.x > 0)
            {
               // Debug.Log("Right Swipe speed: " + speed);

                fidgetSpinner.GetComponent<RotateFidgetScript>().RotateAntiClockWise(speed);
            }
            else if (distance.x < 0)
            {
               // Debug.Log("Left Swipe speed" + speed);
                fidgetSpinner.GetComponent<RotateFidgetScript>().RotateClockWise(speed);
            }
        }
        /*else if (Mathf.Abs(distance.x) < Mathf.Abs(distance.y))
        {
            Debug.Log("Vertical Swipe");

            if (distance.y > 0)
            {
                Debug.Log("Top Swipe");
                fidgetSpinner.GetComponent<RotateFidgetScript>().RotateAntiClockWise(speed);
            }
            else if (distance.y < 0)
            {
                Debug.Log("Down Swipe");
                fidgetSpinner.GetComponent<RotateFidgetScript>().RotateClockWise(speed);
            }
        }*/
    }

    private bool checkIfClickedOutside(GameObject panel, Vector2 touchPosition)
    {
        if (RectTransformUtility.RectangleContainsScreenPoint(
                panel.GetComponent<RectTransform>(),
                touchPosition,
                Camera.main))
        {
            //panel.SetActive(false);
            //Debug.Log("Valid Swipe");
            return true;
        }
        ///Debug.Log("Invalid Swipe");
        return false;
    }

    public static void setMaxSwipe(int value)
    {
        maxUserSwipe = value;
    }
}
