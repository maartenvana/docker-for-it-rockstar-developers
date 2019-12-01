# Goals
1) Show how docker-compose can work for your own project

# Steps

## Show the docker-compose file
It builds your app and runs it.

## Run the services and show it works
```
docker-compose up
```

## Split up the compose files
- docker-compose.app.yaml
- docker-compose.deps.yaml

## Run the splitted files
```
docker-compose -f ./docker-compose.app.yaml -f ./docker-compose.deps.yaml up
docker-compose -f ./docker-compose.app.yaml -f ./docker-compose.deps.yaml up --abort-on-container-exit
```