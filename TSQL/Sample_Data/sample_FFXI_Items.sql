
/*
These are some sample items built in my program that model a few actual FFXI items
As you can see, these were exported from Visual Studio
*/

print '' print '*** Running sample_FFXI_Items.sql ***'
USE [ffxi_equipment_db]
GO
print '' print '*** Inserting Apocalypse ***'
GO

INSERT INTO [dbo].[Item] ([ItemID], [Item_Name]) VALUES (21808, N'Apocalypse')
GO
INSERT INTO [dbo].[Equip_Stats] ([ItemID], [StatID], [Stat_Value]) VALUES (21808, N'Accuracy', 60)
INSERT INTO [dbo].[Equip_Stats] ([ItemID], [StatID], [Stat_Value]) VALUES (21808, N'AddEffectBlindness', 10)
INSERT INTO [dbo].[Equip_Stats] ([ItemID], [StatID], [Stat_Value]) VALUES (21808, N'Afterglow', 1)
INSERT INTO [dbo].[Equip_Stats] ([ItemID], [StatID], [Stat_Value]) VALUES (21808, N'AftermathJAHaste', 10)
INSERT INTO [dbo].[Equip_Stats] ([ItemID], [StatID], [Stat_Value]) VALUES (21808, N'Augmented', 1)
INSERT INTO [dbo].[Equip_Stats] ([ItemID], [StatID], [Stat_Value]) VALUES (21808, N'ilvl', 119)
INSERT INTO [dbo].[Equip_Stats] ([ItemID], [StatID], [Stat_Value]) VALUES (21808, N'ilvlMagicAccuracySkill', 242)
INSERT INTO [dbo].[Equip_Stats] ([ItemID], [StatID], [Stat_Value]) VALUES (21808, N'ilvlMagicDamage', 217)
INSERT INTO [dbo].[Equip_Stats] ([ItemID], [StatID], [Stat_Value]) VALUES (21808, N'ilvlParryingSkill', 269)
INSERT INTO [dbo].[Equip_Stats] ([ItemID], [StatID], [Stat_Value]) VALUES (21808, N'ilvlScytheSkill', 269)
INSERT INTO [dbo].[Equip_Stats] ([ItemID], [StatID], [Stat_Value]) VALUES (21808, N'JobLevelDRK', 99)
INSERT INTO [dbo].[Equip_Stats] ([ItemID], [StatID], [Stat_Value]) VALUES (21808, N'RaceAllRaces', 1)
INSERT INTO [dbo].[Equip_Stats] ([ItemID], [StatID], [Stat_Value]) VALUES (21808, N'Rare', 1)
INSERT INTO [dbo].[Equip_Stats] ([ItemID], [StatID], [Stat_Value]) VALUES (21808, N'RelicDoubleDamage', 20)
INSERT INTO [dbo].[Equip_Stats] ([ItemID], [StatID], [Stat_Value]) VALUES (21808, N'slotMain', 1)
INSERT INTO [dbo].[Equip_Stats] ([ItemID], [StatID], [Stat_Value]) VALUES (21808, N'WeaponDamage', 362)
INSERT INTO [dbo].[Equip_Stats] ([ItemID], [StatID], [Stat_Value]) VALUES (21808, N'WeaponDelay', 513)
INSERT INTO [dbo].[Equip_Stats] ([ItemID], [StatID], [Stat_Value]) VALUES (21808, N'WSCatastrophe', 1)
INSERT INTO [dbo].[Equip_Stats] ([ItemID], [StatID], [Stat_Value]) VALUES (21808, N'WSDCatastropheHidden', 40)
GO

-- This one I made with stats inserted on the fly, which would have to be added in the Stat insert. Not critical that this exists as an example.
/*
print '' print '*** Inserting Gastraphetes ***'

INSERT INTO [dbo].[Item] ([ItemID], [Item_Name]) VALUES (22139, N'Gastraphetes')

INSERT INTO [dbo].[Equip_Stats] ([ItemID], [StatID], [Stat_Value]) VALUES (22139, N'Exclusive', 1)
INSERT INTO [dbo].[Equip_Stats] ([ItemID], [StatID], [Stat_Value]) VALUES (22139, N'GearHaste', 25)
INSERT INTO [dbo].[Equip_Stats] ([ItemID], [StatID], [Stat_Value]) VALUES (22139, N'ilvlMagicDamage', 217)
INSERT INTO [dbo].[Equip_Stats] ([ItemID], [StatID], [Stat_Value]) VALUES (22139, N'ilvlMarksmanshipSkill', 269)
INSERT INTO [dbo].[Equip_Stats] ([ItemID], [StatID], [Stat_Value]) VALUES (22139, N'RAcc', 20)
INSERT INTO [dbo].[Equip_Stats] ([ItemID], [StatID], [Stat_Value]) VALUES (22139, N'RaceAllRaces', 1)
INSERT INTO [dbo].[Equip_Stats] ([ItemID], [StatID], [Stat_Value]) VALUES (22139, N'SlotRanged', 1)
INSERT INTO [dbo].[Equip_Stats] ([ItemID], [StatID], [Stat_Value]) VALUES (22139, N'Snapshot', 10)
INSERT INTO [dbo].[Equip_Stats] ([ItemID], [StatID], [Stat_Value]) VALUES (22139, N'WeaponDamage', 176)
INSERT INTO [dbo].[Equip_Stats] ([ItemID], [StatID], [Stat_Value]) VALUES (22139, N'WeaponDelay', 432)
*/

print '' print '*** Inserting Crepuscular Cloak ***'
GO
INSERT INTO [dbo].[Item] ([ItemID], [Item_Name]) VALUES (23799, N'Crepuscular Cloak')
GO
INSERT INTO [dbo].[Equip_Stats] ([ItemID], [StatID], [Stat_Value]) VALUES (23799, N'Accuracy', 85)
INSERT INTO [dbo].[Equip_Stats] ([ItemID], [StatID], [Stat_Value]) VALUES (23799, N'AGI', 25)
INSERT INTO [dbo].[Equip_Stats] ([ItemID], [StatID], [Stat_Value]) VALUES (23799, N'CannotEquipHead', 1)
INSERT INTO [dbo].[Equip_Stats] ([ItemID], [StatID], [Stat_Value]) VALUES (23799, N'CHR', 61)
INSERT INTO [dbo].[Equip_Stats] ([ItemID], [StatID], [Stat_Value]) VALUES (23799, N'Defense', 264)
INSERT INTO [dbo].[Equip_Stats] ([ItemID], [StatID], [Stat_Value]) VALUES (23799, N'DEX', 55)
INSERT INTO [dbo].[Equip_Stats] ([ItemID], [StatID], [Stat_Value]) VALUES (23799, N'Enmity', -15)
INSERT INTO [dbo].[Equip_Stats] ([ItemID], [StatID], [Stat_Value]) VALUES (23799, N'Evasion', 155)
INSERT INTO [dbo].[Equip_Stats] ([ItemID], [StatID], [Stat_Value]) VALUES (23799, N'Exclusive', 1)
INSERT INTO [dbo].[Equip_Stats] ([ItemID], [StatID], [Stat_Value]) VALUES (23799, N'GearHaste', 9)
INSERT INTO [dbo].[Equip_Stats] ([ItemID], [StatID], [Stat_Value]) VALUES (23799, N'HP', 97)
INSERT INTO [dbo].[Equip_Stats] ([ItemID], [StatID], [Stat_Value]) VALUES (23799, N'ilvl', 119)
INSERT INTO [dbo].[Equip_Stats] ([ItemID], [StatID], [Stat_Value]) VALUES (23799, N'INT', 80)
INSERT INTO [dbo].[Equip_Stats] ([ItemID], [StatID], [Stat_Value]) VALUES (23799, N'JobLevelDRK', 99)
INSERT INTO [dbo].[Equip_Stats] ([ItemID], [StatID], [Stat_Value]) VALUES (23799, N'MagicAccuracy', 85)
INSERT INTO [dbo].[Equip_Stats] ([ItemID], [StatID], [Stat_Value]) VALUES (23799, N'MagicAttackBonus', 85)
INSERT INTO [dbo].[Equip_Stats] ([ItemID], [StatID], [Stat_Value]) VALUES (23799, N'MagicDefenseBonus', 16)
INSERT INTO [dbo].[Equip_Stats] ([ItemID], [StatID], [Stat_Value]) VALUES (23799, N'MagicEvasion', 231)
INSERT INTO [dbo].[Equip_Stats] ([ItemID], [StatID], [Stat_Value]) VALUES (23799, N'MND', 64)
INSERT INTO [dbo].[Equip_Stats] ([ItemID], [StatID], [Stat_Value]) VALUES (23799, N'MP', 97)
INSERT INTO [dbo].[Equip_Stats] ([ItemID], [StatID], [Stat_Value]) VALUES (23799, N'RaceAllRaces', 1)
INSERT INTO [dbo].[Equip_Stats] ([ItemID], [StatID], [Stat_Value]) VALUES (23799, N'Rare', 1)
INSERT INTO [dbo].[Equip_Stats] ([ItemID], [StatID], [Stat_Value]) VALUES (23799, N'slotBody', 1)
INSERT INTO [dbo].[Equip_Stats] ([ItemID], [StatID], [Stat_Value]) VALUES (23799, N'SpellImpact', 1)
INSERT INTO [dbo].[Equip_Stats] ([ItemID], [StatID], [Stat_Value]) VALUES (23799, N'STR', 60)
INSERT INTO [dbo].[Equip_Stats] ([ItemID], [StatID], [Stat_Value]) VALUES (23799, N'VIT', 30)
GO