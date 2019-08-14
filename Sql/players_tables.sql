-- A SQL Script to create proper tables in the `players` (or however you decide to name it) database

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

