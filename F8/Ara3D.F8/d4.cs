using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Intrinsics;
using System.Runtime.Intrinsics.X86;

namespace Ara3D.d4
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public readonly struct d4
    {
        public readonly Vector256<double> Value;

        //-------------------------------------------------------------------------------------
        // Constructors
        //-------------------------------------------------------------------------------------

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public d4(Vector256<double> value) => Value = value;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public d4(double scalar) => Value = Vector256.Create(scalar);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public d4(double d0, double d1, double d2, double d3)
            => Value = Vector256.Create(d0, d1, d2, d3);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public d4(Vector128<double> upper, Vector128<double> lower) => Value = Vector256.Create(lower, upper);

        //-------------------------------------------------------------------------------------
        // Implicit operators 
        //-------------------------------------------------------------------------------------

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator Vector256<double>(d4 value) => value.Value;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator d4(Vector256<double> value) => new(value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator d4(double value) => new(value);

        //-------------------------------------------------------------------------------------
        // Constants
        //-------------------------------------------------------------------------------------

        public static d4 Zero = new(0);
        public static d4 One = new(1);
        public static d4 AllBitsSet = new(Vector256<double>.AllBitsSet);
        public static d4 SignMask = Vector256.Create(0x80000000u).AsDouble();
        public static d4 Indices => Vector256<double>.Indices;

        //-------------------------------------------------------------------------------------
        // Indexer
        //-------------------------------------------------------------------------------------

        public double this[int index]
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => Value.GetElement(index);
        }

        public int Count
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => 4;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double GetElement(int index) => Value.GetElement(index);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Vector128<double> GetLower() => Value.GetLower();

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Vector128<double> GetUpper() => Value.GetUpper();

        //-------------------------------------------------------------------------------------
        // Operator Overloads
        //-------------------------------------------------------------------------------------

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static d4 operator +(d4 left, d4 right) => Vector256.Add(left.Value, right.Value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static d4 operator -(d4 left, d4 right) => Vector256.Subtract(left.Value, right.Value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static d4 operator *(d4 left, d4 right) => Vector256.Multiply(left.Value, right.Value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static d4 operator *(d4 left, double scalar) => Vector256.Multiply(left.Value, scalar);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static d4 operator *(double scalar, d4 right) => Vector256.Multiply(scalar, right.Value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static d4 operator /(d4 left, d4 right) => Vector256.Divide(left.Value, right.Value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static d4 operator /(d4 left, double scalar) => Vector256.Divide(left.Value, scalar);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static d4 operator /(double scalar, d4 right) => Vector256.Divide(new d4(scalar), right.Value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static d4 operator -(d4 value) => Vector256.Negate(value.Value);

        //-------------------------------------------------------------------------------------
        // Bitwise functions
        //-------------------------------------------------------------------------------------

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static d4 AndNot(d4 a, d4 b) => Vector256.AndNot(a.Value, b.Value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static d4 operator &(d4 a, d4 b) => Vector256.BitwiseAnd(a.Value, b.Value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static d4 operator |(d4 a, d4 b) => Vector256.BitwiseOr(a.Value, b.Value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static d4 operator ~(d4 a) => Vector256.OnesComplement(a.Value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static d4 operator ^(d4 a, d4 b) => Vector256.Xor(a.Value, b.Value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static d4 ConditionalSelect(d4 condition, d4 a, d4 b) => Vector256.ConditionalSelect(condition.Value, a.Value, b.Value);

        //-------------------------------------------------------------------------------------
        // Comparison operators 
        //-------------------------------------------------------------------------------------

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static d4 operator ==(d4 a, d4 b) => Vector256.Equals(a.Value, b.Value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static d4 operator !=(d4 a, d4 b) => ~Vector256.Equals(a.Value, b.Value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static d4 operator <(d4 a, d4 b) => Vector256.LessThan(a.Value, b.Value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static d4 operator <=(d4 a, d4 b) => Vector256.LessThanOrEqual(a.Value, b.Value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static d4 operator >(d4 a, d4 b) => Vector256.GreaterThan(a.Value, b.Value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static d4 operator >=(d4 a, d4 b) => Vector256.GreaterThanOrEqual(a.Value, b.Value);

        //-------------------------------------------------------------------------------------
        // Comparison functions
        //-------------------------------------------------------------------------------------

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static d4 Max(d4 a, d4 b) => Vector256.Max(a.Value, b.Value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static d4 Min(d4 a, d4 b) => Vector256.Min(a.Value, b.Value);

        //-------------------------------------------------------------------------------------
        // Basic math functions 
        //-------------------------------------------------------------------------------------

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public d4 Sin() => Vector256.Sin(Value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public d4 Cos() => Vector256.Cos(Value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public (d4, d4) SinCos() => Vector256.SinCos(Value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public d4 Abs() => Vector256.Abs(Value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public d4 Ceiling() => Vector256.Ceiling(Value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public d4 Clamp(d4 min, d4 max) => Vector256.Clamp(Value, min.Value, max.Value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public d4 DegreesToRadians() => Vector256.DegreesToRadians(Value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public d4 CopySign(d4 value, d4 sign) => Vector256.CopySign(value.Value, sign.Value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Dot(d4 a, d4 b) => Vector256.Dot(a.Value, b.Value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public d4 Exp() => Vector256.Exp(Value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public d4 Floor() => Vector256.Floor(Value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static d4 Hypot(d4 x, d4 y) => Vector256.Hypot(x.Value, y.Value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public d4 IsNaN() => Vector256.IsNaN(Value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public d4 IsNegative() => Vector256.IsNegative(Value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public d4 IsPositive() => Vector256.IsPositive(Value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public d4 IsPositiveInfinity() => Vector256.IsPositiveInfinity(Value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public d4 IsZero() => Vector256.IsZero(Value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static d4 Lerp(d4 a, d4 b, d4 t) => Vector256.Lerp(a.Value, b.Value, t.Value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public d4 Log() => Vector256.Log(Value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public d4 Log2() => Vector256.Log2(Value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public d4 RadiansToDegrees() => Vector256.RadiansToDegrees(Value);

        /// <summary>Reciprocal (1/x) of each element</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public d4 Reciprocal() => One / this;

        /// <summary>Approximate reciprocal of the square root of each element: 1 / sqrt(x)</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public d4 ReciprocalSqrt() => One / Sqrt();

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public d4 Round(MidpointRounding mr) => Vector256.Round(Value, mr);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public d4 Sign() => Vector256.CopySign(One.Value, Value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public d4 Sqrt() => Vector256.Sqrt(Value);

        /// <summary>Square each element</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public d4 Square() => this * this;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double Sum() => Vector256.Sum(Value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public d4 Tan()
        {
            var (a, b) = SinCos();
            return a / b;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double FirstElement() => Vector256.ToScalar(Value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public d4 Truncate() => Vector256.Truncate(Value);

        //-------------------------------------------------------------------------------------
        // Pseudo-mutation operators 
        //-------------------------------------------------------------------------------------

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public d4 WithElement(int i, double d) => Vector256.WithElement(this, i, d);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public d4 WithLower(Vector128<double> lower) => Vector256.WithLower(this, lower);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public d4 WithUpper(Vector128<double> upper) => Vector256.WithUpper(this, upper);

        //-------------------------------------------------------------------------------------
        // Overrides
        //-------------------------------------------------------------------------------------

        public override string ToString()
            => $"[{this[0]}, {this[1]}, {this[2]}, {this[3]}]";

        public override bool Equals(object? obj)
            => obj is d4 other && Vector256.EqualsAll(Value, other.Value);

        public override int GetHashCode()
        {
            // Combine hash codes from each element
            int hash = 17;
            for (int i = 0; i < 8; i++)
                hash = hash * 31 + this[i].GetHashCode();
            return hash;
        }
    }
}