docker stop StorageRoomApi
docker rm StorageRoomApi
docker image rm storageroomapi
docker build -t storageroomapi -f Dockerfile .
docker run -d -p 5000:80 --name StorageRoomApi storageroomapi
pause
