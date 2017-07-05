using System;
using System.Diagnostics.CodeAnalysis;
using Xunit;

namespace Dispatcher.Tests
{
    public class HandlerFactoryFacts
    {
        class TypeA { }
        class TypeB { }
        class NoDefaultCtor
        {
            [SuppressMessage("ReSharper", "UnusedParameter.Local")]
            public NoDefaultCtor(string name)
            {
            }
        }

        [Fact]
        public void should_throw_if_registering_type_is_null()
        {
            Assert.Throws<ArgumentNullException>(() => new HandlerFactory().Register(null));
        }

        [Fact]
        public void should_throw_if_type_has_no_default_constructor()
        {
            Assert.Throws<ArgumentException>(() => new HandlerFactory().Register(typeof(NoDefaultCtor)));
        }

        [Fact]
        public void should_create_instance_of_named_type_case_insensitively()
        {
            var factory = new HandlerFactory();
            factory.Register(typeof(TypeA)).Register(typeof(TypeB));

            object instance = factory.Create(typeof(TypeA).FullName.ToLowerInvariant());

            Assert.Equal(typeof(TypeA), instance);
        }

        [Fact]
        public void should_throw_if_type_not_found()
        {
            var factory = new HandlerFactory();
            factory.Register(typeof(TypeA)).Register(typeof(TypeB));

            Assert.Throws<InvalidOperationException>(() => factory.Create("NotExistedType"));
        }
    }
}