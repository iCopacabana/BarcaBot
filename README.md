# BarcaBot
[![License](https://img.shields.io/badge/license-BSD%203--Clause-blue)](https://github.com/TraceLD/BarcaBot/blob/master/LICENSE)
![OS](https://img.shields.io/badge/platform-linux-lightgrey)
[![Build Status](https://travis-ci.org/TraceLD/BarcaBot.svg?branch=master)](https://travis-ci.org/TraceLD/BarcaBot)

A FC Barcelona themed Discord bot for 'Barça Reddit-Gràcies Storm'.

If you are looking for an invite link for BarcaBot, there unfortunately isn't one. While I would love to host it for everyone, I currently don't have the finances to pay for a VPS powerful enough to host the bot for multiple servers, so if you want to use it, you'll have to host it yourself.

If you want to simply deploy BarcaBot on your own VPS/local machine/server so that you can use it in your own Discord server and don't care about how it works or any implementation details skip to the Section 4 of this document, although it is recommended that you read all of it.

## Table of contents
1.  Available commands
2.  Where does the data come from?
3.  Structure of BarcaBot
    1.  Microservices
        -   API Endpoints
    2.  Scripts
    3.  Database
4.  Installation and deployment
    1.  Installing the dependencies
    2.  Compiling
    3.  Preparing the database
    4.  Configuring BarcaBot
    5.  Running BarcaBot
5.  Contributing
6.  TODO

## 1. Available Commands
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

## 2. Where does the data come from?

BarcaBot requires a lot of data to function, from player stats to the match schedule. I've put a lot of thought into where this data should be obtained from. The easiest (and cheapest) solution would be to scrape sites like WhoScored, Flashscores, etc. using one of the scraping libraries (such as Puppeteer.NET) as that would provide access to the largest array of data, however, this solution comes with many legal, as well as ethical conerns, and therefore I've deemed it unsuitable. There are football stats and data provides such as Opta, but the issue with them is that they can cost hundreds, if not thousands of pounds per month.

I've ended up going with a combination of two Freemium APIs - API-FOOTBALL and FootballData. They both provide a basic free tier, which I can use for everything BarcaBot needs. Instead of calling the API every single time a user requests a command, I'm updating my own database with data on a schedule (Hangfire Recurring Cron Jobs). This, while not providing truly real time data, allows me to essentially bypass any rate limits.

If you want to self-host BarcaBot you will need to obtain tokens for those APIs, as well as the plotly charting library. This will be further discussed in Section 4, Subsection 4.

## 3. Structure of BarcaBot
BarcaBot consists of many microservices that combined make up the BarcaBot app.


### I. Microservices

1.  BarcaBot.Bot
The core Discord.NET Bot that is actually responsible for sending messages/images/embeds in response to user commands.

2.  Barcabot.HangfireService
Exposes the API endpoints that add/update Cron jobs that have been discussed in Section 2. It then runs those Cron jobs on a provided schedule. It also provides a Dashboard that allows you to see which jobs have succeeded and which failed, etc. It also allows you to remove previously added jobs.

3.  ChartsMicroservice
Responds to POST requests sent by Barcabot.Bot with Plotly charts that are then forwarded to the end user that has requested the command on Discord.

4.  ImageManipulationService
Responds to POST requests sent by Barcabot.Bot with Player cards that are then forwarded to the end user that has requested the command on Discord.

#### API endpoints
I've mentioned quite a few API endpoints in the above Subsection. In this 'Subsubsection' (is that even a word?) you will find more details about them.

BarcaBot exposes a few API endpoints that the Barcabot.Bot then uses to get some of the data and images. They are hosted on localhost.

Before discussing them further, it is important that you get yourself familiar with the Player `json` object as we will be passing it as the body of POST requests.

Example of the Player object:
```json
{
    "Id": 154,
    "Name": "Lionel Andres Messi Cuccittini",
    "Position": "Attacker",
    "Age": 32,
    "Nationality": "Argentina",
    "Height": "170 cm",
    "Weight": "72 kg",
    "Rating": 8.53,
    "Per90Stats": {
        "Shots": {
            "Total": 5.65,
            "OnTarget": 2.77,
            "PercentageOnTarget": 48.99},
        "Passes": {
            "Total": 49.04,
            "KeyPasses": 3.02,
            "Accuracy": 80.0},
            "Tackles": {
                "TotalTackles": 0.43,
                "Blocks": 0.0,
                "Interceptions": 0.14
            },
        "Duels": {
            "Won": 7.37,
            "PercentageWon": 51.6
        },
        "Dribbles": {
                "Attempted": 7.35,
                "Won": 4.42,
                "PercentageWon": 60.12
        },
        "Fouls": {
            "Drawn": 2.2,
            "Committed": 0.71
        }
    },
    "Goals": {
        "Total": 51,
        "Conceded": 0,
        "Assists": 18
    }
}
```

Now that you're familiar with the Player object we can discuss the endpoints.

1.  GET `localhost:3000/`
Shows the status of the Charts microservice.
2.  POST `localhost:3000/stats/player`
Produces a stat chart for a given Player object (found in the POST request's body).
3.  POST `localhost:3000/stats/players`
Produces a stat chart for a list of given 2 Player objects (found in the POST request's body) (e.g. `[playerObject1, playerObject2]`).
4.  POST `localhost:4000/player_cards/attacker/`
Produces an attacker type player card for a given Player object (found in the POST request's body).
5.  POST `localhost:4000/player_cards/midfielder/`
Produces a midfielder type player card for a given Player object (found in the POST request's body).
6.  POST `localhost:4000/player_cards/defender/`
Produces a defender type player card for a given Player object (found in the POST request's body).
7.  POST `localhost:4000/player_cards/goalie/`
Produces a goalkeeper type player card for a given Player object (found in the POST request's body).
8.  GET `localhost:5001/`
Shows the status of the HangfireService microservice.
9.  GET `localhost:5001/`
Shows the status of the HangfireService microservice.
10. `localhost:5001/hangfire`
This is not a RESTFUL endpoint but a dashboard that allows you to see the status of all the Cron jobs that have been previously discussed as well as remove them.
11. GET `localhost:5001/api/footballdatajobs`
Adds/updates the following Cron jobs and returns the status of the method that adds them:
    - Update LaLiga scorers every 2 minutes
    - Update UCL scorers every 2 minutes
    - Update FCB's matches schedule every 2 minutes

12. GET `localhost:5001/api/playersjobs`
Adds/updates the following Cron jobs and returns the status of the method that adds them:
    - Update all the player stats every 24h

### II. Scripts
A few scripts come packaged with BarcaBot to aid in the installation and deployment of it. I think it's important that you know which one of them does before you run any of them (remember, never run mysterious scripts). Some of them you may choose not to use (for example the ones to run the bot, if you for example prefer to convert each microservice into a Linux `systemd` service instead of running them using `nohup` in combination with provided scripts).

1.  `pip.sh`
This script installs all of the libraries the `ImageManipulationMicroservice` relies on for the current user. It relies on `pip` for Python 3 being under the `pip3` command.

2.  `publish_all.sh`
This script compiles the C# microservices as well as installs `ChartsMicroservice`'s node dependencies in the `node_modules` folder (it does not install anything globally, so don't worry).

3.  `run_bot.sh`
Runs the BarcaBot.Bot microservice and keeps it alive in case it crashes for whatever reason. Meant to be used in combination with `run_all.sh`.

4.  `run_charts_microservice.sh`
Runs the ChartsMicroservice and keeps it alive in case it crashes for whatever reason. Meant to be used in combination with `run_all.sh`.

5.  `run_img_microservice.sh`
Runs the ImageManipulationMicroservice and keeps it alive in case it crashes for whatever reason. Meant to be used in combination with `run_all.sh`.

6.  `run_hangfire_microservice.sh`
Runs the Barca.HangfireService microservice and keeps it alive in case it crashes for whatever reason. Meant to be used in combination with `run_all.sh`.

7.  `run_all.sh`
Runs all the previously mentioned run scripts in the background to launch all the microservices.

### III. Database
BarcaBot uses PostgreSQL 11 databases to store all the data. I've packaged SQL Scripts in the `Sql/` dir to aid in the preparation of each of the databases. This is also where you can see what tables it uses. This is further discussed in Sections 2 and 4.

## 4. Installation and deployment
This section describes how to install and deploy the bot. A several scripts that come packaged with BarcaBot will be used in this section. To learn more about them and what they exactly do go to Section 3, Subsection 2 of this document.

**The following guide is for Linux only!**

### I. Installing the dependencies
1.  Python 3.7.4 or newer
BarcaBot has been written for Python 3.7.4. Realistically any version of Python 3.7 should work just fine, although this has not been tested. Python < 3.6 is known to not work due to Literal String Interpolation not being present.
In addition to installing Python 3.7.4 you will also need to install the following `pip` libraries:
```
- Pillow
- pyyaml
- Flask
- flask-restful
- gevent
```
If you want to install the libraries automatically I've included a script in the `/Scripts` that does exactly that. It depends on `pip` for Python 3 being under `pip3` command. If yours isn't, you will have to install the libraries manually.

You can learn how to install Python and use `pip` [here](https://python101.readthedocs.io/pl/latest/env/linux.html).

2.  node.js
BarcaBot has been written with `node.js v12` in mind, although `v10` should also work, however, this has not been tested.

You can learn how to install node.js [here](https://nodejs.org/en/download/).

3.  .NET Core 2.2 SDK
BarcaBot targets .NET Core 2.2 and you will need to both compile the C# microservices from source as well as run them, so the runtime alone is not going to be enough and you will need to install the whole SDK.

You can learn how to do that [here](https://dotnet.microsoft.com/download/linux-package-manager/rhel/sdk-2.2.401).

4.  PostgreSQL 11
BarcaBot utilizes PostgreSQL as its database. The version that has been tested and is proven to work with BarcaBot is `postgresql11-server` from the `https://yum.postgresql.org/11/fedora/fedora-30-x86_64/pgdg-fedora-repo-latest.noarch.rpm` repository. It is expected for the bot to work perfectly with PostgreSQL 11 on all Linux distors that it is available on, although this has not been tested.

The bot is known to not work with PostgreSQL 9 due to its incompatibility with Hangfire which is a C# library that BarcaBot uses.

5.  3rd party libraries
BarcaBot in addition to using the dependencies discussed above also uses a lot of other 3rd party libraries in the form of node libraries (or 'modules') and nuget packages. Those will not be discussed as they are going to be automatically resolved when you run the compile script which we will do in the next step. If you really want to see what they are you can look at `ChartsMicroservice/package.json`, as well as individual `.csproj` files.

### II. Compiling
1.  `git clone` this repository.

2.  Run the script `Scripts/publish_all.sh`. It will run the `dotnet publish` command that compiles the solution as well as `node install` to resolve the dependencies of the ChartsMicroservice.

### III. Preparing the database
1.  Create 3 databases, one for FootballData, one for Hangfire, and one for API-FOOTBALL. You can name them however you want.

2.  Run the `Sql/footballdata_tables.sql` SQL script in the FootballData database to create the neccessary tables.

3.  Run the `Sql/players_tables.sql` SQL script in the API-FOOTBALL database to create the neccessary tables.

4.  Configure PostgreSQL so that you can access it using a username and a password. Your favourite search engine is going to be your best friend here.

You do not need to do anything with the Hangfire database as it will be automatically configured by the `Barcabot.HangfireService` microservice the first time you run the bot.


### IV. Configuring BarcaBot
Before you run the bot, you need to configure it.

1.  Obtain the neccessary API tokens (keys)
    1.  FootballData token (key) can be obtained from [here](https://www.football-data.org/).
    2.  API-FOOTBALL token (key) can be obtained from [here](https://rapidapi.com/api-sports/api/api-football/pricing).
    3.  Sign up for plotly and obtain a username and a token. You can do so [here](https://plot.ly/Auth/login/?next=%2Fsettings%2Fapi).
    4.  Obtain a Discord API token from [here](https://discordapp.com/developers/).

2.  Rename `sample_config.yaml` file to `config.yaml`.
3.  Replace all the placeholders in the `config.yaml` file with your own tokens, username and database names.

### V. Running BarcaBot
You have two options here:

1.  Run BarcaBot using `Scripts/run_all.sh`. This will simply launch all the microservices in the background and keep them alive in case they crash (may be hard to stop).

2.  Convert each microservice to a `systemd` service, and then enable and start each of the services. Again, your favourite search engine is going to be your best friend here.

The choice is yours.

## Contributing
To contribute to the project you don't even need to know how to code! Bug reports and feature requests are just as valuable as Pull Requests (though if you want to, please do open one, and I'll make sure to review it; make sure, however, that you document all the changes you've made well in the commit messages as well as the PR description) and the easiest way to support the project is just giving this repository a star.

## TODO
To see the TODO list please click [here](https://github.com/TraceLD/BarcaBot/blob/master/TODO).
