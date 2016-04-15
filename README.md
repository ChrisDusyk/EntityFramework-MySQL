I recently did a project involving ASP.NET 4.6.1, Entity Framework 6.1.3, and Amazon Aurora. Aurora is Amazon's distributed database engine, designed to take advantage of the processing available in a large cloud datacentre. I initially found it very challenging to connect Entity Framework code-first to MySQL, however I've found info in a few places and can put together a better tutorial and sample here. Once I figured it all out, it turned out to be quite easy. Certainly not quite as easy as if I were using MS SQL Server, but that's not always an option. Many of the MySQL-variants are much cheaper to run than MS SQL Server, especially for a smaller company.

## What you need
For this you will need Visual Studio 2013 or 2015 Community (or higher), and a MySQL-based database. I used a free-tier MariaDB instance on AWS, however anything that is MySQL or a fork of it will work fine. 

# Building it
## Project setup
First, open Visual Studio (I'm using Visual Studio 2015), and create a new ASP.NET 4.6.1 web application. Any version of .NET that's 4.5.0 and up will work for this, as we're not using any functionality specific to 4.6.1.
 
![Create Project](http://i.imgur.com/aYUIQPv.png?2 "Create Project") 

I've named the project MariaCodeFirst, as I am using a MariaDB instance on AWS while testing this.

![Project Options](http://i.imgur.com/aIkPrwk.png?3 "Project Options") 

Click on "OK" to continue to the next screen. Here, choose the ASP.NET template you want. I'm using MVC in this example. Also change the Authentication option to "No Authentication". Authentication can be added later, and isn't necessary for this sample.

![ASP.NET Templates](http://i.imgur.com/Fgi2KJw.png?1 "ASP.NET Templates") 

Visual Studio will now create your project and solution. Once everything has been created, we'll add the NuGet packages you'll need. Right-click on the web app project, and select "Manage NuGet Packages..."

![NuGet](http://i.imgur.com/x99V9Nr.png?2 "Manage NuGet Packages") 

Once NuGet has opened, you can update any packages that need to be updated. The default web app template is usually a few versions behind on most packages.

After updating the default packages, it's time to add the Entity Framework and MySql.Data packages we need. In NuGet, add the following packages to your project:

![Entity Framework](http://i.imgur.com/1it75QQ.png?1 "Entity Framework") 
![MySql.Data](http://i.imgur.com/MBXUi7q.png?1 "MySql.Data") 

Once you've added the packages, build your project again to download the DLLs from NuGet.

## Data setup
In this example, I'm going to use a "code-first from existing database" setup. I'll create a Customers table in the database to import into the data model. Connect to your MySQL-based database and enter the following code:

    CREATE TABLE TestDB.Customers
    (
        CustomerId   INT PRIMARY KEY AUTO_INCREMENT,
        CustomerName VARCHAR(200) NOT NULL,
        CreatedDate  DATETIME NOT NULL
    );
    CREATE UNIQUE INDEX Customers_CustomerId_uindex ON TestDB.Customers (CustomerId);

This will give us a table to import into the model to start with. We'll add more tables through code-first migrations later.

## Creating the Entity Framework context
Now, we'll create the Entity Framework context. First, create a new folder named "Entities" under the "Models" folder:

![Entities](http://i.imgur.com/UnfOzbt.png?1 "Entites") 

Next, right-click on the "Entities" folder, and go to Add > New Item...

![New Item](http://i.imgur.com/L71M0uT.png?1 "New Item") 

In the New Item dialog, go to Visual C# > Data and select "ADO.NET Entity Data Model". Enter the name you want to give your database context and click "Add":

![ADONET Context](http://i.imgur.com/vQIZiSE.png?1 "ADONET Context") 

Next you'll be taken to the Entity Data Model Wizard. Here, choose "Code First from database and click "Next":

![Choose Type](http://i.imgur.com/xDC8n3o.png?1 "Choose Type") 

On the screen of the wizard, you'll need to set up the data connection. Click on "New Connection" to set up your connection:

![Data Connection](http://i.imgur.com/LHJcB9D.png?1 "Data Connection") 

Enter your database's connection information in the Connection Properties dialog:

![New Connection](http://i.imgur.com/yDkih1t.png?2 "New Connection") 

Once you close the Connection Properties dialog, your connection information will appear in the Entity Data Model Wizard. Click "Next":

![Connection Setup](http://i.imgur.com/6UtoNPQ.png?2 "Connection Setup") 

Choose the Customers table in the list and click "Finish":

![Finish Context](http://i.imgur.com/zPwI7e9.png?1 "Finish Context") 

The NuGet installs should have added everything you need to the web.config, but let's confirm that to be on the safe side. Go to the web.config file and look for the <entityFramework> and <system.data> sections. They should look something like this:

![Web.config](http://i.imgur.com/PMrYH3d.png?1 "Web.config") 

## Setting up the context classes
Now that the context has been created, there are a few more steps to make it all work properly for code-first. First we'll create the DbInitializer. Right-click on the Entities folder and go to Add > Class:

![DbInitializer class](http://i.imgur.com/pDB2Esr.png?1 "DbInitializer class") 

Enter the class name and click "Add":

![Create class](http://i.imgur.com/PCyp2HP.png?1 "Create class") 

In the new class, enter the following code:

![DbInitializer code](http://i.imgur.com/wBjxlRI.png?1 "DbInitializer code") 

It derives from the context, running through an Entity Framework partial that will tell the migration to create the database if it doesn't already exist. We then implement the Seed method, which can be used to seed the database with data when running a migration.

Back in the context class, add the following line above the class declaration:

    [DbConfigurationType(typeof(MySql.Data.Entity.MySqlEFConfiguration))]

And the following line to the MariaDBContext constructor:

    Database.SetInitializer<MariaDBContext>(new MariaDbInitializer());

So that it looks like this:

![Context class done](http://i.imgur.com/aJ1OImx.png?1 "Context class done") 

## Code-first migrations
Next we will need to enable code-first migrations. To do this, open the Package Manger Console and type in: 

    Enable-Migrations 
If everything has been done correctly so far, it will respond with:

    Checking if the context targets an existing database...
    Code First Migrations enabled for project MariaCodeFirst.

This will create the code-first Configuration.cs file and a Migrations folder:

![Migrations folder](http://i.imgur.com/JAMI8bE.png?1 "Migrations folder") 

Open the Configuration.cs file and add the following class to the bottom of the namespace, below the Configuration class:

    public class MySqlHistoryContext : HistoryContext
    {
        public MySqlHistoryContext(DbConnection connection, string defaultSchema) : base(connection, defaultSchema)
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<HistoryRow>().Property(h => h.MigrationId)
                .HasMaxLength(100)
                .IsRequired();

            modelBuilder.Entity<HistoryRow>().Property(h => h.ContextKey)
                .HasMaxLength(200)
                .IsRequired();
        }
    }

![History class](http://i.imgur.com/LUjw3Ca.png?1 "History class") 

And finally, add the following lines to the Configuration constructor:

    SetSqlGenerator("MySql.Data.MySqlClient", new MySql.Data.Entity.MySqlMigrationSqlGenerator());

    SetHistoryContextFactory("MySql.Data.MySqlClient", (conn, schema) => new MySqlHistoryContext(conn, schema));

![Config constructor](http://i.imgur.com/NWdIpMG.png?1 "Config constructor") 

Finally, it's time to create and run the actual migration (in this case, just to ensure we're connected to the database and that it will work) Open the Package Manager Console again and type:

    Add-Migration 'InitialMigration'

You will see the following message when it's done scaffolding the migration:

    Scaffolding migration 'InitialMigration'.

Next, to update the database, type in the following command to apply the migration to the database:

    Update-Database

It will respond with:

    Specify the '-Verbose' flag to view the SQL statements being applied to the target database.
    Applying explicit migrations: [201604150720588_InitialMigration].
    Applying explicit migration: 201604150720588_InitialMigration.
    Running Seed method.

If you check the database now, the Customers table will exist in the database, as well as the Entity Framework __Migrations table.

I hope this helps you get started with Entity Framework code-first with a MySQL-based database engine. I had a lot of issues with this initially, and it turned out to be a lot easier than I first thought. Hopefully this saves you a couple headaches!