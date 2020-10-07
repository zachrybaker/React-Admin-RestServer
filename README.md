# Getting started with the Demo app

This app is basically a template, or recipie if you will,
of using [Loresoft's](www.loresoft.com) DAL generator in conjunction
with a workable [implementation](https://github.com/zachrybaker/ra-data-rest-client) of
[react-admin's](https://github.com/marmelab/react-admin) data provider,
to serve a REST API compliant with the expected REST dialect to build react-admin apps with minimal code.

## 1 Autogenerate your Data Access Layer (your "DAL")

TL'DR: follow Loresoft's [Generate-ASP-NET-Web-API](http://www.loresoft.com/Generate-ASP-NET-Web-API) walkthru.

Heads up - their tool is oriented around being used directly in a web app. To generate your DAL elsewhere is more work.

You need to generate your DAL into this project and correct startup.cs for the conn string and db context. Then your app will compile.
My demo basically follows their walkthru, with a few tweaks explained below. I used the sql script in the demo project to build my database.

### 1.a. Build or initialize a generator configuration file (generation.yml)

You may create your own yaml file from the start with this command, from the solution folder:

> efg initialize -c "[db conn]" --id "d106eb49-28ab-49be-a838-0229c48642d2" --name "ConnectionStrings:DemoDBConnectionString" -d ./ReactAdminNetCoreServerAPI.DemoAPI

You can also start with the included generation.yml file, just change the guid to match your user secrets file, your db conn details, etc.

### 1.b. Generate the Code for the DAL (the EF Core modek, the mappers, and DTOs):

I ran this from the solution folder:

> efg generate -d "ReactAdminNetCoreServerAPI.DemoAPI"

Now that you have the basic Web API project setup, you can run efg generate after any database change to keep all your entity and view models in sync with the database.
Generated regions are what get touched, but it is fairly smart to keep your edits on the context.

## 2. Update your startup.cs

Correct the db conn string and db context data if you haven't already.

I'd suggest you add logging while you're here. Serilog seems like a good go-to.

## 3. Implement IHaveIdentifier<T> on data entities

In order for the core functionality of the API to work,
you will want to modify generated entity classes in the Data -> Entities folder to implement this interface.

This allows for you to create controllers that hopefully just have constructors, to serve that data type! You can extend them as necessary as we will see shortly.

Note that the identifier is usually the primary key, but this is not strictly required.

You may be wondering what to do if you use compound keys. If so, see below.

## 4. Add controllers to expose your API

From here you can start adding controllers for entities you want to expose.

They should inherit from either

- EntityWithIdControllerBase or
- ReadOnlyEntityWithControllerBase or
- One of the "Convenience" controllers in the folder by the same name.

Unless you need to do something special, you're done.

Run your API in peace. Work on your UI. Get more coffee.

## 5. Handle outliers to the react-admin-expected schema & dialect.

In the real world, often we have to live with existing data schema.  
We can't touch it, or can't afford to touch it, or need to minimize accomidations for a UI.

One big accomidation react-admin expects of you is to have your entities
(they call them resources on the client)
to use a single string property, called "Id", as your key.

### <em>When an entity doesn't use a string identifier/key:</em>

No worries.  
The generic type in IHaveIdentifier<T> deals with this for you in the base controller.

### <em>When an entity doesn't use "Id" for its identifier/primary key</em>

No problem.
In the constructor for the controller, just specify the name of the property in the 3rd argument.  
Override the base controller's GetIdentifierEqualityFn to provide the sensible Linq expression to find records by the identifier, whatever it is.

Here's an exmaple:

```[Route("api/[controller]")]
    public class TestKVPController :
        EntityWithReferenceIdController<
            TestKVP, TestKVPReadModel, TestKVPCreateModel, TestKVPUpdateModel, string>
    {
        public TestKVPController(MyDBContext dataContext, IMapper mapper) :
            base(dataContext, mapper, "Key") { }

        protected override Expression<Func<TestKVP, bool>> GetIdentifierEqualityFn(string o)
            => (x => x.Key == o);
    }
```

Keep in mind that your client app is going to need to specify in the data provider instantiation the identifier name.  
See [zachrybaker/ra-data-rest-client](https://github.com/zachrybaker/ra-data-rest-client) for example.

You will also need to add the Id property to satisfy the IHaveIdentifier<T> on the entity class.

```public partial class TaskExtended : IHaveIdentifier<Guid>
    {
        public TaskExtended()
        {
            #region Generated Constructor
            #endregion
        }

        [NotMapped]
        public Guid Id { get { return TaskId; } set { TaskId = value;} }

        #region Generated Properties
        public Guid TaskId { get; set; }
```

If modifying the client-side data provider's constructor for this reason is too obnoxious to you, feel free to also add the Id Property to the DTOs like the above.

### When an entity uses a composite key

If you don't have a single unique property on your entity, you're going to have to add one to your data model.  
Sorry. Your existing keys and so on can remain and are still useful for filtering data,
but to satisfy the ability to get related data by identifier, you just have to have some sort of unique property.
This is the limit of what we can work around on react-admin's dialect and core functionality.

With that unique identifier, you can now make your entity implement IHaveIdentifier<T>.

In your controller you may need to specify the property name in the constructor, and in GetIdentifierEqualityFn as you see above.

You may need to override the API's create method to generate a key manually before passign it on.

#### Entity/mapper doesn't filter soft-deleted data

No problem. Override the base controller's WithFilter delegate so that it includes the filter you need.

```protected override IQueryable<Department> WithFilter(IQueryable<Department> queryable)
    {
        return queryable
            .Include(x => x.Adlocs)
            .Where(x => x.Active && x.Adlocs.Count(y => y.ActiveFlag == true) > 0);
    }
```

In the above example we force queries to Department to filter to only those who are

- Active
- Have more than one active 'Adloc' children

## Making certain entities as read-only.

There are two steps to this:

1. Add the entity type name in the exclude list for the create and update node in the generation.yml file.
2. Cleanup any previously-generated DTO/Mapper code:
   - Remove the create/update models from the Domain\Models folder.
   - Remove the mappings in your Domain\Mapping\\[entity].cs file.

Note that there's a read-only base controller as well you probably want to inherit from to serve this object.
