﻿using System;
using System.Collections.Generic;
using FluentAssertions;
using Xunit;

namespace Light.GuardClauses.Tests.TypeAssertionsTests
{
    public sealed class IsDerivingFromTests
    {
        // All the following facts (except the argument null tests) can be 
        // merged into a single theory when the xunit test runner correctly
        // executes theories with Type arguments: https://github.com/xunit/xunit/issues/1353
        [Fact]
        public void ReferenceTypeValidBaseClass()
        {
            TestIsDerivingFrom(typeof(string), typeof(object), true);
        }

        [Fact]
        public void ValueTypeValidBaseClass()
        {
            TestIsDerivingFrom(typeof(int), typeof(object), true);
        }

        [Fact]
        public void SameNonGenericType()
        {
            TestIsDerivingFrom(typeof(double), typeof(double), false);
            TestIsDerivingFrom(typeof(string), typeof(string), false);
        }

        [Fact]
        public void ValueTypeNonObjectBaseClass()
        {
            TestIsDerivingFrom(typeof(double), typeof(ValueType), true);
        }

        [Fact]
        public void EnumTypeValidBaseClass()
        {
            TestIsDerivingFrom(typeof(ConsoleColor), typeof(Enum), true);
            TestIsDerivingFrom(typeof(Enum), typeof(ConsoleColor), false);
        }

        [Fact]
        public void ClosedBoundGenericTypeWithValidGenericTypeDefinition()
        {
            TestIsDerivingFrom(typeof(StringDictionary), typeof(Dictionary<,>), true);
            TestIsDerivingFrom(typeof(Dictionary<,>), typeof(StringDictionary), false);
        }

        [Fact]
        public void ClosedBoundGenericTypeWithObject()
        {
            TestIsDerivingFrom(typeof(Dictionary<int, object>), typeof(object), true);
            TestIsDerivingFrom(typeof(object), typeof(Dictionary<int, object>), false);
        }

        [Fact]
        public void SameGenericType()
        {
            TestIsDerivingFrom(typeof(Dictionary<string, object>), typeof(Dictionary<string, object>), false);
            TestIsDerivingFrom(typeof(Dictionary<,>), typeof(Dictionary<string, object>), false);
        }

        [Fact]
        public void GenericTypeDefinitionOfSameType()
        {
            TestIsDerivingFrom(typeof(Dictionary<int, object>), typeof(Dictionary<,>), false);
        }

        [Fact]
        public void WrongBaseType()
        {
            TestIsDerivingFrom(typeof(Exception), typeof(ArgumentException), false);
        }

        private static void TestIsDerivingFrom(Type type, Type baseClass, bool expected)
        {
            type.IsDerivingFrom(baseClass).Should().Be(expected);
        }

        [Fact]
        public void TypeNull()
        {
            var type = default(Type);

            // ReSharper disable once ExpressionIsAlwaysNull
            Action act = () => type.IsDerivingFrom(typeof(object));

            act.Should().Throw<ArgumentNullException>()
               .And.ParamName.Should().Be(nameof(type));
        }

        [Fact]
        public void BaseClassNull()
        {
            var baseClass = default(Type);

            // ReSharper disable once ExpressionIsAlwaysNull
            Action act = () => typeof(string).IsDerivingFrom(baseClass);

            act.Should().Throw<ArgumentNullException>()
               .And.ParamName.Should().Be(nameof(baseClass));
        }

        public sealed class StringDictionary : Dictionary<string, object> { }
    }
}