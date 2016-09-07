# HackerNews
Small Hacker News Scraper

### Usage

```cmd
hackernews.exe --posts n
```

In case 'n' has a wrong value, the application prints out the usage:  
Usage: hackernews --posts <n>  
Where: 1 <= n <= 100

The application prints the results to STDOUT and the error messages to STDERR.

### Used libraries

- [FluentCommandLineParser] - To parse the command line using a simple rule.
- [Microsoft.Net.Http] - Portable version of System.Net.Http - used for the HackerNews API
- [Newtonsoft.Json] - Serializing/Deserializing JSON
- [Portable.Ninject] - Portable version of Ninject - Dependency Injection
- [NUnit] - For unit tests
