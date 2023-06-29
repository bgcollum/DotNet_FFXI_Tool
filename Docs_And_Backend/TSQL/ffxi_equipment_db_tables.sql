/* Translating MySQL database to TSQL*/

/* Check whether the database exists, if so, drop it */
IF EXISTS(SELECT 1 FROM master.dbo.sysdatabases
			WHERE name = 'ffxi_equipment_db')
			
BEGIN
	DROP DATABASE ffxi_equipment_db
	print '' print '*** dropping database ffxi_equipment_db'
END
GO

print '' print '*** creating database ffxi_equipment_db'
GO
CREATE DATABASE ffxi_equipment_db
GO

print '' print '*** using database ffxi_equipment_db'
GO
USE [ffxi_equipment_db]
GO

/* Per-Table drop table stuff omitted */

/* User Table and data */
print '' print '*** creating user table'
GO
CREATE TABLE [dbo].[User] (
	[UserID]		[int]			IDENTITY(100000,1)	NOT NULL,
	[User_Name]		[nvarchar](50)						NOT NULL,
	[Email]			[nvarchar](100)						NOT NULL,
	[PasswordHash]	[nvarchar](100)						NOT NULL DEFAULT
		/* username: newuser generated via SHA256 */
		'9c9064c59f1ffa2e174ee754d2979be80dd30db552ec03e7e327e9b1a4bd594e',
	/* Primary and alternate keys */
	CONSTRAINT	[pk_UserID]			PRIMARY KEY([UserID]),
	CONSTRAINT	[ak_User_Name]		UNIQUE([User_Name]),
	CONSTRAINT	[ak_Email]			UNIQUE([Email])
)
GO

/* User roles */
print '' print '*** Creating role table for user permissions'
GO
CREATE TABLE [dbo].[Role] (
	[RoleID]			[nvarchar](50)	NOT NULL,
	[Role_Description]	[nvarchar](256)	NULL,
	CONSTRAINT [pk_RoleID] PRIMARY KEY ([RoleID])
)
GO

/* User Role join table to join User and Role */
print '' print '*** Creating User_Role table'
CREATE TABLE [dbo].[User_Role] (
	[UserID]		[int]			NOT NULL,
	[RoleID]		[nvarchar](50)	NOT NULL,
	CONSTRAINT	[fk_User_Role_UserID] FOREIGN KEY ([UserID])
		REFERENCES [dbo].[User]([UserID]),
	CONSTRAINT	[fk_User_Role_RoleID] FOREIGN KEY ([RoleID])
		REFERENCES [dbo].[Role]([RoleID]),
	CONSTRAINT	[pk_User_Role_UserID_RoleID] PRIMARY KEY ([UserID], [RoleID])
)
GO

/* Player Character table */
print '' print '*** Creating role table for Player Character'
GO
CREATE TABLE [dbo].[Player_Character] (
	[CharacterID]		[int]			IDENTITY(100000,1)	NOT NULL,
	[UserID]			[int]			NOT NULL,
	[Char_Name]			[nvarchar](50)	NOT NULL,
	CONSTRAINT	[fk_Player_Character_UserID] FOREIGN KEY ([UserID])
		REFERENCES [dbo].[User]([UserID]),
	CONSTRAINT	[pk_CharacterID]	PRIMARY KEY ([CharacterID]),
	CONSTRAINT	[ak_Char_Name]			UNIQUE([Char_Name])
)
GO

/* Master Stat List */
print '' print '*** Creating Master Stat List table'
GO
CREATE TABLE [dbo].[Stat] (
	[StatID]			[nvarchar](50)		NOT NULL
	CONSTRAINT [pk_Stat] PRIMARY KEY ([StatID])
)
GO

/* StatAlias - List of alternate names for a stat (Can include language options) */
print '' print '*** Creating Stat Alias table'
GO
CREATE TABLE [dbo].[StatAlias] (
	[StatID]			[nvarchar](50)	NOT NULL,
	[StatAlias]			[nvarchar](100)	NOT NULL
	CONSTRAINT	[fk_Stat_StatID] FOREIGN KEY ([StatID])
		REFERENCES [dbo].[Stat]([StatID]) ON UPDATE CASCADE,
	CONSTRAINT [uk_StatAlias] UNIQUE([StatAlias])
)
GO

/* Master Equipment List */
print '' print '*** Creating Master Equipment List table'
GO
CREATE TABLE [dbo].[Item] (
	[ItemID]		[int]			NOT NULL,	/* Manual ID assignment */
	[Item_Name]		[nvarchar](50)	NOT NULL,	/* NOT unique */
	CONSTRAINT	[pk_Item] PRIMARY KEY ([ItemID])
)
GO

/*
Equip Stats join table joins Master Stat List and Master Equipment List
This represents individual item instances!
*/
print '' print '*** Creating Equip_Stats table'
CREATE TABLE [dbo].[Equip_Stats] (
	[ItemID]		[int]			NOT NULL,
	[StatID]		[nvarchar](50)	NOT NULL,
	[Stat_Value]	[int]			NOT NULL,
	CONSTRAINT	[fk_Equip_Stats_ItemID] FOREIGN KEY ([ItemID])
		REFERENCES [dbo].[Item]([ItemID]) ON UPDATE CASCADE,
	CONSTRAINT	[fk_Equip_Stats_StatID] FOREIGN KEY ([StatID])
		REFERENCES [dbo].[Stat]([StatID]) ON UPDATE CASCADE,
	CONSTRAINT	[pk_Equip_Stats_ItemID_StatID] PRIMARY KEY ([ItemID], [StatID])
)
GO

/* Creating Inventory Table (Individual character inventories) */
print '' print '*** Creating Inventory table'
CREATE TABLE [dbo].[Inventory] (
	[InventoryID]		[int]			IDENTITY(100000,1)	NOT NULL,
	[CharacterID]		[int]			NOT NULL,
	[ItemID]			[int]			NOT NULL,
	[Item_Alias]		[nvarchar](50)	NULL,	/* Defaults to NULL */

	CONSTRAINT	[fk_Inventory_CharacterID] FOREIGN KEY ([CharacterID])
		REFERENCES [dbo].[Player_Character]([CharacterID]) ON UPDATE CASCADE,
	CONSTRAINT	[fk_Inventory_ItemID] FOREIGN KEY ([ItemID])
		REFERENCES [dbo].[Item]([ItemID]) ON UPDATE CASCADE,
	CONSTRAINT	[pk_InventoryID] PRIMARY KEY ([InventoryID])
)
GO

/*
Augment Stats join table
This represents character specific item augments
*/
print '' print '*** Creating Augment_Stats table'
CREATE TABLE [dbo].[Augment_Stats] (
	[InventoryID]	[int]			NOT NULL,
	[StatID]		[nvarchar](50)	NOT NULL,
	[Stat_Value]	[int]			NOT NULL,
	CONSTRAINT	[fk_Augment_Stats_InventoryID] FOREIGN KEY ([InventoryID])
		REFERENCES [dbo].[Inventory]([InventoryID]) ON UPDATE CASCADE,
	CONSTRAINT	[fk_Augment_Stats_StatID] FOREIGN KEY ([StatID])
		REFERENCES [dbo].[Stat]([StatID]) ON UPDATE CASCADE,
	CONSTRAINT	[pk_Augment_Stats_InventoryID_StatID] PRIMARY KEY ([InventoryID], [StatID])
)
GO

/*
Equipment Set join table
This represents character specific equipment sets
*/
print '' print '*** Creating Equipment_Set table'
CREATE TABLE [dbo].[Equipment_Set] (
	[SetID]			[int]			IDENTITY(100000,1)	NOT NULL,
	[Set_Name]		[nvarchar](50)	NOT NULL,
	[CharacterID]	[int]			NOT NULL,
	CONSTRAINT	[fk_Equipment_Set_CharacterID] FOREIGN KEY ([CharacterID])
		REFERENCES [dbo].[Player_Character]([CharacterID]),
	CONSTRAINT	[pk_Equipment_Set] PRIMARY KEY ([SetID])
)
GO

/*
Inventory Equipment Set join table
This table connects player's inventories to their Equipment sets
*/
print '' print '*** Creating Inventory Equip Set table'
CREATE TABLE [dbo].[Inventory_Equipment_Set] (
	[InventoryID]	[int]		NOT NULL,
	[setID]			[int]		NOT NULL,
	CONSTRAINT	[fk_Inventory_Equipment_Set_InventoryID] FOREIGN KEY ([InventoryID])
		REFERENCES [dbo].[Inventory]([InventoryID]),
	CONSTRAINT	[fk_Inventory_Equipment_Set_SetID] FOREIGN KEY ([SetID])
		REFERENCES [dbo].[Equipment_Set]([SetID]),
	CONSTRAINT	[pk_Inventory_Equipment_Set_InventoryID_SetID] PRIMARY KEY ([InventoryID], [SetID])
)
GO
