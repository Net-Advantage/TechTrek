﻿namespace Nabs.Core.Application.Abstractions;

public sealed class BearerTokenSettings
{
    public string Secret { get; set; } = default!;
    public string Issuer { get; set; } = default!;
    public string Audience { get; set; } = default!;
}
