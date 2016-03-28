﻿using System;
using FluentAssertions;
using Light.GuardClauses.Tests.CustomMessagesAndExceptions;
using Xunit;

namespace Light.GuardClauses.Tests
{
    public sealed class MustNotBeSameAsTests : ICustomMessageAndExceptionTestDataProvider
    {
        [Theory(DisplayName = "MustNotBeSameAs must throw an ArgumentException when the specified references point to the same instance.")]
        [InlineData("Foo")]
        [InlineData("Bar")]
        public void ReferencesEqual(string reference)
        {
            Action act = () => reference.MustNotBeSameAs(reference, nameof(reference));

            act.ShouldThrow<ArgumentException>()
               .And.Message.Should().Contain($"{nameof(reference)} must not point to the object instance \"{reference}\", but it does.");
        }

        [Theory(DisplayName = "MustNotBeSameAs must not throw an exception when the specified references point to different instances.")]
        [InlineData("Hello", "World")]
        [InlineData("1", "2")]
        [InlineData(new object[] { }, new object[] { "Foo" })]
        public void ReferencesDifferent<T>(T first, T second) where T : class
        {
            Action act = () => first.MustNotBeSameAs(second);

            act.ShouldNotThrow();
        }

        public void PopulateTestDataForCustomExceptionAndCustomMessageTests(CustomMessageAndExceptionTestData testData)
        {
            testData.Add(new CustomExceptionTest(exception => "foo".MustNotBeSameAs("foo", exception: exception)));

            testData.Add(new CustomMessageTest<ArgumentException>(message => "foo".MustNotBeSameAs("foo", message: message)));
        }
    }
}