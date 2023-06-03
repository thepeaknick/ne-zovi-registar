# for test
docker rm  sql_server2022 --force 
docker rm  nezovireg_api --force 


$confirmation = Read-Host "Remove api-web_api image?[y/n]"
if ($confirmation -eq 'y') {
	docker rmi api-web_api
}

docker-compose up -d
$confirmation = Read-Host "Start migration?[y/n]"
if ($confirmation -eq 'y') {
	docker exec nezovireg_api /app/NeZoviReg.Migrations.Ef
}
