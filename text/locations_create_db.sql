create table version
(
 db_version text not null
);

create table country_names
( 
 locale text not null,
 code text not null,  
 name text not null, 
 primary key (locale, code)
);

create table country_ranks
( 
 locale text not null,
 code text not null,  
 rank int not null, 
 primary key (locale, code)
);

drop index country_ranks_locale_rank_idx
create index country_ranks_locale_rank_idx on country_ranks(locale, rank desc)

-- рейтинг по странам locale=RU
--select cr.code, cn.name, cr.rank 
--from country_ranks cr
--inner join country_names cn on cr.locale = cn.locale and cr.code = cn.code
--where cr.locale = 'RU'
--order by rank desc