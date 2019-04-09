using System;
using Xamarin.UITest;
using Xamarin.UITest.Queries;

namespace AutomationTests
{
    public class AppInitializer
    {
        public static IApp StartApp(Platform platform)
        {
            return ConfigureApp.iOS.InstalledApp("com.streets-heaver.compucareward").StartApp();
        }
    }
}