using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasDebugConsoleHandler : MonoBehaviour
{
    private Coroutine _findOVRCoroutine;
    void Start()
    {
        _findOVRCoroutine = StartCoroutine(nameof(FindOvrCam));
    }

    private IEnumerator FindOvrCam()
    {
        var OvrCam = FindObjectOfType<OVRCameraRig>();
        
        // found OvrCam in scene
        if (OvrCam != null)
        {
            Debug.Log("Found OVRCamera");
            // left, center, right cameras
            var camArr = OvrCam.transform.GetComponentsInChildren<Camera>();
            foreach (var cam in camArr)
            {
                Debug.Log("foreach");
                if (cam.name == "CenterEyeAnchor")
                {
                    Debug.Log("Found CenterEyeAnchor");
                    this.transform.parent = cam.transform;

                    var rectTransform = this.GetComponent<RectTransform>();
                    rectTransform.localPosition = new Vector3(0f, 0f, 1.05f);
                    rectTransform.localScale = new Vector3(0.001f, 0.001f, 0.001f);

                    StopCoroutine(_findOVRCoroutine);
                    yield break;
                }
            }
        }

        yield return new WaitForSeconds(0.1f);
        StartCoroutine(nameof(FindOvrCam));

        yield break;
    }
}
