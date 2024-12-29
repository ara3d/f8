using System.Numerics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Ara3D.F8.Tests
{
    public static class RandomInputs
    {
        public static Random Rng = new Random(0);
        public const int Count = 200_000;

        public static float[] GenerateFloats(int cnt)
        {
            var r = new float[cnt];
            for (var i = 0; i < cnt; i++)
            {
                r[i] = Rng.NextSingle();
            }

            return r;
        }

        public static unsafe T[] Generate<T>(int cnt)
        {
            var sizeInFloats = cnt * sizeof(T) / 4;
            var r = new T[cnt];
            var floats = GenerateFloats(sizeInFloats);
            
            fixed (void* ptrSrc = floats, ptrDest = r)
            {
                var byteSize = sizeInFloats * 4;
                Buffer.MemoryCopy(
                    ptrSrc, ptrDest, byteSize, byteSize);
            }

            return r;
        }

        public static f8[] SimdFloats = Generate<f8>(Count * 16);
        public static SimdVector3[] SimdPoints = Generate<SimdVector3>(Count);
        public static SimdTriangle[] SimdTriangles = Generate<SimdTriangle>(Count);

        public static float[] Floats = GenerateFloats(Count * 16 * 8);
        public static Vector3[] Points = Generate<Vector3>(Count * 8);
        public static Triangle[] Triangles = Generate<Triangle>(Count * 8);
    }
}