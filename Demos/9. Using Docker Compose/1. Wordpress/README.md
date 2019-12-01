# Goals
1) Show how docker-compose works
2) Show how this makes it way easier to configure your environment/services

# Steps

## Show the docker-compose file
- 2 Services
- Wordpress "depends" on db
- Ports dont need to be mapped to host for the db
- Volume for the data to keep it persisted (warning: only in docker not on disk)

## Run the services
```
docker-compose up
```