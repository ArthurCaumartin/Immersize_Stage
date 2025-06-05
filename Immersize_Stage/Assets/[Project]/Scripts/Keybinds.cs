using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

using PlayBook.Services.CameraController;

public class Keybinds : MonoBehaviour {
    [SerializeField] private CameraController cameraController;
    [SerializeField] private readonly float _moveSpeed = 5f;

    private CancellationTokenSource _cancelSource;

    private void Start() {
        _cancelSource = new CancellationTokenSource();
        _ = ListenAsync(_cancelSource.Token);
    }

    private async Task ListenAsync(CancellationToken token) {
        for (; !token.IsCancellationRequested ;) {
            await Task.Yield();

            if (Input.GetKeyDown(KeyCode.Alpha1)) cameraController.CurrentCameraType = PlayBook.Services.CameraController.CameraType.TopDown;
            if (Input.GetKeyDown(KeyCode.Alpha2)) cameraController.CurrentCameraType = PlayBook.Services.CameraController.CameraType.ThirdPerson;
            if (Input.GetKeyDown(KeyCode.Alpha3)) cameraController.CurrentCameraType = PlayBook.Services.CameraController.CameraType.FreeLook;
            if (Input.GetKeyDown(KeyCode.Alpha4)) cameraController.CurrentCameraType = PlayBook.Services.CameraController.CameraType.FirstPerson;

            Vector3 movement = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            transform.Translate(movement * _moveSpeed * Time.deltaTime, Space.World);
        }
    }

    private void OnDestroy() {
        _cancelSource?.Cancel();
        _cancelSource?.Dispose();
    }
}
