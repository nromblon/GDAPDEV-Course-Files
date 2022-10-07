using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime : MonoBehaviour, ITappable, ISwipeable
{
    public float speed = 10f;
    private Vector3 targetPos = Vector3.zero;

    private void OnEnable()
    {
        targetPos = transform.position;
    }

    private void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, targetPos, speed * Time.deltaTime);
    }

    public void OnTap()
    {
        Destroy(gameObject);
    }

    public void OnSwipe(SwipeEventArgs e)
    {
        Vector3 dir = Vector3.Normalize(e.SwipeVector);

        targetPos += dir * 5;
    }
}
