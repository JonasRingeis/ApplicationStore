# Application Store

This is an example project to learn the concept of WPF and how to use the MVVM project structure for it.

The project consists of the WPF app itself and a `ms sql server` next to it.

It uses the WpfUI Library alongside Dapper to map the database results.


## Local Setup
To be able to start the application you'll need to have the database running. For that simply execute the following.
```bash
docker compose up -d
```

This will start up a docker container runnning ms sql server.

To stop it simply execute this.
```bash
docker compose down
```
