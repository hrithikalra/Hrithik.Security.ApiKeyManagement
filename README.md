🔐 Hrithik.Security.ApiKeyManagement

Enterprise-grade API key generation, validation, revocation, expiration, and scope-based authorization for ASP.NET Core APIs.

Designed for fintech, banking, SaaS, and partner integrations, this library helps teams implement API keys correctly and securely — avoiding common pitfalls like plaintext storage and weak authorization.

✨ Features

🔑 Secure API key generation (cryptographically strong)

🔒 Hashed API key storage (never stores plaintext keys)

🎯 Scope-based authorization per endpoint

⏱️ Key expiration support

🚫 Instant key revocation

🔁 Zero-downtime key rotation workflows

⚙️ ASP.NET Core middleware + attributes

🧩 Pluggable storage (in-memory, EF Core, custom)

🚀 Installation
dotnet add package Hrithik.Security.ApiKeyManagement

🛠️ Quick Start
1️⃣ Register Services
using Hrithik.Security.ApiKeyManagement.Extensions;
using Hrithik.Security.ApiKeyManagement.Stores;

builder.Services.AddApiKeyManagement(options =>
{
    options.HashingSecret = "your-secure-server-secret";
});

// Development / testing
builder.Services.AddSingleton<IApiKeyStore, InMemoryApiKeyStore>();

2️⃣ Add Middleware (Order Matters)
app.UseApiKeyManagement();


Place this before request signing, replay protection, or authorization middleware.

3️⃣ Protect Endpoints with Scopes
using Hrithik.Security.ApiKeyManagement.Attributes;

app.MapPost("/transfer",
    [RequireApiKeyScope("payments:write")]
    (decimal amount) =>
{
    return Results.Ok("Transfer successful");
});

🔑 Generating API Keys
using Hrithik.Security.ApiKeyManagement.Models;

var apiKey = apiKeyService.GenerateKey(
    name: "IBM Production",
    scopes: new[]
    {
        new ApiKeyScope("payments:read"),
        new ApiKeyScope("payments:write")
    },
    expiresAt: DateTime.UtcNow.AddDays(30)
);


⚠️ The raw API key is shown only once.
Store it securely on the client side.

🔐 How API Key Validation Works

Client sends X-API-KEY header

Server hashes the key

Hashed value is compared against stored hash

Key status, expiry, and scopes are enforced

Request proceeds only if valid

🔁 Key Rotation (Zero Downtime)

This library supports safe key rotation workflows:

Generate a new API key

Keep the old key active temporarily

Distribute the new key to the client

Revoke or expire the old key

await apiKeyService.RevokeAsync(oldKeyId);


Rotation scheduling and client notification are intentionally left to the host application.

🧠 Design Philosophy

API keys represent integration identity, not end users

Keys are immutable and auditable

Security decisions are explicit and deterministic

No hidden behavior or silent credential changes

🧪 Storage Options
Store	Use Case
InMemoryApiKeyStore	Local dev, tests, demos
EF Core Store	Production (coming next)
Custom Store	Redis, DynamoDB, Vault, etc.
🔐 Security Best Practices

Never store API keys in plaintext

Always rotate keys periodically

Use scopes instead of multiple APIs

Combine with request signing and replay protection for maximum security

📌 Typical Use Cases

Fintech & payment APIs

Partner integrations (B2B)

Internal microservices

Secure public APIs

Banking & regulated systems

📄 License

MIT License

🤝 Contributing

Issues, discussions, and PRs are welcome.
Security improvements and feedback are highly appreciated.

⭐ Final Note

This library focuses on doing one thing well:
secure, correct, and maintainable API key management.

If you’re building serious APIs, this gives you a solid foundation.


📧 Contact

Author: Hrithik
Email: hrithikkalra11@gmail.com

NuGet: Hrithik.Security.ReplayProtection

☕ **Support my work**  
If you find this package helpful, consider buying me a coffee ❤️  
👉 [Buy Me a Coffee](https://www.buymeacoffee.com/alkylhalid9)
or
consider supporting its development:
👉 https://github.com/sponsors/hrithikalra
