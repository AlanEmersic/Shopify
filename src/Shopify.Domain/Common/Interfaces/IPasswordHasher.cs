﻿using ErrorOr;

namespace Shopify.Domain.Common.Interfaces;

public interface IPasswordHasher
{
    public ErrorOr<string> HashPassword(string password);
    bool IsCorrectPassword(string password, string hash);
}