# SQL Formatter

[![NuGet version](https://badge.fury.io/nu/SqlFormatter.svg)](https://badge.fury.io/nu/SqlFormatter)
[![Build Status](https://travis-ci.org/ronymmoura/sql-formatter.svg?branch=master)](https://travis-ci.org/ronymmoura-dotnet/sql-formatter)

C# Library to turn your SQL pretty.

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

## To be implemented

- Formatting of querys with CREATE, ALTER, DROP
- A better way to format subquerys
