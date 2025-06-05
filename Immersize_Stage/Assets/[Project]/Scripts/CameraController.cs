using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System;
using UnityEngine;

namespace PlayBook.Services.CameraController {
    public enum CameraType { TopDown, ThirdPerson, FreeLook, FirstPerson }

    [RequireComponent(typeof(Camera))]
    public class CameraController : MonoBehaviour {
        #region Serialized Fields
            [Header("Cameras")]
            [SerializeField] private Camera _topDownCam;
            [SerializeField] private Camera _thirdPersonCam;
            [SerializeField] private Camera _freeLookCam;
            [SerializeField] private Camera _firstPersonCam;


            [Header("Follow Settings")]
            [SerializeField] protected Transform target;
            [SerializeField] protected LayerMask cameraCollisionMask;

            [SerializeField] protected internal float cameraMinDistanceToWall = 0.4f;
            [SerializeField] protected internal float verticalSmoothTime = 0.04f;
            [SerializeField] protected internal float horizontalSmoothTime = 0.02f;
            [SerializeField] protected internal float collisionSmoothTime = 0.2f;
            [SerializeField] protected internal float followSpeed = 5f;

            [SerializeField] protected internal Vector3 cameraOriginOffset = Vector3.zero;

            [SerializeField] protected internal Vector3 topDownOffset = new(0, 25, 0);
            [SerializeField] protected internal Vector3 thirdPersonOffset = new(0, 2, -10);
            [SerializeField] protected internal Vector3 freeLookOffset = new(0, 2, -12);
            [SerializeField] protected internal Vector3 firstPersonOffset = new(0, 1.6f, -2);
        #endregion

        private readonly Dictionary<CameraType, Camera> _cameraDict = new();
        private CameraType _currentType;

        private CancellationTokenSource _cancelSource;

        private float _freeLookYaw = 0f;
        private float _freeLookPitch = 20f;
        private float _lastMouseInputTime = 0f;

        private readonly float _mouseSensitivity = 1.5f;
        private readonly float _pitchMin = 10f, _pitchMax = 70f;
        private readonly float _returnDelay = 5f;

        private readonly bool _isInitializedFromConstructor = false;

        private Vector3 _delayedHorizontalPosition, _delayedVerticalPosition, _verticalVelocity, _horizontalVelocity
            = Vector3.zero;

        private float _cameraDistance, _distanceVelocity = 0f;

        public CameraType CurrentCameraType {
            get => _currentType;
            set {
                if (_currentType == value) return;
                _currentType = value;

                SwitchCamera(_currentType);
            }
        }

        public Camera this[CameraType type] {
            get => _cameraDict.TryGetValue(type, out var cam) ? cam : null;
            set {
                if (value != null)
                    _cameraDict[type] = value;
            }
        }

        #region Constructors
            public CameraController() {
                _cameraDict.Clear();
                _currentType = CameraType.TopDown;
                // _isInitializedFromConstructor = true;
            }

            public CameraController(Transform target, float followSpeed) : this() {
                this.target = target;
                this.followSpeed = followSpeed;
            }

            public CameraController(Vector3 topDownOffset, Vector3 thirdPersonOffset, Vector3 freeLookOffset, Vector3 firstPersonOffset) : this() {
                this.topDownOffset = topDownOffset;
                this.thirdPersonOffset = thirdPersonOffset;
                this.freeLookOffset = freeLookOffset;
                this.firstPersonOffset = firstPersonOffset;
            }

            public void Init() {
                this[CameraType.TopDown] = _topDownCam;
                this[CameraType.ThirdPerson] = _thirdPersonCam;
                this[CameraType.FreeLook] = _freeLookCam;
                this[CameraType.FirstPerson] = _firstPersonCam;

                if (!Initialize())
                    target ??= GameObject.FindGameObjectWithTag("Player")?.GetComponent<Rigidbody>()?.transform;
            }

        #endregion
        private void Reset() => Awake();
        private void Awake() {
            if (_isInitializedFromConstructor) return;
        
            this[CameraType.TopDown] = _topDownCam;
            this[CameraType.ThirdPerson] = _thirdPersonCam;
            this[CameraType.FreeLook] = _freeLookCam ??= GetComponent<Camera>();
            this[CameraType.FirstPerson] = _firstPersonCam;
        }

        private void Start() {
            if (_isInitializedFromConstructor) return;

            if (!Initialize())
                target ??= GameObject.FindGameObjectWithTag("Player")?.GetComponent<Rigidbody>()?.transform;
        }

        protected bool Initialize() {
            if (target == null) return false;

            CurrentCameraType = CameraType.TopDown;

            _cancelSource = new CancellationTokenSource();
            _ = ListenAsync(_cancelSource.Token);

            return true;
        }
        private void Update() {
            
                if (!_cameraDict.TryGetValue(_currentType, out var cam) || cam == null || target == null) return;

                _ = _currentType switch {
                    CameraType.TopDown => Run(() => SmoothFollow(cam, topDownOffset, true)),
                    CameraType.ThirdPerson => Run(() => SmoothFollow(cam, thirdPersonOffset, true)),
                    CameraType.FreeLook => Run(() => HandleFreeLook(cam)),
                    CameraType.FirstPerson => Run(() => FirstPersonFollow(cam)),
                    _ => Run(() => Debug.LogWarning($"[CameraController] Unknown CameraType: {_currentType}"))
                };
        }

        private async Task ListenAsync(CancellationToken token) {
            for (; !token.IsCancellationRequested ;) {
                await Task.Yield();

                if (!_cameraDict.TryGetValue(_currentType, out var cam) || cam == null || target == null) continue;

                _ = _currentType switch {
                    CameraType.TopDown => Run(() => SmoothFollow(cam, topDownOffset, true)),
                    CameraType.ThirdPerson => Run(() => SmoothFollow(cam, thirdPersonOffset, true)),
                    CameraType.FreeLook => Run(() => HandleFreeLook(cam)),
                    CameraType.FirstPerson => Run(() => FirstPersonFollow(cam)),
                    _ => Run(() => Debug.LogWarning($"[CameraController] Unknown CameraType: {_currentType}"))
                };
            }
        }

        private int Run(System.Action action) {
            action?.Invoke(); return 0;
        }

        private void SwitchCamera(CameraType type) {
            foreach (var cam in _cameraDict.Values)
                if (cam != null) cam.enabled = false;

            _ = type switch {
                CameraType.TopDown => EnableCam(CameraType.TopDown),
                CameraType.ThirdPerson => EnableCam(CameraType.ThirdPerson),
                CameraType.FreeLook => EnableCam(CameraType.FreeLook),
                CameraType.FirstPerson => EnableCam(CameraType.FirstPerson),
                _ => LogUnknownType(type)
            };
        }

        private int EnableCam(CameraType type) {
            if (_cameraDict.TryGetValue(type, out var cam) && cam != null) {
                cam.enabled = true; return 1;
            }

            Debug.LogWarning($"[CameraController] Camera for {type} is null.");
            return 0;
        }

        private int LogUnknownType(CameraType type) {
            Debug.LogWarning($"[CameraController] Unknown CameraType: {type}");
            return 0;
        }

        #region Camera Methods

            private void SmoothFollow(Camera cam, Vector3 offset, bool lookAt) {
                if (target == null) return;

                Vector3 followPosition = target.position + cameraOriginOffset;

                Vector3 horizontalOffset = Vector3.ProjectOnPlane(offset, Vector3.up);
                Vector3 verticalOffset = offset - horizontalOffset;

                Vector3 horizontalTarget = Vector3.ProjectOnPlane(followPosition + horizontalOffset, Vector3.up);
                Vector3 verticalTarget = followPosition + verticalOffset - horizontalTarget;

                if (_delayedHorizontalPosition != horizontalTarget) {
                    _delayedHorizontalPosition = Vector3.SmoothDamp(
                        _delayedHorizontalPosition, horizontalTarget,
                        ref _horizontalVelocity, horizontalSmoothTime, Mathf.Infinity, Time.deltaTime
                    );
                }

                if (_delayedVerticalPosition != verticalTarget) {
                    _delayedVerticalPosition = Vector3.SmoothDamp(
                        _delayedVerticalPosition, verticalTarget,
                        ref _verticalVelocity, verticalSmoothTime, Mathf.Infinity, Time.deltaTime
                    );
                }

                Vector3 smoothedFollowPosition = _delayedHorizontalPosition + _delayedVerticalPosition;
                Vector3 desiredCamPos = smoothedFollowPosition;

                Vector3 camDirection = (cam.transform.position - smoothedFollowPosition).normalized;
                Vector3 rawCamOffset = offset.magnitude * camDirection;

                if (Physics.Raycast(smoothedFollowPosition, rawCamOffset.normalized, out RaycastHit hit, offset.magnitude + cameraMinDistanceToWall, cameraCollisionMask)) {
                    float camMargin = cameraMinDistanceToWall / Mathf.Sin(Vector3.Angle(hit.normal, -rawCamOffset.normalized) * Mathf.Deg2Rad);
                    _cameraDistance = Mathf.SmoothDamp(_cameraDistance, hit.distance - camMargin, ref _distanceVelocity, collisionSmoothTime);
                } else {
                    _cameraDistance = Mathf.SmoothDamp(_cameraDistance, offset.magnitude, ref _distanceVelocity, collisionSmoothTime);
                }

                cam.transform.position = smoothedFollowPosition + rawCamOffset.normalized * _cameraDistance;

                if (lookAt) {
                    cam.transform.rotation = Quaternion.Slerp(
                        cam.transform.rotation,
                        Quaternion.LookRotation(followPosition - cam.transform.position),
                        followSpeed * Time.deltaTime
                    );
                }
            }
            private void HandleFreeLook(Camera cam) {
                if (target == null || cam == null) return;

                float mouseX = Input.GetAxis("Mouse X");
                float mouseY = Input.GetAxis("Mouse Y");

                bool isUserControllingCamera = Mathf.Abs(mouseX) > 0.01f || Mathf.Abs(mouseY) > 0.01f;

                if (isUserControllingCamera) {
                    _freeLookYaw += mouseX * _mouseSensitivity;

                    _freeLookPitch = Mathf.Clamp(_freeLookPitch - mouseY * _mouseSensitivity, _pitchMin, _pitchMax);
                    _lastMouseInputTime = Time.time;
                }

                Quaternion targetRotation = Quaternion.Euler(_freeLookPitch, _freeLookYaw, 0f);
                Vector3 desiredOffsetDir = targetRotation * Vector3.back;
                Vector3 desiredPosition = target.position + desiredOffsetDir * freeLookOffset.magnitude;

                Vector3 camDir = (desiredPosition - target.position).normalized;
                float targetDistance = freeLookOffset.magnitude;

                Debug.DrawRay(target.position, camDir * (targetDistance + cameraMinDistanceToWall), Color.red, 0.1f);

                if (Physics.Raycast(target.position, camDir, out RaycastHit hit, targetDistance + cameraMinDistanceToWall, cameraCollisionMask)) {
                    float margin = cameraMinDistanceToWall / Mathf.Sin(Vector3.Angle(hit.normal, -camDir) * Mathf.Deg2Rad);
                    targetDistance = Mathf.Min(hit.distance - margin, targetDistance);
                }

                desiredPosition = target.position + desiredOffsetDir * targetDistance;

                Vector3 velocity = Vector3.zero;
                cam.transform.position = Vector3.SmoothDamp(cam.transform.position, desiredPosition, ref velocity, 0.1f);

                if (Time.time - _lastMouseInputTime > _returnDelay) {
                    _ = ResetCameraAfterDelay(_freeLookCam, _returnDelay, _cancelSource.Token);
                } else cam.transform.rotation = Quaternion.Slerp(cam.transform.rotation, targetRotation, followSpeed * Time.deltaTime);
            }

            private async Task ResetCameraAfterDelay(Camera cam, float delay, CancellationToken token) {
                try {
                    await Task.Delay(TimeSpan.FromSeconds(delay), token);
                    
                    if (!token.IsCancellationRequested) {
                        cam.transform.position = target.position + freeLookOffset;
                        cam.transform.LookAt(target);
                    }
                } catch (TaskCanceledException e) {
                    Debug.Log($"[CameraController] ResetCameraAfterDelay was cancelled: {e.Message}");
                }
            }

            private void FirstPersonFollow(Camera cam) { }

            public static async Task ShakeCameraAsync(Camera cam, float duration, float magnitude) { }

        #endregion

        private void OnDestroy() {
            _cancelSource?.Cancel();
            _cancelSource?.Dispose();
        }
    }
}