
Create database MovieReviewer
Create table [User] (
	id int identity(1,1) primary key,
	[status] bit default 0 not null,
	Email varchar(50) not null,
	[Password] varchar(50) not null,
	[Role] int default 2 not null,
	Img varchar(max) 
)
Create table Genre(
	GenreId int identity(1,1) primary key,
	GenreName varchar(255) not null
)
create table Movie(
	MovieId int identity(1,1) primary key,
	MovieName varchar(255) not null,
	[Description] varchar(255) not null,
	trailer varchar(255) not null,
	Poster varchar(255) not null,
	Duration int not null,
	OriginalLanguage varchar(255),
	DateCreate date default getdate(),
)
Create table GenreMovie(
	MovieId int  REFERENCES Movie(MovieId),
	GenreId int REFERENCES Genre(GenreId),
	Primary key(MovieId,GenreId)
)
Create table Actor(
	ActorId int identity(1,1) primary key,
	Name varchar(max) not null,
	Gender bit not null,
	Dob Date ,
	Img varchar(255) not null,
	Detail varchar(255),
	Nationality varchar(255),
)
Create Table ActorMovie(
	MovieId int  REFERENCES Movie(MovieId),
	ActorId int REFERENCES Actor(ActorId),
	Primary key(MovieId,ActorId)
)
Create table MovieImg(
	MovieId int references Movie(MovieId) primary key,
	Img varchar(255) 
)
Create table Rate(
	RateId int identity(1,1) primary key,
	RateContent nvarchar(255) ,
	RateStar int not null,
	UserId int  REFERENCES [User](id),
	MovieId int references Movie(MovieId),
	RateDate date default getdate()
)
Create table RateLike(
	RateId int  REFERENCES Rate(Rateid) not null,
	UserId int  REFERENCES [User](id) not null,
	
)
Create table FavouriteList(
	Id int identity(1,1) primary key,
	MovieId int references Movie(MovieId),
	UserId int  REFERENCES [User](id)
)
Create table WatchList(
	Id int identity(1,1) primary key,
	MovieId int references Movie(MovieId),
	UserId int  REFERENCES [User](id)

)
ALTER TABLE Actor
ADD Name varchar(max);
alter table movie 
add  DateCreate date default getdate()

Insert into [User](status,Email,Password,Role,Img) values (1,'huyheotmhp2001@gmail.com','123456',1,'https://www.google.com/url?sa=i&url=https%3A%2F%2Fvov.vn%2Fvan-hoa%2Fsan-khau-dien-anh%2Fson-ye-jin-tai-xuat-man-anh-nho-sau-con-sot-ha-canh-noi-anh-870073.vov&psig=AOvVaw1zk77bC1FW4EVpzbw1WcU-&ust=1682262558445000&source=images&cd=vfe&ved=0CBEQjRxqFwoTCKD2q8Xivf4CFQAAAAAdAAAAABAD'),
Insert into [User](status,Email,Password,Role,Img) values (1,'huyheotmhp2002@gmail.com','123456',1,'https://www.google.com/url?sa=i&url=https%3A%2F%2Fvov.vn%2Fvan-hoa%2Fsan-khau-dien-anh%2Fson-ye-jin-tai-xuat-man-anh-nho-sau-con-sot-ha-canh-noi-anh-870073.vov&psig=AOvVaw1zk77bC1FW4EVpzbw1WcU-&ust=1682262558445000&source=images&cd=vfe&ved=0CBEQjRxqFwoTCKD2q8Xivf4CFQAAAAAdAAAAABAD'),
Insert into Movie values('The classic','Description','VU1ah3RxIww','https://thegioidienanh.vn/stores/news_dataimages/huonggiang/042018/10/20/1127_xemphimon_co-dien.jpg',155,'Korea')
select * from [User]