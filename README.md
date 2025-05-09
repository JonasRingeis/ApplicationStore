# Application Store

This is an example project to learn the concept of WPF and how to use the MVVM project structure for it.

The project consists of the WPF app itself and a `ms sql server` next to it.

It uses the WpfUI Library alongside Dapper to map the database results.


## Local Setup

1. To be able to start the application you'll need to have the database running. For that simply execute the following.
   ```bash
   cd Installer
   docker compose up -d
   ```
    This will start up a docker container running ms sql server in the background.<br> 
    By default the docker container will **stay running**, even when the **system restarted**. 
    
    > To stop it simply run ``docker compose down``
   
2. Then the database and tables need to be set up.

   For this you'll need to connect to the database and execute [this sql script](./Installer/create_database.sql).

3. Then when executing the app you'll need to have one env variable set.
   
   | name   | value                                                                                                               |
   |--------|---------------------------------------------------------------------------------------------------------------------|
   | DB_URI | Server=localhost,1433;Database=<DATABASE_NAME>;User Id=sa;Password=SECURE_PASSWORD_123;TrustServerCertificate=True; |
