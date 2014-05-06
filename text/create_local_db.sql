attach database 'C:\Users\ra\Desktop\travels\db\locations.db' as locations;
attach database 'C:\Users\ra\Desktop\travels\db\tonkosti_locations.db' as tonkosti_locations;

create table countries
(
 id integer primary key autoincrement not null, 
 name text not null, 
 is_important integer not null, 
 rank integer not null default 0
);

create index countries_rank_idx on countries(rank desc);

create table locations
(
 id integer primary key autoincrement not null, 
 parent_id integer, 
 country_id integer not null,  
 name text not null,
 level integer not null, 
 is_capital integer not null,
  
 foreign key (parent_id) references locations(id), 
 foreign key (country_id) references countries(id)
);

create index locations_parent_id_idx on locations(parent_id);
create index locations_country_id_idx on locations(country_id);

insert into countries
select *, 0 from tonkosti_locations.countries

insert into locations
select * from tonkosti_locations.locations

update countries
set [rank] = 
(
    select (case when count(t.[rank]) > 0 then t.[rank] else 0 end)
    from
    (
      select l_cr.[rank] 
      from tonkosti_locations.[countries] tl_c
      join locations.[country_names] l_cn on tl_c.[name] = l_cn.[name]
      join locations.[country_ranks] l_cr on l_cn.[code] = l_cr.[code]
      where l_cn.[locale] = 'RU' 
        and l_cr.[locale] = 'RU' 
        and tl_c.[id] = countries.[id]
    ) t
)