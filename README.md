# DirScan
Scan directories for data about file data, (counts, names, size) and summary information for all directories in the specified path


### Prerequisites
- Visual Studio 2019
- !["https://wixtoolset.org/releases/"](WiX Toolset 3.11)
- SQL Server database (if you choose to log into db rather than a file)
###### This table (DirScanLog) is required in the database of your choice should you care to log things to SQL Server. The below script will create the table and primary key for the Id field.
---
    create table dbo.DirScanLog(
        Id int identity(1,1) not null,
        [File] varchar(max) not null,
        [Size] bigint not null,
        [DateCreated] varchar(20) not null,
        [DateLastModified] varchar(20) not null,
        [Owner] varchar(255) not null,
        [FileAttributes] varchar(512) not null,

    constraint [PK__DirScanLog] primary key clustered 
    ( [Id] asc )with (pad_index = off, statistics_norecompute = off, ignore_dup_key = off, allow_row_locks = on, allow_page_locks = on, fillfactor = 80) on [primary]
    ) on [primary];
    GO
---
