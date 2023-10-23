# Tibber.TechnicalCase

## How to run it

First make sure you installed docker and docker compose on your system then use this command to run the application, 
```Console
docker compose up --build -d
```

you can validate the successful run using this command, 
```Console
docker compose ps
```
2 container should be runing one postgres database and one application, 

## How to access
After validating the application is runing successfuly, You can use this link to access the swagger documentation (http://localhost:5000/swagger/index.html)
