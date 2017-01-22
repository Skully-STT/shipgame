using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationClamp {

	public enum Axis
	{
		X,
		Y,
		Z
	}

	public static Quaternion ClampAroundAxis(Quaternion q, Axis axis, float minimum, float maximum)
	{
		q.x /= q.w;
		q.y /= q.w;
		q.z /= q.w;
		q.w = 1.0f;

		float angle = 2.0f * Mathf.Rad2Deg * Mathf.Atan(axis == Axis.X ? q.x : axis == Axis.Y ? q.y : q.z);
		angle = Mathf.Clamp(angle, minimum, maximum);

		switch (axis)
		{
			case Axis.X:
				q.x = Mathf.Tan(0.5f * Mathf.Deg2Rad * angle);
				break;
			case Axis.Y:
				q.y = Mathf.Tan(0.5f * Mathf.Deg2Rad * angle);
				break;
			case Axis.Z:
				q.z = Mathf.Tan(0.5f * Mathf.Deg2Rad * angle);
				break;
			default:
				throw new ArgumentOutOfRangeException("axis", axis, null);
		}

		return q;
	}
}
