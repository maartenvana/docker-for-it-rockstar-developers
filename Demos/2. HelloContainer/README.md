# Goal
Show how to build a simple container

# Steps

## Create dockerfile
```
FROM ubuntu

COPY hello.sh .

ENTRYPOINT [ "hello.sh" ]
```

## Build the image
```
docker build -t helloexample . 
```

## Run the container
```
docker run -it --rm --name hellocontainer helloexample
```

## Side note for Windows users building in their CI on linux
Add chmod run command
```
RUN chmod +x /scripts/hello.sh
```