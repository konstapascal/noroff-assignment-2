USE [SuperheroesDb];

ALTER TABLE [dbo].[Assistant]
ADD [SuperheroId] int
CONSTRAINT [SuperheroId] 
FOREIGN KEY ([SuperheroId]) REFERENCES [dbo].[Superhero] ([Id]) ON DELETE NO ACTION ON UPDATE NO ACTION;