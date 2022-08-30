using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnArrow : MonoBehaviour
{
    [SerializeField] float ShotForce = 4f;
    [SerializeField] Transform ShotPoint;
    [SerializeField] GameObject Arrow;

    public void Shoot()
    {
        GameObject NewArrow = Instantiate(Arrow, ShotPoint.position, ShotPoint.rotation);
        NewArrow.GetComponent<Rigidbody2D>().velocity = ShotPoint.transform.right * -1f * ShotForce;
    }
}
