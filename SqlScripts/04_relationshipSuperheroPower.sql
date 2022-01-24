
CREATE TABLE [SuperheroPower]
(
    [SuperheroId] INT NOT NULL,
    [PowerId] INT NOT NULL,
    FOREIGN KEY ([SuperheroId]) REFERENCES [Superhero],
    FOREIGN KEY ([PowerId]) REFERENCES [Power],
    CONSTRAINT [SuperheroPowerId] PRIMARY KEY ([SuperheroId], [PowerId])
);