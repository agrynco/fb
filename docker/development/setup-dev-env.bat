docker-compose -f docker-compose_development.yml -p family_budget_develop up --force-recreate -d

SET currentPath=%~dp0

CD ..\..\DAL.EF\

update_db_with_migrator-Development.bat

CD currentPath 