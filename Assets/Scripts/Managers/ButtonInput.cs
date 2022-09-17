using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonInput : SingletonMonoBehaviour<ButtonInput>
{

    LocalPlayerScript localPlayerScript;
    AnimationManager playerAnimator;

    [Header("Private Variables")]
    [SerializeField]
    Vector2 movement;
    [SerializeField]
    bool dash;
    [SerializeField]
    bool sneak;
    [SerializeField]
    bool shield;
    [SerializeField]
    bool attack;


    //--------------------

    private void Start()
    {
        localPlayerScript = PlayerSingleton.instance.gameObject.GetComponent<LocalPlayerScript>();
        playerAnimator = AnimationManager.Instance;
    }
    private void Update()
    {
        ButtonInputs();
    }


    //--------------------


    void ButtonInputs()
    {
        movement.x = Input.GetAxis("Horizontal");
        movement.y = Input.GetAxis("Vertical");
        playerAnimator.SetWalkingSpeedAnimation(movement);
        dash = Input.GetButton("Jump");
        sneak = Input.GetButton("Running");
        shield = Input.GetButton("Fire2");
        attack = Input.GetButton("Fire1");



        if (Input.GetKeyDown("9"))
        {
            SceneManager.LoadScene("TestMap - Aleksander");
            GameEvents.current.CharacterCreationOver();
        }
        if (Input.GetKeyDown("8"))
        {
            SceneManager.LoadScene("TestScene");
            GameEvents.current.CharacterCreationOver();
        }
    }


    //--------------------


    public float GetMovementX()
    {
        return movement.x;
    }
    public float GetMovementY()
    {
        return movement.y;
    }
    public bool GetDashInput()
    {
        return dash;
    }
    public bool GetSneakInput()
    {
        return sneak;
    }
    public bool GetShieldInput()
    {
        return shield;
    }
    public bool GetAttackInput()
    {
        return attack;
    }

}
