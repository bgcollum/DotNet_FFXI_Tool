ECHO off

rem Build tables
sqlcmd -S localhost -E -i ffxi_equipment_db_tables.sql

sqlcmd -S localhost -E -i Sample_Data\sample_user_data.sql

rem Stats first
sqlcmd -S localhost -E -i Sample_Data\sample_stats.sql
rem populate items (Requires stats to populate)
sqlcmd -S localhost -E -i Sample_Data\sample_items.sql
rem populate inventories (Requires items to populate)
sqlcmd -S localhost -E -i Sample_Data\sample_inventories.sql

sqlcmd -S localhost -E -i Stored_Procedures\stored_procedures.sql

rem Populate the DB with some examples of real FFXI items
sqlcmd -S localhost -E -i Sample_Data\sample_FFXI_Items.sql

rem server is localhost

ECHO .
ECHO if no errors appear DB was created
PAUSE