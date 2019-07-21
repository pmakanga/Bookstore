Create BookStore Db
==================
BookStore

Create TblUser
==============

USE [BookStore]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[TblUser](
    [UserID] [int] IDENTITY(1,1) NOT NULL,
    [FullName] [varchar](50) NULL,
    [Email] [varchar](50) NULL,
    [Password] [varbinary](128) NULL,
    [Salt] [varbinary](128) NULL,
 CONSTRAINT [PK_TblUser] PRIMARY KEY CLUSTERED
(
    [UserID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

Create TblBooks
===============

USE [BookStore]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[TblBook](
    [BookID] [int] IDENTITY(1,1) NOT NULL,
    [ISBN] [varchar](50) NULL,
    [Title] [varchar](100) NULL,
    [Author] [varchar](50) NULL,
    [Description] [varchar](200) NULL,
    [Publisher] [varchar](50) NULL,
    [PublishedYear] [int] NULL,
    [Price] [decimal](18, 0) NULL,
 CONSTRAINT [PK_TblBook] PRIMARY KEY CLUSTERED
(
    [BookID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

Create Asp.NetCore Api
======================
dotnet  new webapi –o BookstoreAPI –n BookstoreAPI

Install the following Packages (ignore versions)
================================================
dotnet add package Microsoft.EntityFrameworkCore.Tools -v 2.1.2
dotnet add package Microsoft.EntityFrameworkCore.SqlServer -v 2.1.2
dotnet add package Microsoft.EntityFrameworkCore.SqlServer.Design -v 1.1.6
dotnet add package Microsoft.VisualStudio.Web.CodeGeneration.Design -v 2.1.5
dotnet add package Microsoft.VisualStudio.Web.CodeGeneration.Tools -v 2.0.4
dotnet add package AutoMapper.Extensions.Microsoft.DependencyInjection -v 5.0.1


Build Application
=================
dotnet build

configure connections to Microsoft SQL Server Database
======================================================
Add below configuration to appsettings.json

"ConnectionStrings": {
  "SQLConnection": "Server=.;Database=BookStore;Trusted_Connection=True;User Id=sa;Password=q;Integrated Security=false;MultipleActiveResultSets=true"
}

configuration for Token secret
==============================
"AppSettings": {
    "Token": "MySecretTokenForJwt"
}

Add BookStoreContext in Startup.cs
==================================
services.AddDbContext<BookStoreContext>(options => options.UseSqlServer(Configuration.GetConnectionString("SQLConnection")));


Add below lines under ConfigurationServices
===========================================
services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
  .AddJwtBearer(options => {
      options.TokenValidationParameters = new TokenValidationParameters
      {
          ValidateIssuerSigningKey = true,
          IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII
              .GetBytes(Configuration.GetSection("AppSettings:Token").Value)),
          ValidateIssuer = false,
          ValidateAudience = false
      };
  });

Add below lines under Configure method
=====================================
app.UseCors(x => x.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
app.UseAuthentication();

Generate Models and Context from Microsoft SQL Server Database
==============================================================
dotnet ef dbcontext scaffold "Server=HP-840\SQLEXPRESS;Database=BookStore;Trusted_Connection=True;User Id=sa;Password=edmondb;Integrated Security=false;" Microsoft.EntityFrameworkCore.SqlServer -o Models







