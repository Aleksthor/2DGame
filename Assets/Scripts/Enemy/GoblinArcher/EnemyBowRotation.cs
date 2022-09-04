using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBowRotation : MonoBehaviour
{
    [Header("Sprite and Transform References")]
    [SerializeField] Transform Player;
    [SerializeField] Transform Pivot;
    [SerializeField] Transform Hand;
    [SerializeField] Transform Bow;

    [SerializeField] SpriteRenderer Body;
    [SerializeField] SpriteRenderer Head;
    [SerializeField] SpriteRenderer Hat;
    [SerializeField] SpriteRenderer FacialHair;
    [SerializeField] SpriteRenderer HandRenderer;
    [SerializeField] SpriteRenderer BowRenderer;

    GameObject Parent;

    [Header("Private Variables")]
    [SerializeField]
    public bool hasAgro = false;
    [SerializeField]
    private Vector2 Frame1;
    [SerializeField]
    private Vector2 Frame2;
    [SerializeField]
    private Vector2 direction;
    [SerializeField]
    private bool flipState = false;
    [SerializeField]
    private bool flipLastDirection = false;

    private GameObject player;

    void Start()
    {
        Parent = transform.parent.gameObject;
        Frame1 = Parent.transform.position;

        if (Player == null)
        {
            player = GameObject.Find("Player");
            Player = player.transform;
        }
    }
    




    // Update is called once per frame
    void LateUpdate()
    {
        if (flipState)
        {
            
            Frame1 = Parent.transform.position;
            direction = Frame1 - Frame2;
            flipState = false;
        }
        else
        {
            Frame2 = Parent.transform.position;
            direction = Frame2 - Frame1;
            flipState = true;
        }


        if (hasAgro)
        {
            Bow.transform.localPosition = new Vector2(0.02f, 0f);
            Hand.transform.localPosition = new Vector2(-0.13f, -0.06f);
            HandRenderer.flipX = false;
            BowRenderer.flipX = false;
            Vector2 PlayerPosition = Player.transform.position;
            Vector2 PivotPosition = new Vector2(Pivot.transform.position.x, Pivot.transform.position.y - 0.3f);

            Vector2 Direction = PlayerPosition - PivotPosition;
            transform.right = Direction * -1f;
            transform.position = PivotPosition + Direction.normalized / 1.5f;

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
        else
        {

            if (direction.x > 0f || flipLastDirection)
            {
                Body.flipX = true;
                Head.flipX = true;
                Hat.flipX = true;
                FacialHair.flipX = true;
                Hand.transform.localPosition = new Vector2(Hand.transform.localPosition.x * -1f, Hand.transform.localPosition.y);
                Bow.transform.localPosition = new Vector2(-0.02f, 0f);
                HandRenderer.flipX = true;
                BowRenderer.flipX = true;

                flipLastDirection = true;
            }
            if (direction.x < 0f || !flipLastDirection)
            {
                Bow.transform.localPosition = new Vector2(0.02f, 0f);
                Body.flipX = false;
                Head.flipX = false;
                FacialHair.flipX = false;
                Hat.flipX = false;
                HandRenderer.flipX = false;
                BowRenderer.flipX = false;

                flipLastDirection = false;
            }

        }

        







    }


}
