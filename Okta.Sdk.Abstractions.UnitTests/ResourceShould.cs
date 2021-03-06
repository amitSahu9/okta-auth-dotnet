﻿// <copyright file="ResourceShould.cs" company="Okta, Inc">
// Copyright (c) 2018 - present Okta, Inc. All rights reserved.
// Licensed under the Apache 2.0 license. See the LICENSE file in the project root for full license information.
// </copyright>

using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using Okta.Sdk.Abstractions.UnitTests.Internal;
using Xunit;

namespace Okta.Sdk.Abstractions.UnitTests
{
    public class ResourceShould
    {
        [Fact]
        public void NotThrowForNullData()
        {
            var resource = new BaseResource();

            resource.Should().NotBeNull();
        }

        [Fact]
        public void InstantiateDerivedResourceFromScratch()
        {
            var resource = new TestResource();

            resource.Should().NotBeNull();
            resource.Foo.Should().BeNullOrEmpty();
            resource.Bar.Should().BeNull();
        }

        [Fact]
        public void InstantiateDerivedResourceWithData()
        {
            var data = new Dictionary<string, object>(StringComparer.Ordinal)
            {
                ["foo"] = "bar!",
                ["bar"] = true,
            };

            var factory = new ResourceFactory(null, null, null);
            var resource = factory.CreateNew<TestResource>(data);

            resource.Foo.Should().Be("bar!");
            resource.Bar.Should().Be(true);
        }

        [Fact]
        public void AccessStringProperty()
        {
            var data = new Dictionary<string, object>(StringComparer.Ordinal)
            {
                ["foo"] = "abc",
                ["empty"] = string.Empty,
                ["nothing"] = null,
            };

            var factory = new ResourceFactory(null, null, null);
            var resource = factory.CreateNew<BaseResource>(data);

            resource.GetProperty<string>("foo").Should().Be("abc");
            resource.GetProperty<string>("empty").Should().Be(string.Empty);
            resource.GetProperty<string>("nothing").Should().BeNull();
            resource.GetProperty<string>("reallyNothing").Should().BeNull();
        }

        [Fact]
        public void RoundtripStringProperty()
        {
            var factory = new ResourceFactory(null, null, null);
            var resource = factory.CreateNew<BaseResource>(null);

            resource.GetProperty<string>("foo").Should().BeNull();

            resource["foo"] = "bar";
            resource.GetProperty<string>("foo").Should().Be("bar");
        }

        [Fact]
        public void AccessBooleanProperty()
        {
            var data = new Dictionary<string, object>(StringComparer.Ordinal)
            {
                ["yes"] = true,
                ["no"] = false,
                ["nothing"] = null,
            };

            var factory = new ResourceFactory(null, null, null);
            var resource = factory.CreateNew<BaseResource>(data);

            resource.GetProperty<bool?>("yes").Should().BeTrue();
            resource.GetProperty<bool?>("no").Should().BeFalse();
            resource.GetProperty<bool?>("nothing").Should().BeNull();
            resource.GetProperty<bool?>("reallyNothing").Should().BeNull();
        }

        [Fact]
        public void RoundtripBooleanProperty()
        {
            var factory = new ResourceFactory(null, null, null);
            var resource = factory.CreateNew<BaseResource>(null);

            resource.GetProperty<bool?>("foo").Should().BeNull();

            resource["foo"] = true;
            resource.GetProperty<bool?>("foo").Should().BeTrue();
        }

        [Fact]
        public void AccessIntProperty()
        {
            var data = new Dictionary<string, object>(StringComparer.Ordinal)
            {
                ["min"] = int.MinValue,
                ["max"] = int.MaxValue,
                ["nothing"] = null,
            };

            var factory = new ResourceFactory(null, null, null);
            var resource = factory.CreateNew<BaseResource>(data);

            resource.GetProperty<int?>("min").Should().Be(int.MinValue);
            resource.GetProperty<int?>("max").Should().Be(int.MaxValue);
            resource.GetProperty<int?>("nothing").Should().BeNull();
            resource.GetProperty<int?>("reallyNothing").Should().BeNull();
        }

        [Fact]
        public void RoundtripIntProperty()
        {
            var factory = new ResourceFactory(null, null, null);
            var resource = factory.CreateNew<BaseResource>(null);

            resource.GetProperty<int?>("foo").Should().BeNull();

            resource["foo"] = 12345;
            resource.GetProperty<int?>("foo").Should().Be(12345);
        }

        [Fact]
        public void AccessLongProperty()
        {
            var data = new Dictionary<string, object>(StringComparer.Ordinal)
            {
                ["min"] = long.MinValue,
                ["max"] = long.MaxValue,
                ["nothing"] = null,
            };

            var factory = new ResourceFactory(null, null, null);
            var resource = factory.CreateNew<BaseResource>(data);

            resource.GetProperty<long?>("min").Should().Be(long.MinValue);
            resource.GetProperty<long?>("max").Should().Be(long.MaxValue);
            resource.GetProperty<long?>("nothing").Should().BeNull();
            resource.GetProperty<long?>("reallyNothing").Should().BeNull();
        }

        [Fact]
        public void RoundtripLongProperty()
        {
            var factory = new ResourceFactory(null, null, null);
            var resource = factory.CreateNew<BaseResource>(null);

            resource.GetProperty<long?>("foo").Should().BeNull();

            resource["foo"] = 123456789000;
            resource.GetProperty<long?>("foo").Should().Be(123456789000);
        }

        [Fact]
        public void AccessDateTimeProperty()
        {
            var data = new Dictionary<string, object>(StringComparer.Ordinal)
            {
                ["dto"] = new DateTimeOffset(2015, 12, 27, 20, 15, 00, TimeSpan.FromHours(-6)),
                ["iso"] = "2016-11-06T17:05:30.400-08:00",
                ["nothing"] = null,
            };

            var factory = new ResourceFactory(null, null, null);
            var resource = factory.CreateNew<BaseResource>(data);

            resource.GetProperty<DateTimeOffset?>("dto").Should().Be(new DateTimeOffset(2015, 12, 27, 20, 15, 00, TimeSpan.FromHours(-6)));
            resource.GetProperty<DateTimeOffset?>("iso").Should().Be(new DateTimeOffset(2016, 11, 6, 17, 05, 30, 400, TimeSpan.FromHours(-8)));
            resource.GetProperty<DateTimeOffset?>("nothing").Should().BeNull();
            resource.GetProperty<DateTimeOffset?>("reallyNothing").Should().BeNull();
        }

        [Fact]
        public void RoundtripDateTimeProperty()
        {
            var factory = new ResourceFactory(null, null, null);
            var resource = factory.CreateNew<BaseResource>(null);

            resource.GetProperty<DateTimeOffset?>("foo").Should().BeNull();

            resource["foo"] = new DateTimeOffset(2015, 12, 27, 20, 15, 00, TimeSpan.FromHours(-6));
            resource.GetProperty<DateTimeOffset?>("foo").Should().Be(new DateTimeOffset(2015, 12, 27, 20, 15, 00, TimeSpan.FromHours(-6)));
        }

        [Fact]
        public void AccessListProperty()
        {
            var data = new Dictionary<string, object>(StringComparer.Ordinal)
            {
                ["things"] = new List<object>() { "foo", "bar", "baz" },
            };

            var factory = new ResourceFactory(null, null, null);
            var resource = factory.CreateNew<BaseResource>(data);

            var things = resource.GetArrayProperty<string>("things");
            things.Should().BeEquivalentTo("foo", "bar", "baz");
        }

        [Fact]
        public void AccessListOfResources()
        {
            var data = new Dictionary<string, object>(StringComparer.Ordinal)
            {
                ["things"] = new List<object>()
                {
                    new Dictionary<string, object>(StringComparer.Ordinal)
                    {
                        ["foo"] = "John",
                        ["baz"] = "Foobar",
                    },
                    new Dictionary<string, object>(StringComparer.Ordinal)
                    {
                        ["foo"] = "Jane",
                        ["baz"] = "Qux",
                    },
                },
            };

            var factory = new ResourceFactory(null, null, null);
            var resource = factory.CreateNew<BaseResource>(data);

            var profiles = resource.GetArrayProperty<TestResource>("things");
            profiles.Should().HaveCount(2);
            profiles.First().Foo.Should().Be("John");
            profiles.First().Baz.Should().Be("Foobar");
            profiles.Last().Foo.Should().Be("Jane");
            profiles.Last().Baz.Should().Be("Qux");
        }

        [Fact]
        public void RoundtripListProperty()
        {
            var factory = new ResourceFactory(null, null, null);
            var resource = factory.CreateNew<BaseResource>(null);

            resource.GetArrayProperty<string>("foo").Should().BeEmpty();

            var things = new[] { "favorite", "strings" }.AsEnumerable();

            resource["foo"] = things;
            resource.GetArrayProperty<string>("foo").Should().BeEquivalentTo("favorite", "strings");
        }

        [Fact]
        public void AccessEnumProperty()
        {
            var data = new Dictionary<string, object>(StringComparer.Ordinal)
            {
                ["status1"] = "foo",
                ["status2"] = "BAR",
                ["status3"] = "bAZ",
                ["status4"] = "something",
            };

            var factory = new ResourceFactory(null, null, null);
            var resource = factory.CreateNew<BaseResource>(data);

            resource.GetProperty<TestEnum>("status1").Should().Be(TestEnum.Foo);
            resource.GetProperty<TestEnum>("status2").Should().Be(TestEnum.Bar);
            resource.GetProperty<TestEnum>("status3").Should().Be(TestEnum.Baz);
            resource.GetProperty<TestEnum>("status4").Should().Be(new TestEnum("SOMETHING"));
        }

        [Fact]
        public void AccessListEnumProperty()
        {
            var data = new Dictionary<string, object>(StringComparer.Ordinal)
            {
                ["things"] = new List<object>()
                {
                    TestEnum.Foo,
                    TestEnum.Bar,
                    TestEnum.Baz,
                },
            };

            var factory = new ResourceFactory(null, null, null);
            var resource = factory.CreateNew<BaseResource>(data);
            var things = resource.GetArrayProperty<TestEnum>("things");

            things.Should().NotBeNullOrEmpty();
            things.Should().HaveCount(3);

            // Test collection item equality a few different ways:

            things.First().Should().Be(TestEnum.Foo);
            things.ElementAt(1).Should().Be(TestEnum.Bar);
            things.Last().Should().Be(TestEnum.Baz);

            things.Contains(TestEnum.Foo).Should().BeTrue();

            things.Should().Contain(TestEnum.Foo);
            things.Should().Contain(TestEnum.Bar);
            things.Should().Contain(TestEnum.Baz);
        }

        [Fact]
        public void ConvertListStringToListEnumProperty()
        {
            var data = new Dictionary<string, object>(StringComparer.Ordinal)
            {
                ["things"] = new List<object>() { "foo", "bar", "baz"},
            };

            var factory = new ResourceFactory(null, null, null);
            var resource = factory.CreateNew<BaseResource>(data);
            var things = resource.GetArrayProperty<TestEnum>("things");

            things.Should().NotBeNullOrEmpty();
            things.Should().HaveCount(3);

            // Test collection item equality a few different ways:
            things.First().Should().Be(TestEnum.Foo);
            things.ElementAt(1).Should().Be(TestEnum.Bar);
            things.Last().Should().Be(TestEnum.Baz);

            things.Contains(TestEnum.Foo).Should().BeTrue();

            things.Should().Contain(TestEnum.Foo);
            things.Should().Contain(TestEnum.Bar);
            things.Should().Contain(TestEnum.Baz);
        }

        [Fact]
        public void RoundtripEnumProperty()
        {
            var factory = new ResourceFactory(null, null, null);
            var resource = factory.CreateNew<BaseResource>(null);

            resource.GetProperty<TestEnum>("foo").Should().BeNull();

            resource["foo"] = TestEnum.Foo;
            resource.GetProperty<TestEnum>("foo").Should().Be(TestEnum.Foo);
        }

        [Fact]
        public void TrackInstanceModifications()
        {
            var resource = new TestResource()
            {
                Foo = "xyz",
            };

            resource.GetData().Keys.Should().BeEquivalentTo("foo");

            resource.Bar = true;

            resource.GetData().Count.Should().Be(2);
            resource.GetData().Should().Contain(new KeyValuePair<string, object>("foo", "xyz"));
            resource.GetData().Should().Contain(new KeyValuePair<string, object>("bar", true));
        }
    }
}
