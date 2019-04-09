using System;
using System.IO;
using System.Linq;
using NUnit.Framework;
using Xamarin.UITest;
using Xamarin.UITest.Queries;

namespace AutomationTests
{
    [TestFixture(Platform.iOS)]
    public class Tests
    {
        IApp app;
        Platform platform;

        public Tests(Platform platform)
        {
            this.platform = platform;
        }

        [SetUp]
        public void BeforeEachTest()
        {
            app = AppInitializer.StartApp(platform);
        }

        [Test]
        public void SaveSettingsAreValid()
        {
            app.Screenshot("Login Page");
            app.Tap(x => x.Text("API"));
            app.EnterText("https://dev8-api.streets-heaver.com/Farm/Compucare8_dev_wardapi/api");
            app.Tap(x => x.Text("Identity"));
            app.EnterText("https://dev8-api.streets-heaver.com/Farm/Compucare8_dev_identity");
            app.Screenshot("Form Filled in");
            app.Tap(c => c.Text("Save"));

            AppResult[] results = app.WaitForElement(c => c.Marked("Sign In"));
            app.Screenshot("Login Page");
            Assert.IsTrue(results.Any(), "Login Sign in Button not found");
        }
    }
}
