using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBowRotation : MonoBehaviour
{

    [SerializeField] Transform Player;
    [SerializeField] Transform Pivot;

    [SerializeField] SpriteRenderer Body;
    [SerializeField] SpriteRenderer Head;
    [SerializeField] SpriteRenderer Hat;
    [SerializeField] SpriteRenderer FacialHair;


    

    // Update is called once per frame
    void LateUpdate()
    {



        Vector2 PlayerPosition = Player.transform.position;
        Vector2 PivotPosition = new Vector2 (Pivot.transform.position.x, Pivot.transform.position.y - 0.3f) ;

        Vector2 Direction = PlayerPosition - PivotPosition;
        transform.right = Direction * -1f;
        transform.position = PivotPosition + Direction.normalized / 2f;

        if (Direction.x > 0f)
        {
            Body.flipX = true;
            Head.flipX = true;
            Hat.flipX = true;
            FacialHair.flipX = true;
        }
        else
        {
            Body.flipX = false;
            Head.flipX = false;
            FacialHair.flipX = false;
            Hat.flipX = false;
        }



    }


}
