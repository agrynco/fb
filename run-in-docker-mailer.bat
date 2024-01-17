SET PROJECT_NAME=family-budget

docker build -t %PROJECT_NAME%/mailsender.console -f MailSender.Console/Dockerfile .

docker run -e ASPNETCORE_ENVIRONMENT=Development -d --name %PROJECT_NAME%-mailsender.console %PROJECT_NAME%/mailsender.console
