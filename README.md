# Introduction

1. Issues tracking: [youtrack](http://yt.grynco.com.ua/projects/dad9baef-55a7-463a-a76d-2a9a9da1a221)
2. Documentation: [YouTrack](http://yt.grynco.com.ua/articles/FB)
3.

Sources [Gitea](http://gitea.grynco.com.ua/agrynco/fb-web-api), [Pull requests](http://gitea.grynco.com.ua/agrynco/fb-web-api/pulls)

# Getting Started

TODO: Guide users through getting your code up and running on their own system. In this section you can talk about:

1. Install [SDK 8](https://dotnet.microsoft.com/en-us/download/dotnet/8.0)
2. Install [docker container](https://docs.docker.com/get-docker/)
3. Perform  `\docker\development\setup-dev-env.bat`
4. Add `127.0.0.1     MYSQL_DB_SRV` to the [hosts](https://en.wikipedia.org/wiki/Hosts_(file)) file
5. Perform `\DAL.EF\update_db_with_migrator-Development.bat`
6. Start `\Web.API\Web.API.csproj`
5. Browse http://localhost/swagger

# CI/CD

[CI/CD](http://tc.grynco.com.ua/project/NetProjects_FamilyBudget?mode=builds)

# Build and Test

Perform \fb-web-api\docker\development\setup-dev-env.bat

Go to root folder of the solution and perform command

    docker build -t web.api -f Web.API/Dockerfile .

# Contribute

If you want to learn more about creating good readme files then refer the
following [guidelines](https://docs.microsoft.com/en-us/azure/devops/repos/git/create-a-readme?view=azure-devops). You
can also seek inspiration from the below readme files:

- [ASP.NET Core](https://github.com/aspnet/Home)
- [Visual Studio Code](https://github.com/Microsoft/vscode)
- [JetBrains Rider](https://www.jetbrains.com/rider/)

# Versioning

Major.Minor.Build.Revision

1. Major - major changes in the architecture
2. Minor - new features
3. Build - bug fixes
4. Revision - minor changes