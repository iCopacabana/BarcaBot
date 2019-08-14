# BarcaBot
A FC Barcelona themed Discord bot for 'Barça Reddit-Gràcies Storm'.

## Table of contents
1. Installation
    1. Dependencies
    2. Compile (`dotnet publish`)
    3. Prepare the database
    4. Run
2. Available commands
3. Contributing
4. TODO

## Installation
This chapter describes how to install BarcaBot and run it on your machine.

### Dependencies
Let's start by installing all the neccessary dependencies.

#### PostgreSQL
BarcaBot utilizes PostgreSQL as its database. The version that has been tested and is proven to work with BarcaBot is `postgresql11-server` from the `https://yum.postgresql.org/11/fedora/fedora-30-x86_64/pgdg-fedora-repo-latest.noarch.rpm` repository. It is expected for the bot to work perfectly with PostgreSQL 11 on all Operating Systems that it is available on, although this has not been tested.

The bot is known to not work with PostgreSQL 9 due to its incompatibility with Hangfire which is a C# library that BarcaBot uses.

#### Python 3.7
BarcaBot has been written for Python 3.7.4. Python <3.6 is known to not work due to Literal String Interpolation not being present. In addition to installing Python 3.7.4 you will also need to install following libraries:
```
- Pillow
- pyyaml
- Flask
- flask-restful
- gevent
```

A script (`Scripts/pip.sh`) has been included to automatically install those on Fedora. It uses `pip3 --user` to install the libraries. It should be compatible with whatever OS you may have as long as you `pip` for Python 3 under `pip3`.

#### Node.js
BarcaBot has been written with `node.js v12` in mind, although `v10` should also work, however, this has not beent tested. To view view all the `node` dependencies go to `ChartsMicroservice/package.json`.

#### .NET Core
You will .NET Core SDK 2.2 to compile and run BarcaBot. You can see all the individual `nuget` packages BarcaBot uses by checking individual `.csproj` files.

### Compile
Clone this repository by using `git clone` and then to compile BarcaBot please run the included `Scripts/publish_all.sh` script.

### Prepare the database.
Create 3 databases, one for FootballData, one for Hangfire, and one for API-Football. You can name them however you want. Create tables that match models in `Barcabot/Barcabot.Common/DataModels`. In the future I plan to include SQL Scripts that will do this for you.

### Run
Before running please rename `sample_config.yaml` located in the root directory to `config.yaml` and fill it in with appropriate data. Make sure database names in `config.yaml` match the database names you gave to your databases in the previous step.

To run BarcaBot please launch the `Scripts/run_all.sh` script.

## Available commands
`=help`
Lists all the available commands.

`=nextmatch`
Shows the next match FC Barcelona will play (in more detail than -schedule).

`=schedule`
Shows the upcoming games for FC Barcelona.

`=player playername`
Generates a player card with up to date stats (updated daily).

`=playerchart player`
Generates a stat chart with up to date date stats (updated daily).

`=playerschart player1 player2`
Generates a stat chart with up to date date stats (updated daily) that compares stats of selected players.

`=laligascorers`
Shows up to date (updated every minute) top goal scorers for the LaLiga Santander.

`=uclscorers`
Shows up to date (updated every minute) top goal scorers for the UEFA Champions League.

`=feature`
Shows you how to request a feature.

`=github`
Shows a link to BarcaBot's main Github repo.

`=issue`
Shows you how to report an issue.

## Contributing
To contribute to the project you don't even need to know how to code! Bug reports and feature requests are just as valuable as Pull Requests (though if you want to, please do open one, and I'll make sure to review it; make sure, however, that you document all the changes you've made well in the commit messages as well as the PR description) and the easiest way to support the project is just giving this repository a star.

## TODO
To see the TODO list please click [here](https://github.com/TraceLD/BarcaBot/blob/master/TODO).
