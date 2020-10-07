# React-Admin NetCore ServerAPI
A .Net Core REST API builder that auto generates EF entities automapped to CRUD models and exposes them in a [react-admin](https://github.com/marmelab/react-admin)-compatible [REST dialect](https://github.com/zachrybaker/ra-data-rest-client).


## CRUD API quickly, using patterns and DAL [re]generation.
It takes very little time or code to stand up a REST API, complete with Swagger UI.

The data access layer (your "DAL") auto [re]generation is based on [Loresoft's](www.loresoft.com) [EFCore generator](https://github.com/loresoft/EntityFrameworkCore.Generator).  

Exposing CRUD actions on your DAL entities happens by giving them an interface at the class level.  This allows them to be used against mapped DTOs (also auto-generated) in controllers.  The controllers will serve your API compliant with a more workable [implementation](https://github.com/zachrybaker/ra-data-rest-client) of
[react-admin's](https://github.com/marmelab/react-admin) "Simple REST" data provider.  

## Choose your path
There are three projects in this repo:
1. Common -> contains base controllers that translate bwtween the chosen react-admin REST dialect and an EF Core DAL.
2. DemoAPI -> example of a complete SQL-based REST API.
3. APITemplate -> a REST WebAPI project with dependencies in place and ready for you to auto generate models/entities and define your controllers.

Any of these three projects are usable starting points for your project.  But the intention is you start with APITemplate, and take the steps described below.  I've dropped an installSQL.sql file in the APITemplate project so that you can follow along in that project to see how easy it is to build the demo app yourself!


## 1. Autogenerate your Data Access Layer (your "DAL")

TL'DR: follows Loresoft's [Generate-ASP-NET-Web-API](http://www.loresoft.com/Generate-ASP-NET-Web-API) walkthru.

Heads up - their tool is oriented around being used directly in a web app. To generate your DAL elsewhere is more work.

You need to generate your DAL into this project and correct startup.cs for the conn string and db context. Then your app will compile.
DemoAPI basically follows their walkthru, with a few tweaks:

### 1.a. Build or initialize a generator configuration file (generation.yml)

You may create your own yaml file from scratch with this command, from the solution folder:

> efg initialize -c "[db conn]" --id "d106eb49-28ab-49be-a838-0229c48642d2" --name "ConnectionStrings:DemoDBConnectionString" -d ./ReactAdminNetCoreServerAPI.DemoAPI

However you can also start with the included generation.yml file which instructs efg to skip query generation and makes some things read-only.  Either way make sure and change the guid to match your user secrets file, your db conn details, etc.

### 1.b. [re]Generate the Code for the DAL (the EF Core modek, the mappers, and DTOs):

I ran this from the solution folder:

> efg generate -d "ReactAdminNetCoreServerAPI.DemoAPI"

Now that you have the basic Web API project setup, you can run efg generate after any database change to keep all your entity and view models in sync with the database.
Note that generated code regions are what get touched, your edits outside those regions survive regeneration.

## 2. Update your startup.cs

I'd suggest you add logging while you're here. Serilog seems like a good go-to.

## 3. Implement IHaveIdentifier&lg;T&gt; on data entities

To expose data to the REST API, modify the generated entity classes in the Data -> Entities folder to implement the IHaveIdentifier&lg;T&gt; interface, where T is the type of identifier your entity uses.

This allows for you to create empty controllers that hopefully <em>just</em> have a constructor, to serve DTO-based CRUD for these entities! But you can extend them as necessary as we will see shortly.

Note that the identifier is usually also the primary key, but this is not strictly required. For example, you may be wondering what to do if you use compound keys. Don't worry, you're covered (keep reading).

## 4. Add empty controllers to expose your API

They should inherit from:

- EntityWithIdControllerBase or
- ReadOnlyEntityWithControllerBase or
- One of the "Convenience" controllers in the folder by the same name.

Unless you need to do something special, you're done!  Run your API in peace. 

## 5. Handle outliers to the react-admin-expected schema & dialect.

In the real world, often we have to live with existing data schema.  
We can't touch it, or can't afford to touch it, or need to minimize accomidations for a UI.

<strong>Note: One big accomidation</strong> react-admin expects of you is to have your entities
(they call them resources on the client)
 use a single string property, called "Id", as your key.

### <em>When an entity doesn't use a string identifier/key:</em>

No worries.  
The generic type in IHaveIdentifier&lg;T&gt; deals with this for you in the base controller.

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

You will also need to add the "NotMapped" Id property to satisfy the IHaveIdentifier&lg;T&gt; on the entity class.

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

### <em>When an entity uses a composite key</em>

If you don't have a single unique property on your entity, you're going to need to add one to your SQL data model.  
Sorry. Your existing keys and so on can remain and are also useful for filtering data,
but to satisfy the ability to get related data by identifier, you must have some sort of unique property.
This is the limit of what we can work around on react-admin's dialect and core functionality.

With that unique identifier in place, regen your DAL.  Now you can now make your entity implement IHaveIdentifier<T>.

In your controller you need to specify that property name in the constructor, and in GetIdentifierEqualityFn as you see above.

Don't forget that if your database doesn't autogenerate this uinque property on insert, that you need to override the API's create action to generate a key manually before the DAL-facing method call.

#### <em>Entity/mapper doesn't know my business rules or how to filter my data</em>

No problem. Override the base controller's WithFilters delegate so that it includes the filter you need.

```protected override IQueryable<Department> WithFilters(IQueryable<Department> queryable)
    {
        return queryable
            .Include(x => x.Adlocs)
            .Where(x => x.Active && x.Adlocs.Count(y => y.ActiveFlag == true) > 0);
    }
```

In the above example we force queries to Department to filter to only those who are:
- Active
- Have more than one active 'Adloc' children

You could also build detail or list view models by including related entities.  For example, let's say we want the user detail data to join users to userRoles to role, so that both user and userroles?  Add this to the controller:

```protected override IQueryable<User> IncludeTheseForDetailResponse(IQueryable<User> queryable) => 
        queryable
            .Include(x => x.UserRoles)         // get the user's role IDs...
            .ThenInclude(y => y.Role)          // and then those corresponding roles.
            .Where(x => x.IsDeleted == false); // This where could have been saved for the WithFilters override...
```

There is a corresponding method for list views. 

The other thing we have to do is instruct the read model of User to include the List of UserRoleReadModel...
````public partial class UserReadModel
    {
    
        public List<UserRoleReadModel> UserRoles { get; set; }
````
...and the UserRoleReadModel the list of RoleReadModel.

Note that running custom business logic code COULD be another extension method delegate that we add to the base controllers.  

````// Copyright (c) 2017 Jon P Smith, GitHub: JonPSmith, web: http://www.thereformedprogrammer.net/
    // Licensed under MIT licence. See License.txt in the project root for license information.
    
    using System.Collections.Immutable;
    using System.ComponentModel.DataAnnotations;
    
    namespace BizLogic.GenericInterfaces
    {
        public interface IBizAction<in TIn, out TOut>//#A
        {
            IImmutableList<ValidationResult> 
                Errors { get; }      //#B
            bool HasErrors { get; }  //#B
            TOut Action(TIn dto);                  //#C
        }
    }
````
At this time we haven't done that, but perhaps we will soon?

## Making certain entities read-only.

There are two steps to this:

1. Add the entity type name in the exclude list for the create and update node in the generation.yml file (so future regeneration doesn't recreate what you are about to remove).
2. Cleanup any previously-generated DTO/Mapper code:
   - Remove the create/update models from the Domain\Models folder.
   - Remove the mappings in your Domain\Mapping\\[entity].cs file.

Note that there's a read-only base controller as well you probably want to inherit from to serve this object.
