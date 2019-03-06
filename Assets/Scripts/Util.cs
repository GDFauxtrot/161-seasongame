using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A static helper class for storing useful functions and operations.
/// </summary>
public static class Util
{
    /// <summary>
    /// Calculates the linear parameter t that produces the
    /// interpolant value within the unclamped range of [a, b].
    /// </summary>
    public static float InverseLerpUnclamped(float a, float b, float t)
    {
        // Subtract range of [a, b] to [0, b - a], including t.
        float b2 = b - a;
        float t2 = t - a;

        // Edge case
        if (b2 == 0)
            return 0;

        return t2 / b2;
    }

    // https://answers.unity.com/questions/661383/whats-the-most-efficient-way-to-rotate-a-vector2-o.html
    /// <summary>
    /// Rotates a Vector2 by a given amount in degrees.
    /// </summary>
    public static Vector2 Vec2Rotate(Vector2 v, float degrees) {
        float radians = degrees * Mathf.Deg2Rad;
        float sin = Mathf.Sin(radians);
        float cos = Mathf.Cos(radians);

        float tx = v.x;
        float ty = v.y;
 
        return new Vector2(cos * tx - sin * ty, sin * tx + cos * ty);
    }
}
