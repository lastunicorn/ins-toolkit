# INS Toolkit

[![GitHub Repo](https://img.shields.io/badge/github-repo-blue?logo=github)](https://github.com/lastunicorn/ins-toolkit) [![GitHub Build](https://img.shields.io/github/actions/workflow/status/lastunicorn/ins-toolkit/build-master.yml?logo=github)](https://github.com/lastunicorn/ins-toolkit/actions/workflows/build-master.yml) [![NuGet Version](https://img.shields.io/nuget/v/DustInTheWind.Ins.Toolkit?logo=nuget)](https://www.nuget.org/packages/DustInTheWind.Ins.Toolkit) [![NuGet Downloads](https://img.shields.io/nuget/dt/DustInTheWind.Ins.Toolkit?logo=nuget)](https://www.nuget.org/packages/DustInTheWind.Ins.Toolkit)

`INS Toolkit` is a .NET library that helps working with files and data from INS.

INS is the Romanian National Statistics Institute (Institutul Național de Statistică)

- https://insse.ro

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

## CPI (Yearly)

CPI = Consumer Price Index

### a) In the Browser

1. Open https://insse.ro web page in a browser.
2. Navigate to "Date Statistice" -> "Serii de date" -> "IPC - serii de date" -> "IPC - serie de date anuala"

OR

1. Access directly the URL:
   - https://insse.ro/cms/ro/content/ipc%E2%80%93serie-de-date-anuala

### b) Parse the Web Page

There is no computer friendly access to the data, so, the only approach is to read the HTML page and extract the data from there.

```csharp
using DustInTheWind.Ins.Toolkit;

YearlyCpiWebPage webPage = new();
IEnumerable<YearlyCpiRecord> records = await webPage.EnumerateRecords();

foreach (YearlyCpiRecord record in records)
{
    ...
}
```

## CPI (Quarterly)

CPI = Consumer Price Index

### a) In the Browser

1. Open https://insse.ro web page in a browser.
2. Navigate to "Date Statistice" -> "Serii de date" -> "IPC - serii de date" -> "IPC - serie de date trimestriala"

OR

1. access directly the URL:
   - https://insse.ro/cms/ro/content/ipc-serie-de-date-trimestriala

### b) Parse the Web Page

There is no computer friendly access to the data, so, the only approach is to read the HTML page and extract the data from there.

```csharp
using DustInTheWind.Ins.Toolkit;

QuarterlyCpiWebPage webPage = new();
IEnumerable<QuarterlyCpiRecord> records = await webPage.EnumerateRecords();

foreach (QuarterlyCpiRecord record in records)
{
    ...
}
```

## Average Wage (Yearly)

### a) In the Browser

1. Open https://insse.ro web page in a browser.
2. Navigate to "Date Statistice" -> "Serii de date" -> "Câștiguri - serii de date" -> "Câștiguri salariale din 1938 - serie anuală"

OR

1. access directly the URL:
   - https://insse.ro/cms/ro/content/c%C3%A2%C8%99tiguri-salariale-din-1938-serie-anual%C4%83-0

### b) Parse the Web Page

There is no computer friendly access to the data, so, the only approach is to read the HTML page and extract the data from there.

```csharp
using DustInTheWind.Ins.Toolkit;

YearlyAverageWageWebPage webPage = new();
IEnumerable<YearlyAverageWageRecord> records = await webPage.EnumerateRecords();

foreach (YearlyAverageWageRecord record in records)
{
    ...
}
```

## Average Wage (Monthly)

### a) In the Browser

1. Open https://insse.ro web page in a browser.
2. Navigate to "Date Statistice" -> "Serii de date" -> "Câștiguri - serii de date" -> "Câștiguri salariale - din 1991, serie lunară"

OR

1. access directly the URL:
   - https://insse.ro/cms/ro/content/c%C3%A2%C8%99tiguri-salariale-din-1991-serie-lunar%C4%83

### b) Parse the Web Page

There is no computer friendly access to the data, so, the only approach is to read the HTML page and extract the data from there.

```csharp
using DustInTheWind.Ins.Toolkit;

MonthlyAverageWageWebPage webpage = new();
IEnumerable<MonthlyAverageWageRecord> records = await webpage.EnumerateRecords();

foreach (MonthlyAverageWageRecord record in records)
{
    ...
}
```

## Demo Project

The repository includes a sample CLI project in `sources/Ins.Toolkit.Demo` that demonstrates:

- download and and extract data from the INS web pages
- printing parsed data.

You can use this project as a reference implementation for your own importer/exporter tools.
