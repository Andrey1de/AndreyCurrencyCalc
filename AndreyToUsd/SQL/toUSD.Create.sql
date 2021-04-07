CREATE TABLE RateUsd (
    code          CHAR (3)     CONSTRAINT firstkey PRIMARY KEY,
    name          VARCHAR (30) NOT NULL,
    rate          DOUBLE       NOT NULL,
    bid           DOUBLE       NOT NULL,
    ask           DOUBLE       NOT NULL,
    stored        DATE,
    lastRefreshed DATE
);
