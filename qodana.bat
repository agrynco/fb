SET currentPath=%~dp0

docker run --rm -it -p 8080:8080 -v %currentPath%:/data/project/ -v %currentPath%/result:/data/results/ jetbrains/qodana-dotnet --show-report