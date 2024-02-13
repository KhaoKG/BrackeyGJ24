using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class PlayerFollowOffset : MonoBehaviour
{
    Camera mainCamera;
    CinemachineVirtualCamera vCamera;
    CinemachineTransposer transposer;

    [SerializeField]
    Player player;

    float baseOffsetZ;
    [SerializeField]
    float cam2dOffset;
    [SerializeField]
    float offsetMoveSpeed = 2f;

    private void Start() {
        mainCamera = Camera.main;

        vCamera = GetComponent<CinemachineVirtualCamera>();
        transposer = vCamera.GetCinemachineComponent<CinemachineTransposer>();

        baseOffsetZ = transposer.m_FollowOffset.z;
    }

    private void Update() {
        Vector2 mousePos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        Vector2 offsetDirection = cam2dOffset * (mousePos - (Vector2)player.transform.position).normalized;

        //Debug.Log(offsetDirection.magnitude);

        transposer.m_FollowOffset = Vector3.MoveTowards(transposer.m_FollowOffset, 
            new Vector3(offsetDirection.x, offsetDirection.y, baseOffsetZ), offsetMoveSpeed * Time.deltaTime);
    }


}
