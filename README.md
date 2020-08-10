# SEARCH FIGHT
REQUIRENMENTS
- DOT NET CORE 3.1 SDK https://dotnet.microsoft.com/download/dotnet-core
- GOOGLE CHROME INSTALLED ON THE MACHINE exact Version 84.0.4147.105 

to restore dependencies
```
dotnet restore
```

to change version of chrome
```
dotnet add package Selenium.WebDriver.ChromeDriver --version 85.0.4183.3800-beta
```

to run the project
```
dotnet run
```

to list dependencies
```
dotnet list package
```

usage
```
dotnet run .net java kotlin
```

## NOTE:
Works almost always, depends about the resources and internet connection.
If driver returns an error Its cuz parsing delay or lag.
## TODO:
- Async tab handling
- Network error handling
- quotation marks support :(


