-- A SQL Script to create neccessary tables in the `barcabot` (or however you decided to name it) database

create table laligascorers
(
	scorerid integer not null
		constraint laligascorers_pk
			primary key,
	scorername varchar not null,
	scorerteam varchar not null,
	scorergoals integer
);

create table matches
(
	matchid integer not null
		constraint matches_pk
			primary key,
	matchcompetition varchar not null,
	matchdate varchar not null,
	matchstadium varchar,
	matchhometeam varchar not null,
	matchawayteam varchar not null,
	matchtotalmatches integer,
	matchtotalgoals integer,
	matchwins integer,
	matchdraws integer,
	matchlosses integer
);

create table uclscorers
(
	scorerid integer not null
		constraint uclscorers_pk
			primary key,
	scorername varchar not null,
	scorerteam varchar not null,
	scorergoals integer
);

create table player
(
	id integer not null
		constraint player_pk
			primary key,
	name varchar,
	position varchar,
	age integer,
	nationality varchar,
	height varchar,
	weight varchar,
	rating double precision,
	shotstotal double precision,
	shotsontarget double precision,
	shotspercentageontarget double precision,
	passestotal double precision,
	passeskeypasses double precision,
	passesaccuracy double precision,
	tacklestotaltackles double precision,
	tacklesblocks double precision,
	tacklesinterceptions double precision,
	duelswon double precision,
	duelspercentagewon double precision,
	dribblesattempted double precision,
	dribbleswon double precision,
	dribblespercentagewon double precision,
	foulsdrawn double precision,
	foulscommitted double precision,
	goalstotal integer,
	goalsconceded integer,
	goalsassists integer
);