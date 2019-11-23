# Goal
1) Show how you can easily persist data

# Steps

## Run MYSQL with a volume attached
```
docker run --name mysql -v c:\temp\datadir:/var/lib/mysql -p 3306:3306 -e MYSQL_ROOT_PASSWORD=my-secret-pw mysql:latest
```

## Stop it
```
docker stop mysql
```

## Remove it
```
docker rm mysql
```

## Run it again
```
docker run --name mysql -v c:\temp\datadir:/var/lib/mysql -p 3306:3306 -e MYSQL_ROOT_PASSWORD=my-secret-pw mysql:latest
```

## Result
Startup is now way faster since its not creating anything