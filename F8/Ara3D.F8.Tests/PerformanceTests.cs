using System;
using System.Diagnostics;
using System.Numerics;
using System.Runtime.CompilerServices;

namespace Ara3D.F8.Tests
{
    public static class PerformanceTests
    {
        // --------------------------------------------------------------
        // Helper / Random generation methods
        // --------------------------------------------------------------

        public const int MaxCount = 400_000;
        public const int NumIterations = 100;

        // --------------------------------------------------------------
        // The RunTest utility (given)
        // --------------------------------------------------------------

        public static void RunComparison<T0, TR0, T1, TR1>(
            T0[] input1,
            T1[] input2,
            TR0[] output1,
            TR1[] output2, 
            Action<T0[], TR0[]> test1, 
            Action<T1[], TR1[]> test2)
        {
            try
            {
                var sw1 = new Stopwatch();
                var sw2 = new Stopwatch();
                for (var j = 0; j < NumIterations; j++)
                {
                    sw1.Start();
                    test1(input1, output1);
                    sw1.Stop();

                    sw2.Start();
                    test2(input2, output2);
                    sw2.Stop();
                }

                Console.WriteLine($"Test 1 took {sw1.ElapsedMilliseconds} msec with {input1.Length} input elements and {NumIterations} iterations");
                Console.WriteLine($"Test 2 took {sw2.ElapsedMilliseconds} msec with {input2.Length} input elements and {NumIterations} iterations");
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error occurred {e.Message}");
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ComputeSimdNormals(SimdTriangle[] inputs, SimdVector3[] outputs)
        {
            var cnt = inputs.Length;
            for (int i = 0; i < cnt; i++)
            {
                outputs[i] = inputs[i].Normal();
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ComputeNormals(Triangle[] inputs, Vector3[] outputs)
        {
            var cnt = inputs.Length;
            for (int i = 0; i < cnt; i++)
            {
                outputs[i] = inputs[i].Normal();
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ComputeSimdPerimeters(SimdTriangle[] inputs, f8[] outputs)
        {
            var cnt = inputs.Length;
            for (int i = 0; i < cnt; i++)
            {
                outputs[i] = inputs[i].Perimeter();
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ComputePerimeters(Triangle[] inputs, float[] outputs)
        {
            var cnt = inputs.Length;
            for (int i = 0; i < cnt; i++)
            {
                outputs[i] = inputs[i].Perimeter();
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ComputeSimdBounds(SimdVector3[] inputs, SimdVector3[] outputs)
        {
            var cnt = inputs.Length;
            outputs[0] = inputs[0];
            outputs[1] = inputs[0];
            for (int i = 0; i < cnt; i++)
            {
                outputs[0] = SimdVector3.Min(outputs[0], inputs[i]);
                outputs[1] = SimdVector3.Max(outputs[1], inputs[i]);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ComputeBounds(Vector3[] inputs, Vector3[] outputs)
        {
            var cnt = inputs.Length;
            outputs[0] = inputs[0];
            outputs[1] = inputs[0];
            for (int i = 0; i < cnt; i++)
            {
                outputs[0] = Vector3.Min(outputs[0], inputs[i]);
                outputs[1] = Vector3.Max(outputs[1], inputs[i]);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ComputeQuadratic(float[] inputs, float[] outputs)
        {
            for (int i = 0; i < outputs.Length; i++)
            {
                var a = inputs[i * 4];
                var b = inputs[i * 4 + 1];
                var c = inputs[i * 4 + 2];
                var x = inputs[i * 4 + 3];
                outputs[i] = a * (x * x) + b * x + c;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ComputeSimdQuadratic(f8[] inputs, f8[] outputs)
        {
            for (int i = 0; i < outputs.Length; i++)
            {
                var a = inputs[i * 4];
                var b = inputs[i * 4 + 1];
                var c = inputs[i * 4 + 2];
                var x = inputs[i * 4 + 3];
                outputs[i] = a * (x * x) + b * x + c;
            }
        }

        [Test]
        public static void NormalsBenchmark()
        {
            var input1 = RandomInputs.SimdTriangles;
            var input2 = RandomInputs.Triangles;
            var output1 = new SimdVector3[input1.Length];
            var output2 = new Vector3[input2.Length];

            RunComparison(input1, input2, output1, output2, 
                ComputeSimdNormals, ComputeNormals);
        }

        [Test]
        public static void PerimetersBenchmark()
        {
            var input1 = RandomInputs.SimdTriangles;
            var input2 = RandomInputs.Triangles;
            var output1 = new f8[input1.Length];
            var output2 = new float[input2.Length];

            RunComparison(input1, input2, output1, output2,
                ComputeSimdPerimeters, ComputePerimeters);
        }

        [Test]
        public static void BoundsBenchmark()
        {
            var input1 = RandomInputs.SimdPoints;
            var input2 = RandomInputs.Points;
            var output1 = new SimdVector3[2];
            var output2 = new Vector3[2];

            RunComparison(input1, input2, output1, output2, 
                ComputeSimdBounds, ComputeBounds);
        }

        [Test]
        public static void BoundsQuadratic()
        {
            var input1 = RandomInputs.SimdFloats;
            var input2 = RandomInputs.Floats;
            var output1 = new f8[input1.Length / 4];
            var output2 = new float[input2.Length / 4];

            RunComparison(input1, input2, output1, output2,
                ComputeSimdQuadratic, ComputeQuadratic);
        }
    }
}
