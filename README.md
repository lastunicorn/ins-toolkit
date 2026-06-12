# INS Toolkit

[![GitHub Repo](https://img.shields.io/badge/github-repo-blue?logo=github)](https://github.com/lastunicorn/ins-toolkit) [![GitHub Build](https://img.shields.io/github/actions/workflow/status/lastunicorn/ins-toolkit/build-master.yml?logo=github)](https://github.com/lastunicorn/ins-toolkit/actions/workflows/build-master.yml) [![NuGet Version](https://img.shields.io/nuget/v/DustInTheWind.Ins.Toolkit?logo=nuget)](https://www.nuget.org/packages/DustInTheWind.Ins.Toolkit) [![NuGet Downloads](https://img.shields.io/nuget/dt/DustInTheWind.Ins.Toolkit?logo=nuget)](https://www.nuget.org/packages/DustInTheWind.Ins.Toolkit)

`INS Toolkit` is a .NET library that helps working with files and data from INS.

INS is the Romanian National Statistics Institute (Institutul Național de Statistică)

- https://insse.ro

The package is published as `DustInTheWind.Ins.Toolkit`.

## Installation

Package Manager:

```powershell
Install-Package DustInTheWind.Ins.Toolkit
```

.NET CLI:

```bash
dotnet add package DustInTheWind.Ins.Toolkit
```

## Runtime Requirements

- Library target framework: `.NET 8.0` (`net8.0`)

## Quick Start (Yearly Inflation)

Download inflation data per year and per quarter from the INS website.

The data is found only in a HTML web page, no computer friendly access to it. The approach is to read the HTML page and extract the data.

### a) Manual (by year)

1. Open https://insse.ro web page in a browser.
2. "Date Statistice" -> "Serii de date" -> "IPC - serii de date" -> "IPC - serie de date anuala"
3. OR access directly the URL:
   - https://insse.ro/cms/ro/content/ipc%E2%80%93serie-de-date-anuala

### b) Parse the web page

```csharp
using DustInTheWind.Ins.Toolkit;

YearlyInflationWebPage yearlyInflationWebPage = new();
IAsyncEnumerable<YearlyInflationRecord> inflationRecords = yearlyInflationWebPage.EnumerateInflationRecords();

await foreach (YearlyInflationRecord inflationRecord in inflationRecords)
{
    ...
}
```

## Quick Start (Quarterly Inflation)

### a) Manual (by quarter)

1. Open https://insse.ro web page in a browser.
2. "Date Statistice" -> "Serii de date" -> "IPC - serii de date" -> "IPC - serie de date trimestriala"
3. OR access directly the URL:
   - https://insse.ro/cms/ro/content/ipc-serie-de-date-trimestriala

### b) Parse the web page

```csharp
using DustInTheWind.Ins.Toolkit;

QuarterlyInflationWebPage quarterlyInflationWebPage = new();
IAsyncEnumerable<QuarterlyInflationRecord> inflationRecords = quarterlyInflationWebPage.EnumerateInflationRecords();

await foreach (QuarterlyInflationRecord inflationRecord in inflationRecords)
{
    ...
}
```

## Demo Project

The repository includes a sample CLI project in `sources/Ins.Toolkit.Demo` that demonstrates:

- download and parse the INS web page
- printing parsed data.

You can use this project as a reference implementation for your own importer/exporter tools.
