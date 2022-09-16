using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpriteManager : MonoBehaviour
{

    public List<SpriteRenderer> spriteComponents;


    public Transform Weapon;
    public Transform Effect;


    private Vector2 Frame1;
    private Vector2 Frame2;
    public Vector2 direction;
    private bool flipState = false;

    public bool flipLastDirection = false;
    private bool attacking = false;

    public Vector2 leftAttackLocalPosition = new Vector2(0f, 0f);
    private Vector2 rightAttackLocalPosition = new Vector2(-0f, 0f);

    public LocalEnemyScript localEnemyScript;
    private Vector2 playerPosition;





    // Start is called before the first frame update
    void Start()
    {
        Weapon = transform.Find("Weapon").GetComponent<Transform>();
        Effect = transform.Find("Effects").GetComponent<Transform>();

        Frame1 = transform.position;
        if(localEnemyScript == null)
        {
            localEnemyScript = gameObject.GetComponent<LocalEnemyScript>();
        }
       

        GameEvents.current.OnEnemyMeleeAttack += EnemyMeleeAttack;

    }

    // Update is called once per frame
    void LateUpdate()
    {

        if (flipState)
        {
            Frame1 = transform.position;
            direction = Frame1 - Frame2;
            flipState = false;
        }
        else
        {
            Frame2 = transform.position;
            direction = Frame2 - Frame1;
            flipState = true;
        }

        if (!attacking || localEnemyScript.hit)
        {
            if ((direction.x > 0f || flipLastDirection) && !localEnemyScript.hit)
            {

                
                foreach (SpriteRenderer sprite in spriteComponents)
                {
                    sprite.flipX = true;
                    
                }


                if (Weapon != null)
                {
                    Weapon.transform.localPosition = rightAttackLocalPosition;
                }
                if (Effect != null)
                {
                    Effect.transform.localPosition = new Vector2(Effect.transform.localPosition.x * -1f, Effect.transform.localPosition.y);
                }
                flipLastDirection = true;

            }
            if ((direction.x < 0f || !flipLastDirection) && !localEnemyScript.hit)
            {
                
                foreach (SpriteRenderer sprite in spriteComponents)
                {
                    sprite.flipX = false;
                }
                if (Weapon != null)
                {
                    Weapon.transform.localPosition = leftAttackLocalPosition;
                }
                flipLastDirection = false;

            }
            else
            {
                if (flipLastDirection)
                {
                    foreach (SpriteRenderer sprite in spriteComponents)
                    {
                        sprite.flipX = true;
                    }
                    if (Weapon != null)
                    {
                        Weapon.transform.localPosition = rightAttackLocalPosition;
                    }
                    if (Effect != null)
                    {
                        Effect.transform.localPosition = new Vector2(Effect.transform.localPosition.x * -1f, Effect.transform.localPosition.y);
                    }
                    flipLastDirection = true;
                }
                if (!flipLastDirection)
                {
                    foreach (SpriteRenderer sprite in spriteComponents)
                    {
                        sprite.flipX = false;
                    }
                    if (Weapon != null)
                    {
                        Weapon.transform.localPosition = leftAttackLocalPosition;
                    }
                    flipLastDirection = false;
                }
            }
        }

    }
    void EnemyMeleeAttack(Vector2 Position)
    {
        playerPosition = Position;
    }


    public void StartAttack()
    {
        GameEvents.current.EnemyStartAttack(gameObject);
        attacking = true;
    }
    public void StopAttack()
    {
        GameEvents.current.EnemyStopAttack(gameObject);
        attacking = false;
    }

    public void TurnOffAttack()
    {
        attacking = false;
    }



    public void Flip(bool input)
    {
        if (input)
        {
            foreach (SpriteRenderer sprite in spriteComponents)
            {
                sprite.flipX = true;
            }
            Weapon.transform.localPosition = rightAttackLocalPosition;

            flipLastDirection = true;


        }
        if (!input)
        {
            foreach (SpriteRenderer sprite in spriteComponents)
            {
                sprite.flipX = false;
            }
            Weapon.transform.localPosition = leftAttackLocalPosition;

            flipLastDirection = false;

        }
    }


    public void OnDestroy()
    {
        GameEvents.current.OnEnemyMeleeAttack -= EnemyMeleeAttack;
    }

}
