﻿namespace BusinessLogic.Interfaces.Services;

public interface IHashingService
{
    public string GenerateSalt();

    public string HashPassword(string password, string salt);

    public bool VerifyPassword(string password, string passwordToVerify);
}
