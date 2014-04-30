create table countries
(
 id integer primary key autoincrement not null, 
 name text not null, 
 is_important integer not null 
);

create table locations
(
 id integer primary key autoincrement not null, 
 parent_id integer, 
 country_id integer not null, 
 level integer not null, 
 is_capital integer not null, 
 foreign key (parent_id) references locations(id), 
 foreign key (country_id) references countries(id)
);

create index locations_parent_id_idx on locations(parent_id);
create index locations_country_id_idx on locations(country_id);