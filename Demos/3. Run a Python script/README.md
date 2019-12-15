# Goal
1) Show how simple it is to run a python script
2) Show how to add an environment variable

# Steps

## Add a docker file
```
FROM python:3.8

WORKDIR /app

COPY . /app

RUN pip3 install -r requirements.txt

ENTRYPOINT [ "python", "app.py"]
```

## Build the container
```
docker build -t pythondemo .
```

## Run the container
```
docker run -it --rm pythondemocontainer pythondemo
```

## Convert script to environment variable version
```
import requests
import os

page = requests.get(os.environ['url'])

print(page.content)
```

## Add an ENV to the dockerfile and set a default value (to keep it simple)
```
ENV url=https://google.com
```

## Build the container (again)
```
docker build -t pythondemo .
```

## Run the container now without an environment variable
```
docker run -it --rm --name pythondemocontainer pythondemo
```

## Run the container now with an environment variable
```
docker run -e "url=https://teamupit.nl" -it --rm --name pythondemocontainer pythondemo
```