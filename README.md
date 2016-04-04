# SQL Formatter

[![Build Status](https://travis-ci.org/ronymmoura-dotnet/sql-formatter.svg?branch=master)](https://travis-ci.org/ronymmoura-dotnet/sql-formatter)
[![Gitter](https://badges.gitter.im/ronymmoura-dotnet/sql-formatter.svg)](https://gitter.im/ronymmoura-dotnet/sql-formatter?utm_source=badge&utm_medium=badge&utm_campaign=pr-badge)

Formatter lib to turn your SQL pretty.

## Usage

Import the nuget package:

```
Install-Package SqlFormatter -Pre
```

Import the SqlFormatter namespace contained in SqlFormatter.dll.

```csharp
var formatter = new Formatter("SELECT * FROM TEST");
var formattedSQL = formatter.Format();
```
