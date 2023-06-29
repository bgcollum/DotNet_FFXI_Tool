CREATE DATABASE IF NOT EXISTS FFXI_STAT_SOURCE_DB;

USE FFXI_STAT_SOURCE_DB;

-- Clear join tables first, in order of dependency

DROP TABLE IF EXISTS inventory_equipment_set;
DROP TABLE IF EXISTS equipment_set;

DROP TABLE IF EXISTS conditional_stats;
DROP TABLE IF EXISTS augment_stats;
DROP TABLE IF EXISTS equip_stats;
DROP TABLE IF EXISTS inventory;
DROP TABLE IF EXISTS user_role;
DROP TABLE IF EXISTS player_character;
-- Clear base tables
DROP TABLE IF EXISTS user;
DROP TABLE IF EXISTS role;
DROP TABLE IF EXISTS master_stat_list;
DROP TABLE IF EXISTS master_equipment_list;

-- Create user table
DROP TABLE IF EXISTS user;
CREATE TABLE user
	(
    user_id			INT				PRIMARY KEY		AUTO_INCREMENT,
    user_name		VARCHAR(50)		NOT NULL	UNIQUE,
    email			VARCHAR(100)	NOT NULL	UNIQUE,
	passwordhash	VARCHAR(100)	NOT NULL	DEFAULT
    '9c9064c59f1ffa2e174ee754d2979be80dd30db552ec03e7e327e9b1a4bd594e'
    );
    
-- Create role table
DROP TABLE IF EXISTS role;
CREATE TABLE role
	(
    role_id			VARCHAR(50)	PRIMARY KEY,
    role_description	VARCHAR(256)
    );

-- Create user_role join table
DROP TABLE IF EXISTS user_role;
CREATE TABLE user_role
	(
    user_id			INT			NOT NULL,
    role_id			VARCHAR(50)	NOT NULL,
    -- Create composite primary key
    CONSTRAINT pk_user_role_user_id_role_id PRIMARY KEY (user_id, role_id),
    CONSTRAINT fk_user_role_user_id FOREIGN KEY (user_id)
		REFERENCES user (user_id),
	CONSTRAINT fk_user_role_role_id FOREIGN KEY (role_id)
		REFERENCES role (role_id)
	);

-- Create Player Character table (Can't name it Character because that is a data type!)
DROP TABLE IF EXISTS player_character;
CREATE TABLE player_character
(
	character_id			INT			PRIMARY KEY		AUTO_INCREMENT,
    char_name		VARCHAR(50)	UNIQUE	NOT NULL,
	user_id			INT			NOT NULL,
    CONSTRAINT fk_player_character_user_id	FOREIGN KEY (user_id)
		REFERENCES user (user_id)
);

-- Create Master Stat List: This is a record of every game stat that exists.
DROP TABLE IF EXISTS master_stat_list;
CREATE TABLE master_stat_list
	(
	stat_name		VARCHAR(50)	PRIMARY KEY,
	stat_data_type	VARCHAR(25), -- User reference field
	stat_description	VARCHAR(256) -- User reference field
);

-- Create Master Equipment List: This table represents every individual equippable item that exists in the game
DROP TABLE IF EXISTS master_equipment_list;
CREATE TABLE master_equipment_list
(
	-- item_id			INT		PRIMARY KEY		AUTO_INCREMENT,
    item_id			INT			PRIMARY KEY UNIQUE, -- Removing auto_increment to use existing item_IDs from https://www.ffxiah.com/
    item_name		VARCHAR(50)	NOT NULL	-- Name is not unique to accomodate for REMA versions. Give them per-item aliases.
);

-- This table represents the stat configuration of items
DROP TABLE IF EXISTS equip_stats;
CREATE TABLE equip_stats
(
	item_id			INT			NOT NULL, -- Primary key from master_equipment_list
	stat_name		VARCHAR(50) NOT NULL, -- Primary key from master_stat_list
    stat_value		INT 		NOT NULL,
    -- Create composite primary key
    CONSTRAINT pk_equip_stats_item_id_stat_name PRIMARY KEY (item_id, stat_name),
    CONSTRAINT fk_equip_stats_item_id FOREIGN KEY (item_id)
		REFERENCES master_equipment_list (item_id),
	CONSTRAINT fk_equip_stats_stat_name FOREIGN KEY (stat_name)
		REFERENCES master_stat_list (stat_name)
);


-- This table represents what characters own instances of what items
DROP TABLE IF EXISTS inventory;
CREATE TABLE inventory
(
	inventory_id	INT		PRIMARY KEY		AUTO_INCREMENT, -- This may be more of a challenge. Ask about this
    item_alias		VARCHAR(50)	DEFAULT NULL,	-- Since augmented items can be duplicates of a base item, this is used for alternate names
    character_id			INT		NOT NULL,
    item_id			INT		NOT NULL,
    -- CONSTRAINT pk_inventory_character_id_item_id PRIMARY KEY (character_id, item_id), -- Using inventory_id as primary key
	CONSTRAINT fk_inventory_character_id FOREIGN KEY (character_id)
		REFERENCES player_character (character_id),
	CONSTRAINT fk_inventory_item_id FOREIGN KEY (item_id)
		REFERENCES master_equipment_list (item_id)
);

-- This table represents stat augments on specific instances of an item (Player specific)
DROP TABLE IF EXISTS augment_stats;
CREATE TABLE augment_stats
(
	inventory_id	INT			NOT NULL,
    stat_name		VARCHAR(50)	NOT NULL,
    stat_value		INT			NOT NULL,
    CONSTRAINT pk_augment_stats_inventory_id_stat_name PRIMARY KEY (inventory_id, stat_name),
    CONSTRAINT fk_augment_stats_inventory_id FOREIGN KEY (inventory_id)
		REFERENCES inventory (inventory_id),
	CONSTRAINT fk_augment_stats_stat_name FOREIGN KEY (stat_name)
		REFERENCES master_stat_list (stat_name)
);

-- This table represents individual equipment sets that a character can equip
DROP TABLE IF EXISTS equipment_set;
CREATE TABLE equipment_set
(
	set_id			INT		PRIMARY KEY		AUTO_INCREMENT,
    set_name		VARCHAR(50)		NOT NULL,
    character_id	INT		NOT NULL,
	CONSTRAINT fk_equipment_set_character_id FOREIGN KEY (character_id)
		REFERENCES player_character (character_id)
);

DROP TABLE IF EXISTS inventory_equipment_set;
CREATE TABLE inventory_equipment_set
(
	set_id			INT		NOT NULL,
    inventory_id	INT		NOT NULL,
    CONSTRAINT pk_inventory_equipment_set_set_id_inventory_id PRIMARY KEY (set_id, inventory_id),
    CONSTRAINT fk_inventory_equipment_set_set_id FOREIGN KEY (set_id)
		REFERENCES equipment_set (set_id),
	CONSTRAINT fk_inventory_equipment_set_inventory_id FOREIGN KEY (inventory_id)
		REFERENCES inventory (inventory_id)
);
