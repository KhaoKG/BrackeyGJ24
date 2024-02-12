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

    private void Start() {
        mainCamera = Camera.main;

        vCamera = GetComponent<CinemachineVirtualCamera>();
        transposer = vCamera.GetCinemachineComponent<CinemachineTransposer>();

        baseOffsetZ = transposer.m_FollowOffset.z;
    }

    private void Update() {
        Vector3 mousePos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        Vector2 offsetDirection = cam2dOffset * (mousePos - player.transform.position).normalized;

        transposer.m_FollowOffset = new Vector3(offsetDirection.x, offsetDirection.y, baseOffsetZ);
    }


}
