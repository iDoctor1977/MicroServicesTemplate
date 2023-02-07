docker stop DashboardWeb
docker rm DashboardWeb
docker image rm dashboardweb
docker build -t dashboardweb -f Dockerfile .
docker run -d -p 5000:80 --name DashboardWeb dashboardweb
pause
