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
docker run -it --rm hellocontainer helloexample
```