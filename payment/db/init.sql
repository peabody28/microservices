CREATE TABLE IF NOT EXISTS wallet (
	Id uuid NOT NULL PRIMARY KEY,
   	Number string NOT NULL
);

CREATE TABLE IF NOT EXISTS balance_operation_type (
	Id uuid NOT NULL PRIMARY KEY,
    Code string NOT NULL
);

CREATE TABLE IF NOT EXISTS payment (
	Id uuid NOT NULL PRIMARY KEY,
	WalletFk uuid NOT NULL,
	BalanceOperationTypeFk uuid NOT NULL,
	Amount decimal(10,5) NOT NULL,
    Created TEXT NOT NULL DEFAULT (datetime('now')), 
	FOREIGN KEY (WalletFk) REFERENCES wallet (Id),
	FOREIGN KEY (BalanceOperationTypeFk) REFERENCES balance_operation_type (Id)
);

INSERT OR IGNORE INTO balance_operation_type VALUES('957A723C-3F85-4734-85E8-94668C8F1083','Credit');
INSERT OR IGNORE INTO balance_operation_type VALUES('289CB45D-375A-4A7B-9FFD-AC7E2207C364','Debit');