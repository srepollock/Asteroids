using UnityEngine;
using System;

public class Circle {
    private float x;
    private float y;
    private float r;
    /// <summary>
    /// Empty constructor that creates a circle at origin with a radius of 1
    /// </summary>
    public Circle() {
        x = 0f;
        y = 0f;
        r = 1f;
    }
    /// <summary>
    /// Circle constructor
    /// </summary>
    /// <param name="_x">X position of the center circle</param>
    /// <param name="_y">Y position of the center circle</param>
    /// <param name="_r">Radius of the circle</param>
    public Circle(float _x, float _y, float _r) {
        x = _x;
        y = _y;
        r = _r;
    }
    /// <summary>
    /// Gets the X
    /// </summary>
    /// <returns>X</returns>
    public float getX() { return x; }
    /// <summary>
    /// Gets the Y
    /// </summary>
    /// <returns>Y</returns>
    public float getY() { return y; }
    /// <summary>
    /// Gets the radius
    /// </summary>
    /// <returns>R</returns>
    public float getR() { return r; }
    /// <summary>
    /// Checks if the point is inside the circle object
    /// </summary>
    /// <param name="point">Point to check if inside the circle</param>
    /// <returns>True if the inside; else false</returns>
    public bool Contains(Vector3 point) {
        return Math.Pow((point.x - x), 2) + Math.Pow((point.y - y), 2) < Math.Pow(r, 2);
    }
}