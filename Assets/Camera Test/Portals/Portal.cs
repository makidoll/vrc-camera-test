using System;
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

[UdonBehaviourSyncMode(BehaviourSyncMode.None)]
public class Portal : UdonSharpBehaviour
{
    public Transform secondPortalCameraOffset;
    public Camera secondPortalCamera;
    
    // try un-parenting this some time
    public Transform playerScale;

    public Transform firstPortalQuad;
    public Transform secondPortalQuad;
    
    private void Start()
    {
        secondPortalCamera.gameObject.SetActive(true);
        secondPortalCamera.enabled = true;
    }

    private void LateUpdate()
    {
        var playerHead = Networking.LocalPlayer.GetTrackingData(VRCPlayerApi.TrackingDataType.Head);
        
        // var playerPosition = Networking.LocalPlayer.GetPosition();
        // var playerRotation = Networking.LocalPlayer.GetRotation();
        



        // var secondPortalCameraRotation = localRotation * playerHead.rotation;
        
        var cameraToFirstPortalPositionOffset = firstPortalQuad.position - playerHead.position;
        var secondPortalCameraPosition = secondPortalQuad.position - cameraToFirstPortalPositionOffset;
        
        
        var secondPortalCameraRotation = playerHead.rotation;


        // trying to set the values fighting with unity and vrchat
        secondPortalCameraOffset.rotation = secondPortalCameraRotation * Quaternion.Inverse(secondPortalCamera.transform.localRotation);
        secondPortalCameraOffset.position = secondPortalCameraPosition - secondPortalCameraOffset.rotation * secondPortalCamera.transform.localPosition;
        
        // fixing ipd issue
        var ipdScaleFix = playerScale.localScale;
        ipdScaleFix.x = 1f / ipdScaleFix.x;
        ipdScaleFix.y = 1f / ipdScaleFix.y;
        ipdScaleFix.z = 1f / ipdScaleFix.z;
        secondPortalCamera.transform.localScale = ipdScaleFix;
        
        var vrchatDefaultNearClip = 0.3f;
        var vrchatDefaultFarClip = 1000f;
        secondPortalCamera.nearClipPlane = vrchatDefaultNearClip;
        secondPortalCamera.farClipPlane = vrchatDefaultFarClip;
    }
}
