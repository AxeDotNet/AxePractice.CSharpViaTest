using System;
using System.Diagnostics.CodeAnalysis;
using Xunit;

namespace CSharpViaTest.Langauge.GenericRelated
{
    public class GenericFactoryFacts
    {
        interface ISpeek
        {
            string Speek();
        }

        #region Please modify the code below to pass the test

        // You can change the declaration (except the name) of Duck class and Goat class.

        [SuppressMessage("ReSharper", "ClassNeverInstantiated.Local")]
        class Duck
        {
            public string Speek()
            {
                throw new NotImplementedException();
            }
        }

        [SuppressMessage("ReSharper", "ClassNeverInstantiated.Local")]
        class Goat
        {
            public string Speek()
            {
                throw new NotImplementedException();
            }
        }

        #endregion

        class GenericFactory<U>
        {
            public U Create<T>()
            {
                #region Please implement the method to pass the test

                throw new NotImplementedException();

                #endregion
            }
        }

        [Fact]
        public void should_resolve_generic_instances_goat()
        {
            var factory = new GenericFactory<ISpeek>();
            ISpeek speek = factory.Create<Goat>();

            Assert.Equal("I am a goat.", speek.Speek());
        }

        [Fact]
        public void should_resolve_generic_instances_duck()
        {
            var factory = new GenericFactory<ISpeek>();
            ISpeek speek = factory.Create<Duck>();

            Assert.Equal("Duck is speaking~.", speek.Speek());
        }
    }
}