using System.Collections;
using UnityEngine;

public class Foot : MonoBehaviour
{
    [SerializeField] Player Player;    
    private float verticalSpeedMod;
    private float nullVerticalSpeedMod;
    private float nullForceOfDown;
    private float maxForceOfDown;

    private void OnTriggerEnter(Collider other)
    {
        if(!other.CompareTag("Player"))
        {
            Player.modSpeed = verticalSpeedMod;
            Player.canJump = true;
            Player.forceOfDown = nullForceOfDown;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Player"))
        {
            Player.modSpeed = nullVerticalSpeedMod;
            Player.canJump = false;
            Player.forceOfDown = maxForceOfDown;
        }
    }

    void Start()
    {        
        verticalSpeedMod = Player.verticalSpeedMod;
        nullVerticalSpeedMod = Player.nullVerticalSpeedMod;
        nullForceOfDown = Player.nullForceOfDown;
        maxForceOfDown = Player.maxForceOfDown;
    }    
}
