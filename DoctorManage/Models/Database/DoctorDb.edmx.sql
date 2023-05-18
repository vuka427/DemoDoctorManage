
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 05/18/2023 16:05:46
-- Generated from EDMX file: D:\WorkspaceTTTT\Demo\ProjectDemo\DoctorManage\DoctorManage\Models\Database\DoctorDb.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [DoctorDb];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_DEPARTMENTDOCTORMODEL]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[DOCTOR] DROP CONSTRAINT [FK_DEPARTMENTDOCTORMODEL];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[DOCTOR]', 'U') IS NOT NULL
    DROP TABLE [dbo].[DOCTOR];
GO
IF OBJECT_ID(N'[dbo].[DEPARTMENT]', 'U') IS NOT NULL
    DROP TABLE [dbo].[DEPARTMENT];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'DOCTOR'
CREATE TABLE [dbo].[DOCTOR] (
    [DOCTORID] int IDENTITY(1,1) NOT NULL,
    [DEPARTMENTID] int  NOT NULL,
    [DOCTORNAME] nvarchar(50)  NOT NULL,
    [DOCTORGENDER] bit  NOT NULL,
    [DOCTORDATEOFBIRTH] datetime  NOT NULL,
    [DOCTORMOBILENO] nvarchar(max)  NOT NULL,
    [DOCTORADDRESS] nvarchar(256)  NOT NULL,
    [WORKINGSTARTDATE] datetime  NOT NULL,
    [WORKINGENDDATE] datetime  NOT NULL,
    [CREATEBY] nvarchar(50)  NOT NULL,
    [CREATEDATE] datetime  NOT NULL,
    [UPDATEBY] nvarchar(50)  NOT NULL,
    [UPDATEDATE] datetime  NOT NULL,
    [DELETEFLAG] bit  NOT NULL,
    [DEPARTMENT_DEPARTMENTID] int  NOT NULL
);
GO

-- Creating table 'DEPARTMENT'
CREATE TABLE [dbo].[DEPARTMENT] (
    [DEPARTMENTID] int IDENTITY(1,1) NOT NULL,
    [DEPARTMENTNAME] nvarchar(256)  NOT NULL,
    [CREATEBY] nvarchar(50)  NOT NULL,
    [CREATEDATE] datetime  NOT NULL,
    [UPDATEBY] nvarchar(50)  NOT NULL,
    [UPDATEDATE] datetime  NOT NULL,
    [DELETEFLAG] bit  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [DOCTORID] in table 'DOCTOR'
ALTER TABLE [dbo].[DOCTOR]
ADD CONSTRAINT [PK_DOCTOR]
    PRIMARY KEY CLUSTERED ([DOCTORID] ASC);
GO

-- Creating primary key on [DEPARTMENTID] in table 'DEPARTMENT'
ALTER TABLE [dbo].[DEPARTMENT]
ADD CONSTRAINT [PK_DEPARTMENT]
    PRIMARY KEY CLUSTERED ([DEPARTMENTID] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [DEPARTMENT_DEPARTMENTID] in table 'DOCTOR'
ALTER TABLE [dbo].[DOCTOR]
ADD CONSTRAINT [FK_DEPARTMENTDOCTORMODEL]
    FOREIGN KEY ([DEPARTMENT_DEPARTMENTID])
    REFERENCES [dbo].[DEPARTMENT]
        ([DEPARTMENTID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_DEPARTMENTDOCTORMODEL'
CREATE INDEX [IX_FK_DEPARTMENTDOCTORMODEL]
ON [dbo].[DOCTOR]
    ([DEPARTMENT_DEPARTMENTID]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------