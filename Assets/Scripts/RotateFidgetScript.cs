using UnityEngine;
using UnityEngine.UI;
using GoogleMobileAds.Api;


public enum RotateDirection { CLOCKWISE, ANTICLOCKWISE, AMBIGUOUS };

public class RotateFidgetScript : MonoBehaviour
{
    public Text RPMText;
    public Text Spins;
    public float rotationSpeed;
    public float greaseValue;
    public GameObject playingCanvas;
    public GameObject gameResultCanvas;
    public Text gameScore;

    private bool shouldRotate;
    private float rotateDuration;
    private RotateDirection rotateDirection = RotateDirection.AMBIGUOUS;

    private float angleSum = 0.0f;
    private float currAngle = 0.0f;
    private float rotateAngle = 0.0f;
    private int spinCount;
    private float rpmValue;
    private int circumference;
    private bool isgamePlaying;

    // Use this for initialization
    void Start()
    {
        

        angleSum = 0.0f;
        Spins.text = "0";
        gameScore.text = "0";
        rpmValue = 0;
        circumference = 2 * 3 * 60; // 2*Pi*radius;
        rotateDuration = float.MaxValue;
        isgamePlaying = true;
        shouldRotate = false;
        rpmValue = int.MaxValue;
        //Debug.Log("Start");
    }

    // Update is called once per frame
    void Update()
    {
        
        Spins.text = spinCount.ToString();
        if (rpmValue == int.MaxValue)
        {
            RPMText.text = "0";
        }
        else
        {
            RPMText.text = ((int)rpmValue).ToString();
        }

        if (shouldRotate)
        {
            rotateAngle = rotationSpeed * rpmValue * Time.deltaTime ;

            //Debug.Log("Rotate Angle: " + rotateAngle);

            switch (rotateDirection)
            {

                case RotateDirection.CLOCKWISE:

                    transform.Rotate(Vector3.back, rotateAngle);

                    break;

                case RotateDirection.ANTICLOCKWISE:

                    transform.Rotate(Vector3.forward, rotateAngle);

                    break;

            }
            angleSum += rotateAngle;

            if (angleSum / 360 > 1)
            {

                spinCount++;
                angleSum -= 360;
            }

        }

        if (rpmValue <= 0)
        {
            shouldRotate = false;
            isgamePlaying = false;
            rotateDirection = RotateDirection.AMBIGUOUS;
        }
        if (rpmValue > 0 && shouldRotate)
        {
            //greaseValue += greaseValue / 2; 
            rpmValue -= (greaseValue * Time.deltaTime);
        }

        if (!isgamePlaying)
        {
            isgamePlaying = !isgamePlaying;
            shouldRotate = false;
            rotateDuration = float.MaxValue;
            rpmValue = int.MaxValue;
            SwipeScript.setMaxSwipe(1);
            
            // Update User Max Score........
            if (PlayerPrefs.HasKey("maxScore"))
            {
                int previousMaxScore = PlayerPrefs.GetInt("maxScore");

                if (previousMaxScore < spinCount)
                {
                    PlayerPrefs.SetInt("maxScore", spinCount);
                }
            }
            else
            {
                PlayerPrefs.SetInt("maxScore", spinCount);
            }

            gameScore.text = spinCount.ToString();
            spinCount = 0;
            angleSum = 0.0f;
            playingCanvas.SetActive(false);   // Hide Playing Canvas When Fidget Stop Spins........
            gameResultCanvas.SetActive(true); // Show gameResult Canvas ...........
        }
    }

    // Call On user Left or Top Swipe.........
    public void RotateAntiClockWise(float time)
    {
        //Debug.Log("T1: "+time);
        shouldRotate = true;
        rotateDuration = time;
        rotateDirection = RotateDirection.ANTICLOCKWISE;
        rpmValue = (rotateDuration) / circumference;
        rpmValue *= 60;  // convert into minutes........
        //transform.Rotate(Vector3.forward * rotationSpeed , 120);
    }

    // Call On user Right or Down Swipe.......
    public void RotateClockWise(float time)
    {
        shouldRotate = true;
        rotateDuration = time;
        rotateDirection = RotateDirection.CLOCKWISE;
        rpmValue = (rotateDuration) / circumference;
        rpmValue *= 60;  // convert into minutes........
        //transform.Rotate(0 ,0 , -rotationSpeed);
    }
}
