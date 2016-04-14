I recently did a project involving ASP.NET 4.6.1, Entity Framework 6.1.3, and Amazon Aurora. Aurora is Amazon's distributed database engine, designed to take advantage of the processing available in a large cloud datacentre. I initially found it very challenging to connect Entity Framework code-first to MySQL, however I've found info in a few places and can put together a better tutorial and sample here. Once I figured it all out, it turned out to be quite easy. Certainly not quite as easy as if I were using MS SQL Server, but that's not always an option. Many of the MySQL-variants are much cheaper to run than MS SQL Server, especially for a smaller company.

## What you need
For this you will need Visual Studio 2013 or 2015 Community (or higher), and a MySQL-based database. I used a free-tier MariaDB instance on AWS, however anything that is MySQL or a fork of it will work fine. 

# Building it
First, open Visual Studio (I'm using Visual Studio 2015), and create a new ASP.NET 4.6.1 web application. Any version of .NET that's 4.5.0 and up will work for this, as we're not using any functionality specific to 4.6.1. 
![Create Project](http://i.imgur.com/aYUIQPv.png?2, "Create Project")

I've named the project MariaCodeFirst, as I am using a MariaDB instance on AWS while testing this.
![Project options](http://i.imgur.com/aIkPrwk.png?3, "Project options")

Visual Studio will now create your project and solution. Once everything has been created, we'll add the NuGet packages you'll need. Right-click on the web app project, and select "Manage NuGet Packages..."
![NuGet](http://i.imgur.com/x99V9Nr.png?2, "Manage NuGet Packages")

Once NuGet has opened, you can update any packages that need to be updated. The default web app template is usually a few versions behind on most packages.

After updating the default packages, it's time to add the Entity Framework and MySql.Data packages we need. In NuGet, add the following packages to your project:

![Entity Framework](http://i.imgur.com/1it75QQ.png?1, "Entity Framework")
![MySql.Data](http://i.imgur.com/6Jc3Dot.png?1, "MySql.Data")

Once you've added the packages, build your project again to download the DLLs from NuGet.
