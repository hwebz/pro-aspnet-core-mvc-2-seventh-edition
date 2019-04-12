using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;
using CitiesTagHelpers.Infrastructure.TagHelpers;

namespace CitiesTagHelpers.Tests
{
    public class TagHelpersTests
    {
        [Fact]
        public void TestTagHelper()
        {
            // arrange
            var context = new TagHelperContext(new TagHelperAttributeList(), new Dictionary<object, object>(), "myuniqueid");
            var output = new TagHelperOutput("button", new TagHelperAttributeList(), (cache, encoder) => Task.FromResult<TagHelperContent>(new DefaultTagHelperContent()));

            // act
            var tagHelper = new ButtonTagHelper
            {
                BsButtonColor = "testValue"
            };
            tagHelper.Process(context, output);

            Assert.Equal($"btn btn-{tagHelper.BsButtonColor}", output.Attributes["class"].Value);
        }
    }
}
