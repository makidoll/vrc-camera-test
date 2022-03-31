using System;
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

[UdonBehaviourSyncMode(BehaviourSyncMode.None)]
public class Amiibo : UdonSharpBehaviour
{
    // public Transform playerCameraOffset;
    // public Camera playerCamera;
    
    public Transform doppleCameraOffset;
    public Camera doppleCamera;
    
    // try un-parenting this some time
    public Transform playerScale;

    public Transform amiibo;
    
    private void Start()
    {
        // playerCamera.gameObject.SetActive(true);
        // playerCamera.enabled = true;
        doppleCamera.gameObject.SetActive(true);
        doppleCamera.enabled = true;
    }

    private void LateUpdate()
    {
        var playerHead = Networking.LocalPlayer.GetTrackingData(VRCPlayerApi.TrackingDataType.Head);
        
        var scaleFactor = 0.35f;

        var playerPosition = Networking.LocalPlayer.GetPosition();
        var playerRotation = Networking.LocalPlayer.GetRotation();
        
        var localRotation = Quaternion.Inverse(amiibo.rotation * Quaternion.Inverse(playerRotation));
        var doppleCamRotation = localRotation * playerHead.rotation;
        
        var cameraToAmiiboPos = amiibo.position - playerHead.position;
        var doppleCamPosition = playerPosition - ((localRotation * cameraToAmiiboPos) * (1.0f / scaleFactor));
        
        // trying to set the values fighting with unity and vrchat
        // playerCameraOffset.rotation = playerHead.rotation * Quaternion.Inverse(playerCamera.transform.localRotation);
        // playerCameraOffset.position = playerHead.position - playerCameraOffset.rotation * playerCamera.transform.localPosition;
        doppleCameraOffset.rotation = doppleCamRotation * Quaternion.Inverse(doppleCamera.transform.localRotation);
        doppleCameraOffset.position = doppleCamPosition - doppleCameraOffset.rotation * doppleCamera.transform.localPosition;
        
        // fixing ipd issue
        var ipdScaleFix = playerScale.localScale;
        ipdScaleFix.x = 1f / ipdScaleFix.x;
        ipdScaleFix.y = 1f / ipdScaleFix.y;
        ipdScaleFix.z = 1f / ipdScaleFix.z;
        // playerCamera.transform.localScale = ipdScaleFix;
        doppleCamera.transform.localScale = ipdScaleFix / scaleFactor;
        // doppleCamera.nearClipPlane = playerCamera.nearClipPlane / scaleFactor;
        // doppleCamera.farClipPlane = playerCamera.farClipPlane / scaleFactor;
        
        var vrchatDefaultNearClip = 0.3f;
        var vrchatDefaultFarClip = 1000f;
        doppleCamera.nearClipPlane = vrchatDefaultNearClip / scaleFactor;
        doppleCamera.farClipPlane = vrchatDefaultFarClip / scaleFactor;
    }
}
