# Goals
1) Show how healthchecks can indicate if your app is running correctly
2) This is a fun one for K8s users, since its uncommon knowledge that docker can do this
3) Downside is you have to install curl in your image

# Steps


## Add the health check
```
HEALTHCHECK --interval=5s --retries=3 --start-period=5s --timeout=3s \
  CMD curl -f http://localhost || exit 1
```

## Install curl
```
RUN apt-get update &&\
    apt-get install curl
```


## Build the image
```
docker build -f ./ExampleWebApp/Dockerfile -t example-web-app-with-hc .
```

## Show it Starting and getting Healthy
```
docker run -d --name ExampleWebAppWithHC example-web-app-with-hc
docker ps
```