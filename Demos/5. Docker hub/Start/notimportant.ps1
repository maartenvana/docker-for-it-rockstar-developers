docker build -t nginxdemo .
docker run -it --rm --name nginxcontainer -p 5001:80 nginxdemo