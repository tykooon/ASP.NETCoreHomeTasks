using Company.SqliteDb;

using var dbProvider = new DbProvider("CompanyDbSqlite.yaml");
dbProvider.PrepareDb();
