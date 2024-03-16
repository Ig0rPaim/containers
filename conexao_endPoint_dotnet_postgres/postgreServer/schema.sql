CREATE DATABASE user_testes;

\c user_testes;

CREATE TABLE users(
    ID SERIAL PRIMARY KEY,
    NOME VARCHAR(50) NOT NULL
);
