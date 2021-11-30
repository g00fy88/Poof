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

## Poof.Core
While the server project is hosting the UI-Layer and configures the infrastructure, the Poof.Core projects holds the Usecase-Layer and the Entities-Layer (according to the [theory of Clean Architecture](https://blog.cleancoder.com/uncle-bob/2012/08/13/the-clean-architecture.html)).

### The Use-Case Layer
The UseCases are implemented by using the Snap interface from the ```src/Snaps``` project:
```csharp
public interface ISnap<TResult>
{
    IOutcome<TResult> Convert(IDemand demand);
}
```
A snap is simply said the represenation of a request. It converts a demand (with content and attributes) into an outcome, that can be empty or also have a content. 

To get a first understanding of the functionality of the app, just have a look at the [Private Snap](https://github.com/g00fy88/Poof/blob/main/src/Poof.Core/Snaps/PrivateSnap.cs) object. It summarizes all the UseCases, which the backend provides for a logged-in user.

Let's take a look at the Snap ```GetsDetails```, to give you a basic understanding on how the code works. The snap returns a json object with all the details of the user
```csharp
/// <summary>
/// The details of the user in the identity
/// </summary>
public sealed class GetsDetails : SnapEnvelope<IInput>
{
    /// <summary>
    /// The details of the user in the identity
    /// </summary>
    public GetsDetails(IDataBuilding mem, IIdentity identity) : base(dmd =>
    {
        var user = new UserOf(mem, identity.UserID());

        return
            new JsonRawOutcome(
                new JObject(
                    new JProperty("pseudonym",
                        new JObject(
                            new JProperty("name", new Pseudonym.Name(user).AsString()),
                            new JProperty("number", new Pseudonym.Number(user).Value())
                        )
                    ),
                    new JProperty("picture", new Picture.Base64Url(user).AsString()),
                    new JProperty("points", new Points.Of(user).Value()),
                    new JProperty("takeFactor", new Points.TakeFactor(user).Value()),
                    new JProperty("giveFactor", new Points.GiveFactor(user).Value()),
                    new JProperty("score", new BalanceScore.Total(user).Value())
                )
            );
    })
    { }
}
```

Let' start with the top
```csharp
public sealed class GetsDetails : SnapEnvelope<IInput>
```
```SnapEnvelope``` is simply an encapsulation for the implementation of the snap interface, we saw earlier. In this case the conversion from demand to outcome is instantiated in the base ctor call.


```csharp
    var user = new UserOf(mem, identity.UserID());

    return
        new JsonRawOutcome(
            new JObject(
                new JProperty("pseudonym",
                    new JObject(
                        new JProperty("name", new Pseudonym.Name(user).AsString()),
                        new JProperty("number", new Pseudonym.Number(user).Value())
                    )
                ),
                new JProperty("picture", new Picture.Base64Url(user).AsString()),
                new JProperty("points", new Points.Of(user).Value()),
                new JProperty("takeFactor", new Points.TakeFactor(user).Value()),
                new JProperty("giveFactor", new Points.GiveFactor(user).Value()),
                new JProperty("score", new BalanceScore.Total(user).Value())
            )
        );
```
What happens here at the top is, that the user entity of the person requesting the details is initialized. The DataBuilding ```mem``` gives us access to the database. Only our entity-objects are allowed to use the DataBuilding. We will learn about them later. What happens here is that all the entity details of the user are retrieved and written into a json object, which is then returned as outcome.

This Snap reads data, let us now look at a Snap, that writes data. ```UpdatesUserData``` updates the pseudonym name of the user and also evaluates a pseudonym number, to make the pseudonym unique.
```csharp
/// <summary>
/// Updates the user data.
/// The pseudonym name is updated and a unque pseudonym number is evaluated.
/// </summary>
public sealed class UpdatesUserData : SnapEnvelope<IInput>
{
    /// <summary>
    /// Updates the user data.
    /// The pseudonym name is updated and a unque pseudonym number is evaluated.
    /// </summary>
    public UpdatesUserData(IDataBuilding mem, IIdentity identity) : base(dmd =>
    {
        var pseudonym = dmd.Param("pseudonym");

        var existentNumbers =
            new Yaapii.Atoms.List.Mapped<string, int>(id =>
                new Pseudonym.Number(new UserOf(mem, id)).Value(),
                new Users(mem).List(new Pseudonym.Match(pseudonym))
            );
        var random = new Random();
        var number = random.Next(0, 10000);
        while (existentNumbers.Contains(number))
        {
            number = random.Next(0, 10000);
        }

        new UserOf(mem, identity.UserID()).Update(
            new Pseudonym(pseudonym, number)
        );

        return new EmptyOutcome<IInput>();
    })
    { }
}
```
First, the existent numbers of pseudonym with the requested name in the database are retrieved. Then, random numbers are generated until there is one, that does not yet exist. This number is then used to update the pseudonym of the user.

This is basically it. If our app needs new features, we will simply add additional snaps to the ```PrivateSnap``` object, or adapt the existing ones. But what if new features require structural changes or an expansion of our database structure? In this case we will also need to adapt our entity objects.

### The Entities-Layer (Core)
The Entities consist of three interfaces:
- the catalog, to add, remove and list the entitie ids
- the entity itself, which gives access to its specific memory area (-> ```IDataFloor```)
- the entity input, which represents a specific property of the entity, that can be updated or retrieved

One of our entites are the users.  
You find the catalog of users under ```Entity/User/Users.cs```.
You find the user entity under ```Entity/User/UserOf.cs```. It simply retrieves the data floor of the given user id and gives access to it.
All the other classes under ```Entity/User/``` are entity inputs. To understand how they work, let's have a look at the ```Mail```-class:
```csharp
/// <summary>
/// The mail address of the user
/// </summary>
public sealed class Mail : EntityInputEnvelope
{
    /// <summary>
    /// The mail address of the user
    /// </summary>
    public Mail(string address) : base(floor =>
        floor.Update("mail", address)
    )
    { }

    /// <summary>
    /// The mail address of the user
    /// </summary>
    public sealed class Of : TextEnvelope
    {
        /// <summary>
        /// The mail address of the user
        /// </summary>
        public Of(IEntity user) : base(()=>
            user.Memory().Prop<string>("mail"),
            false
        )
        { }
    }
}
```
An entity input writes something into the data-floor. In this example the property "mail" of the data-floor is updated with the given value. To read this value, we use sub-classes, that get the entity object into their ctors. The usage should always look like this:
```csharp
// write email value:
new UserOf(mem, userId).Update(
    new Mail("my-new@email.com")
);

// read email value:
var mail = new Mail.Of(new UserOf(mem, userId)).AsString();
```
This is it. The entity objects are basically a very versatile way to access the databuilding object (which encapsulates our database). By naming the entity objects as what they are, we do our best to let the code speak for itself, so that it documents itself. This is the goal!

If you want to learn how the databuilding is implemented and how the database is accessed (by using the Entity Framework Core), you can take a look at the ```src/Poof.DB``` project. You can start their with the ```ApplicationDbContext``` and the ```DbBuilding``` classes. For test purposes we also made a class ```TestBuilding``` there, which only uses the data objects but no real database.
