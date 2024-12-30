# f8

An easy to use C# SIMD library designed for common use cases. 

## About 

This is a C# library for .NET 9 that provides a single data structure called `f8`. The `f8` strucut is a 
readonly struct that contains 8 floats.

The `f8` struct is designed to be used much in the same way a `float` type is already used in C#. It provides a 
number of operators and methods that allow you to perform common math and arithemetic operations on multiple 
numbers simultaneously leveraging SIMD intrinsics.

The `f8` struct is essentially a wrapper around `Vector256&lt;float&gt;`. 
This particular size of SIMD data type was chosen over `Vector128` and `Vector512` due to 
the ubiquity of Vector256 operations 
known as [AVX](https://en.wikipedia.org/wiki/Advanced_Vector_Extensions) 
on modern desktop and laptop computers. 


## Motivation 

In theory SIMD intrinsics operating on 256-bit registers that can hold 8 floats at a time 
can provide 8x performance benefits or more compared to scalar operations 
because they operate on wide registers that can hold multiple values. 

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

## Peformance Benefits 

The performance benefits of using a library such as f8 is not always as substantial as one might hope when using it in
some real world scenarios. 

For example in the previous example, we saw only a 33% reduction in execution time using f8 compared to scalar operations.
This is because the JIT compiler is already able to leverage SIMD operations when operating on arrays of floating point numbers.

Similarly when operating on data sets that already use SIMD enhanced algorithms, such as those in System.Numerics, we 
only see modest performance benefits.


## Benchmarks 

```
   PerformanceTests (9 tests) Success
    BarycentricBenchmark Success
Test 1 took 454 msec with 200000 input elements and 100 iterations
Test 2 took 631 msec with 1600000 input elements and 100 iterations

    BoundsBenchmark Success
Test 1 took 235 msec with 200000 input elements and 100 iterations
Test 2 took 1067 msec with 1600000 input elements and 100 iterations

    LerpBenchmark Success
Test 1 took 847 msec with 3200000 input elements and 100 iterations
Test 2 took 1083 msec with 25600000 input elements and 100 iterations

    PerimetersBenchmark Success
Test 1 took 388 msec with 200000 input elements and 100 iterations
Test 2 took 991 msec with 1600000 input elements and 100 iterations

    QuadraticBenchmark Success
Test 1 took 809 msec with 3200000 input elements and 100 iterations
Test 2 took 1200 msec with 25600000 input elements and 100 iterations

    SqrtBenchmark Success
Test 1 took 968 msec with 3200000 input elements and 100 iterations
Test 2 took 1700 msec with 25600000 input elements and 100 iterations

    SumBenchmark Success
Test 1 took 1345 msec with 3200000 input elements and 100 iterations
Test 2 took 1736 msec with 25600000 input elements and 100 iterations

    TriangleNormalsBenchmark Success
Test 1 took 572 msec with 200000 input elements and 100 iterations
Test 2 took 969 msec with 1600000 input elements and 100 iterations

    VectorSumBenchmark Success
Test 1 took 141 msec with 200000 input elements and 100 iterations
Test 2 took 188 msec with 1600000 input elements and 100 iterations
```
