![alt text](https://i.ibb.co/pZFfg2Y/Untitled.png)

## reply:

* There are absolutely no sanity checks or validation, no exception handling, no null checks, no valid error responses to client.
* There are absolutely no comments.
* The manager class's interface is used as a "repository", while it's responsibility is to run the flow of the code.
* IParser has 2 methods - T ParseResult(string data) & List<T> ParseString(string data) - the naming isn't descriptive enough, as a user of this interface I don't understand the difference.
* IRepository's method Update returns ActionResultEnum which contains HTTP specific results. This means our repository is strongly coupled with HTTP Web APIs. Using it somewhere else would make no sense.
* IRepository limits us to use int as our key.. as we discussed during the interview, we expect our infrastructure to be as extendable as possible. (generic key)
* FileRepository - Unsafe file system usage, multi-threaded environment could cause a lot of issues.
* IParser should be an async API instead of calling Task.Run(() => _parser.ParseResult(res)) every time, if I were to use this parser in a different part of the code I would have to also call Task.Run
* GiphyUrlCreator class is a public class inside Manager class - this is a very bad practice, also not using the actual capabilities of nested classes.
