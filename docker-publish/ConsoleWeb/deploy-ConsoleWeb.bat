docker stop ConsoleWeb
docker rm ConsoleWeb
docker image rm consoleweb
docker build -t consoleweb -f Dockerfile .
docker run -d -p 5100:80 --name ConsoleWeb consoleweb
pause
