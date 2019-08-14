-- A SQL Script to create proper tables in the `footballdata` (or however you decide to name it) database

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
