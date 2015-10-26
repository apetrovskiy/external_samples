﻿using System.Collections.Generic;
using Apphbify.Data;
using Apphbify.Services;
using Apphbify.Tests.Helpers;
using Moq;
using Nancy.Testing;
using Xunit;

namespace Apphbify.Tests.Pages.Deploy
{
    public class When_deployment_fails_for_an_unknown_reason
    {
        private Browser _Browser;
        private BrowserResponse _Response;
        private Mock<IDeploymentService> _Deploy;

        public When_deployment_fails_for_an_unknown_reason()
        {
            _Deploy = new Mock<IDeploymentService>(MockBehavior.Strict);
            string slug;
            _Deploy.Setup(d => d.Deploy(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<App>(), It.IsAny<Dictionary<string, string>>(), out slug)).Returns(DeploymentResult.Unspecified);
            _Browser = Testing.CreateBrowser<SecuredPagesModule>(with =>
            {
                with.LoggedInUser();
                with.Deployment(_Deploy);
            });
            _Response = _Browser.Post("/Deploy/jabbr", with =>
            {
                with.FormValue("application_name", "JabbR Test");
                with.FormValue("region_id", "amazon-web-services::us-east-1");
            });
        }

        [Fact]
        public void It_should_redirect_to_get()
        {
            _Response.ShouldHaveRedirectedTo("/Deploy/jabbr");
        }

        [Fact]
        public void It_should_have_an_error_message()
        {
            _Response.ShouldHaveErrorMessage();
        }

        [Fact]
        public void It_should_have_fired_the_deployment()
        {
            string slug;
            _Deploy.Verify(d => d.Deploy(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<App>(), It.IsAny<Dictionary<string, string>>(), out slug), Times.Once());
        }
    }
}