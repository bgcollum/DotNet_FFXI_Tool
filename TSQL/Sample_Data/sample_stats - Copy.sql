print '' print '*** Running sample_stats.sql ***'

USE [ffxi_equipment_db]
GO

print '' print '*** Inserting sample Master Stat List records and sample Stat Aliases'
GO

/* System */

INSERT INTO [dbo].[Stat]([StatID]) VALUES ('PlaceholderStat') GO
INSERT INTO [dbo].[StatAlias]([StatID], [StatAlias]) VALUES ('PlaceholderStat', 'PlaceholderStat') GO
INSERT INTO [dbo].[StatAlias]([StatID], [StatAlias]) VALUES ('PlaceholderStat', 'Placeholder for statless Item') GO

/* Meta */
INSERT INTO [dbo].[Stat]([StatID]) VALUES ('Augmented') GO
INSERT INTO [dbo].[StatAlias]([StatID], [StatAlias]) VALUES ('Augmented', 'Augmented') GO
INSERT INTO [dbo].[StatAlias]([StatID], [StatAlias]) VALUES ('Augmented', 'Aug') GO

INSERT INTO [dbo].[Stat]([StatID]) VALUES ('Exclusive') GO
INSERT INTO [dbo].[StatAlias]([StatID], [StatAlias]) VALUES ('Exclusive', 'Exclusive') GO
INSERT INTO [dbo].[StatAlias]([StatID], [StatAlias]) VALUES ('Exclusive', 'Ex') GO

INSERT INTO [dbo].[Stat]([StatID]) VALUES ('Rare') GO
INSERT INTO [dbo].[StatAlias]([StatID], [StatAlias]) VALUES ('Rare', 'Rare') GO

/* Race restrictions */
INSERT INTO [dbo].[Stat]([StatID]) VALUES ('RaceAllRaces') GO
INSERT INTO [dbo].[StatAlias]([StatID], [StatAlias]) VALUES ('RaceAllRaces', 'RaceAllRaces') GO
INSERT INTO [dbo].[StatAlias]([StatID], [StatAlias]) VALUES ('RaceAllRaces', 'All Races') GO

/* Slot Restrictions */
INSERT INTO [dbo].[Stat]([StatID]) VALUES ('slotMain') GO
INSERT INTO [dbo].[StatAlias]([StatID], [StatAlias]) VALUES ('slotMain', 'slotMain') GO
INSERT INTO [dbo].[StatAlias]([StatID], [StatAlias]) VALUES ('slotMain', 'Main') GO

INSERT INTO [dbo].[Stat]([StatID]) VALUES ('slotBody') GO
INSERT INTO [dbo].[StatAlias]([StatID], [StatAlias]) VALUES ('slotBody', 'slotBody') GO
INSERT INTO [dbo].[StatAlias]([StatID], [StatAlias]) VALUES ('slotBody', 'Body') GO

/* Base Stats */
INSERT INTO [dbo].[Stat]([StatID]) VALUES ('AGI') GO
INSERT INTO [dbo].[StatAlias]([StatID], [StatAlias]) VALUES ('AGI', 'AGI') GO
INSERT INTO [dbo].[StatAlias]([StatID], [StatAlias]) VALUES ('AGI', 'Agility') GO

INSERT INTO [dbo].[Stat]([StatID]) VALUES ('CHR') GO
INSERT INTO [dbo].[StatAlias]([StatID], [StatAlias]) VALUES ('CHR', 'CHR') GO
INSERT INTO [dbo].[StatAlias]([StatID], [StatAlias]) VALUES ('CHR', 'CHA') GO
INSERT INTO [dbo].[StatAlias]([StatID], [StatAlias]) VALUES ('CHR', 'Charisma') GO

INSERT INTO [dbo].[Stat]([StatID]) VALUES ('DEX') GO
INSERT INTO [dbo].[StatAlias]([StatID], [StatAlias]) VALUES ('DEX', 'DEX') GO
INSERT INTO [dbo].[StatAlias]([StatID], [StatAlias]) VALUES ('DEX', 'Dexterity') GO

INSERT INTO [dbo].[Stat]([StatID]) VALUES ('INT') GO
INSERT INTO [dbo].[StatAlias]([StatID], [StatAlias]) VALUES ('INT', 'INT') GO
INSERT INTO [dbo].[StatAlias]([StatID], [StatAlias]) VALUES ('INT', 'Intelligence') GO

INSERT INTO [dbo].[Stat]([StatID]) VALUES ('MND') GO
INSERT INTO [dbo].[StatAlias]([StatID], [StatAlias]) VALUES ('MND', 'Mind') GO
INSERT INTO [dbo].[StatAlias]([StatID], [StatAlias]) VALUES ('MND', 'MND') GO

INSERT INTO [dbo].[Stat]([StatID]) VALUES ('STR') GO
INSERT INTO [dbo].[StatAlias]([StatID], [StatAlias]) VALUES ('STR', 'STR') GO
INSERT INTO [dbo].[StatAlias]([StatID], [StatAlias]) VALUES ('STR', 'Strength') GO

INSERT INTO [dbo].[Stat]([StatID]) VALUES ('VIT') GO
INSERT INTO [dbo].[StatAlias]([StatID], [StatAlias]) VALUES ('VIT', 'VIT') GO
INSERT INTO [dbo].[StatAlias]([StatID], [StatAlias]) VALUES ('VIT', 'Vitality') GO

/* Character Stats */

INSERT INTO [dbo].[Stat]([StatID]) VALUES ('HP') GO
INSERT INTO [dbo].[StatAlias]([StatID], [StatAlias]) VALUES ('HP', 'HP') GO
INSERT INTO [dbo].[StatAlias]([StatID], [StatAlias]) VALUES ('HP', 'Hit Points') GO

INSERT INTO [dbo].[Stat]([StatID]) VALUES ('MP') GO
INSERT INTO [dbo].[StatAlias]([StatID], [StatAlias]) VALUES ('MP', 'MP') GO
INSERT INTO [dbo].[StatAlias]([StatID], [StatAlias]) VALUES ('MP', 'Magic Points') GO

/* General Stats */

INSERT INTO [dbo].[Stat]([StatID]) VALUES ('Enmity') GO
INSERT INTO [dbo].[StatAlias]([StatID], [StatAlias]) VALUES ('Enmity', 'Enmity') GO

INSERT INTO [dbo].[Stat]([StatID]) VALUES ('GearHaste') GO
INSERT INTO [dbo].[StatAlias]([StatID], [StatAlias]) VALUES ('GearHaste', 'GearHaste') GO
INSERT INTO [dbo].[StatAlias]([StatID], [StatAlias]) VALUES ('GearHaste', 'Haste (Equipment)') GO
INSERT INTO [dbo].[StatAlias]([StatID], [StatAlias]) VALUES ('GearHaste', 'Haste (Gear)') GO

/* Defensive stats */
INSERT INTO [dbo].[Stat]([StatID]) VALUES ('Defense') GO
INSERT INTO [dbo].[StatAlias]([StatID], [StatAlias]) VALUES ('Defense', 'Defense') GO
INSERT INTO [dbo].[StatAlias]([StatID], [StatAlias]) VALUES ('Defense', 'DEF') GO

INSERT INTO [dbo].[Stat]([StatID]) VALUES ('Evasion') GO
INSERT INTO [dbo].[StatAlias]([StatID], [StatAlias]) VALUES ('Evasion', 'Evasion') GO
INSERT INTO [dbo].[StatAlias]([StatID], [StatAlias]) VALUES ('Evasion', 'EVA') GO

INSERT INTO [dbo].[Stat]([StatID]) VALUES ('MagicDefenseBonus') GO
INSERT INTO [dbo].[StatAlias]([StatID], [StatAlias]) VALUES ('MagicDefenseBonus', 'MagicDefenseBonus') GO
INSERT INTO [dbo].[StatAlias]([StatID], [StatAlias]) VALUES ('MagicDefenseBonus', 'Magic Defense Bonus') GO
INSERT INTO [dbo].[StatAlias]([StatID], [StatAlias]) VALUES ('MagicDefenseBonus', 'MDB') GO

INSERT INTO [dbo].[Stat]([StatID]) VALUES ('MagicEvasion') GO
INSERT INTO [dbo].[StatAlias]([StatID], [StatAlias]) VALUES ('MagicEvasion', 'MagicEvasion') GO
INSERT INTO [dbo].[StatAlias]([StatID], [StatAlias]) VALUES ('MagicEvasion', 'Magic Evasion') GO
INSERT INTO [dbo].[StatAlias]([StatID], [StatAlias]) VALUES ('MagicEvasion', 'MEva') GO

/* Job Level */
INSERT INTO [dbo].[Stat]([StatID]) VALUES ('JobLevelBLM') GO
INSERT INTO [dbo].[StatAlias]([StatID], [StatAlias]) VALUES ('JobLevelBLM', 'JobLevelBLM') GO
INSERT INTO [dbo].[StatAlias]([StatID], [StatAlias]) VALUES ('JobLevelBLM', 'BLM') GO
INSERT INTO [dbo].[StatAlias]([StatID], [StatAlias]) VALUES ('JobLevelBLM', 'Black Mage') GO

INSERT INTO [dbo].[Stat]([StatID]) VALUES ('JobLevelDRK') GO
INSERT INTO [dbo].[StatAlias]([StatID], [StatAlias]) VALUES ('JobLevelDRK', 'JobLevelDRK') GO
INSERT INTO [dbo].[StatAlias]([StatID], [StatAlias]) VALUES ('JobLevelDRK', 'DRK') GO
INSERT INTO [dbo].[StatAlias]([StatID], [StatAlias]) VALUES ('JobLevelDRK', 'Dark Knight') GO

INSERT INTO [dbo].[Stat]([StatID]) VALUES ('JobLevelGEO') GO
INSERT INTO [dbo].[StatAlias]([StatID], [StatAlias]) VALUES ('JobLevelGEO', 'JobLevelGEO') GO
INSERT INTO [dbo].[StatAlias]([StatID], [StatAlias]) VALUES ('JobLevelGEO', 'GEO') GO
INSERT INTO [dbo].[StatAlias]([StatID], [StatAlias]) VALUES ('JobLevelGEO', 'Geomancer') GO

INSERT INTO [dbo].[Stat]([StatID]) VALUES ('JobLevelRDM') GO
INSERT INTO [dbo].[StatAlias]([StatID], [StatAlias]) VALUES ('JobLevelRDM', 'JobLevelRDM') GO
INSERT INTO [dbo].[StatAlias]([StatID], [StatAlias]) VALUES ('JobLevelRDM', 'RDM') GO
INSERT INTO [dbo].[StatAlias]([StatID], [StatAlias]) VALUES ('JobLevelRDM', 'Red Mage') GO

INSERT INTO [dbo].[Stat]([StatID]) VALUES ('JobLevelSCH') GO
INSERT INTO [dbo].[StatAlias]([StatID], [StatAlias]) VALUES ('JobLevelSCH', 'JobLevelSCH') GO
INSERT INTO [dbo].[StatAlias]([StatID], [StatAlias]) VALUES ('JobLevelSCH', 'SCH') GO
INSERT INTO [dbo].[StatAlias]([StatID], [StatAlias]) VALUES ('JobLevelSCH', 'Scholar') GO

INSERT INTO [dbo].[Stat]([StatID]) VALUES ('JobLevelSMN') GO
INSERT INTO [dbo].[StatAlias]([StatID], [StatAlias]) VALUES ('JobLevelSMN', 'JobLevelSMN') GO
INSERT INTO [dbo].[StatAlias]([StatID], [StatAlias]) VALUES ('JobLevelSMN', 'SMN') GO
INSERT INTO [dbo].[StatAlias]([StatID], [StatAlias]) VALUES ('JobLevelSMN', 'Summoner') GO

INSERT INTO [dbo].[Stat]([StatID]) VALUES ('JobLevelWHM') GO
INSERT INTO [dbo].[StatAlias]([StatID], [StatAlias]) VALUES ('JobLevelWHM', 'JobLevelWHM') GO
INSERT INTO [dbo].[StatAlias]([StatID], [StatAlias]) VALUES ('JobLevelWHM', 'WHM') GO
INSERT INTO [dbo].[StatAlias]([StatID], [StatAlias]) VALUES ('JobLevelWHM', 'White Mage') GO

/* Item Level */
INSERT INTO [dbo].[Stat]([StatID]) VALUES ('ilvl') GO
INSERT INTO [dbo].[StatAlias]([StatID], [StatAlias]) VALUES ('ilvl', 'ilvl') GO
INSERT INTO [dbo].[StatAlias]([StatID], [StatAlias]) VALUES ('ilvl', 'Item Level') GO

INSERT INTO [dbo].[Stat]([StatID]) VALUES ('ilvlMagicAccuracySkill') GO
INSERT INTO [dbo].[StatAlias]([StatID], [StatAlias]) VALUES ('ilvlMagicAccuracySkill', 'ilvlMagicAccuracySkill') GO
INSERT INTO [dbo].[StatAlias]([StatID], [StatAlias]) VALUES ('ilvlMagicAccuracySkill', 'Magic Accuracy Skill (Item Level)') GO

INSERT INTO [dbo].[Stat]([StatID]) VALUES ('ilvlMagicDamage') GO
INSERT INTO [dbo].[StatAlias]([StatID], [StatAlias]) VALUES ('ilvlMagicDamage', 'ilvlMagicDamage') GO
INSERT INTO [dbo].[StatAlias]([StatID], [StatAlias]) VALUES ('ilvlMagicDamage', 'Magic Damage (Item Level)') GO

INSERT INTO [dbo].[Stat]([StatID]) VALUES ('ilvlParryingSkill') GO
INSERT INTO [dbo].[StatAlias]([StatID], [StatAlias]) VALUES ('ilvlParryingSkill', 'ilvlParryingSkill') GO
INSERT INTO [dbo].[StatAlias]([StatID], [StatAlias]) VALUES ('ilvlParryingSkill', 'Parrying Skill (Item Level)') GO

INSERT INTO [dbo].[Stat]([StatID]) VALUES ('ilvlScytheSkill') GO
INSERT INTO [dbo].[StatAlias]([StatID], [StatAlias]) VALUES ('ilvlScytheSkill', 'ilvlScytheSkill') GO
INSERT INTO [dbo].[StatAlias]([StatID], [StatAlias]) VALUES ('ilvlScytheSkill', 'Scythe Skill (Item Level)') GO

/* Generic Weapon */
INSERT INTO [dbo].[Stat]([StatID]) VALUES ('WeaponDamage') GO
INSERT INTO [dbo].[StatAlias]([StatID], [StatAlias]) VALUES ('WeaponDamage', 'WeaponDamage') GO
INSERT INTO [dbo].[StatAlias]([StatID], [StatAlias]) VALUES ('WeaponDamage', 'Damage') GO

INSERT INTO [dbo].[Stat]([StatID]) VALUES ('WeaponDelay') GO
INSERT INTO [dbo].[StatAlias]([StatID], [StatAlias]) VALUES ('WeaponDelay', 'WeaponDelay') GO
INSERT INTO [dbo].[StatAlias]([StatID], [StatAlias]) VALUES ('WeaponDelay', 'Delay') GO

/* Combat Stats */
INSERT INTO [dbo].[Stat]([StatID]) VALUES ('Accuracy') GO
INSERT INTO [dbo].[StatAlias]([StatID], [StatAlias]) VALUES ('Accuracy', 'Accuracy') GO
INSERT INTO [dbo].[StatAlias]([StatID], [StatAlias]) VALUES ('Accuracy', 'Acc') GO

INSERT INTO [dbo].[Stat]([StatID]) VALUES ('Attack') GO
INSERT INTO [dbo].[StatAlias]([StatID], [StatAlias]) VALUES ('Attack', 'Attack') GO
INSERT INTO [dbo].[StatAlias]([StatID], [StatAlias]) VALUES ('Attack', 'Att') GO

/* Magic Stats */
INSERT INTO [dbo].[Stat]([StatID]) VALUES ('DrainPotency') GO
INSERT INTO [dbo].[StatAlias]([StatID], [StatAlias]) VALUES ('DrainPotency', 'DrainPotency') GO
INSERT INTO [dbo].[StatAlias]([StatID], [StatAlias]) VALUES ('DrainPotency', 'Drain Potency') GO

INSERT INTO [dbo].[Stat]([StatID]) VALUES ('MagicAccuracy') GO
INSERT INTO [dbo].[StatAlias]([StatID], [StatAlias]) VALUES ('MagicAccuracy', 'MagicAccuracy') GO
INSERT INTO [dbo].[StatAlias]([StatID], [StatAlias]) VALUES ('MagicAccuracy', 'Magic Accuracy') GO
INSERT INTO [dbo].[StatAlias]([StatID], [StatAlias]) VALUES ('MagicAccuracy', 'MAcc') GO

INSERT INTO [dbo].[Stat]([StatID]) VALUES ('MagicAttackBonus') GO
INSERT INTO [dbo].[StatAlias]([StatID], [StatAlias]) VALUES ('MagicAttackBonus', 'MagicAttackBonus') GO
INSERT INTO [dbo].[StatAlias]([StatID], [StatAlias]) VALUES ('MagicAttackBonus', 'Magic Attack Bonus') GO
INSERT INTO [dbo].[StatAlias]([StatID], [StatAlias]) VALUES ('MagicAttackBonus', 'MAB') GO

/* Additional Effects*/

INSERT INTO [dbo].[Stat]([StatID]) VALUES ('AddEffectBlindness') GO
INSERT INTO [dbo].[StatAlias]([StatID], [StatAlias]) VALUES ('AddEffectBlindness', 'AddEffectBlindness') GO
INSERT INTO [dbo].[StatAlias]([StatID], [StatAlias]) VALUES ('AddEffectBlindness', 'Additional Effect: Blindness') GO

/* Grants weapon skill */
INSERT INTO [dbo].[Stat]([StatID]) VALUES ('WSCatastrophe') GO
INSERT INTO [dbo].[StatAlias]([StatID], [StatAlias]) VALUES ('WSCatastrophe', 'WSCatastrophe') GO
INSERT INTO [dbo].[StatAlias]([StatID], [StatAlias]) VALUES ('WSCatastrophe', 'Catastrophe') GO

/* Grants Spell */
INSERT INTO [dbo].[Stat]([StatID]) VALUES ('SpellImpact') GO
INSERT INTO [dbo].[StatAlias]([StatID], [StatAlias]) VALUES ('SpellImpact', 'SpellImpact') GO
INSERT INTO [dbo].[StatAlias]([StatID], [StatAlias]) VALUES ('SpellImpact', 'Impact') GO

/* Specific Weapon Skill Damage */
INSERT INTO [dbo].[Stat]([StatID]) VALUES ('WSDCatastrophe') GO
INSERT INTO [dbo].[StatAlias]([StatID], [StatAlias]) VALUES ('WSDCatastrophe', 'WSDCatastrophe') GO
INSERT INTO [dbo].[StatAlias]([StatID], [StatAlias]) VALUES ('WSDCatastrophe', 'Catastrophe Damage') GO

INSERT INTO [dbo].[Stat]([StatID]) VALUES ('WSDCatastropheHidden') GO
INSERT INTO [dbo].[StatAlias]([StatID], [StatAlias]) VALUES ('WSDCatastropheHidden', 'WSDCatastropheHidden') GO
INSERT INTO [dbo].[StatAlias]([StatID], [StatAlias]) VALUES ('WSDCatastropheHidden', 'Catastrophe Damage (Hidden)') GO

/* REMA */

INSERT INTO [dbo].[Stat]([StatID]) VALUES ('Afterglow') GO
INSERT INTO [dbo].[StatAlias]([StatID], [StatAlias]) VALUES ('Afterglow', 'Afterglow') GO

INSERT INTO [dbo].[Stat]([StatID]) VALUES ('AftermathJAHaste') GO
INSERT INTO [dbo].[StatAlias]([StatID], [StatAlias]) VALUES ('AftermathJAHaste', 'AftermathJAHaste') GO
INSERT INTO [dbo].[StatAlias]([StatID], [StatAlias]) VALUES ('AftermathJAHaste', 'Aftermath: Haste (Job Ability)') GO

INSERT INTO [dbo].[Stat]([StatID]) VALUES ('RelicDoubleDamage') GO
INSERT INTO [dbo].[StatAlias]([StatID], [StatAlias]) VALUES ('RelicDoubleDamage', 'RelicDoubleDamage') GO
INSERT INTO [dbo].[StatAlias]([StatID], [StatAlias]) VALUES ('RelicDoubleDamage', 'Double Damage (Relic)') GO

/* Restrictions */
INSERT INTO [dbo].[Stat]([StatID]) VALUES ('CannotEquipHead') GO
INSERT INTO [dbo].[StatAlias]([StatID], [StatAlias]) VALUES ('CannotEquipHead', 'CannotEquipHead') GO
INSERT INTO [dbo].[StatAlias]([StatID], [StatAlias]) VALUES ('CannotEquipHead', 'Cannot Equip Headgear') GO
