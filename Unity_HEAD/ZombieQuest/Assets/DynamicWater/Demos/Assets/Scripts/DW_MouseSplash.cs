using UnityEngine;
using LostPolygon.DynamicWaterSystem;

/// <summary>
/// Automates drawing ripples with mouse or touch.
/// </summary>
public class DW_MouseSplash : MonoBehaviour {
    public DynamicWater Water = null;
    public float SplashForce = 10f;
    public float SplashRadius = 0.25f;
    public bool SplashOnlyWhenMoving = false;
    public bool SplashAlways = false;
    public Camera Camera;

    private Vector3 _prevPoint;
    private RaycastHit _hitInfo;

    // Updating the splash generation
    private void FixedUpdate() {
        if (Water == null)
            return;
        
        if (Camera == null) {
            try {
                Camera = Camera.main ?? GetComponent<Camera>();
            } catch {
            }

            if (Camera == null) {
                Debug.LogError("No Camera attached and no active Camera was found, please set the Camera property for DW_MouseSplash to work", this);
                gameObject.SetActive(false);
                
                return;
            }
        }

        // Creating a ray from camera to world
        Ray ray;
        if (DW_GUILayout.IsRuntimePlatformMobile()) {
            if (Input.touchCount == 0)
                return;

            ray = Camera.ScreenPointToRay(Input.touches[0].position);
        } else {
            ray = Camera.ScreenPointToRay(Input.mousePosition);
        }

        // Checking for collision
        Physics.Raycast(ray, out _hitInfo, Mathf.Infinity, 1 << LayerMask.NameToLayer(DynamicWater.PlaneColliderLayerName));

        // Creating a splash line between previous position and current
        Vector3 currentPoint = _hitInfo.transform != null ? _hitInfo.point : Vector3.zero;
        bool doSplash = 
            GUIUtility.hotControl == 0 &&
            (SplashOnlyWhenMoving || (!SplashOnlyWhenMoving && _prevPoint != currentPoint)) &&
            (SplashAlways || Input.GetMouseButton(0) || Input.touchCount > 0);
        if (doSplash) {
            if (_hitInfo.transform != null && Water != null && _prevPoint != Vector3.zero) {
                Water.GetWaterLevel(_hitInfo.point.x, _hitInfo.point.y, _hitInfo.point.z);
                Water.CreateSplash(_prevPoint, _hitInfo.point, SplashRadius, -SplashForce * Time.deltaTime);
            }
        }

        _prevPoint = currentPoint;
    }
}