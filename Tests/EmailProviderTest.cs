using System.Reflection;
using System.Runtime.ExceptionServices;

namespace Tests;

public class EmailProviderTest
{
    [Theory]
    [InlineData(true, false)]
    [InlineData(false, true)]
    public void It_throws_an_exception_when_the_email_provider_is_not_initialized(bool isInitialized, bool throwsException)
    {
        Type type = typeof(EmailProvider.EmailProvider);
        var provider = Activator.CreateInstance(type);

        if (isInitialized)
        {
            MethodInfo initializeMethod = type.GetMethod("Initialize", BindingFlags.Public | BindingFlags.Static | BindingFlags.Instance)!;
            initializeMethod.Invoke(provider, ["test host", "test@mail.com", "fake password"]);
        }

        MethodInfo assertMethod = type.GetMethod("AssertInitialized", BindingFlags.NonPublic | BindingFlags.Instance)!;

        if (!throwsException)
        {
            assertMethod.Invoke(provider, []);
            return;
        }

        Assert.Throws<ArgumentNullException>(() => {
            try
            {
                assertMethod.Invoke(provider, []);
            }
            catch (TargetInvocationException ex)
            {
                ExceptionDispatchInfo.Capture(ex.InnerException!).Throw();
            }
        });
    }
}
