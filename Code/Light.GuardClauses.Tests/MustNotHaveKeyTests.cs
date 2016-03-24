﻿using System;
using System.Collections.Generic;
using FluentAssertions;
using Light.GuardClauses.Exceptions;
using Light.GuardClauses.Tests.CustomMessagesAndExceptions;
using Xunit;
using TestData = System.Collections.Generic.IEnumerable<object[]>;

namespace Light.GuardClauses.Tests
{
    public sealed class MustNotHaveKeyTests : ICustomMessageAndExceptionTestDataProvider
    {
        [Theory(DisplayName = "MustNotHaveKey must throw a DictioanaryException when the specified key is part of the dictionary.")]
        [MemberData(nameof(HasKeyData))]
        public void HasKey<TKey>(IDictionary<TKey, object> dictionary, TKey key)
        {
            Action act = () => dictionary.MustNotHaveKey(key, nameof(dictionary));

            act.ShouldThrow<DictionaryException>()
               .And.Message.Should().Contain($"{nameof(dictionary)} must not contain key \"{key}\".");
        }

        public static readonly TestData HasKeyData =
            new[]
            {
                new object[] { new Dictionary<char, object> { ['a'] = 1, ['b'] = 2 }, 'a' },
                new object[] { new Dictionary<int, object> { [42] = "What", [18554] = "Is", [-332] = "Going", [15] = "On" }, -332 }
            };

        [Theory(DisplayName = "MustNotHaveKey must not throw an exception when the specified key is not part of the dictionary.")]
        [MemberData(nameof(DoesNotHaveKeyData))]
        public void DoesNotHaveKey<TKey>(IDictionary<TKey, object> dictionary, TKey key)
        {
            Action act = () => dictionary.MustNotHaveKey(key);

            act.ShouldNotThrow();
        }

        public static readonly TestData DoesNotHaveKeyData =
            new[]
            {
                new object[] { new Dictionary<string, object> { ["Id 1"] = "value 1", ["Id 2"] = 2, ["Id 3"] = ConsoleColor.DarkBlue }, "foo" },
                new object[] { new Dictionary<Guid, object> { [Guid.NewGuid()] = "Hey", [Guid.NewGuid()] = "there" }, Guid.Empty }
            };

        [Fact(DisplayName = "MustNotHaveKey must throw an ArgumentNullException when the specified dictionary is null.")]
        public void DictionaryNull()
        {
            Dictionary<string, object> dictionary = null;

            // ReSharper disable once ExpressionIsAlwaysNull
            Action act = () => dictionary.MustNotHaveKey("foo", nameof(dictionary));

            act.ShouldThrow<ArgumentNullException>()
               .And.ParamName.Should().Be(nameof(dictionary));
        }

        public void PopulateTestDataForCustomExceptionAndCustomMessageTests(CustomMessageAndExceptionTestData testData)
        {
            testData.Add(new CustomExceptionTest(exception => new Dictionary<char, string> { ['a'] = "value" }.MustNotHaveKey('a', exception: exception)));
            testData.Add(new CustomExceptionTest(exception => ((Dictionary<char, string>) null).MustNotHaveKey('f', exception: exception)));

            testData.Add(new CustomMessageTest<DictionaryException>(message => new Dictionary<char, string> { ['a'] = "value" }.MustNotHaveKey('a', message: message)));
            testData.Add(new CustomMessageTest<ArgumentNullException>(message => ((Dictionary<char, string>) null).MustNotHaveKey('f', message: message)));
        }
    }
}