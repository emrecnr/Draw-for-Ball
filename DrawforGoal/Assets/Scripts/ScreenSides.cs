using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenSides : MonoBehaviour
{
    [SerializeField] private BoxCollider2D leftCollider;
    [SerializeField] private BoxCollider2D rightCollider;
    [SerializeField] private BoxCollider2D bottomCollider;

    private void Start()
    {
        float screenWidth = GameManager.Instance.screenWidth;
        float screenHeight = GameManager.Instance.screenHeight;

        leftCollider.transform.position = new Vector3(-screenWidth - leftCollider.size.x / 2f, 0);
        rightCollider.transform.position = new Vector3(screenWidth + leftCollider.size.x / 2f, 0);
        bottomCollider.transform.position = new Vector3(0, -screenHeight - bottomCollider.size.y*2, 0);

        Destroy(this);
    }
}
