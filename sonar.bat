dotnet sonarscanner begin /k:"FB" /d:sonar.host.url="http://sonar.grynco.com.ua"  /d:sonar.login="sqp_8daca529172cd74a39a72c6c03c67ebd413ddd71"
dotnet build
dotnet sonarscanner end /d:sonar.login="sqp_8daca529172cd74a39a72c6c03c67ebd413ddd71"