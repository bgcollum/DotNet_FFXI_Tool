print '' print '*** Running sample_user_data.sql ***'

USE [ffxi_equipment_db]
GO

print '' print '*** Inserting User test records'
GO
INSERT INTO [dbo].[User]
		([User_Name], [Email])
	VALUES
		('AnonUser',	'anonymous@no.email'),
		('AdminMan',	'admin@db.test'),
		('GenericUser',	'generic@db.test')
GO

print '' print '*** Inserting sample role records'
GO
INSERT INTO [dbo].[Role]
		([RoleID],	[Role_Description])
	VALUES
	('Administrator',			'Database Administrator, can alter the master tables'),
	('Contributor',				'High-Level user, allowed to run CRUD functions on items'),
	('Registered_User',			'Normal user permissions, can add and populate characters and their inventories'),
	('Anonymous_User',			'Reduced user permissions, can view but not create')
GO

print '' print '*** Inserting sample User_Role records'
GO
INSERT INTO [dbo].[User_Role]
		([UserID], [RoleID])
	VALUES
		(100000,	'Anonymous_User'),
		(100001,	'Administrator'),
		(100001,	'Contributor'),
		(100001,	'Registered_User'),
		(100002,	'Contributor'),
		(100002,	'Registered_User')
GO

print '' print '*** Inserting sample Player_Character records'
GO
INSERT INTO [dbo].[Player_Character]
		([UserID], [Char_Name])
	VALUES
		(100001,	'AdminMans Man - Blast Hardcheese'),
		(100001,	'AdminMans Man - Zap Rousedower'),
		(100002,	'GenericUsersGuy - Anatoli Smorin'),
		(100002,	'GenericUsersGuy - Todd Bonzalez'),
		(100002,	'GenericUsersGuy - Bobson Dugnutt')
GO