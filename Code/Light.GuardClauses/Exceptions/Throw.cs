﻿using System;

namespace Light.GuardClauses.Exceptions
{
    /// <summary>
    ///     Provides static methods that throw default exceptions.
    /// </summary>
    public abstract class Throw
    {
        /// <summary>
        ///     Throws the default <see cref="ArgumentNullException" />, using the optional parameter name and message.
        /// </summary>
        public static void ArgumentNull(string parameterName = null, string message = null)
        {
            throw new ArgumentNullException(parameterName, message ?? $"{parameterName ?? "The value"} must not be null.");
        }

        /// <summary>
        ///     Throws the default <see cref="ArgumentDefaultException" /> indicating that a value is the default value of its type, using the optional parameter name and message.
        /// </summary>
        public static void ArgumentDefault(string parameterName = null, string message = null)
        {
            throw new ArgumentDefaultException(parameterName, message ?? $"{parameterName ?? "The value"} must not be the default value.");
        }

        /// <summary>
        ///     Throws the default <see cref="ArgumentNotNullException" /> indicating that a value is not null, using the optional parameter name and message.
        /// </summary>
        public static void ArgumentNotNull(object value, string parameterName = null, string message = null)
        {
            throw new ArgumentNotNullException(parameterName, message ?? $"{parameterName ?? "The value"} must be null, but it actually is \"{value}\".");
        }

        /// <summary>
        ///     Throws the default <see cref="TypeMismatchException" /> indicating that a reference cannot be downcasted, using the optional parameter name and message.
        /// </summary>
        public static void TypeMismatch(object parameter, Type targetType, string parameterName = null, string message = null)
        {
            throw new TypeMismatchException(parameterName, message ?? $"{parameterName ?? "The object"} \"{parameter}\" cannot be downcasted to \"{targetType}\".");
        }

        /// <summary>
        ///     Throws the default <see cref="NullableHasNoValueException" /> indicating that a <see cref="Nullable{T}" /> has no value, using the optional parameter name and message.
        /// </summary>
        public static void NullableHasNoValue(string parameterName = null, string message = null)
        {
            throw new NullableHasNoValueException(parameterName, message ?? $"{parameterName ?? "The nullable"} must have a value, but it actually is null.");
        }

        /// <summary>
        ///     Throws the default <see cref="NullableHasValueException" /> indicating that a <see cref="Nullable{T}" /> has a value, using the optional paramter name and message.
        /// </summary>
        public static void NullableHasValue<T>(T value, string parameterName = null, string message = null) where T : struct
        {
            throw new NullableHasValueException(parameterName, message ?? $"{parameterName ?? "The nullable"} must have no value, but it actually is \"{value}\".");
        }

        /// <summary>
        ///     Throws the default <see cref="EnumValueNotDefinedException" /> indicating that a value is not one of the constants defined in an enum, using the optional paramter name and message.
        /// </summary>
        public static void EnumValueNotDefined<T>(T parameter, string parameterName = null, string message = null)
        {
            throw new EnumValueNotDefinedException(parameterName, message ?? $"{parameterName ?? "The value"} \"{parameter}\" must be one of the defined constants of enum \"{parameter.GetType()}\", but it is not.");
        }

        /// <summary>
        ///     Throws the default <see cref="EmptyGuidException" /> indicating that a GUID is empty, using the optional parameter name and message.
        /// </summary>
        public static void EmptyGuid(string parameterName = null, string message = null)
        {
            throw new EmptyGuidException(parameterName, message ?? $"{parameterName ?? "The value"} must be a valid GUID, but it actually is an empty one.");
        }

        /// <summary>
        ///     Throws the default <see cref="ArgumentException" /> indicating that a boolean is true instead of false, using the optional parameter name and message.
        /// </summary>
        public static void BooleanTrue(string parameterName = null, string message = null)
        {
            throw new ArgumentException(message ?? $"{parameterName ?? "The boolean value"} must be false, but it actually is true.", parameterName);
        }

        /// <summary>
        ///     Throws the default <see cref="ArgumentException" /> indicating that a boolean is false instead of true, using the optional parameter name and message.
        /// </summary>
        public static void BooleanFalse(string parameterName = null, string message = null)
        {
            throw new ArgumentException(message ?? $"{parameterName ?? "The boolean value"} must be true, but it actually is false.", parameterName);
        }

        /// <summary>
        ///     Throws the default <see cref="ArgumentOutOfRangeException" /> indicating that a comparable value must not be less than the given boundary value, using the optional parameter name and message.
        /// </summary>
        public static void MustNotBeLessThan<T>(T parameter, T boundary, string parameterName, string message) where T : IComparable<T>
        {
            throw new ArgumentOutOfRangeException(parameterName, message ?? $"{parameterName ?? "The value"} must not be less than {boundary}, but it actually is {parameter}.");
        }

        /// <summary>
        ///     Throws the exception that is returned by <paramref name="exceptionFactory" />.
        /// </summary>
        public static void CustomException(Func<Exception> exceptionFactory)
        {
            throw exceptionFactory();
        }

        /// <summary>
        ///     Throws the exception that is returned by <paramref name="exceptionFactory" />. <paramref name="value" /> is passed to <paramref name="exceptionFactory" />.
        /// </summary>
        public static void CustomException<T>(Func<T, Exception> exceptionFactory, T value)
        {
            throw exceptionFactory(value);
        }
    }
}