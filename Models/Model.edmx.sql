
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 05/27/2021 15:26:54
-- Generated from EDMX file: D:\I3332\webs\StackOverFlow\Models\Model.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [StackOverFlow];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK__Answer__question__33D4B598]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Answers] DROP CONSTRAINT [FK__Answer__question__33D4B598];
GO
IF OBJECT_ID(N'[dbo].[FK__Comment__answer___38996AB5]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Comments] DROP CONSTRAINT [FK__Comment__answer___38996AB5];
GO
IF OBJECT_ID(N'[dbo].[FK__Comment__creator__36B12243]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Comments] DROP CONSTRAINT [FK__Comment__creator__36B12243];
GO
IF OBJECT_ID(N'[dbo].[FK__Comment__questio__37A5467C]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Comments] DROP CONSTRAINT [FK__Comment__questio__37A5467C];
GO
IF OBJECT_ID(N'[dbo].[FK__Profile__univers__2E1BDC42]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Profiles] DROP CONSTRAINT [FK__Profile__univers__2E1BDC42];
GO
IF OBJECT_ID(N'[dbo].[FK__Question__creato__30F848ED]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Questions] DROP CONSTRAINT [FK__Question__creato__30F848ED];
GO
IF OBJECT_ID(N'[dbo].[FK__uni_fact___branc__2A4B4B5E]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[uni_fact_branch] DROP CONSTRAINT [FK__uni_fact___branc__2A4B4B5E];
GO
IF OBJECT_ID(N'[dbo].[FK__uni_fact___factu__2B3F6F97]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[uni_fact_branch] DROP CONSTRAINT [FK__uni_fact___factu__2B3F6F97];
GO
IF OBJECT_ID(N'[dbo].[FK__uni_fact___unive__29572725]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[uni_fact_branch] DROP CONSTRAINT [FK__uni_fact___unive__29572725];
GO
IF OBJECT_ID(N'[dbo].[FK__tag_quest__quest__3C69FB99]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tag_question] DROP CONSTRAINT [FK__tag_quest__quest__3C69FB99];
GO
IF OBJECT_ID(N'[dbo].[FK__tag_quest__tag_I__3D5E1FD2]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tag_question] DROP CONSTRAINT [FK__tag_quest__tag_I__3D5E1FD2];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[Answers]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Answers];
GO
IF OBJECT_ID(N'[dbo].[Branches]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Branches];
GO
IF OBJECT_ID(N'[dbo].[Comments]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Comments];
GO
IF OBJECT_ID(N'[dbo].[Factulties]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Factulties];
GO
IF OBJECT_ID(N'[dbo].[Profiles]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Profiles];
GO
IF OBJECT_ID(N'[dbo].[Questions]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Questions];
GO
IF OBJECT_ID(N'[dbo].[Tags]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Tags];
GO
IF OBJECT_ID(N'[dbo].[Universities]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Universities];
GO
IF OBJECT_ID(N'[dbo].[uni_fact_branch]', 'U') IS NOT NULL
    DROP TABLE [dbo].[uni_fact_branch];
GO
IF OBJECT_ID(N'[dbo].[tag_question]', 'U') IS NOT NULL
    DROP TABLE [dbo].[tag_question];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Answers'
CREATE TABLE [dbo].[Answers] (
    [answer_ID] int IDENTITY(1,1) NOT NULL,
    [text] varchar(255)  NOT NULL,
    [votes] int  NULL,
    [isApproved] bit  NULL,
    [date] datetime  NOT NULL,
    [imagePath] varchar(255)  NULL,
    [question_ID] int  NULL
);
GO

-- Creating table 'Branches'
CREATE TABLE [dbo].[Branches] (
    [branch_ID] int IDENTITY(1,1) NOT NULL,
    [Name] varchar(255)  NOT NULL
);
GO

-- Creating table 'Comments'
CREATE TABLE [dbo].[Comments] (
    [comment_ID] int IDENTITY(1,1) NOT NULL,
    [text] varchar(255)  NOT NULL,
    [date] datetime  NOT NULL,
    [creator_ID] int  NULL,
    [question_ID] int  NULL,
    [answer_ID] int  NULL
);
GO

-- Creating table 'Factulties'
CREATE TABLE [dbo].[Factulties] (
    [factulty_ID] int IDENTITY(1,1) NOT NULL,
    [Name] varchar(255)  NOT NULL
);
GO

-- Creating table 'Profiles'
CREATE TABLE [dbo].[Profiles] (
    [creator_ID] int IDENTITY(1,1) NOT NULL,
    [Email] varchar(255)  NOT NULL,
    [Password] varchar(255)  NOT NULL,
    [username] varchar(255)  NOT NULL,
    [major] varchar(255)  NOT NULL,
    [university_ID] int  NULL
);
GO

-- Creating table 'Questions'
CREATE TABLE [dbo].[Questions] (
    [question_ID] int IDENTITY(1,1) NOT NULL,
    [title] varchar(255)  NOT NULL,
    [body] varchar(255)  NOT NULL,
    [date] nvarchar(max)  NULL,
    [imagePath] varchar(255)  NULL,
    [creator_ID] int  NULL
);
GO

-- Creating table 'Tags'
CREATE TABLE [dbo].[Tags] (
    [tag_ID] int IDENTITY(1,1) NOT NULL,
    [tag] int  NULL
);
GO

-- Creating table 'Universities'
CREATE TABLE [dbo].[Universities] (
    [university_ID] int IDENTITY(1,1) NOT NULL,
    [Name] varchar(255)  NOT NULL
);
GO

-- Creating table 'uni_fact_branch'
CREATE TABLE [dbo].[uni_fact_branch] (
    [university_ID] int  NULL,
    [branch_ID] int  NULL,
    [factulty_ID] int  NULL,
    [id] int IDENTITY(1,1) NOT NULL
);
GO

-- Creating table 'tag_question'
CREATE TABLE [dbo].[tag_question] (
    [question_ID] int  NULL,
    [tag_ID] int  NULL,
    [id] int IDENTITY(1,1) NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [answer_ID] in table 'Answers'
ALTER TABLE [dbo].[Answers]
ADD CONSTRAINT [PK_Answers]
    PRIMARY KEY CLUSTERED ([answer_ID] ASC);
GO

-- Creating primary key on [branch_ID] in table 'Branches'
ALTER TABLE [dbo].[Branches]
ADD CONSTRAINT [PK_Branches]
    PRIMARY KEY CLUSTERED ([branch_ID] ASC);
GO

-- Creating primary key on [comment_ID] in table 'Comments'
ALTER TABLE [dbo].[Comments]
ADD CONSTRAINT [PK_Comments]
    PRIMARY KEY CLUSTERED ([comment_ID] ASC);
GO

-- Creating primary key on [factulty_ID] in table 'Factulties'
ALTER TABLE [dbo].[Factulties]
ADD CONSTRAINT [PK_Factulties]
    PRIMARY KEY CLUSTERED ([factulty_ID] ASC);
GO

-- Creating primary key on [creator_ID] in table 'Profiles'
ALTER TABLE [dbo].[Profiles]
ADD CONSTRAINT [PK_Profiles]
    PRIMARY KEY CLUSTERED ([creator_ID] ASC);
GO

-- Creating primary key on [question_ID] in table 'Questions'
ALTER TABLE [dbo].[Questions]
ADD CONSTRAINT [PK_Questions]
    PRIMARY KEY CLUSTERED ([question_ID] ASC);
GO

-- Creating primary key on [tag_ID] in table 'Tags'
ALTER TABLE [dbo].[Tags]
ADD CONSTRAINT [PK_Tags]
    PRIMARY KEY CLUSTERED ([tag_ID] ASC);
GO

-- Creating primary key on [university_ID] in table 'Universities'
ALTER TABLE [dbo].[Universities]
ADD CONSTRAINT [PK_Universities]
    PRIMARY KEY CLUSTERED ([university_ID] ASC);
GO

-- Creating primary key on [id] in table 'uni_fact_branch'
ALTER TABLE [dbo].[uni_fact_branch]
ADD CONSTRAINT [PK_uni_fact_branch]
    PRIMARY KEY CLUSTERED ([id] ASC);
GO

-- Creating primary key on [id] in table 'tag_question'
ALTER TABLE [dbo].[tag_question]
ADD CONSTRAINT [PK_tag_question]
    PRIMARY KEY CLUSTERED ([id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [question_ID] in table 'Answers'
ALTER TABLE [dbo].[Answers]
ADD CONSTRAINT [FK__Answer__question__33D4B598]
    FOREIGN KEY ([question_ID])
    REFERENCES [dbo].[Questions]
        ([question_ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK__Answer__question__33D4B598'
CREATE INDEX [IX_FK__Answer__question__33D4B598]
ON [dbo].[Answers]
    ([question_ID]);
GO

-- Creating foreign key on [answer_ID] in table 'Comments'
ALTER TABLE [dbo].[Comments]
ADD CONSTRAINT [FK__Comment__answer___38996AB5]
    FOREIGN KEY ([answer_ID])
    REFERENCES [dbo].[Answers]
        ([answer_ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK__Comment__answer___38996AB5'
CREATE INDEX [IX_FK__Comment__answer___38996AB5]
ON [dbo].[Comments]
    ([answer_ID]);
GO

-- Creating foreign key on [creator_ID] in table 'Comments'
ALTER TABLE [dbo].[Comments]
ADD CONSTRAINT [FK__Comment__creator__36B12243]
    FOREIGN KEY ([creator_ID])
    REFERENCES [dbo].[Profiles]
        ([creator_ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK__Comment__creator__36B12243'
CREATE INDEX [IX_FK__Comment__creator__36B12243]
ON [dbo].[Comments]
    ([creator_ID]);
GO

-- Creating foreign key on [question_ID] in table 'Comments'
ALTER TABLE [dbo].[Comments]
ADD CONSTRAINT [FK__Comment__questio__37A5467C]
    FOREIGN KEY ([question_ID])
    REFERENCES [dbo].[Questions]
        ([question_ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK__Comment__questio__37A5467C'
CREATE INDEX [IX_FK__Comment__questio__37A5467C]
ON [dbo].[Comments]
    ([question_ID]);
GO

-- Creating foreign key on [university_ID] in table 'Profiles'
ALTER TABLE [dbo].[Profiles]
ADD CONSTRAINT [FK__Profile__univers__2E1BDC42]
    FOREIGN KEY ([university_ID])
    REFERENCES [dbo].[Universities]
        ([university_ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK__Profile__univers__2E1BDC42'
CREATE INDEX [IX_FK__Profile__univers__2E1BDC42]
ON [dbo].[Profiles]
    ([university_ID]);
GO

-- Creating foreign key on [creator_ID] in table 'Questions'
ALTER TABLE [dbo].[Questions]
ADD CONSTRAINT [FK__Question__creato__30F848ED]
    FOREIGN KEY ([creator_ID])
    REFERENCES [dbo].[Profiles]
        ([creator_ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK__Question__creato__30F848ED'
CREATE INDEX [IX_FK__Question__creato__30F848ED]
ON [dbo].[Questions]
    ([creator_ID]);
GO

-- Creating foreign key on [branch_ID] in table 'uni_fact_branch'
ALTER TABLE [dbo].[uni_fact_branch]
ADD CONSTRAINT [FK__uni_fact___branc__2A4B4B5E]
    FOREIGN KEY ([branch_ID])
    REFERENCES [dbo].[Branches]
        ([branch_ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK__uni_fact___branc__2A4B4B5E'
CREATE INDEX [IX_FK__uni_fact___branc__2A4B4B5E]
ON [dbo].[uni_fact_branch]
    ([branch_ID]);
GO

-- Creating foreign key on [factulty_ID] in table 'uni_fact_branch'
ALTER TABLE [dbo].[uni_fact_branch]
ADD CONSTRAINT [FK__uni_fact___factu__2B3F6F97]
    FOREIGN KEY ([factulty_ID])
    REFERENCES [dbo].[Factulties]
        ([factulty_ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK__uni_fact___factu__2B3F6F97'
CREATE INDEX [IX_FK__uni_fact___factu__2B3F6F97]
ON [dbo].[uni_fact_branch]
    ([factulty_ID]);
GO

-- Creating foreign key on [university_ID] in table 'uni_fact_branch'
ALTER TABLE [dbo].[uni_fact_branch]
ADD CONSTRAINT [FK__uni_fact___unive__29572725]
    FOREIGN KEY ([university_ID])
    REFERENCES [dbo].[Universities]
        ([university_ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK__uni_fact___unive__29572725'
CREATE INDEX [IX_FK__uni_fact___unive__29572725]
ON [dbo].[uni_fact_branch]
    ([university_ID]);
GO

-- Creating foreign key on [question_ID] in table 'tag_question'
ALTER TABLE [dbo].[tag_question]
ADD CONSTRAINT [FK__tag_quest__quest__3C69FB99]
    FOREIGN KEY ([question_ID])
    REFERENCES [dbo].[Questions]
        ([question_ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK__tag_quest__quest__3C69FB99'
CREATE INDEX [IX_FK__tag_quest__quest__3C69FB99]
ON [dbo].[tag_question]
    ([question_ID]);
GO

-- Creating foreign key on [tag_ID] in table 'tag_question'
ALTER TABLE [dbo].[tag_question]
ADD CONSTRAINT [FK__tag_quest__tag_I__3D5E1FD2]
    FOREIGN KEY ([tag_ID])
    REFERENCES [dbo].[Tags]
        ([tag_ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK__tag_quest__tag_I__3D5E1FD2'
CREATE INDEX [IX_FK__tag_quest__tag_I__3D5E1FD2]
ON [dbo].[tag_question]
    ([tag_ID]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------