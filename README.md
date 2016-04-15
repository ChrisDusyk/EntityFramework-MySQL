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

[GitHub](https://github.com/LordCheese/EntityFramework-MySQL)