# Ara3D.F8

An easy to use C# SIMD library designed for common use cases. 

## About 

This is a C# library for .NET 9 that provides a single data structure called `f8`. The `f8` data type is a 
readonly struct that contains 8 floats and that is designed to be used much in the same way a `float` type is already used in C#. 

It provides a number of operators and methods that allow you to perform common math and arithemetic operations on multiple 
numbers simultaneously leveraging SIMD intrinsics.

The `f8` struct is implemented as a wrapper around `Vector256&lt;float&gt;`. 
This particular size of SIMD data type was chosen over `Vector128` and `Vector512` due to 
the ubiquity of Vector256 operations 
known as [AVX](https://en.wikipedia.org/wiki/Advanced_Vector_Extensions) 
on modern desktop and laptop computers. 

## Motivation 

In theory SIMD intrinsics operating on 256-bit registers that can hold 8 floats at a time 
can provide up to 8x performance benefits compared to scalar operations 
because they operate on wide registers that can hold multiple values. 

> Note: in practice the performance benefits of using SIMD instrinsics are often far less than 8x. See the section 
below on [performance benefits](#performance-benefits) for more details.

One challenge with SIMD intrinsics is that they are not easy to use, and can force programmers to write
code that is non-idiomatic. Ideally as a programmer we want to express algorithms in a straightforward way,
that maps to the problem domain. 

Using `f8` we can write data-parallel code in the same way we would write scalar code. 
Consider for example the following code for computing the quadratic formula for a set of inputs. 


```csharp
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
```

If we have numerical types that consistent entirely of floating point fields (e.g., a `Vector3`) we can replace the floating point 
fields with an `f8` struct and the code will automatically be SIMD accelerated.

```
public readonly struct SimdVector3
{
    public readonly f8 X;
    public readonly f8 Y;
    public readonly f8 Z;

    public SimdVector3(in f8 x, in f8 y, in f8 z) 
        => (X, Y, Z) = (x, y, z);

    public static SimdVector3 operator +(in SimdVector3 a, in SimdVector3 b) 
        => new(a.X + b.X, a.Y + b.Y, a.Z + b.Z);   
    ...
}
```

## Performance Benefits 

Using `f8` in a number of benchmarks we have observed consistent performance benefits (in terms of execution speed) from 
25% to 350%.  

The performance benefits of using a library such as f8 was not as substantial as we initially expected when using it in
our real world scenarios. 

For example in the previous example, we saw only a 33% reduction in execution time using f8 compared to scalar operations.
This is because the JIT compiler is already able to leverage SIMD operations when operating on arrays of floating point numbers.

Similarly when operating on data sets that already use SIMD enhanced algorithms, such as those in System.Numerics, we 
only see modest performance benefits.


## Benchmarks 

```
    PerformanceTests (10 tests) Success
    
    BarycentricBenchmark Success
The SIMD version took 445 msec with 200000 input elements and 100 iterations
The Scalar version took 625 msec with 1600000 input elements and 100 iterations
The SIMD version was 40.4% faster

    BoundsBenchmark Success
The SIMD version took 231 msec with 200000 input elements and 100 iterations
The Scalar version took 1047 msec with 1600000 input elements and 100 iterations
The SIMD version was 353.2% faster

    CircleBenchmark Success
The SIMD version took 2620 msec with 800000 input elements and 100 iterations
The Scalar version took 8137 msec with 6400000 input elements and 100 iterations
The SIMD version was 210.6% faster

    LerpBenchmark Success
The SIMD version took 791 msec with 3200000 input elements and 100 iterations
The Scalar version took 1022 msec with 25600000 input elements and 100 iterations
The SIMD version was 29.2% faster

    PerimetersBenchmark Success
The SIMD version took 363 msec with 200000 input elements and 100 iterations
The Scalar version took 948 msec with 1600000 input elements and 100 iterations
The SIMD version was 161.2% faster

    QuadraticBenchmark Success
The SIMD version took 757 msec with 3200000 input elements and 100 iterations
The Scalar version took 1111 msec with 25600000 input elements and 100 iterations
The SIMD version was 46.8% faster

    SqrtBenchmark Success
The SIMD version took 767 msec with 3200000 input elements and 100 iterations
The Scalar version took 1185 msec with 25600000 input elements and 100 iterations
The SIMD version was 54.5% faster

    SumBenchmark Success
The SIMD version took 1030 msec with 3200000 input elements and 100 iterations
The Scalar version took 1294 msec with 25600000 input elements and 100 iterations
The SIMD version was 25.6% faster

    TriangleNormalsBenchmark Success
The SIMD version took 456 msec with 200000 input elements and 100 iterations
The Scalar version took 822 msec with 1600000 input elements and 100 iterations
The SIMD version was 80.3% faster

    VectorSumBenchmark Success
The SIMD version took 125 msec with 200000 input elements and 100 iterations
The Scalar version took 168 msec with 1600000 input elements and 100 iterations
The SIMD version was 34.4% faster
```

## Final Words

Leveraging SIMD operations effectively can both be challenging and rewarding.
As a professional software developer we need to find a way to leverage 
the benefits while minimizing additional complexity.

This library is one attempt at achieving that, and we hope you find it useful.

We would be very appreciative of additional contributions, feedback, 
and suggestions for improvement, that follow in the spirit of the library.