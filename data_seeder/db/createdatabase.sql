CREATE DATABASE testautomation;
\connect testautomation;
CREATE TABLE users
(
    id BIGSERIAL PRIMARY KEY NOT NULL,
    username VARCHAR(50) NOT NULL,
    password VARCHAR(250) NOT NULL,
    role VARCHAR (50) NOT NULL,
    active BOOLEAN NOT NULL
);
CREATE UNIQUE INDEX users_id_uindex ON users (id)