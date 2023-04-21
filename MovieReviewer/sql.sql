Create database MovieReviewer
Create table [User] (
	id int identity(1,1) primary key,
	[status] bit default 0 not null,
	Email varchar(50) not null,
	[Password] varchar(50) not null,
	[Role] int default 2 not null,
	Img varchar default 'abc'
)
Create table Genre(
	GenreId int identity(1,1) primary key,
	GenreName varchar not null
)
create table Movie(
	MovieId int identity(1,1) primary key,
	MovieName varchar not null,
	[Description] varchar not null,
	trailer varchar not null,
	Poster varchar not null,
	Duration int not null,
	OriginalLanguage varchar,
)
Create table GenreMovie(
	MovieId int  REFERENCES Movie(MovieId),
	GenreId int REFERENCES Genre(GenreId),
	Primary key(MovieId,GenreId)
)
Create table Actor(
	ActorId int identity(1,1) primary key,
	Gender bit not null,
	Dob Date ,
	Img varchar not null,
	Detail varchar,
	Nationality varchar,
)
Create Table ActorMovie(
	MovieId int  REFERENCES Movie(MovieId),
	ActorId int REFERENCES Actor(ActorId),
	Primary key(MovieId,ActorId)
)
Create table MovieImg(
	MovieId int references Movie(MovieId) primary key,
	img varchar 
)
Create table Rate(
	RateId int identity(1,1) primary key,
	RateContent nvarchar ,
	RateStar int not null,
	UserId int  REFERENCES [User](id),
)
Create table RateLike(
	RateId int  REFERENCES Rate(Rateid) not null,
	UserId int  REFERENCES [User](id) not null,
	Primary key (RateId,UserId)
)
Create table FavouriteList(
	MovieId int references Movie(MovieId),
	UserId int  REFERENCES [User](id)
	Primary key(MovieId,UserId)
)
Create table WatchList(
	MovieId int references Movie(MovieId),
	UserId int  REFERENCES [User](id)
	Primary key(MovieId,UserId)
)