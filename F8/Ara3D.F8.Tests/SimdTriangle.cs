using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Ara3D.F8.Tests
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public readonly struct SimdTriangle
    {
        public readonly SimdVector3 A;
        public readonly SimdVector3 B;
        public readonly SimdVector3 C;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public SimdTriangle(in SimdVector3 a, in SimdVector3 b, in SimdVector3 c) => (A, B, C) = (a, b, c);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public SimdVector3 Normal() => SimdVector3.Cross(B - A, C - A).Normal();

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public f8 Perimeter() => (B - A).Length() + (C - B).Length() + (A - C).Length();

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public SimdVector3 Barycentric(f8 u, f8 v) => A * (1 - u - v) + B * u + C * v;
    }
}