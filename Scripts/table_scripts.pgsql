drop table if exists public.user cascade;

create table if not exists public.user(
	id int generated always as identity primary key,
	name varchar(20) not null,
	password varchar(200) not null,
	is_admin boolean default(false) not null
);

drop table if exists public.city cascade;

create table if not exists public.city(
    id int generated always as identity primary key,
    name varchar(20) not null
);

drop table if exists public.multiplex cascade;

create table if not exists public.multiplex(
    id int generated always as identity primary key,
    name varchar(20) not null,
    city_id int not null,
    total_seats int not null,
    constraint fk_multiplex_city_id foreign key (city_id) references public.city (id)
);

drop table if exists public.movie cascade;

create table if not exists public.movie (
	id int generated always as identity primary key,
	name varchar(50) not null,
	genre_id int not null, -- considered as enum in code (since it has only less items)
	language_id int not null, -- considered as enum in code (since it has only less items)
	multiplex_id int not null, -- considered as enum in code (since it has only less items)
    constraint fk_movie_multiplex_id foreign key (multiplex_id) references public.multiplex (id)
);

drop table if exists public.user_booking cascade;

create table if not exists public.user_booking(
	id int generated always as identity primary key,
	movie_id int not null,
	user_id int not null,
	seats int not null,
	constraint fk_user_booking_movie_id foreign key (movie_id) references public.movie (id),
	constraint fk_user_booking_user_id foreign key (user_id) references public.user (id)
);
