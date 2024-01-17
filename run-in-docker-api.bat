SET PROJECT_NAME=family-budget

docker build -t %PROJECT_NAME%/web.api -f Web.API/Dockerfile .

docker run -e ASPNETCORE_ENVIRONMENT=Development -d -p 5000:80 --name %PROJECT_NAME%-Web.API %PROJECT_NAME%/web.api