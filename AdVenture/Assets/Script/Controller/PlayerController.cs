using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private PlayerAction playerActionControl;

    private void Awake()
    {
        playerActionControl = new PlayerAction();
        InitEvent();
    }

    private void OnEnable()
    {
        playerActionControl.Enable();
    }

    private void OnDisable()
    {
        playerActionControl.Disable();
    }


    private void InitEvent()
    {
        playerActionControl.Player.Walk.performed += ctx => Moov(ctx.ReadValue<Vector2>());
    }

    private void Moov(Vector2 parametre)
    {
        Vector3 positionMoov = new Vector3(0,0,0);
        switch(parametre.x)
        {
            case > 0 :
                positionMoov.x += 1;
                break;
            case < 0: 
                positionMoov.x -= 1;
                break;
        }
        switch (parametre.y)
        {
            case > 0 :
                positionMoov.y += 1;
                break;
            case < 0:
                positionMoov.y -=1 ;
                break;
        }

        gameObject.transform.position += new Vector3(parametre.x,parametre.y,0);
    }
}
