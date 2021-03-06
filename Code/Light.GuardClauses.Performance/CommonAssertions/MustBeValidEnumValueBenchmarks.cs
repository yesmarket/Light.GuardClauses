﻿using System;
using System.Reflection;
using BenchmarkDotNet.Attributes;
using Light.GuardClauses.Exceptions;

namespace Light.GuardClauses.Performance.CommonAssertions
{
    public class MustBeValidEnumValueBenchmarks : DefaultBenchmark
    {
        public readonly ConsoleColor EnumValue = ConsoleColor.Blue;
        public readonly BindingFlags FlagsEnumValue = BindingFlags.Static | BindingFlags.Public | BindingFlags.DeclaredOnly;

        [Benchmark(Baseline = true)]
        public ConsoleColor NoFlagsBaseVersion()
        {
            if (Enum.IsDefined(typeof(ConsoleColor), EnumValue) == false) throw new EnumValueNotDefinedException(nameof(EnumValue));
            return EnumValue;
        }

        [Benchmark]
        public ConsoleColor LightGuardClausesNoFlagsWithParameterName() => EnumValue.MustBeValidEnumValue(nameof(EnumValue));

        [Benchmark]
        public ConsoleColor LightGuardCLausesNoFlagsWithCustomException() => EnumValue.MustBeValidEnumValue(() => new Exception());

        [Benchmark]
        public ConsoleColor LightGuardClausesNoFlagsWithCustomParameterizedException() => EnumValue.MustBeValidEnumValue(v => new Exception($"Value {v} is not defined in enum."));

        [Benchmark]
        public ConsoleColor OldVersionNoFlags() => EnumValue.OldMustBeValidEnumValue(nameof(EnumValue));

        [Benchmark]
        public BindingFlags LightGuardClausesFlagsWithParameterName() => FlagsEnumValue.MustBeValidEnumValue(nameof(FlagsEnumValue));

        [Benchmark]
        public BindingFlags LightGuardClausesFlagsWithCustomException() => FlagsEnumValue.MustBeValidEnumValue(() => new Exception());

        [Benchmark]
        public BindingFlags LightGuardClausesFlagsWithCustomParameterizedException() => FlagsEnumValue.MustBeValidEnumValue(v => new Exception($"Value {v} is not defined in enum."));
    }

    public static class MustBeValidEnumValueExtensionMethods
    {
        public static T OldMustBeValidEnumValue<T>(this T parameter, string parameterName = null, string message = null, Func<Exception> exception = null) where T : struct, IConvertible, IComparable, IFormattable
        {
            if (parameter.IsValidEnumValue())
                return parameter;

            throw exception?.Invoke() ?? new EnumValueNotDefinedException(parameterName, message ?? $"{parameterName ?? "The value"} \"{parameter}\" must be one of the defined constants of enum \"{parameter.GetType()}\", but it is not.");
        }
    }
}