CREATE TABLE IF NOT EXISTS user (
	Id uuid PRIMARY KEY,
   	Name string NOT NULL
);

CREATE TABLE IF NOT EXISTS user_status (
	Id uuid NOT NULL PRIMARY KEY,
	UserFk uuid NOT NULL,
   	Status boolean NOT NULL DEFAULT 0, 
	FOREIGN KEY (UserFk) 
      REFERENCES user (Id) 
         ON DELETE CASCADE 
         ON UPDATE NO ACTION
);