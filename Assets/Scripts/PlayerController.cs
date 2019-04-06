using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 10;
    public float dampening = 10;

    float targetRotation;

    void Update()
    {
        MoveForward();
        CheckForRotation();
    }

    void CheckForRotation()
    {
#if UNITY_EDITOR
        if (Input.GetKeyDown(KeyCode.LeftArrow))
            targetRotation -= 90;
        else if (Input.GetKeyDown(KeyCode.RightArrow))
            targetRotation += 90;
#else
        if (Input.GetMouseButtonDown(0))
            targetRotation += Input.mousePosition.x > Screen.width / 2 ? 90 : -90;
#endif
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, targetRotation, 0), .25f);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Constants.COLLECTIBLE_TAG))
        {
            GameManager.Instance.AddATail();
            Destroy(other.gameObject);
            GameManager.Instance.SpawnACollectible();
        }
        else if (other.CompareTag(Constants.FINISH_TAG))
            GameManager.Instance.RestartLevel();
    }

    void MoveForward()
    {
        transform.position += transform.forward * speed * Time.deltaTime;
        transform.position = Vector3.LerpUnclamped(transform.position, transform.position + transform.right,
            Mathf.Cos(Time.timeSinceLevelLoad * 100 * Mathf.Deg2Rad) / dampening);
    }
}
