print '' print '*** Running sample_items.sql ***'

USE [ffxi_equipment_db]
GO

print '' print '*** Inserting sample Equipment records'
GO
INSERT INTO [dbo].[Item]
		([ItemID], [Item_Name])
	VALUES
	/* Skipping conditional stats for now */
		(1,'Helmet of thinking')
		,(2,'Cuirass of not getting stabbed')
		,(3,'Gauntlets of squishing')
		,(4,'Pants of keeping your bits in')
		,(5,'Boots of running urgently')
		,(6,'Codpiece of variable attractiveness')
		/* FFXI Item Demo Items */
GO

print '' print '*** Inserting sample statistics for sample equipment'
GO
INSERT INTO [dbo].[Equip_Stats]
		/* Add latent effects later! */
		([ItemID], [StatID], [Stat_Value])
	VALUES
		/* Helmet of thinking (INT 25 MND 15) */
		(1, 'INT', 25),
		(1, 'MND', 15),
		/* Cuirass of not getting stabbed (VIT 50 AGI 13) */
		(2, 'VIT', 50),
		(2, 'AGI', 13),
		/* Gauntlets of squishing (STR 30 DEX 12) */
		(3, 'STR', 30),
		(3, 'DEX', 12),
		/* Pants of keeping your bits in (VIT 16 AGI -10) */
		(4, 'VIT', 16),
		(4, 'AGI', -10),
		/* Boots of running urgently (DEX 30 CHR 5) */
		(5, 'DEX', 30),
		(5, 'CHR', 5),
		/* Codpiece of variable attractiveness (CHR30 skipping latent CHR-60) */
		(6 , 'CHR', 30)
GO