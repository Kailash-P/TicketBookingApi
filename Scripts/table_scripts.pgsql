drop table if exists public.user;

create table if not exists public.user(
	id int generated always as identity primary key,
	name varchar(20) not null,
	password varchar(200) not null,
	is_admin boolean default(false) not null
);

drop table if exists public.movie;

create table if not exists public.movie (
	id int generated always as identity primary key,
	name varchar(50) not null,
	genre_id int not null, -- considered as enum in code (since it has only less items)
	language_id int not null -- considered as enum in code (since it has only less items)
);

drop table if exists public.user_booking;

create table if not exists public.user_booking(
	id int generated always as identity primary key,
	movie_id int not null,
	user_id int not null,
	constraint fk_user_booking_movie_id foreign key (movie_id) references public.movie (id),
	constraint fk_user_booking_user_id foreign key (user_id) references public.user (id)
);
