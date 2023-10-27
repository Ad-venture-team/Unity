using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public enum CharacterState
    {
        FREE,
        UI,
    }

    public CharacterState characterState;


    [SerializeField] private PlayerAction playerActionControl;
    [SerializeField] private float speed;
    private Vector2 moovInput = new Vector2(0,0);

    private void Awake()
    {
        playerActionControl = new PlayerAction();
        InitEvent();
    }

    private void Update()
    {
        Moov(moovInput);
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
        playerActionControl.Player.Walk.performed += ctx => moovInput = ctx.ReadValue<Vector2>();
        playerActionControl.Player.Walk.canceled += ctx => moovInput = Vector2.zero;
    }

    private void Moov(Vector3 parametre)
    {
        float plusX = 0;
        float plusY = 0;

        switch(parametre.x)
        {
            case > 0 :
                plusX = 1*speed;
                break;
            case < 0: 
                plusX = -1 * speed;
                break;
        }
        switch (parametre.y)
        {
            case > 0 :
                plusY = 1 * speed;
                break;
            case < 0:
                plusY =-1 * speed;
                break;
        }

        transform.position += parametre.normalized * speed * Time.deltaTime;
    }
}
