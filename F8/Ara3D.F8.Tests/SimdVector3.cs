using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Ara3D.F8.Tests
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public readonly struct SimdVector3
    {
        public readonly Vector8 X;
        public readonly Vector8 Y;
        public readonly Vector8 Z;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public SimdVector3(in Vector8 x, in Vector8 y, in Vector8 z) => (X, Y, Z) = (x, y, z);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static SimdVector3 operator +(in SimdVector3 a, in SimdVector3 b) => new(a.X + b.X, a.Y + b.Y, a.Z + b.Z);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static SimdVector3 operator-(in SimdVector3 a, in SimdVector3 b) => new(a.X - b.X, a.Y - b.Y, a.Z - b.Z);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static SimdVector3 operator *(in SimdVector3 a, in float scalar) => new(a.X * scalar, a.Y * scalar, a.Z * scalar);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static SimdVector3 operator *(in SimdVector3 a, in Vector8 scalar) => new(a.X * scalar, a.Y * scalar, a.Z * scalar);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static SimdVector3 operator /(in SimdVector3 a, in float scalar) => new(a.X / scalar, a.Y / scalar, a.Z / scalar);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static SimdVector3 operator /(in SimdVector3 a, in Vector8 scalar) => new(a.X / scalar, a.Y / scalar, a.Z / scalar);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static SimdVector3 Min(in SimdVector3 a, in SimdVector3 b) => new(Vector8.Min(a.X, b.X), Vector8.Min(a.Y, b.Y), Vector8.Min(a.Z, b.Z));
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static SimdVector3 Max(in SimdVector3 a, in SimdVector3 b) => new(Vector8.Max(a.X, b.X), Vector8.Max(a.Y, b.Y), Vector8.Max(a.Z, b.Z));

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Vector8 LengthSquared() => X.Square() + Y.Square() + Z.Square();

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Vector8 Length() => LengthSquared().Sqrt();

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static SimdVector3 Cross(in SimdVector3 a, in SimdVector3 b) => new(a.Y * b.Z - a.Z * b.Y, a.Z * b.X - a.X * b.Z, a.X * b.Y - a.Y * b.X);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public SimdVector3 Normal() => this / Length();
    }
}