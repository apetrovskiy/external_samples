﻿namespace Nancy.Tests.Functional.Tests
{
    using System;
    using Bootstrapper;
    using Modules;
    using Testing;
    using Xunit;

    public class ViewBagTests
    {
        private readonly INancyBootstrapper bootstrapper;

        private readonly Browser browser;

        public ViewBagTests()
        {
            this.bootstrapper = new ConfigurableBootstrapper(
                    configuration => configuration.Modules(new Type[] { typeof(RazorTestModule) }));

            this.browser = new Browser(bootstrapper);
        }

        [Fact]
        public void Should_render_content_from_viewbag()
        {
            // Given
            // When
            var response = browser.Get(
                @"/razor-viewbag",
                with =>
                {
                    with.HttpRequest();
                });

            // Then
            Assert.True(response.Body.AsString().Contains(@"Hello Bob"));
        }

        [Fact]
        public void Should_render_content_from_viewbags()
        {
            // Given
            // When
            var response = browser.Get(
                @"/razor-viewbag",
                with =>
                {
                    with.HttpRequest();
                });

            // Then
            Assert.True(response.Body.AsString().Contains(@"Hello Bob"));
        }

        [Fact]
        public void Should_serialize_ViewBag()
        {
            // Given
            var response = browser.Get(
                @"/razor-viewbag-serialized",
                with =>
                {
                    with.HttpRequest();
                    with.Accept("application/json");
                });
                
            // When
            var model = response.Body.DeserializeJson<ViewBagModel>();

            // Then
            Assert.Equal("Bob", model.Name);
        }

        public class ViewBagModel
        {
            public string Name { get; set; }
        }
    }
}
