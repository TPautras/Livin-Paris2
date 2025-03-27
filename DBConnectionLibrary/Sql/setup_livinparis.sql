-- Création de la base de données livinparis (si elle n'existe pas déjà)
CREATE DATABASE IF NOT EXISTS livinparis;

-- Si un objet serveur existe déjà sous le nom "livinparis_server", on le supprime
DROP SERVER IF EXISTS livinparis_server;

-- Création de l'objet serveur qui pointe vers la base livinparis
CREATE SERVER livinparis_server
    FOREIGN DATA WRAPPER mysql
    OPTIONS (
    USER 'root',
    PASSWORD 'root',  -- Remplacez par votre mot de passe
    HOST 'localhost',
    PORT 3306,
    DATABASE 'livinparis'
    );
