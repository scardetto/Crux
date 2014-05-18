using System;
using System.Globalization;
using System.Security.Cryptography;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using NUnit.Framework;
using Rhino.Mocks;

namespace Crux.Caching.Tests
{
    [TestFixture]
    public class CacheVariableTester
    {
        private const int INDEX = -1;
        private RuntimeCache _cache;
        private ICacheFilePathBuilder _cachePathBuilder;

        [SetUp]
        public void SetUp()
        {
            _cachePathBuilder = MockRepository.GenerateMock<ICacheFilePathBuilder>();
            _cache = new RuntimeCache();
        }

        [Test]
        public void should_build()
        {
            var variable = GetValidBuilder().Create();

            variable.Value.Index.Should().Be(INDEX);
        }

        [Test]
        public void should_ensure_cache_key_is_provided()
        {
            GetBuilder().Invoking(b => b.Create())
                .ShouldThrow<CachingException>()
                .WithMessage("Cache key not specified.*");
        }

        [Test]
        public void should_ensure_initializer_is_provided()
        {
            GetBuilder()
                .WithKey(GenerateRandomKey())
                .Invoking(b => b.Create())
                .ShouldThrow<CachingException>()
                .WithMessage("Initializer not specified.*");
        }

        [Test]
        public void should_timeout()
        {
            var variable = GetValidBuilder()
                .AddAbsoluteExpiration(1.Seconds())
                .Create();

            variable.Value.Index.Should().Be(INDEX);
            variable.HasValue.Should().BeTrue();

            Thread.Sleep(1.Seconds());

            variable.HasValue.Should().BeFalse();
        }

        [Test]
        public void should_eager_fetch()
        {
            var variable = GetValidBuilder()
                .EagerFetch()
                .Create();

            variable.HasValue.Should().BeTrue();
        }

        [Test]
        public void should_lazy_load()
        {
            var variable = GetValidBuilder()
                .LazyLoad()
                .Create();

            variable.HasValue.Should().BeFalse();
        }

        [Test]
        public void should_synchronize_initialization()
        {
            // While it's possible for multiple initializations to occur,
            // the framework will attempt to minimize duplicate initialization
            // by sychronizing subsequent threads after the first initialization
            // has begun.

            // Each time the cache is initialized the 
            // value of this object will be incremented 
            // after a simulated delay.
            var value = new IncrementingCachedObject();

            var variable = GetBuilderOfLong()
                .WithKey(GenerateRandomKey())
                .InitializeWith(value.Increment)
                .TimeoutAfter(10.Seconds())
                .Create();

            // Start the new initialization
            var initial = new Task<long>(() => variable.Value);
            initial.Start();

            // Introduce a small delay to ensure the initial thread has time to start.
            Thread.Sleep(1000);

            // Simulate additional threads requesting the same resource.
            var competingThreads = new[] {
                Task.Factory.StartNew(() => variable.Value),
                Task.Factory.StartNew(() => variable.Value),
                Task.Factory.StartNew(() => variable.Value),
                Task.Factory.StartNew(() => variable.Value),
                Task.Factory.StartNew(() => variable.Value)
            };

            // This is the result of the first initialization
            var result = initial.Result;

            // All competing initializations should have the same value.
            foreach (var task in competingThreads) {
                task.Result.Should().Be(result);
            }
        }

        private CacheVariableBuilder<CachedObjectForTest> GetBuilder()
        {
            return new CacheVariableBuilder<CachedObjectForTest>(_cache, _cachePathBuilder);
        }

        private CacheVariableBuilder<long> GetBuilderOfLong()
        {
            return new CacheVariableBuilder<long>(_cache, _cachePathBuilder);
        }

        private CacheVariableBuilder<CachedObjectForTest> GetValidBuilder()
        {
            return GetBuilder()
                .WithKey(GenerateRandomKey())
                .InitializeWith(() => new CachedObjectForTest(INDEX));
        }

        private string GenerateRandomKey()
        {
            var buffer = new byte[4];
            new RNGCryptoServiceProvider().GetBytes(buffer);

            return BitConverter.ToInt32(buffer, 0).ToString(CultureInfo.InvariantCulture);
        }
    }

    public class CachedObjectForTest
    {
        public int Index { get; set; }

        public CachedObjectForTest(int index)
        {
            Index = index;
        }
    }

    public class IncrementingCachedObject
    {
        private long _index;

        public long Increment()
        {
            // Simulate a long initiatlization
            Thread.Sleep(5000);

            return Interlocked.Increment(ref _index);
        }

        public long Current()
        {
            return Interlocked.Read(ref _index);
        }
    }
}

