<h1 align="center">Noroff Assignment 2</h1>
<p align="center">
	<img src="sql-logo.png" width="300">
</p>

[![standard-readme compliant](https://img.shields.io/badge/standard--readme-OK-green.svg?style=flat-square)](https://github.com/RichardLitt/standard-readme)

Noroff assignment number 2, written by Konstantinos Pascal and Darius Davidonis.

## Table of Contents

-  [Install](#install)
-  [Usage](#usage)
-  [Maintainers](#maintainers)
-  [Contributing](#contributing)
-  [License](#license)

## Install

Clone the repository using:

```
git clone https://github.com/konstapascal/noroff-assignment-2.git
```

## Usage

Open the **Chinook_SqlServer_AutoIncrementPKs.sql** file with **SQL Server Management Studio** and execute the script. This will create your **Chinook** database and its tables.

Go to the **ChinookReader/DataAccess/ConnectionHelper.cs** file and change **builder.DataSource** to your own server name. This can be found from within **SSMS** by:

1. Right clicking on your server inside the **Object Explorer**
2. Click on **Properties**, last option
3. Copy the value of the **Name** field

You may now run the **Program.cs** file to see the ouput of the different operations. Changes are also reflected inside the actual database.

## Maintainers

[@konstapascal](https://github.com/konstapascal)\
[@dariusdav](https://github.com/dariusdav)

## Contributing

PRs accepted.

Small note: If editing the README, please conform to the [standard-readme](https://github.com/RichardLitt/standard-readme) specification.

## License

MIT © 2022 Konstantinos Pascal &amp; Darius Davidonis
