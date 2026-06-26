# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## Project Overview

`INS Toolkit` is a .NET 8.0 library (`DustInTheWind.Ins.Toolkit`) that scrapes and parses statistical data from the Romanian National Statistics Institute (INS) website at https://insse.ro. It is published as a NuGet package under the `DustInTheWind` namespace.

## Build & Run Commands

```bash
# Restore dependencies
dotnet restore ./Ins.Toolkit.slnx --configfile ./nuget.config

# Build (Release)
dotnet build ./Ins.Toolkit.slnx -c Release --no-restore

# Build (Debug, for development)
dotnet build ./Ins.Toolkit.slnx

# Run the demo project (edit Program.cs to uncomment the use case first)
dotnet run --project sources/Ins.Toolkit.Demo
```

There are no automated tests in this repository. Manual testing is done via the Demo project.

## Architecture

Each data type (e.g. yearly CPI, monthly average wage) follows the same three-layer pattern:

1. **Public facade** (`sources/Ins.Toolkit/*.cs`) — e.g. `YearlyCpiWebPage`. This is the API surface that consumers call. It owns the `HttpClient`, constructs the request, and returns `IEnumerable<*Record>`.

2. **Internal web layer** (`sources/Ins.Toolkit/Web/<DataType>/`) — Three files per data type:
   - `*HttpRequest.cs` — builds the `HttpRequestMessage` (headers, URL, etc.)
   - `*HttpResponse.cs` — wraps the `HttpResponseMessage` and returns the HTML as a stream
   - `*HtmlDocument.cs` — uses `HtmlAgilityPack` to parse the HTML table and yields typed records

3. **Record types** (`sources/Ins.Toolkit/*Record.cs`) — Simple data containers (e.g. `YearlyCpiRecord`, `QuarterlyCpiRecord`, `MonthlyAverageWageRecord`, `YearlyAverageWageRecord`).

Supporting types:
- `FlexibleDecimal` (`Web/FlexibleDecimal.cs`) — handles INS's inconsistent decimal separator (`.` vs `,`)
- `MonthDate` — value type representing a `MM/YYYY` date
- `YearQuarter` — value type for quarterly data
- `InsException` — custom exception base

The `Ins.Toolkit.Demo` project contains use-case classes under `UseCases/` that demonstrate each data type; uncomment the desired one in `Program.cs` to run it.

## Code Conventions

From `.github/copilot-instructions.md`:

- Do not use `var`; always use the explicit type.
- Use `new()` (target-typed new) when instantiating objects.
- In object initializer syntax with more than one property, write each on its own line.
- Omit curly braces for single-line `if`, `for`, and `using` bodies.
- In LINQ lambdas, name the item parameter `x`.
- XML doc comments only on public types exposed in the NuGet package; omit them for internal types.

### Test naming (if tests are added)

- One test file per public method/constructor: `<MethodName>Tests.cs`
- Group test files in a `<ClassName>Tests/` directory
- Naming pattern: `Having<setup>_When<action>_Then<expectation>`
- Use block bodies (not expression bodies) in `Assert.Throws` lambdas.

## Publishing

NuGet is published automatically when a `vMAJOR.MINOR.PATCH` tag is pushed. The version in `Directory.Build.props` is a placeholder (`0.0.0.0`); the real version is injected by the CI pipeline at build time via `-p:Version=`.

Assembly name convention: `DustInTheWind.Ins.Toolkit` (prefix applied via `AssemblyName` in the csproj).
