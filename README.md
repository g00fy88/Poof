# Poof
The Open Source Server-Client App of "Points Of Good"

## Usage
If you want to run the app locally on your computer, just clone the repository and run the ```src/Poof.WebApp/Server``` project. 
You can run the app in Debug by starting the project with Visual Studio.

## Repository Structure
The Poof.WebApp is a Blazor WebAssembly App with a server, that hosts the user accounts over an SQL-Database.

The ```src\Poof.WebApp\Client``` projects holds the UI, which is build with razor components.

The ```src/Poof.Core``` project holds the core functionality of the backend.  
The ```src/Poof.DB``` project encapsulates the SQL-Database access and uses the [EntityFramework Core](https://docs.microsoft.com/en-us/ef/core/).  
The ```src/Poof.Talk``` project holds objects for the Client (UI) to talk with the backend.

The architecture in this software project follows the principles of [Elegant Objects](https://www.elegantobjects.org/) as well as the theories of Robert Cecil Martin and his books *Clean Architecture* and *Clean Coding*

### Poof.Core
While the server project is hosting the UI-Layer and configures the infrastructure, the Poof.Core projects holds the Usecase-Layer and the Entities-Layer (according to the [theory of Clean Architecture](https://blog.cleancoder.com/uncle-bob/2012/08/13/the-clean-architecture.html)).

The UseCases are implemented by using the Snap interface from the ```src/Snaps``` project:
```csharp
public interface ISnap<TResult>
{
    IOutcome<TResult> Convert(IDemand demand);
}
```
A snap is simply said the represenation of a request. It converts a demand (with content and attributes) into an outcome, that can be empty or also have a content. 

To get a first understanding of the functionality of the app, just have a look at the [Private Snap](https://github.com/g00fy88/Poof/blob/main/src/Poof.Core/Snaps/PrivateSnap.cs) object. It summarizes all the UseCases, which the backend provides for a logged-in user.

Let's take a look at the Snap ```GetsUserTransactions```, to give you a basic understanding on how the code works. The snap returns a list of all the transactions, which the user was part of:
```csharp
/// <summary>
/// Returns a list of the identity user's transactions with all the details
/// </summary>
public sealed class GetsUserTransactions : SnapEnvelope<IInput>
{
    /// <summary>
    /// Returns a list of the identity user's transactions with all the details
    /// </summary>
    public GetsUserTransactions(IDataBuilding mem, IIdentity identity) : base(dmd =>
    {
        var userId = identity.UserID();
        var userTransactions = 
            new Transactions(mem).List(
                new Participant.Match(userId), 
                new Date.SortMatch()
            );
        var result = new JArray();

        foreach(var id in userTransactions)
        {
            var transaction = new TransactionOf(mem, id);
            var giveSide = new GiveSide.Entity(transaction).AsString();
            var takeSide = new TakeSide.Entity(transaction).AsString();
            result.Add(
                new JObject(
                    new JProperty("title", new Title.Of(transaction).AsString()),
                    new JProperty("date", new Date.Of(transaction).Value().ToString("dd/MM/yyyy H:mm:ss zzz")),
                    new JProperty("amount", new Amount.Of(transaction).Value()),
                    new JProperty("type", giveSide == userId ? "give" : "receive"),
                    new JProperty(giveSide == userId ? "me" : "other",
                        new JObject(
                            new JProperty("name", new Pseudonym.Name(new UserOf(mem, giveSide)).AsString()),
                            new JProperty("score", new BalanceScore.Total(new UserOf(mem, giveSide)).Value()),
                            new JProperty("pictureUrl", new Picture.Base64Url(new UserOf(mem, giveSide)).AsString())
                        )
                    ),
                    new JProperty(takeSide == userId ? "me" : "other",
                        new JObject(
                            new JProperty("name", new Pseudonym.Name(new UserOf(mem, takeSide)).AsString()),
                            new JProperty("score", new BalanceScore.Total(new UserOf(mem, takeSide)).Value()),
                            new JProperty("pictureUrl", new Picture.Base64Url(new UserOf(mem, takeSide)).AsString())
                        )
                    )
                )
            );
        }

        return new JsonRawOutcome(new JSONOf(result));
    })
    { }
}
```

Let' start with the top
```csharp
public sealed class GetsUserTransactions : SnapEnvelope<IInput>
```
```SnapEnvelope``` is simply an encapsulation for the implementation of the snap interface, we saw earlier. In this case the conversion from demand to outcome is instantiated in the base ctor call.


```csharp
var userId = identity.UserID();
var userTransactions = 
    new Transactions(mem).List(
        new Participant.Match(userId), 
        new Date.SortMatch()
    );
var result = new JArray();

foreach(var id in userTransactions)
```
What happens here at the top is, that the user id of the person requesting the transactions is evaluated. The DataBuilding ```mem``` gives us access to the database. Only our entity-objects are allowed to use the DataBuilding. We will learn about them later. What we do here is that we list all the transactions in the DataBuilding with some filters:
- Only transactions, where the current user is a participant in
- Sorted after the transaction date

Then we initialize an empty json array and iterate through our transactions. 
We access the data of each transaction entity and fill in the details into the json array. Each transaction consists of a Give- and a Take-Side, but the response expects these sides to be divided into "me" and "other" and also which type ("give"/"receive") the transaction is from the current users point ("me"). So we make some conversion tricks here.

