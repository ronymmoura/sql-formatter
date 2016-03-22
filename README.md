# SQL Formatter

[![Build Status](https://travis-ci.org/ronymmoura-dotnet/sql-formatter.svg?branch=master)](https://travis-ci.org/ronymmoura-dotnet/sql-formatter)

Formatter lib to turn your SQL pretty.

## Usage

Import the SqlFormatter namespace contained in SqlFormatter.dll.

```csharp
var formatter = new Formatter("SELECT * FROM TEST");
var formattedSQL = formatter.Format();
```
