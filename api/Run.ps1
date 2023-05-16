# for test
#docker rm  sql_server2022 --force 
#docker rm  nezovireg_api --force 

docker-compose up -d
docker exec nezovireg_api /app/NeZoviReg.Migrations.Ef