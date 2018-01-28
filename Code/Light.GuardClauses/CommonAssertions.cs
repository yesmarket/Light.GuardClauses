using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.CompilerServices;
using Light.GuardClauses.Exceptions;
using Light.GuardClauses.FrameworkExtensions;

namespace Light.GuardClauses
{
    /// <summary>
    ///     The <see cref="CommonAssertions" /> class contains the most common assertions like MustNotBeNull and assertions that are not directly related to
    ///     any categories like collection assertions or string assertions.
    /// </summary>
    public static class CommonAssertions
    {
        /// <summary>
        ///     Ensures that the specified parameter is not null, or otherwise throws an <see cref="ArgumentNullException" />.
        /// </summary>
        /// <typeparam name="T">The type of the parameter to be checked.</typeparam>
        /// <param name="parameter">The value to be checked  for null.</param>
        /// <param name="parameterName">The name of the parameter (optional).</param>
        /// <param name="message">The message that will be injected into the <see cref="ArgumentNullException" />.</param>
        /// <exception cref="ArgumentNullException"> Thrown when <paramref name="parameter" /> is null.</exception>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T MustNotBeNull<T>(this T parameter, string parameterName = null, string message = null) where T : class
        {
            if (parameter == null)
                Throw.ArgumentNull(parameterName, message);
            return parameter;
        }

        /// <summary>
        ///     Ensures that the specified parameter is not null, or otherwise throws your custom exception.
        /// </summary>
        /// <typeparam name="T">The type of the parameter to be checked.</typeparam>
        /// <param name="parameter">The value to be checked for null.</param>
        /// <param name="exceptionFactory">The delegate that creates the exception to be thrown.</param>
        /// <exception cref="Exception">Your custom exception when <paramref name="parameter" /> is null.</exception>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T MustNotBeNull<T>(this T parameter, Func<Exception> exceptionFactory) where T : class
        {
            if (parameter == null)
                Throw.CustomException(exceptionFactory);

            return parameter;
        }

        /// <summary>
        ///     Ensures that the specified parameter is not the default value, or otherwise throws an <see cref="ArgumentNullException" />
        ///     for reference types, or an <see cref="ArgumentDefaultException" /> for value types.
        /// </summary>
        /// <typeparam name="T">The type of the parameter to be checked.</typeparam>
        /// <param name="parameter">The parameter to be checked.</param>
        /// <param name="parameterName">The name of the parameter (optional).</param>
        /// <param name="message">The message that will be injected into the <see cref="ArgumentNullException" /> or <see cref="ArgumentDefaultException" /> (optional).</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="parameter" /> is null.</exception>
        /// <exception cref="ArgumentDefaultException">Thrown when <paramref name="parameter" /> is a value type and the default value.</exception>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T MustNotBeDefault<T>(this T parameter, string parameterName = null, string message = null)
        {
            if (default(T) == null)
            {
                if (parameter == null)
                    Throw.ArgumentNull(parameterName, message);
                return parameter;
            }

            if (parameter.Equals(default(T)))
                Throw.ArgumentDefault(parameterName, message);
            return parameter;
        }


        /// <summary>
        ///     Ensures that the specified parameter is not the default value, or otherwise throws your custom exception.
        /// </summary>
        /// <typeparam name="T">The type of the parameter to be checked.</typeparam>
        /// <param name="parameter">The parameter to be checked.</param>
        /// <param name="exceptionFactory">The delegate that creates the exception to be thrown.</param>
        /// <exception cref="Exception">Your custom exception when <paramref name="parameter" /> is the default value.</exception>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T MustNotBeDefault<T>(this T parameter, Func<Exception> exceptionFactory)
        {
            if (default(T) == null)
            {
                if (parameter == null)
                    Throw.CustomException(exceptionFactory);
                return default(T);
            }

            if (parameter.Equals(default(T)))
                Throw.CustomException(exceptionFactory);
            return parameter;
        }

        /// <summary>
        ///     Ensures that the specified parameter is not null when <typeparamref name="T" /> is a reference type, or otherwise
        ///     throws an <see cref="ArgumentNullException" />. NOTICE: you should only use this assertion in generic contexts,
        ///     use <see cref="MustNotBeNull{T}(T,string,string)" /> by default.
        /// </summary>
        /// <typeparam name="T">The type of the parameter to be checked.</typeparam>
        /// <param name="parameter">The parameter to be checked.</param>
        /// <param name="parameterName">The name of the parameter (optional).</param>
        /// <param name="message">The message that will be injected into the <see cref="ArgumentNullException" /> (optional).</param>
        /// <exception cref="ArgumentNullException">Thrown when <typeparamref name="T" /> is a reference type and <paramref name="parameter" /> is null.</exception>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T MustNotBeNullReference<T>(this T parameter, string parameterName = null, string message = null)
        {
            if (default(T) != null)
                return parameter;

            if (parameter == null)
                Throw.ArgumentNull(parameterName, message);
            return parameter;
        }

        /// <summary>
        ///     Ensures that the specified parameter is not null when <typeparamref name="T" /> is a reference type, or otherwise
        ///     throws your custom exception. NOTICE: you should only use this assertion in generic contexts,
        ///     use <see cref="MustNotBeNull{T}(T,Func{Exception})" /> by default.
        /// </summary>
        /// <typeparam name="T">The type of the parameter to be checked.</typeparam>
        /// <param name="parameter">The parameter to be checked.</param>
        /// <param name="exceptionFactory">The delegate that creates the exception to be thrown.</param>
        /// <exception cref="Exception">Your custom exception when <typeparamref name="T" /> is a reference type and <paramref name="parameter" /> is null.</exception>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T MustNotBeNullReference<T>(this T parameter, Func<Exception> exceptionFactory)
        {
            if (default(T) != null)
                return parameter;

            if (parameter == null)
                Throw.CustomException(exceptionFactory);
            return parameter;
        }

        /// <summary>
        ///     Ensures that the specified parameter is null, or otherwise throws an <see cref="ArgumentNotNullException" />.
        /// </summary>
        /// <typeparam name="T">The type of the parameter to be checked.</typeparam>
        /// <param name="parameter">The parameter to be checked.</param>
        /// <param name="parameterName">The name of the parameter (optional).</param>
        /// <param name="message">The message that will be injected into the <see cref="ArgumentNotNullException" /> (optional).</param>
        /// <exception cref="ArgumentNotNullException">Thrown when <paramref name="parameter" /> is not null.</exception>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T MustBeNull<T>(this T parameter, string parameterName = null, string message = null) where T : class
        {
            if (parameter != null)
                Throw.ArgumentNotNull(parameter, parameterName, message);
            return null;
        }

        /// <summary>
        ///     Ensures that the specified parameter is null, or otherwise throws your custom exception.
        /// </summary>
        /// <typeparam name="T">The type of the parameter to be checked.</typeparam>
        /// <param name="parameter">The parameter to be checked.</param>
        /// <param name="exceptionFactory">The delegate that creates the exception to be thrown.</param>
        /// <exception cref="Exception">Your custom exception when <paramref name="parameter" /> is not null.</exception>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T MustBeNull<T>(this T parameter, Func<Exception> exceptionFactory) where T : class
        {
            if (parameter != null)
                Throw.CustomException(exceptionFactory);
            return null;
        }

        /// <summary>
        ///     Ensures that the specified parameter is null, or otherwise throws your custom exception.
        /// </summary>
        /// <typeparam name="T">The type of the parameter to be checked.</typeparam>
        /// <param name="parameter">The parameter to be checked.</param>
        /// <param name="exceptionFactory">The delegate that creates the exception to be thrown.</param>
        /// <exception cref="Exception">Your custom exception when <paramref name="parameter" /> is not null.</exception>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T MustBeNull<T>(this T parameter, Func<T, Exception> exceptionFactory) where T : class
        {
            if (parameter != null)
                Throw.CustomException(exceptionFactory, parameter);
            return null;
        }

        /// <summary>
        ///     Ensures that parameter is of the specified type and returns the downcasted value, or otherwise throws a <see cref="TypeMismatchException" />.
        /// </summary>
        /// <typeparam name="T">The type of the parameter to be checked.</typeparam>
        /// <param name="parameter">The parameter to be checked.</param>
        /// <param name="parameterName">The name of the parameter (optional).</param>
        /// <param name="message">The message that will be injected into the <see cref="TypeMismatchException" /> (optional). </param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="parameter" /> is null.</exception>
        /// <exception cref="TypeMismatchException">Thrown when <paramref name="parameter" /> cannot be downcasted to the specified value.</exception>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T MustBeOfType<T>(this object parameter, string parameterName = null, string message = null) where T : class
        {
            if (parameter.MustNotBeNull(parameterName) is T castedValue)
                return castedValue;

            Throw.TypeMismatch(parameter, typeof(T), parameterName, message);
            return null;
        }

        /// <summary>
        ///     Ensures that parameter is of the specified type and returns the downcasted value, or otherwise throws your custom exception.
        /// </summary>
        /// <typeparam name="T">The type of the parameter to be checked.</typeparam>
        /// <param name="parameter">The parameter to be checked.</param>
        /// <param name="exceptionFactory">The delegate that creates the exception to be thrown.</param>
        /// <param name="parameterName">The name of the parameter (optional).</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="parameter" /> is null.</exception>
        /// <exception cref="Exception">Your custom exception thrown when <paramref name="parameter" /> cannot be downcasted to the specified value.</exception>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T MustBeOfType<T>(this object parameter, Func<Exception> exceptionFactory, string parameterName = null) where T : class
        {
            if (parameter.MustNotBeNull(parameterName) is T castedValue)
                return castedValue;

            Throw.CustomException(exceptionFactory);
            return null;
        }

        /// <summary>
        ///     Ensures that parameter is of the specified type and returns the downcasted value, or otherwise throws your custom exception.
        /// </summary>
        /// <typeparam name="T">The type of the parameter to be checked.</typeparam>
        /// <param name="parameter">The parameter to be checked.</param>
        /// <param name="exceptionFactory">The delegate that creates the exception to be thrown.</param>
        /// <param name="parameterName">The name of the parameter (optional).</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="parameter" /> is null.</exception>
        /// <exception cref="Exception">Your custom exception thrown when <paramref name="parameter" /> cannot be downcasted to the specified value.</exception>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T MustBeOfType<T>(this object parameter, Func<object, Exception> exceptionFactory, string parameterName = null) where T : class
        {
            if (parameter.MustNotBeNull(parameterName) is T castedValue)
                return castedValue;

            Throw.CustomException(exceptionFactory, parameter);
            return null;
        }

        /// <summary>
        ///     Ensures that the specified nullable has a value, or otherwise throws a <see cref="NullableHasNoValueException" />.
        /// </summary>
        /// <typeparam name="T">The type of the struct encapsulated by the nullable.</typeparam>
        /// <param name="parameter">The parameter to be checked.</param>
        /// <param name="parameterName">The name of the parameter (optional).</param>
        /// <param name="message">The message that will be injected into the <see cref="NullableHasNoValueException" /> (optional).</param>
        /// <exception cref="NullableHasNoValueException">Thrown when <paramref name="parameter" /> is null.</exception>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T? MustHaveValue<T>(this T? parameter, string parameterName = null, string message = null) where T : struct
        {
            if (parameter.HasValue == false)
                Throw.NullableHasNoValue(parameterName, message);
            return parameter;
        }

        /// <summary>
        ///     Ensures that the specified nullable has a value, or otherwise throws your custom exception.
        /// </summary>
        /// <typeparam name="T">The type of the struct encapsulated by the nullable.</typeparam>
        /// <param name="parameter">The parameter to be checked.</param>
        /// <param name="exceptionFactory">The delegate that creates the exception to be thrown.</param>
        /// <exception cref="Exception">Your custom exception thrown when <paramref name="parameter" /> is null.</exception>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T? MustHaveValue<T>(this T? parameter, Func<Exception> exceptionFactory) where T : struct
        {
            if (parameter.HasValue == false)
                Throw.CustomException(exceptionFactory);
            return parameter;
        }

        /// <summary>
        ///     Ensures that the specified nullable has no value, or otherwise throws a <see cref="NullableHasValueException" />.
        /// </summary>
        /// <typeparam name="T">The type of the struct encapsulated by the nullable.</typeparam>
        /// <param name="parameter">The parameter to be checked.</param>
        /// <param name="parameterName">The name of the parameter (optional).</param>
        /// <param name="message">The message that will be injected into the <see cref="NullableHasNoValueException" /> (optional).</param>
        /// <exception cref="NullableHasValueException">Thrown when <paramref name="parameter" /> has a value.</exception>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T? MustNotHaveValue<T>(this T? parameter, string parameterName = null, string message = null) where T : struct
        {
            if (parameter.HasValue)
                Throw.NullableHasValue(parameter.Value, parameterName, message);
            return null;
        }

        /// <summary>
        ///     Ensures that the specified nullable has no value, or otherwise throws your custom exception.
        /// </summary>
        /// <typeparam name="T">The type of the struct encapsulated by the nullable.</typeparam>
        /// <param name="parameter">The parameter to be checked.</param>
        /// <param name="exceptionFactory">The delegate that creates the exception to be thrown.</param>
        /// <exception cref="Exception">Your custom exception thrown when <paramref name="parameter" /> has a value.</exception>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T? MustNotHaveValue<T>(this T? parameter, Func<Exception> exceptionFactory) where T : struct
        {
            if (parameter.HasValue)
                Throw.CustomException(exceptionFactory);
            return null;
        }

        /// <summary>
        ///     Ensures that the specified nullable has no value, or otherwise throws your custom exception.
        /// </summary>
        /// <typeparam name="T">The type of the struct encapsulated by the nullable.</typeparam>
        /// <param name="parameter">The parameter to be checked.</param>
        /// <param name="exceptionFactory">The delegate that creates the exception to be thrown.</param>
        /// <exception cref="Exception">Your custom exception thrown when <paramref name="parameter" /> has a value.</exception>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T? MustNotHaveValue<T>(this T? parameter, Func<T, Exception> exceptionFactory) where T : struct
        {
            if (parameter.HasValue)
                Throw.CustomException(exceptionFactory, parameter.Value);
            return null;
        }

        /// <summary>
        ///     Ensures that the specified value is defined in its corresponding enum type, or otherwise throws an <see cref="EnumValueNotDefinedException" />.
        /// </summary>
        /// <typeparam name="T">The enum type of the parameter.</typeparam>
        /// <param name="parameter">The parameter to be checked.</param>
        /// <param name="parameterName">The name of the parameter (optional).</param>
        /// <param name="message">The message that will be injected into the <see cref="EnumValueNotDefinedException" /> (optional).</param>
        /// <exception cref="EnumValueNotDefinedException">Thrown when the specified enum value is not defined.</exception>
        /// <exception cref="ArgumentException">Thrown when <paramref name="parameter" /> is not a value of an enum.</exception>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T MustBeValidEnumValue<T>(this T parameter, string parameterName = null, string message = null)
        {
            if (parameter.IsValidEnumValue() == false)
                Throw.EnumValueNotDefined(parameter, parameterName, message);
            return parameter;
        }

        /// <summary>
        ///     Ensures that the specified value is defined in its corresponding enum type, or otherwise throws an <see cref="EnumValueNotDefinedException" />.
        /// </summary>
        /// <typeparam name="T">The enum type of the parameter.</typeparam>
        /// <param name="parameter">The parameter to be checked.</param>
        /// <param name="exceptionFactory">The delegate that creates the exception to be thrown.</param>
        /// <exception cref="Exception">Your custom exception thrown when the specified enum value is not defined.</exception>
        /// <exception cref="ArgumentException">Thrown when <paramref name="parameter" /> is not a value of an enum.</exception>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T MustBeValidEnumValue<T>(this T parameter, Func<Exception> exceptionFactory)
        {
            if (parameter.IsValidEnumValue() == false)
                Throw.CustomException(exceptionFactory);
            return parameter;
        }

        /// <summary>
        ///     Ensures that the specified value is defined in its corresponding enum type, or otherwise throws an <see cref="EnumValueNotDefinedException" />.
        /// </summary>
        /// <typeparam name="T">The enum type of the parameter.</typeparam>
        /// <param name="parameter">The parameter to be checked.</param>
        /// <param name="exceptionFactory">The delegate that creates the exception to be thrown.</param>
        /// <exception cref="Exception">Your custom exception thrown when the specified enum value is not defined.</exception>
        /// <exception cref="ArgumentException">Thrown when <paramref name="parameter" /> is not a value of an enum.</exception>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T MustBeValidEnumValue<T>(this T parameter, Func<T, Exception> exceptionFactory)
        {
            if (parameter.IsValidEnumValue() == false)
                Throw.CustomException(exceptionFactory, parameter);
            return parameter;
        }

        /// <summary>
        ///     Checks if the specified value is a valid enum value of its type.
        /// </summary>
        /// <param name="parameter">The enum value to be checked.</param>
        /// <returns>True if the specified value is a valid value of an enum type, else false.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="parameter" /> is null.</exception>
        /// <exception cref="ArgumentException">Thrown when <paramref name="parameter" /> is not a value of an enum.</exception>
        public static bool IsValidEnumValue(this object parameter)
        {
            var enumType = parameter.MustNotBeNull(nameof(parameter)).GetType();
            var typeInfo = enumType.GetTypeInfo();
            if (typeInfo.IsEnum == false)
                throw new ArgumentException($"The value \"{parameter}\" is of type \"{typeInfo}\" which is not an enum type.", nameof(parameter));

            var fields = typeInfo.DeclaredFields.AsReadOnlyList();

            // If enum does not have the flags attribute, then just get all fields via reflection and check if one is equal to the given value
            if (typeInfo.GetCustomAttribute(Types.FlagsAttributeType) == null)
            {
                for (var i = 0; i < fields.Count; i++)
                {
                    var field = fields[i];
                    if (field.IsStatic &&
                        field.IsLiteral &&
                        field.GetValue(null).Equals(parameter))
                        return true;
                }

                return false;
            }

            // Else check if the value is a valid flags combination
            var enumInfo = EnumInfo.FromEnumFields(fields);
            if (enumInfo.NumberOfConstants == 0)
                return false;

            return enumInfo.UnderlyingType == Types.UInt64Type ? CheckEnumFlagsUsingUInt64Conversion(Convert.ToUInt64(parameter), fields, enumInfo.NumberOfConstants) : CheckEnumFlagsUsingInt64Conversion(Convert.ToInt64(parameter), fields, enumInfo.NumberOfConstants);
        }

        private static bool CheckEnumFlagsUsingInt64Conversion(long parameterValue, IReadOnlyList<FieldInfo> enumFields, int numberOfEnumConstants)
        {
            var enumValues = new long[numberOfEnumConstants];
            var currentIndex = 0;
            for (var i = 0; i < enumFields.Count; i++)
            {
                var field = enumFields[i];
                if (!field.IsStatic || !field.IsLiteral)
                    continue;
                var enumValue = Convert.ToInt64(field.GetValue(null));
                if (enumValue == parameterValue)
                    return true;

                enumValues[currentIndex++] = enumValue;
            }

            Array.Sort(enumValues);

            var bit = 1L;
            if (parameterValue < bit)
                return false;

            var currentStartIndex = 0;

            while (true)
            {
                if ((bit & parameterValue) != 0)
                {
                    currentStartIndex = UpdateIndexIfNecessary(bit, currentStartIndex, enumValues);
                    if (currentStartIndex == -1)
                        return false;

                    if (FindEnumFlag(bit, currentStartIndex, enumValues) == false)
                        return false;
                }

                var newBit = bit << 1;
                if (newBit > parameterValue || newBit < bit)
                    break;

                bit = newBit;
            }

            return true;

            int UpdateIndexIfNecessary(long currentBit, int index, long[] sortedEnumValues)
            {
                var value = sortedEnumValues[index];
                while (value < currentBit)
                {
                    if (++index >= sortedEnumValues.Length)
                        return -1;

                    value = sortedEnumValues[index];
                }

                return index;
            }

            bool FindEnumFlag(long currentBit, int startingIndex, long[] sortedEnumValues)
            {
                for (var i = startingIndex; i < sortedEnumValues.Length; i++)
                {
                    var enumValue = sortedEnumValues[i];
                    if ((enumValue & currentBit) != 0)
                        return true;
                }

                return false;
            }
        }

        private static bool CheckEnumFlagsUsingUInt64Conversion(ulong parameterValue, IReadOnlyList<FieldInfo> enumFields, int numberOfEnumConstants)
        {
            var enumValues = new ulong[numberOfEnumConstants];
            var currentIndex = 0;
            for (var i = 0; i < enumFields.Count; i++)
            {
                var field = enumFields[i];
                if (!field.IsStatic || !field.IsLiteral)
                    continue;
                var enumValue = Convert.ToUInt64(field.GetValue(null));
                if (enumValue == parameterValue)
                    return true;

                enumValues[currentIndex++] = enumValue;
            }

            Array.Sort(enumValues);

            var bit = 1UL;
            if (parameterValue < bit)
                return false;

            var currentStartIndex = 0;

            while (true)
            {
                if ((bit & parameterValue) != 0)
                {
                    currentStartIndex = UpdateIndexIfNecessary(bit, currentStartIndex, enumValues);
                    if (currentStartIndex == -1)
                        return false;

                    if (FindEnumFlag(bit, currentStartIndex, enumValues) == false)
                        return false;
                }

                var newBit = bit << 1;
                if (newBit > parameterValue || newBit < bit)
                    break;

                bit = newBit;
            }

            return true;

            int UpdateIndexIfNecessary(ulong currentBit, int index, ulong[] sortedEnumValues)
            {
                var value = sortedEnumValues[index];
                while (value < currentBit)
                {
                    if (++index >= sortedEnumValues.Length)
                        return -1;

                    value = sortedEnumValues[index];
                }

                return index;
            }

            bool FindEnumFlag(ulong currentBit, int startingIndex, ulong[] sortedEnumValues)
            {
                for (var i = startingIndex; i < sortedEnumValues.Length; i++)
                {
                    var enumValue = sortedEnumValues[i];
                    if ((enumValue & currentBit) != 0)
                        return true;
                }

                return false;
            }
        }

        /// <summary>
        ///     Ensures that the specified GUID is not empty, or otherwise throws an <see cref="EmptyGuidException" />.
        /// </summary>
        /// <param name="parameter">The parameter to be checked.</param>
        /// <param name="parameterName">The name of the parameter (optional).</param>
        /// <param name="message">The message that will be injected into the <see cref="EmptyGuidException" /> (optional).</param>
        /// <exception cref="EmptyGuidException">Thrown when <paramref name="parameter" /> is an empty GUID.</exception>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Guid MustNotBeEmpty(this Guid parameter, string parameterName = null, string message = null)
        {
            if (parameter == Guid.Empty)
                Throw.EmptyGuid(parameterName, message);
            return parameter;
        }

        /// <summary>
        ///     Ensures that the specified GUID is not empty, or otherwise throws your custom exception.
        /// </summary>
        /// <param name="parameter">The parameter to be checked.</param>
        /// <param name="exceptionFactory">The delegate that creates the exception to be thrown.</param>
        /// <exception cref="Exception">Your custom exception thrown when <paramref name="parameter" /> is an empty GUID.</exception>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Guid MustNotBeEmpty(this Guid parameter, Func<Exception> exceptionFactory)
        {
            if (parameter == Guid.Empty)
                Throw.CustomException(exceptionFactory);
            return parameter;
        }

        /// <summary>
        ///     Checks if the specified GUID is an empty one.
        /// </summary>
        /// <param name="parameter">The GUID to be checked.</param>
        /// <returns>True if the GUID is empty, else false.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsEmpty(this Guid parameter)
        {
            return parameter == Guid.Empty;
        }

        /// <summary>
        ///     Ensures that the specified Boolean value is false, or otherwise throws an <see cref="ArgumentException" />.
        /// </summary>
        /// <param name="parameter">The paramter to be checked.</param>
        /// <param name="parameterName">The name of the parameter (optional).</param>
        /// <param name="message">The message that will be injected into the <see cref="ArgumentException" /> (optional).</param>
        /// <param name="exception">
        ///     The exception that is thrown when the specified bool is true (optional).
        ///     Please note that <paramref name="message" /> and <paramref name="parameterName" /> are both ignored when you specify exception.
        /// </param>
        /// <exception cref="ArgumentException">Thrown when <paramref name="parameter" /> is true and no <paramref name="exception" /> is specified.</exception>
        public static bool MustBeFalse(this bool parameter, string parameterName = null, string message = null, Func<Exception> exception = null)
        {
            if (parameter == false)
                return false;

            throw exception?.Invoke() ?? new ArgumentException(message ?? $"{parameterName ?? "The value"} must be false, but you specified true.", parameterName);
        }

        /// <summary>
        ///     Ensures that the specified Boolean value is true, or otherwise throws an <see cref="ArgumentException" />.
        /// </summary>
        /// <param name="parameter">The paramter to be checked.</param>
        /// <param name="parameterName">The name of the parameter (optional).</param>
        /// <param name="message">The message that will be injected into the <see cref="ArgumentException" /> (optional).</param>
        /// <param name="exception">
        ///     The exception that is thrown when the specified bool is false (optional).
        ///     Please note that <paramref name="message" /> and <paramref name="parameterName" /> are both ignored when you specify exception.
        /// </param>
        /// <exception cref="ArgumentException">Thrown when <paramref name="parameter" /> is false and no <paramref name="exception" /> is specified.</exception>
        public static bool MustBeTrue(this bool parameter, string parameterName = null, string message = null, Func<Exception> exception = null)
        {
            if (parameter)
                return true;

            throw exception?.Invoke() ?? new ArgumentException(message ?? $"{parameterName ?? "The value"} must be true, but you specified false.", parameterName);
        }

        /// <summary>
        ///     Ensures that the specified Boolean value is not false, or otherwise throws an <see cref="ArgumentException" />.
        /// </summary>
        /// <param name="parameter">The paramter to be checked.</param>
        /// <param name="parameterName">The name of the parameter (optional).</param>
        /// <param name="message">The message that will be injected into the <see cref="ArgumentException" /> (optional).</param>
        /// <param name="exception">
        ///     The exception that is thrown when the specified bool is false (optional).
        ///     Please note that <paramref name="message" /> and <paramref name="parameterName" /> are both ignored when you specify exception.
        /// </param>
        /// <exception cref="ArgumentException">Thrown when <paramref name="parameter" /> is true and no <paramref name="exception" /> is specified.</exception>
        public static bool MustNotBeFalse(this bool parameter, string parameterName = null, string message = null, Func<Exception> exception = null)
        {
            if (parameter) return true;

            throw exception?.Invoke() ?? new ArgumentException(message ?? $"{parameterName ?? "The value"} must not be false, but you specified false.", parameterName);
        }

        /// <summary>
        ///     Ensures that the specified Boolean value is not true, or otherwise throws an <see cref="ArgumentException" />.
        /// </summary>
        /// <param name="parameter">The paramter to be checked.</param>
        /// <param name="parameterName">The name of the parameter (optional).</param>
        /// <param name="message">The message that will be injected into the <see cref="ArgumentException" /> (optional).</param>
        /// <param name="exception">
        ///     The exception that is thrown when the specified bool is true (optional).
        ///     Please note that <paramref name="message" /> and <paramref name="parameterName" /> are both ignored when you specify exception.
        /// </param>
        /// <exception cref="ArgumentException">Thrown when <paramref name="parameter" /> is false and no <paramref name="exception" /> is specified.</exception>
        public static bool MustNotBeTrue(this bool parameter, string parameterName = null, string message = null, Func<Exception> exception = null)
        {
            if (parameter == false) return false;

            throw exception?.Invoke() ?? new ArgumentException(message ?? $"{parameterName ?? "The value"} must not be true, but you specified true.", parameterName);
        }

        private struct EnumInfo
        {
            public readonly int NumberOfConstants;
            public readonly Type UnderlyingType;

            private EnumInfo(int numberOfConstants, Type underlyingType)
            {
                NumberOfConstants = numberOfConstants.MustBeGreaterThanOrEqualTo(0, nameof(numberOfConstants));
                if (numberOfConstants > 0)
                    underlyingType.MustNotBeNull(nameof(underlyingType));
                UnderlyingType = underlyingType;
            }

            public static EnumInfo FromEnumFields(IReadOnlyList<FieldInfo> enumFields)
            {
                var numberOfConstants = default(int);
                var underlyingType = default(Type);

                for (var i = 0; i < enumFields.Count; i++)
                {
                    var field = enumFields[i];

                    if (underlyingType == null && field.IsSpecialName && field.IsPublic)
                        underlyingType = field.FieldType;

                    if (field.IsStatic == false || field.IsLiteral == false)
                        continue;

                    numberOfConstants++;
                }

                return new EnumInfo(numberOfConstants, underlyingType);
            }
        }
    }
}