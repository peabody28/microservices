CREATE TABLE IF NOT EXISTS role (
	Id uuid NOT NULL PRIMARY KEY,
   	Code string NOT NULL
);

CREATE TABLE IF NOT EXISTS user (
	Id uuid NOT NULL PRIMARY KEY,
	RoleFk uuid NULL,
   	Name string NOT NULL, 
	PasswordHash string NOT NULL, 
	FOREIGN KEY (RoleFk) REFERENCES role (Id) 
);

INSERT OR IGNORE INTO role (Id, Code) VALUES('40FB385F-6C30-406E-A8C2-8CDD4A9734B6', 'Default');