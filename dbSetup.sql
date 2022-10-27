CREATE TABLE
    IF NOT EXISTS accounts(
        /* REVIEW accounts have a string because it uses the id that is returned from auth0 */
        id VARCHAR(255) NOT NULL primary key COMMENT 'primary key',
        createdAt DATETIME DEFAULT CURRENT_TIMESTAMP COMMENT 'Time Created',
        updatedAt DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT 'Last Update',
        name varchar(255) COMMENT 'User Name',
        email varchar(255) COMMENT 'User Email',
        picture varchar(255) COMMENT 'User Picture'
    ) default charset utf8 COMMENT '';

CREATE TABLE
    IF NOT EXISTS albums(
        /* REVIEW we use int for all ID's and auto_increment for our database to handle counting 1,2,3,.. */
        id INT NOT NULL PRIMARY KEY AUTO_INCREMENT,
        createdAt DATETIME DEFAULT CURRENT_TIMESTAMP COMMENT 'Time Created',
        updatedAt DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT 'Last Update',
        title VARCHAR(255) NOT NULL,
        coverImg VARCHAR(255) NOT NULL,
        creatorId VARCHAR(255) NOT NULL,
        category VARCHAR(255) NOT NULL,
        archived TINYINT default 0,
        FOREIGN KEY (creatorId) REFERENCES accounts(id)
    ) default charset utf8 COMMENT '';

CREATE TABLE
    IF NOT EXISTS pictures(
        id INT NOT NULL PRIMARY KEY AUTO_INCREMENT,
        createdAt DATETIME DEFAULT CURRENT_TIMESTAMP COMMENT 'Time Created',
        updatedAt DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT 'Last Update',
        imgUrl VARCHAR(255) NOT NULL,
        creatorId VARCHAR(255) NOT NULL,
        albumId INT NOT NULL,
        FOREIGN KEY (creatorId) REFERENCES accounts(id),
        FOREIGN KEY (albumId) REFERENCES albums(id)
    ) default charset utf8 COMMENT '';

CREATE TABLE
    IF NOT EXISTS album_members(
        id INT NOT NULL PRIMARY KEY AUTO_INCREMENT,
        createdAt DATETIME DEFAULT CURRENT_TIMESTAMP COMMENT 'Time Created',
        updatedAt DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT 'Last Update',
        albumId INT NOT NULL,
        accountId VARCHAR(255) NOT NULL,
        FOREIGN KEY (albumId) REFERENCES albums(id),
        FOREIGN KEY (accountId) REFERENCES accounts(id)
    ) default charset utf8 COMMENT '';

SELECT
    alb.*,
    a.id AS accountId,
    a.*
FROM albums alb
    JOIN accounts a ON a.id = alb.creatorId;

SELECT
    alb.*,
    COUNT(am.id) AS MemberCount,
    a.*
FROM albums alb
    JOIN accounts a ON a.id = alb.creatorId
    LEFT JOIN album_members am ON am.albumId = alb.id
GROUP BY alb.id;