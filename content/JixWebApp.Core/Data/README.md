
EF CORE CHEAT SHEET

Add Migration
- add-migration <name>

Update Database
- update-database
- drop-database

Roll Back
- update-database <lastGoodMigrationId>

Generate Migration SQL Script
- Script-Migration -i <startMigrationId>

Generate Roll Back Migration SQL Script
- Script-Migration -i <latestMigrationId> <lastGoodMigrationId>
