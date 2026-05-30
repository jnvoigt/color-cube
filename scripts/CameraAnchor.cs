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
			Debug.WriteLine($"is moving  {_movementDirection}");
			var f = (float)delta;

			var invertBasis = Basis.Inverse();
			Rotate(Vector3.Up * invertBasis, -_movementDirection.X * f);
			Rotate(Vector3.Right * invertBasis, -_movementDirection.Y * f);
		}
		
		
		_movementDirection = Vector2.Zero;
	}
}
