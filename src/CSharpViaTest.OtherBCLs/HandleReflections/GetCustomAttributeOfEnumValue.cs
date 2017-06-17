using System;
using System.Linq;
using System.Reflection;
using Xunit;

namespace CSharpViaTest.OtherBCLs.HandleReflections
{
    static class EnumDescriptionExtension
    {
        public static string GetDescription<T>(this T value)
        {
            if (!(value is Enum))
            {
                throw new NotSupportedException();
            }

            MyEnumDescriptionAttribute attribute = value.GetType()
                .GetMember(value.ToString())
                .Single()
                .GetCustomAttribute<MyEnumDescriptionAttribute>();
            return attribute == null ? value.ToString() : attribute.Description;
        }
    }

    [AttributeUsage(AttributeTargets.Field)]
    class MyEnumDescriptionAttribute : Attribute
    {
        public MyEnumDescriptionAttribute(string description)
        {
            Description = description;
        }

        public string Description { get; }
    }

    public class GetCustomAttributeOfEnumValue
    {
        enum ForTest
        {
            [MyEnumDescription("Awesome name!")]
            ValueWithDescription,
            ValueWithoutDescription
        }

        [Fact]
        public void should_throw_if_null()
        {
            Assert.Throws<ArgumentNullException>(() => default(string).GetDescription());
        }

        [Fact]
        public void should_throw_if_not_enum()
        {
            Assert.Throws<NotSupportedException>(() => 1.GetDescription());
        }

        [Fact]
        public void should_get_custom_attribute()
        {
            Assert.Equal("Awesome name!", ForTest.ValueWithDescription.GetDescription());
        }

        [Fact]
        public void should_get_origin_value()
        {
            Assert.Equal("ValueWithoutDescription", ForTest.ValueWithoutDescription.GetDescription());
        }
    }
}