print '' print '*** Running sample_inventories.sql ***'

USE [ffxi_equipment_db]
GO

print '' print '*** Inserting sample Inventory item records'
GO
INSERT INTO [dbo].[Inventory]
		([CharacterID], [ItemID], [Item_Alias])
	VALUES
		/* CharacterID 100001,	'AdminMans Man - Blast Hardcheese' */
		(100001, 1, 'Electronic brain pancake crystal elderly'),
		(100001, 3, NULL),
		/* CharacterID 100002, AdminMans Man - Zap Rousedower'*/
		(100002, 4, "Zap's Prodigious Prominence"),
		(100002, 5, "Webbed Feet of Not Drowning"),
		/* 100003,	'GenericUsersGuy - Anatoli Smorin' */
		(100003, 2, NULL),
		(100003, 3, NULL),
		(100003, 5, NULL)
GO