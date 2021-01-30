using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class GestureController : MonoBehaviour
{
    [Header("Trigger")]
    public SteamVR_Action_Single trigger;
    public float triggerThreshold = 0.9f;
    public bool isDetecting = false;

    GameObject hmd;
    GameObject left_hand;
    GameObject right_hand;

    [Header("Hand Position")]
    public float distanceThreshold = 0.5f;
    float left_posY_start = 0;
    float right_posY_start = 0;
    //float left_posY_end = 0;
    //float right_posY_end = 0;
    float left_posY_highest = 0;
    float right_posY_highest = 0;

    bool fly_signal = false;
    bool land_signal = false;

    void Awake()
    {
        hmd = GameObject.Find("VRCamera");
        left_hand = GameObject.Find("LeftHand");
        right_hand = GameObject.Find("RightHand");
    }

    void Update()
    {
        fly_signal = false;
        land_signal = false;
        float leftTriggerValue = trigger.GetAxis(SteamVR_Input_Sources.LeftHand);
        float rightTriggerValue = trigger.GetAxis(SteamVR_Input_Sources.RightHand);

        if (!isDetecting)
        {
            if (leftTriggerValue > triggerThreshold && rightTriggerValue > triggerThreshold)
            {
                isDetecting = true;
                // Record where we start detect
                left_posY_start = left_hand.transform.localPosition.y;
                right_posY_start = right_hand.transform.localPosition.y;
                left_posY_highest = left_posY_start;
                right_posY_highest = right_posY_start;
            }
        }
        
        if (isDetecting)
        {
            if (leftTriggerValue < triggerThreshold || rightTriggerValue < triggerThreshold)
            {
                // Let go trigger, end detection, and land to ground
                isDetecting = false;
                land_signal = true;
            }
            else
            {
                // Update highest position
                if (left_hand.transform.localPosition.y > left_posY_highest)
                {
                    // update left highest position
                    left_posY_highest = left_hand.transform.localPosition.y;
                }
                if (right_hand.transform.localPosition.y > right_posY_highest)
                {
                    // update right highest position
                    right_posY_highest = right_hand.transform.localPosition.y;
                }

                if ((left_hand.transform.localPosition.y <= (left_posY_highest - distanceThreshold))
                 && (right_hand.transform.localPosition.y <= (right_posY_highest - distanceThreshold)))
                {
                    fly_signal = true;

                    // Reset highest positions
                    left_posY_highest = left_hand.transform.localPosition.y;
                    right_posY_highest = right_hand.transform.localPosition.y;
                }
            }
        }
    }

    public bool Input_Fly()
    {
        return (fly_signal || Input.GetKeyDown(KeyCode.Space));
    }

    public bool Input_land()
    {
        return (land_signal || Input.GetKeyDown(KeyCode.S));
    }

    /*
     
     
     
     
     
     */
}
