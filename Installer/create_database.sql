CREATE DATABASE app_store;  
GO

BEGIN --#region create tables

USE app_store;

CREATE TABLE
    dbo.checksum_algorithms
(
    algorithm_id        INT identity(1, 1)      not null
    ,algorithm_name      VARCHAR(50)             not null
        CONSTRAINT unique_checksum_algorithm_algorithm_name UNIQUE (algorithm_name)
    ,CONSTRAINT pk_checksum_algorithms PRIMARY KEY (algorithm_id asc)
);

CREATE TABLE
    dbo.publishers
(
    publisher_id            INT identity(1, 1)      not null
    ,publisher_name         varchar(255)            not null
    ,publisher_description  VARCHAR(5000)           not null
    ,CONSTRAINT pk_publishers PRIMARY KEY (publisher_id asc)
);

CREATE TABLE
    dbo.applications
(
    application_id      INT identity(1, 1)      not null
    ,title              varchar(255)            not null
    ,description        VARCHAR(5000)           not null
    ,publisher_id       INT                     not null
        CONSTRAINT fk_applications_publisher_id FOREIGN KEY 
            REFERENCES dbo.publishers( publisher_id )
    ,CONSTRAINT pk_applications PRIMARY KEY (application_id asc)
);

CREATE TABLE
    dbo.application_versions
(
    application_version_id      INT identity(1, 1)          not null
    ,application_id             INT                         not null
        CONSTRAINT fk_application_versions_application_id FOREIGN KEY
            REFERENCES dbo.applications( application_id )
    ,uploaded_at                datetime                    not null
        CONSTRAINT default_application_versions_uploaded_at DEFAULT SYSUTCDATETIME()
    ,version_name               varchar(255)                not null
    ,download_url               varchar(500)                not null
    ,checksum_hash              varchar(255)                null
    ,checksum_algorithm_id      INT                         null
    ,CONSTRAINT pk_application_versions PRIMARY KEY (application_version_id asc)
);

INSERT INTO checksum_algorithms (algorithm_name)
VALUES ('SHA1'), ('SHA256'), ('SHA512'), ('MD5');

END
--#endregion

BEGIN --#region mock data

USE app_store;
    
INSERT INTO
    dbo.publishers (publisher_name, publisher_description)
VALUES
(
    'Mozilla',
    'Child company of the non-profit Mozilla Foundation.'
),
(
    'Google',
    'For-profit company owned by alphabet.'
);

INSERT INTO
    dbo.applications (title, description, publisher_id)
VALUES
(
    'Firefox',
    'Privacy oriented browser.',
    1
),
(
    'Chrome',
    'State of the art browser.',
    2
);

INSERT INTO 
    dbo.application_versions
(
    application_id,
    checksum_hash,
    checksum_algorithm_id,
    download_url,
    version_name
)
values
(
    1,
    '1bf4241bcc1c41a516f6313243153851ddcc49c22cd23fb8d5371a4b06b839235924f77b206a41880f71f47be0636e9ff2c7873f99daba106c7ee311513bb2dc',
    3,
    'https://ftp.mozilla.org/pub/firefox/releases/137.0/win64/en-US/Firefox%20Setup%20137.0.exe',
    '137.0.0'
),
( 
    1,
    'fe23jon2g56f3f4sd',
    3,
    'https://ftp.mozilla.org/pub/firefox/releases/137.0.1/win64/en-US/Firefox%20Setup%20137.0.1.exe',
    '137.0.1'
),
(
    1,
    '1bf4241bcc1c41a516f6313243153851ddcc49c22cd23fb8d5371a4b06b839235924f77b206a41880f71f47be0636e9ff2c7873f99daba106c7ee311513bb2dc',
    3,
    'https://ftp.mozilla.org/pub/firefox/releases/137.0.2/win64/en-US/Firefox%20Setup%20137.0.2.exe',
    '137.0.2'
),
(
    1,
    'efe09b091a9ed55d4b504235f7f70838c64d633f8e95c10091ccb46239bd951f6d8ee68cf749be701e8e6b15f1aaadee67703e4bb2f1ff220a32fc7229e0c072',
    3,
    'https://ftp.mozilla.org/pub/firefox/releases/138.0/win64/en-US/Firefox%20Setup%20138.0.exe',
    '138.0.0'
),
(
    2,
    'feruih29uidsa',
    2,
    'https://chrome.com/download',
    '169.27.14'
),
(
    2,
    '3e2fnm9iouin9sd',
    2,
    'https://chrome.com/download',
    '169.27.15'
);

END
--#endregion
