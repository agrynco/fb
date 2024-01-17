docker stop family_budget_production_api
docker rm family_budget_production_api
docker rmi family_budget_production-web-api

CALL setup-prod-env.bat