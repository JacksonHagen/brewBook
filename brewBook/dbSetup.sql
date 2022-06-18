CREATE TABLE IF NOT EXISTS accounts(
    id VARCHAR(255) NOT NULL primary key COMMENT 'primary key',
    createdAt DATETIME DEFAULT CURRENT_TIMESTAMP COMMENT 'Time Created',
    updatedAt DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT 'Last Update',
    name varchar(255) COMMENT 'User Name',
    email varchar(255) COMMENT 'User Email',
    picture varchar(255) COMMENT 'User Picture'
) default charset utf8 COMMENT '';

CREATE TABLE IF NOT EXISTS beans(
    id INT NOT NULL AUTO_INCREMENT primary key,
    origin TEXT NOT NULL,
    roaster TEXT NOT NULL,
    process TEXT,
    roastLevel INT NOT NULL,
		tastingNotes TEXT,
    creatorId VARCHAR(255) NOT NULL,
    FOREIGN KEY(creatorId) REFERENCES accounts(id)
) default charset utf8;

CREATE TABLE IF NOT EXISTS espressorecipes(
    id INT NOT NULL AUTO_INCREMENT primary key,
    creatorId VARCHAR(255) NOT NULL,
    beanId INT NOT NULL,
    title VARCHAR(255) DEFAULT "An Espresso Recipe",
    dose DECIMAL(3, 1) NOT NULL,
    temp INT,
    grinder VARCHAR(255) DEFAULT NULL,
    grindSetting VARCHAR(255) DEFAULT NULL,
    description TEXT NOT NULL,
    yield DECIMAL(4, 1) NOT NULL,
    preinfusionTime INT,
    extractionTime INT NOT NULL,
    machine VARCHAR(255),
    FOREIGN KEY(creatorId) REFERENCES accounts(id) ON DELETE CASCADE,
    FOREIGN KEY(beanId) REFERENCES beans(id)
) default charset utf8;

CREATE TABLE IF NOT EXISTS filterrecipes(
    id INT NOT NULL AUTO_INCREMENT primary key,
    creatorId VARCHAR(255) NOT NULL,
    beanId INT NOT NULL,
    title VARCHAR(255) DEFAULT "An Espresso Recipe",
    dose DECIMAL(3, 1) NOT NULL,
    temp INT,
    grinder VARCHAR(255) DEFAULT NULL,
    grindSetting VARCHAR(255) DEFAULT NULL,
    description TEXT NOT NULL,
		timeInSeconds INT NOT NULL,
		totalWeight INT NOT NULL,
		brewer VARCHAR(255),
		FOREIGN KEY(creatorId) REFERENCES accounts(id) ON DELETE CASCADE,
    FOREIGN KEY(beanId) REFERENCES beans(id)
) default charset utf8