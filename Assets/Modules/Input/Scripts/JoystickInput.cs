using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace InputModule
{
    public sealed class JoystickInput : MonoBehaviour
    {
        private const int MOUSE_BUTTON = 0;

        private const float MIN_MAGNITUDE = 0.05f;

        public event Action<Vector2> OnPositionStarted;
        
        public event Action<Vector2> OnPositionMoved;

        public event Action<Vector2> OnDirectionMoved;

        public event Action<Vector2> OnPositionEnded;
        
        public event Action OnCanceled;

        /// <summary>
        ///     <para>State.</para>
        /// </summary>
        private bool isHoldStarted;

        private bool isMoveStarted;

        private Vector2 centerScreenPosition;

        private EventSystem eventSystem;

        private void Awake()
        {
            this.eventSystem = EventSystem.current;
        }

        private void Update()
        {
#if UNITY_EDITOR
            this.UpdateMouse();
#else
            this.UpdateTouch();
#endif
        }


#if UNITY_EDITOR
        private void UpdateMouse()
        {
            if (Input.GetMouseButtonDown(MOUSE_BUTTON) && !this.IsPointerOverGameObject())
            {
                this.StartInput(Input.mousePosition);
            }
            else if (this.isHoldStarted && Input.GetMouseButton(MOUSE_BUTTON))
            {
                this.ProcessMove(Input.mousePosition);
            }
            else if (this.isHoldStarted && Input.GetMouseButtonUp(MOUSE_BUTTON))
            {
                this.EndInput(Input.mousePosition);
            }
        }
        
        private bool IsPointerOverGameObject()
        {
            if (ReferenceEquals(this.eventSystem, null))
            {
                return false;
            }

            return this.eventSystem.IsPointerOverGameObject();
        }
#else
        private void UpdateTouch()
        {
            var touchCount = Input.touchCount;
            if (touchCount < 1)
            {
                return;
            }

            var touch = Input.GetTouch(0);
            var touchPhase = touch.phase;
            if (touchPhase == TouchPhase.Began && !this.IsPointerOverGameObject(touch.fingerId))
            {
                this.StartInput(touch.position);
            }
            else if (this.isHoldStarted)
            {
                this.ProcessMove(touch.position);
            }
            else if (this.isHoldStarted && (touchPhase == TouchPhase.Canceled || touchPhase == TouchPhase.Ended))
            {
                this.EndInput(touch.position);
            }
        }
        
        private bool IsPointerOverGameObject(int fingerId)
        {
            if (ReferenceEquals(this.eventSystem, null))
            {
                return false;
            }

            return this.eventSystem.IsPointerOverGameObject(fingerId);
        }
#endif

        private void StartInput(Vector2 inputPosition)
        {
            this.isHoldStarted = true;
            this.centerScreenPosition = inputPosition;
            this.OnPositionStarted?.Invoke(inputPosition);
        }

        private void ProcessMove(Vector2 inputPosition)
        {
            var screenVector = inputPosition - this.centerScreenPosition;
            if (this.isMoveStarted || screenVector.magnitude > MIN_MAGNITUDE)
            {
                this.isMoveStarted = true;
                this.OnPositionMoved?.Invoke(inputPosition);
                this.OnDirectionMoved?.Invoke(screenVector.normalized);
            }
        }

        private void EndInput(Vector2 inputPosition)
        {
            this.isMoveStarted = false;
            this.isHoldStarted = false;
            this.OnPositionEnded?.Invoke(inputPosition);
        }

        public void CancelInput()
        {
            if (this.isHoldStarted)
            {
                this.isMoveStarted = false;
                this.isHoldStarted = false;
                this.OnCanceled?.Invoke();
            }
        }
    }
}