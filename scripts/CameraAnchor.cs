using Godot;
using System;
using System.Diagnostics;

public partial class CameraAnchor : Node3D
{
	private bool _isMoving = false;
	private Vector2 _movementDirection = Vector2.Zero;
	
	
	public override void _UnhandledInput(InputEvent @event)
	{
		if (@event is InputEventMouseButton {ButtonIndex: MouseButton.Right} mouseButton)
		{
			_isMoving = mouseButton.Pressed;
		} else if (@event is InputEventMouseMotion eventMouseMotion)
		{
			_movementDirection = eventMouseMotion.Relative;
		}
	}

	public override void _Process(double delta)
	{
		if (_isMoving)
		{
			var f = (float)delta;

			var invertBasis = Basis.Inverse();
			Rotate(Vector3.Up * invertBasis, -_movementDirection.X * f);
			Rotate(Vector3.Right * invertBasis, -_movementDirection.Y * f);
		}
		
		
		_movementDirection = Vector2.Zero;
	}
	
	
	public override void _PhysicsProcess(double delta)
	{
		var zoom = Input.GetAxis("zoom_out", "zoom_in");
		var zoom_speed = 4f;
		if (zoom != 0)
		{
			var camera3D = GetNode<Camera3D>("%Camera3D");
			var camera3DPosition = camera3D.Position;
			var current = camera3DPosition.Z;
			camera3DPosition.Z = Math.Clamp(1.5f, current + zoom * zoom_speed *(float)delta, 10f);
			camera3D.Position = camera3DPosition;
		}
		
		
	}
}
