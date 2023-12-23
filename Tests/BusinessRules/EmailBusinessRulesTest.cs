﻿using BusinessLogic.BusinessRules;

namespace Tests.BusinessRules;

public class EmailBusinessRulesTest
{
    [Theory]
    [InlineData("test@email.com", true)]
    [InlineData("", false)]
    [InlineData(null, false)]
    [InlineData(" ", false)]
    [InlineData("a", false)]
    [InlineData("a@", false)]
    [InlineData("a@b.", false)]
    [InlineData("a@.", false)]
    [InlineData("a@b.c", false)]
    [InlineData("ab.c", false)]
    [InlineData("ab.", false)]
    [InlineData(".", false)]
    [InlineData("@", false)]
    public void It_validates_the_email(string email, bool isValid)
    {
        Assert.Equal(isValid, EmailBusinessRules.IsValidEmail(email));
    }

    [Theory]
    [InlineData("test@email.com", false)]
    [InlineData("", true)]
    [InlineData(null, true)]
    [InlineData("a", true)]
    [InlineData("a@", true)]
    [InlineData("a@b.", true)]
    [InlineData("a@.", true)]
    [InlineData("a@b.c", true)]
    [InlineData("ab.c", true)]
    [InlineData("ab.", true)]
    [InlineData(".", true)]
    [InlineData("@", true)]
    public void It_asserts_the_email(string email, bool throwsException)
    {
        if (!throwsException)
        {
            EmailBusinessRules.AssertValidEmail(email);
            return;
        }

        Assert.Throws<ArgumentException>(() => EmailBusinessRules.AssertValidEmail(email));
    }
}