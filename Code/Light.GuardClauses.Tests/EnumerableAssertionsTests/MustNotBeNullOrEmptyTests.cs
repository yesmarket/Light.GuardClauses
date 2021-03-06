﻿using System;
using System.Collections.Generic;
using FluentAssertions;
using Light.GuardClauses.Exceptions;
using Light.GuardClauses.Tests.CustomMessagesAndExceptions;
using Xunit;

namespace Light.GuardClauses.Tests.EnumerableAssertionsTests
{
    public sealed class MustNotBeNullOrEmptyTests : ICustomMessageAndExceptionTestDataProvider
    {
        [Fact(DisplayName = "MustNotBeNullOrEmpty must throw an ArgumentNullException when the collection is null.")]
        public void ListNull()
        {
            List<string> list = null;

            // ReSharper disable once ExpressionIsAlwaysNull
            Action act = () => list.MustNotBeNullOrEmpty(nameof(list));

            act.Should().Throw<ArgumentNullException>()
               .And.ParamName.Should().Be(nameof(list));
        }

        [Fact(DisplayName = "MustNotBeNullOrEmpty must throw an EmptyCollectionException when the parameter is a collection with no items.")]
        public void ListEmpty()
        {
            // ReSharper disable once CollectionNeverUpdated.Local
            var list = new List<int>();

            Action act = () => list.MustNotBeNullOrEmpty(nameof(list));

            act.Should().Throw<EmptyCollectionException>()
               .And.ParamName.Should().Be(nameof(list));
        }

        [Theory(DisplayName = "MustNotBeNullOrEmpty must not throw an exception when the parameter is a collection with at least one item.")]
        [MemberData(nameof(ListNotEmptyTestData))]
        public void ListNotEmpty(List<int> list)
        {
            var result = list.MustNotBeNullOrEmpty(nameof(list));

            result.Should().BeSameAs(list);
        }

        public static readonly IEnumerable<object[]> ListNotEmptyTestData =
            new[]
            {
                new object[] { new List<int> { 1 } },
                new object[] { new List<int> { 1, 2, 3 } },
                new object[] { new List<int> { 10, -11, 187, 22557 } }
            };

        void ICustomMessageAndExceptionTestDataProvider.PopulateTestDataForCustomExceptionAndCustomMessageTests(CustomMessageAndExceptionTestData testData)
        {
            testData.Add(new CustomExceptionTest(exception => new object[0].MustNotBeNullOrEmpty(exception: exception)))
                    .Add(new CustomMessageTest<EmptyCollectionException>(message => new object[0].MustNotBeNullOrEmpty(message: message)));
        }
    }
}