# INS Toolkit

`INS Toolkit` is a .NET library that helps working with files and data from the INS .

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

## Quick Start

Download inflation data per year and per trimester from the INS website.

The data is found only in a HTML web page, no computer friendly access to it. The approach is to read the HTML page and extract the data.

### Manual (series by year)

1. Open https://insse.ro web page in a browser.
2. "Date Statistice" -> "Serii de date" -> "IPC - serii de date" -> "IPC - serie de date anuala"
3. OR access directly the URL:
   - https://insse.ro/cms/ro/content/ipc%E2%80%93serie-de-date-anuala

### Manual (series by trimester)

1. Open https://insse.ro web page in a browser.
2. "Date Statistice" -> "Serii de date" -> "IPC - serii de date" -> "IPC - serie de date trimestriala"
3. OR access directly the URL:
   - https://insse.ro/cms/ro/content/ipc-serie-de-date-trimestriala

### b) Parse the web page

```csharp
using DustInTheWind.Ins.Toolkit;

TBD
```

## `Transaction` Record

Each row is mapped to:

- TBD
  

## Demo Project

The repository includes a sample CLI project in `sources/Ins.Toolkit.Demo` that demonstrates:

- download and parse the INS web page
- printing parsed data.

You can use this project as a reference implementation for your own importer/exporter tools.
