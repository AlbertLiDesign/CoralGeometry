﻿using System;

namespace CoralGeometry
{
    public class Vector3D
    {
        /// <summary>
        /// Represents a vector in Euclidean space.
        /// </summary>
        internal double _x = 0.0;
        internal double _y = 0.0;
        internal double _z = 0.0;

        /// <summary>
        /// Get the length of a vector
        /// </summary>        
        /// <returns>The length</returns>
        public double Length
        {
            get { return (double)Math.Sqrt(this._x * this._x + this._y * this._y + this._z * this._z); }
        }

        /// <summary>
        /// Get the square length of a vector
        /// </summary>        
        /// <returns>The length</returns>
        public double SqrLength
        {
            get { return this._x * this._x + this._y * this._y + this._z * this._z; }
        }

        /// <summary>
        /// Get a origin vector.
        /// </summary>        
        /// <returns>Return a origin vector.</returns>
        public static Vector3D Origin = new Vector3D(0.0f, 0.0f, 0.0f);

        public Vector3D()
        {
            _x = 0.0f;
            _y = 0.0f;
            _z = 0.0f;
        }

        /// <summary>
        /// Constructs a new vector from 3 double precision numbers.
        /// </summary>
        /// <param name="x">X component of vector.</param>
        /// <param name="y">Y component of vector.</param>
        /// <param name="z">Z component of vector.</param>
        public Vector3D(double x, double y, double z)
        {
            _x = x;
            _y = y;
            _z = z;
        }

        /// <summary>
        /// Gets or sets the X (first) component of this vector.
        /// </summary>
        public double X { get { return _x; } set { _x = value; } }

        /// <summary>
        /// Gets or sets the Y (second) component of this vector.
        /// </summary>
        public double Y { get { return _y; } set { _y = value; } }

        /// <summary>
        /// Gets or sets the Z (third) component of this vector.
        /// </summary>
        public double Z { get { return _z; } set { _z = value; } }

        /// <summary>
        /// Computes a hash number that represents the current vector.
        /// </summary>
        /// <returns>A hash code that is not unique for each vector.</returns>
        public override int GetHashCode()
        {
            // MSDN docs recommend XOR'ing the internal values to get a hash code
            return _x.GetHashCode() ^ _y.GetHashCode() ^ _z.GetHashCode();
        }

        /// <summary>
        /// Sums up two vectors.
        /// </summary>
        /// <param name="v1">A vector.</param>
        /// <param name="v2">A second vector.</param>
        /// <returns>A new vector that results from the componentwise addition of the two vectors.</returns>
        public static Vector3D operator +(Vector3D v1, Vector3D v2)
        {
            return new Vector3D(v1._x + v2._x, v1._y + v2._y, v1._z + v2._z);
        }

        /// <summary>
        /// Subtracts one vector from another.
        /// </summary>
        /// <param name="v1">A vector.</param>
        /// <param name="v2">A second vector.</param>
        /// <returns>The first vector minus the second vector</returns>
        public static Vector3D operator -(Vector3D v1, Vector3D v2)
        {
            return new Vector3D(v1._x - v2._x, v1._y - v2._y, v1._z - v2._z);
        }

        /// <summary>
        /// Multiplies a vector by a number, having the effect of scaling it.
        /// </summary>
        /// <param name="vector">A vector.</param>
        /// <param name="t">A number.</param>
        /// <returns>A new vector that is the original vector coordinatewise multiplied by t.</returns>
        public static Vector3D operator *(Vector3D vector, double t)
        {
            return new Vector3D(vector._x * t, vector._y * t, vector._z * t);
        }

        /// <summary>
        /// Multiplies a vector by a number, having the effect of scaling it.
        /// </summary>
        /// <param name="t">A number.</param>
        /// <param name="vector">A vector.</param>
        /// <returns>A new vector that is the original vector coordinatewise multiplied by t.</returns>
        public static Vector3D operator *(double t, Vector3D vector)
        {
            return new Vector3D(vector._x * t, vector._y * t, vector._z * t);
        }

        /// <summary>
        /// Computes the dot product of two vectors.
        /// </summary>
        /// <param name="vector">A vector.</param>
        /// <param name="other">Another vector.</param>
        /// <returns>The result number of dot product.</returns>
        public static double operator *(Vector3D vector, Vector3D other)
        {
            return vector._x * other._x + vector._y * other._y + vector._z * other._z;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="vector">A vector.</param>
        /// <param name="t">A number.</param>
        /// <returns>A new vector that is the original vector coordinatewise multiplied by t.</returns>
        public static Vector3D operator /(Vector3D vector, double t)
        {
            return new Vector3D(vector._x / t, vector._y / t, vector._z / t);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="t">A number.</param>
        /// <param name="vector">A vector.</param>
        /// <returns>A new vector that is the original vector coordinatewise multiplied by t.</returns>
        public static Vector3D operator /(double t, Vector3D vector)
        {
            return new Vector3D(vector._x / t, vector._y / t, vector._z / t);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="vector">A vector.</param>
        /// <param name="other">Another vector.</param>
        /// <returns>The result number of dot product.</returns>
        public static double operator /(Vector3D vector, Vector3D other)
        {
            return vector._x / other._x + vector._y / other._y + vector._z / other._z;
        }

        /// <summary>
        /// Computes the cross product (or vector product, or exterior product) of two vectors.
        /// <para>This operation is not commutative.</para>
        /// </summary>
        /// <param name="other"> Another vector.</param>
        /// <returns>A new vector that is perpendicular to both this vector and another vector,
        /// <para>has Length == a.Length * b.Length and</para>
        /// <para>with a result that is oriented following the right hand rule.</para>
        /// </returns>
        public Vector3D CrossProduct(Vector3D other)
        {
            return new Vector3D(_y * other._z - other._y * _z, _z * other._x - other._z * _x, _x * other._y - other._x * _y);
        }

        public override string ToString()
        {
            return string.Format("{0}, {1}, {2}", this._x, this._y, this._z);
        }

        /// <summary>
        /// Determines whether two vectors have equal values.
        /// </summary>
        /// <param name="a">The first vector.</param>
        /// <param name="b">The second vector.</param>
        /// <returns>true if the components of the two vectors are exactly equal; otherwise false.</returns>
        public static bool operator ==(Vector3D a, Vector3D b)
        {
            return (a._x == b._x && a._y == b._y && a._z == b._z);
        }

        /// <summary>
        /// Determines whether two vectors have different values.
        /// </summary>
        /// <param name="a">The first vector.</param>
        /// <param name="b">The second vector.</param>
        /// <returns>true if the two vectors differ in any component; false otherwise.</returns>
        public static bool operator !=(Vector3D a, Vector3D b)
        {
            return (a._x != b._x || a._y != b._y || a._z != b._z);
        }

        /// <summary>
        /// Determines whether the specified System.Object is a Vector3f and has the same values as the present vector.
        /// </summary>
        /// <param name="obj">The specified object.</param>
        /// <returns>true if obj is Vector3f and has the same components as this; otherwise false.</returns>
        public override bool Equals(object obj)
        {
            return (obj is Vector3D && this == (Vector3D)obj);
        }

        /// <summary>
        /// Determines whether the specified vector has the same values as the present vector.
        /// </summary>
        /// <param name="vector">The specified vector.</param>
        /// <returns>true if vector has the same components as this; otherwise false.</returns>
        public bool Equals(Vector3D vector)
        {
            return this == vector;
        }

        /// <summary>
        /// Get the distance between two vectors.
        /// </summary>
        /// <param name="vector"> Another vector. </param>
        /// <returns>Return the distance between two vectors.</returns>
        public double DistanceTo(Vector3D vector)
        {
            return (this - vector).Length;
        }

        /// <summary>
        /// Get the reversed vector.
        /// </summary>
        /// <returns>Return the reversed vector.</returns>
        public Vector3D Reverse()
        {
            return new Vector3D(X * -1.0f, Y * -1.0f, Z * -1.0f);
        }

        /// <summary>
        /// Unitize the vector.
        /// </summary>
        /// <returns></returns>
        public Vector3D Unitize()
        {
            return this / Length;
        }
    }
}
