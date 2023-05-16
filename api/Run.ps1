cd d:\Projects\nezovireg.git_docker\api\

# for test
#docker rm  sql_server2022 --force 
#docker rm  nezovireg_api --force 

docker-compose up -d
#docker exec nezovireg_api NeZoviReg.Migrations.Ef.exe
dotnet run \src\NeZoviReg.Migrations.Ef\bin\Release\net7.0\NeZoviReg.Migrations.Ef.exe