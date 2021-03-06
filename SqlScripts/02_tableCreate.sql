USE [SuperheroesDb];
GO

CREATE TABLE [Superhero]
(
    [Id] INT NOT NULL IDENTITY,
    [Name] NVARCHAR(160) NOT NULL,
    [Alias] NVARCHAR(160) NOT NULL,
    [Origin] NVARCHAR(160) NOT NULL,
    CONSTRAINT [PK_Superhero] PRIMARY KEY CLUSTERED ([Id])
);

CREATE TABLE [Assistant]
(
    [Id] INT NOT NULL IDENTITY,
    [Name] NVARCHAR(160) NOT NULL,
    CONSTRAINT [PK_Assitant] PRIMARY KEY CLUSTERED ([Id])
);

CREATE TABLE [Power]
(
    [Id] INT NOT NULL IDENTITY,
    [Name] NVARCHAR(160) NOT NULL,
    [Description] NVARCHAR(160) NOT NULL,
    CONSTRAINT [PK_Power] PRIMARY KEY CLUSTERED ([Id])
);