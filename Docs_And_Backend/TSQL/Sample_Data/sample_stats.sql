print '' print '*** Running sample_stats.sql ***'

USE [ffxi_equipment_db]
GO

print '' print '*** Inserting sample Master Stat List records and sample Stat Aliases'
GO

/* System */

INSERT INTO [dbo].[Stat]([StatID]) VALUES ('PlaceholderStat')
INSERT INTO [dbo].[StatAlias]([StatID], [StatAlias]) VALUES ('PlaceholderStat', 'PlaceholderStat')
INSERT INTO [dbo].[StatAlias]([StatID], [StatAlias]) VALUES ('PlaceholderStat', 'Placeholder for statless Item')

/* Meta */
INSERT INTO [dbo].[Stat]([StatID]) VALUES ('Augmented')
INSERT INTO [dbo].[StatAlias]([StatID], [StatAlias]) VALUES ('Augmented', 'Augmented')
INSERT INTO [dbo].[StatAlias]([StatID], [StatAlias]) VALUES ('Augmented', 'Aug')

INSERT INTO [dbo].[Stat]([StatID]) VALUES ('Exclusive')
INSERT INTO [dbo].[StatAlias]([StatID], [StatAlias]) VALUES ('Exclusive', 'Exclusive')
INSERT INTO [dbo].[StatAlias]([StatID], [StatAlias]) VALUES ('Exclusive', 'Ex')

INSERT INTO [dbo].[Stat]([StatID]) VALUES ('Rare')
INSERT INTO [dbo].[StatAlias]([StatID], [StatAlias]) VALUES ('Rare', 'Rare')

/* Race restrictions */
INSERT INTO [dbo].[Stat]([StatID]) VALUES ('RaceAllRaces')
INSERT INTO [dbo].[StatAlias]([StatID], [StatAlias]) VALUES ('RaceAllRaces', 'RaceAllRaces')
INSERT INTO [dbo].[StatAlias]([StatID], [StatAlias]) VALUES ('RaceAllRaces', 'All Races')

/* Slot Restrictions */
INSERT INTO [dbo].[Stat]([StatID]) VALUES ('slotMain')
INSERT INTO [dbo].[StatAlias]([StatID], [StatAlias]) VALUES ('slotMain', 'slotMain')
INSERT INTO [dbo].[StatAlias]([StatID], [StatAlias]) VALUES ('slotMain', 'Main')

INSERT INTO [dbo].[Stat]([StatID]) VALUES ('slotBody')
INSERT INTO [dbo].[StatAlias]([StatID], [StatAlias]) VALUES ('slotBody', 'slotBody')
INSERT INTO [dbo].[StatAlias]([StatID], [StatAlias]) VALUES ('slotBody', 'Body')

/* Base Stats */
INSERT INTO [dbo].[Stat]([StatID]) VALUES ('AGI')
INSERT INTO [dbo].[StatAlias]([StatID], [StatAlias]) VALUES ('AGI', 'AGI')
INSERT INTO [dbo].[StatAlias]([StatID], [StatAlias]) VALUES ('AGI', 'Agility')

INSERT INTO [dbo].[Stat]([StatID]) VALUES ('CHR')
INSERT INTO [dbo].[StatAlias]([StatID], [StatAlias]) VALUES ('CHR', 'CHR')
INSERT INTO [dbo].[StatAlias]([StatID], [StatAlias]) VALUES ('CHR', 'CHA')
INSERT INTO [dbo].[StatAlias]([StatID], [StatAlias]) VALUES ('CHR', 'Charisma')

INSERT INTO [dbo].[Stat]([StatID]) VALUES ('DEX')
INSERT INTO [dbo].[StatAlias]([StatID], [StatAlias]) VALUES ('DEX', 'DEX')
INSERT INTO [dbo].[StatAlias]([StatID], [StatAlias]) VALUES ('DEX', 'Dexterity')

INSERT INTO [dbo].[Stat]([StatID]) VALUES ('INT')
INSERT INTO [dbo].[StatAlias]([StatID], [StatAlias]) VALUES ('INT', 'INT')
INSERT INTO [dbo].[StatAlias]([StatID], [StatAlias]) VALUES ('INT', 'Intelligence')

INSERT INTO [dbo].[Stat]([StatID]) VALUES ('MND')
INSERT INTO [dbo].[StatAlias]([StatID], [StatAlias]) VALUES ('MND', 'Mind')
INSERT INTO [dbo].[StatAlias]([StatID], [StatAlias]) VALUES ('MND', 'MND')

INSERT INTO [dbo].[Stat]([StatID]) VALUES ('STR')
INSERT INTO [dbo].[StatAlias]([StatID], [StatAlias]) VALUES ('STR', 'STR')
INSERT INTO [dbo].[StatAlias]([StatID], [StatAlias]) VALUES ('STR', 'Strength')

INSERT INTO [dbo].[Stat]([StatID]) VALUES ('VIT')
INSERT INTO [dbo].[StatAlias]([StatID], [StatAlias]) VALUES ('VIT', 'VIT')
INSERT INTO [dbo].[StatAlias]([StatID], [StatAlias]) VALUES ('VIT', 'Vitality')

/* Character Stats */

INSERT INTO [dbo].[Stat]([StatID]) VALUES ('HP')
INSERT INTO [dbo].[StatAlias]([StatID], [StatAlias]) VALUES ('HP', 'HP')
INSERT INTO [dbo].[StatAlias]([StatID], [StatAlias]) VALUES ('HP', 'Hit Points')

INSERT INTO [dbo].[Stat]([StatID]) VALUES ('MP')
INSERT INTO [dbo].[StatAlias]([StatID], [StatAlias]) VALUES ('MP', 'MP')
INSERT INTO [dbo].[StatAlias]([StatID], [StatAlias]) VALUES ('MP', 'Magic Points')

/* General Stats */

INSERT INTO [dbo].[Stat]([StatID]) VALUES ('Enmity')
INSERT INTO [dbo].[StatAlias]([StatID], [StatAlias]) VALUES ('Enmity', 'Enmity')

INSERT INTO [dbo].[Stat]([StatID]) VALUES ('GearHaste')
INSERT INTO [dbo].[StatAlias]([StatID], [StatAlias]) VALUES ('GearHaste', 'GearHaste')
INSERT INTO [dbo].[StatAlias]([StatID], [StatAlias]) VALUES ('GearHaste', 'Haste (Equipment)')
INSERT INTO [dbo].[StatAlias]([StatID], [StatAlias]) VALUES ('GearHaste', 'Haste (Gear)')

/* Defensive stats */
INSERT INTO [dbo].[Stat]([StatID]) VALUES ('Defense')
INSERT INTO [dbo].[StatAlias]([StatID], [StatAlias]) VALUES ('Defense', 'Defense')
INSERT INTO [dbo].[StatAlias]([StatID], [StatAlias]) VALUES ('Defense', 'DEF')

INSERT INTO [dbo].[Stat]([StatID]) VALUES ('Evasion')
INSERT INTO [dbo].[StatAlias]([StatID], [StatAlias]) VALUES ('Evasion', 'Evasion')
INSERT INTO [dbo].[StatAlias]([StatID], [StatAlias]) VALUES ('Evasion', 'EVA')

INSERT INTO [dbo].[Stat]([StatID]) VALUES ('MagicDefenseBonus')
INSERT INTO [dbo].[StatAlias]([StatID], [StatAlias]) VALUES ('MagicDefenseBonus', 'MagicDefenseBonus')
INSERT INTO [dbo].[StatAlias]([StatID], [StatAlias]) VALUES ('MagicDefenseBonus', 'Magic Defense Bonus')
INSERT INTO [dbo].[StatAlias]([StatID], [StatAlias]) VALUES ('MagicDefenseBonus', 'MDB')

INSERT INTO [dbo].[Stat]([StatID]) VALUES ('MagicEvasion')
INSERT INTO [dbo].[StatAlias]([StatID], [StatAlias]) VALUES ('MagicEvasion', 'MagicEvasion')
INSERT INTO [dbo].[StatAlias]([StatID], [StatAlias]) VALUES ('MagicEvasion', 'Magic Evasion')
INSERT INTO [dbo].[StatAlias]([StatID], [StatAlias]) VALUES ('MagicEvasion', 'MEva')

/* Job Level */
INSERT INTO [dbo].[Stat]([StatID]) VALUES ('JobLevelBLM')
INSERT INTO [dbo].[StatAlias]([StatID], [StatAlias]) VALUES ('JobLevelBLM', 'JobLevelBLM')
INSERT INTO [dbo].[StatAlias]([StatID], [StatAlias]) VALUES ('JobLevelBLM', 'BLM')
INSERT INTO [dbo].[StatAlias]([StatID], [StatAlias]) VALUES ('JobLevelBLM', 'Black Mage')

INSERT INTO [dbo].[Stat]([StatID]) VALUES ('JobLevelDRK')
INSERT INTO [dbo].[StatAlias]([StatID], [StatAlias]) VALUES ('JobLevelDRK', 'JobLevelDRK')
INSERT INTO [dbo].[StatAlias]([StatID], [StatAlias]) VALUES ('JobLevelDRK', 'DRK')
INSERT INTO [dbo].[StatAlias]([StatID], [StatAlias]) VALUES ('JobLevelDRK', 'Dark Knight')

INSERT INTO [dbo].[Stat]([StatID]) VALUES ('JobLevelGEO')
INSERT INTO [dbo].[StatAlias]([StatID], [StatAlias]) VALUES ('JobLevelGEO', 'JobLevelGEO')
INSERT INTO [dbo].[StatAlias]([StatID], [StatAlias]) VALUES ('JobLevelGEO', 'GEO')
INSERT INTO [dbo].[StatAlias]([StatID], [StatAlias]) VALUES ('JobLevelGEO', 'Geomancer')

INSERT INTO [dbo].[Stat]([StatID]) VALUES ('JobLevelRDM')
INSERT INTO [dbo].[StatAlias]([StatID], [StatAlias]) VALUES ('JobLevelRDM', 'JobLevelRDM')
INSERT INTO [dbo].[StatAlias]([StatID], [StatAlias]) VALUES ('JobLevelRDM', 'RDM')
INSERT INTO [dbo].[StatAlias]([StatID], [StatAlias]) VALUES ('JobLevelRDM', 'Red Mage')

INSERT INTO [dbo].[Stat]([StatID]) VALUES ('JobLevelSCH')
INSERT INTO [dbo].[StatAlias]([StatID], [StatAlias]) VALUES ('JobLevelSCH', 'JobLevelSCH')
INSERT INTO [dbo].[StatAlias]([StatID], [StatAlias]) VALUES ('JobLevelSCH', 'SCH')
INSERT INTO [dbo].[StatAlias]([StatID], [StatAlias]) VALUES ('JobLevelSCH', 'Scholar')

INSERT INTO [dbo].[Stat]([StatID]) VALUES ('JobLevelSMN')
INSERT INTO [dbo].[StatAlias]([StatID], [StatAlias]) VALUES ('JobLevelSMN', 'JobLevelSMN')
INSERT INTO [dbo].[StatAlias]([StatID], [StatAlias]) VALUES ('JobLevelSMN', 'SMN')
INSERT INTO [dbo].[StatAlias]([StatID], [StatAlias]) VALUES ('JobLevelSMN', 'Summoner')

INSERT INTO [dbo].[Stat]([StatID]) VALUES ('JobLevelWHM')
INSERT INTO [dbo].[StatAlias]([StatID], [StatAlias]) VALUES ('JobLevelWHM', 'JobLevelWHM')
INSERT INTO [dbo].[StatAlias]([StatID], [StatAlias]) VALUES ('JobLevelWHM', 'WHM')
INSERT INTO [dbo].[StatAlias]([StatID], [StatAlias]) VALUES ('JobLevelWHM', 'White Mage')

/* Item Level */
INSERT INTO [dbo].[Stat]([StatID]) VALUES ('ilvl')
INSERT INTO [dbo].[StatAlias]([StatID], [StatAlias]) VALUES ('ilvl', 'ilvl')
INSERT INTO [dbo].[StatAlias]([StatID], [StatAlias]) VALUES ('ilvl', 'Item Level')

INSERT INTO [dbo].[Stat]([StatID]) VALUES ('ilvlMagicAccuracySkill')
INSERT INTO [dbo].[StatAlias]([StatID], [StatAlias]) VALUES ('ilvlMagicAccuracySkill', 'ilvlMagicAccuracySkill')
INSERT INTO [dbo].[StatAlias]([StatID], [StatAlias]) VALUES ('ilvlMagicAccuracySkill', 'Magic Accuracy Skill (Item Level)')

INSERT INTO [dbo].[Stat]([StatID]) VALUES ('ilvlMagicDamage')
INSERT INTO [dbo].[StatAlias]([StatID], [StatAlias]) VALUES ('ilvlMagicDamage', 'ilvlMagicDamage')
INSERT INTO [dbo].[StatAlias]([StatID], [StatAlias]) VALUES ('ilvlMagicDamage', 'Magic Damage (Item Level)')

INSERT INTO [dbo].[Stat]([StatID]) VALUES ('ilvlParryingSkill')
INSERT INTO [dbo].[StatAlias]([StatID], [StatAlias]) VALUES ('ilvlParryingSkill', 'ilvlParryingSkill')
INSERT INTO [dbo].[StatAlias]([StatID], [StatAlias]) VALUES ('ilvlParryingSkill', 'Parrying Skill (Item Level)')

INSERT INTO [dbo].[Stat]([StatID]) VALUES ('ilvlScytheSkill')
INSERT INTO [dbo].[StatAlias]([StatID], [StatAlias]) VALUES ('ilvlScytheSkill', 'ilvlScytheSkill')
INSERT INTO [dbo].[StatAlias]([StatID], [StatAlias]) VALUES ('ilvlScytheSkill', 'Scythe Skill (Item Level)')

/* Generic Weapon */
INSERT INTO [dbo].[Stat]([StatID]) VALUES ('WeaponDamage')
INSERT INTO [dbo].[StatAlias]([StatID], [StatAlias]) VALUES ('WeaponDamage', 'WeaponDamage')
INSERT INTO [dbo].[StatAlias]([StatID], [StatAlias]) VALUES ('WeaponDamage', 'Damage')

INSERT INTO [dbo].[Stat]([StatID]) VALUES ('WeaponDelay')
INSERT INTO [dbo].[StatAlias]([StatID], [StatAlias]) VALUES ('WeaponDelay', 'WeaponDelay')
INSERT INTO [dbo].[StatAlias]([StatID], [StatAlias]) VALUES ('WeaponDelay', 'Delay')

/* Combat Stats */
INSERT INTO [dbo].[Stat]([StatID]) VALUES ('Accuracy')
INSERT INTO [dbo].[StatAlias]([StatID], [StatAlias]) VALUES ('Accuracy', 'Accuracy')
INSERT INTO [dbo].[StatAlias]([StatID], [StatAlias]) VALUES ('Accuracy', 'Acc')

INSERT INTO [dbo].[Stat]([StatID]) VALUES ('Attack')
INSERT INTO [dbo].[StatAlias]([StatID], [StatAlias]) VALUES ('Attack', 'Attack')
INSERT INTO [dbo].[StatAlias]([StatID], [StatAlias]) VALUES ('Attack', 'Att')

/* Magic Stats */
INSERT INTO [dbo].[Stat]([StatID]) VALUES ('DrainPotency')
INSERT INTO [dbo].[StatAlias]([StatID], [StatAlias]) VALUES ('DrainPotency', 'DrainPotency')
INSERT INTO [dbo].[StatAlias]([StatID], [StatAlias]) VALUES ('DrainPotency', 'Drain Potency')

INSERT INTO [dbo].[Stat]([StatID]) VALUES ('MagicAccuracy')
INSERT INTO [dbo].[StatAlias]([StatID], [StatAlias]) VALUES ('MagicAccuracy', 'MagicAccuracy')
INSERT INTO [dbo].[StatAlias]([StatID], [StatAlias]) VALUES ('MagicAccuracy', 'Magic Accuracy')
INSERT INTO [dbo].[StatAlias]([StatID], [StatAlias]) VALUES ('MagicAccuracy', 'MAcc')

INSERT INTO [dbo].[Stat]([StatID]) VALUES ('MagicAttackBonus')
INSERT INTO [dbo].[StatAlias]([StatID], [StatAlias]) VALUES ('MagicAttackBonus', 'MagicAttackBonus')
INSERT INTO [dbo].[StatAlias]([StatID], [StatAlias]) VALUES ('MagicAttackBonus', 'Magic Attack Bonus')
INSERT INTO [dbo].[StatAlias]([StatID], [StatAlias]) VALUES ('MagicAttackBonus', 'MAB')

/* Additional Effects*/

INSERT INTO [dbo].[Stat]([StatID]) VALUES ('AddEffectBlindness')
INSERT INTO [dbo].[StatAlias]([StatID], [StatAlias]) VALUES ('AddEffectBlindness', 'AddEffectBlindness')
INSERT INTO [dbo].[StatAlias]([StatID], [StatAlias]) VALUES ('AddEffectBlindness', 'Additional Effect: Blindness')

/* Grants weapon skill */
INSERT INTO [dbo].[Stat]([StatID]) VALUES ('WSCatastrophe')
INSERT INTO [dbo].[StatAlias]([StatID], [StatAlias]) VALUES ('WSCatastrophe', 'WSCatastrophe')
INSERT INTO [dbo].[StatAlias]([StatID], [StatAlias]) VALUES ('WSCatastrophe', 'Catastrophe')

/* Grants Spell */
INSERT INTO [dbo].[Stat]([StatID]) VALUES ('SpellImpact')
INSERT INTO [dbo].[StatAlias]([StatID], [StatAlias]) VALUES ('SpellImpact', 'SpellImpact')
INSERT INTO [dbo].[StatAlias]([StatID], [StatAlias]) VALUES ('SpellImpact', 'Impact')

/* Specific Weapon Skill Damage */
INSERT INTO [dbo].[Stat]([StatID]) VALUES ('WSDCatastrophe')
INSERT INTO [dbo].[StatAlias]([StatID], [StatAlias]) VALUES ('WSDCatastrophe', 'WSDCatastrophe')
INSERT INTO [dbo].[StatAlias]([StatID], [StatAlias]) VALUES ('WSDCatastrophe', 'Catastrophe Damage')

INSERT INTO [dbo].[Stat]([StatID]) VALUES ('WSDCatastropheHidden')
INSERT INTO [dbo].[StatAlias]([StatID], [StatAlias]) VALUES ('WSDCatastropheHidden', 'WSDCatastropheHidden')
INSERT INTO [dbo].[StatAlias]([StatID], [StatAlias]) VALUES ('WSDCatastropheHidden', 'Catastrophe Damage (Hidden)')

/* REMA */

INSERT INTO [dbo].[Stat]([StatID]) VALUES ('Afterglow')
INSERT INTO [dbo].[StatAlias]([StatID], [StatAlias]) VALUES ('Afterglow', 'Afterglow')

INSERT INTO [dbo].[Stat]([StatID]) VALUES ('AftermathJAHaste')
INSERT INTO [dbo].[StatAlias]([StatID], [StatAlias]) VALUES ('AftermathJAHaste', 'AftermathJAHaste')
INSERT INTO [dbo].[StatAlias]([StatID], [StatAlias]) VALUES ('AftermathJAHaste', 'Aftermath: Haste (Job Ability)')

INSERT INTO [dbo].[Stat]([StatID]) VALUES ('RelicDoubleDamage')
INSERT INTO [dbo].[StatAlias]([StatID], [StatAlias]) VALUES ('RelicDoubleDamage', 'RelicDoubleDamage')
INSERT INTO [dbo].[StatAlias]([StatID], [StatAlias]) VALUES ('RelicDoubleDamage', 'Double Damage (Relic)')

/* Restrictions */
INSERT INTO [dbo].[Stat]([StatID]) VALUES ('CannotEquipHead')
INSERT INTO [dbo].[StatAlias]([StatID], [StatAlias]) VALUES ('CannotEquipHead', 'CannotEquipHead')
INSERT INTO [dbo].[StatAlias]([StatID], [StatAlias]) VALUES ('CannotEquipHead', 'Cannot Equip Headgear')


/* End stat insertion */
GO

print '' print '*** Sample stats and aliases complete'
GO