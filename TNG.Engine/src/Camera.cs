using System.Numerics;

namespace TNG.Engine;

internal class Camera {
    private Matrix4x4 projectionMatrix;
    private Matrix4x4 viewMatrix;
    private Vector2 position;

    public Camera(Vector2 position) {
        this.position = position;
        this.projectionMatrix = new Matrix4x4();
        this.viewMatrix = new Matrix4x4();
        adjustProjection();
    }

    private void adjustProjection() {
        projectionMatrix = Matrix4x4.Identity;
        projectionMatrix = Matrix4x4.CreateOrthographicOffCenter(0.0f, 32.0f * 40.0f, 0.0f, 32.0f * 21.0f, 0.0f, 100.0f);
    }

    public Matrix4x4 GetViewMatrix() {
        Vector3 cameraFront = new Vector3(0.0f, 0.0f, -1.0f);
        Vector3 cameraUp = new Vector3(0.0f, 1.0f, 0.0f);
        cameraFront.X += position.X;
        cameraFront.Y += position.Y;
        this.viewMatrix = Matrix4x4.Identity;
        this.viewMatrix = Matrix4x4.CreateLookAt(new Vector3(position.X, position.Y, 20.0f), cameraFront, cameraUp);
        return this.viewMatrix;
    }

    public Matrix4x4 GetProjectionMatrix() {
        return this.projectionMatrix;
    }
}