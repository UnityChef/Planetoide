using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace EcoMundi.Managers
{
    public class CameraManager : MonoBehaviour
    {
        [Header("Configs")]
        [Range(-10f, -20f)]
        public float cameraDistance;
        private Vector3 _cameraOffset;
        private float _cameraZoomValue;

        [Range(0f,359f)]
        public float swipeOrbitAngle;

        [Header("Components")]
        [SerializeField]
        private Camera _myCamera;
        [SerializeField]
        private Transform _pivotTransform;

        // Touches
        private Touch touchZero;

        // Position
        private Vector2 _previousPosition;
        private Vector2 _orbitDirection;

        private void Awake()
        {
            GameManager.OnFakeUpdate += OnUpdate;
        }

        private void OnDestroy()
        {
            GameManager.OnFakeUpdate -= OnUpdate;
        }
        private void Start()
        {
            _cameraZoomValue = cameraDistance;
            _myCamera.transform.localPosition = new Vector3(0, 0, cameraDistance);
        }

        private void OnUpdate()
        {
            if (EventSystem.current.currentSelectedGameObject == null)
            {
                if (Input.touchCount == 1)
                {
                    touchZero = Input.GetTouch(0);

                    if (touchZero.phase == TouchPhase.Began)
                    {
                        _previousPosition = touchZero.position;
                    }
                    else if (touchZero.phase == TouchPhase.Moved)
                    {
                        _orbitDirection = (_previousPosition - touchZero.position).normalized;
                        
                        _pivotTransform.Rotate(Vector3.right, _orbitDirection.y * swipeOrbitAngle);
                        _pivotTransform.Rotate(Vector3.up, -_orbitDirection.x * swipeOrbitAngle);

                        _previousPosition = touchZero.position;
                    }
                }
            }
        }

        public void ModifyZoom(float p_zoomValue)
        {
            _cameraZoomValue += p_zoomValue;
            _cameraZoomValue = Mathf.Clamp(_cameraZoomValue, -20f, -8f);

            _myCamera.transform.localPosition = new Vector3(0, 0, _cameraZoomValue);
        }
    }
}

