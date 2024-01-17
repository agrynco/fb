FROM ubuntu as base

RUN apt-get update && \
      apt-get -y install sudo

RUN apt-get update && apt-get --assume-yes install wget

WORKDIR /.NET
RUN wget https://download.visualstudio.microsoft.com/download/pr/5226a5fa-8c0b-474f-b79a-8984ad7c5beb/3113ccbf789c9fd29972835f0f334b7a/dotnet-sdk-8.0.100-linux-x64.tar.gz
RUN mkdir -p $HOME/dotnet && tar zxf dotnet-sdk-8.0.100-linux-x64.tar.gz -C $HOME/dotnet
RUN export DOTNET_ROOT=$HOME/dotnet
RUN export PATH=$PATH:$HOME/dotnet

ENV DOTNET_ROOT="/root/dotnet"
ENV PATH="$PATH:/root/dotnet"

WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM base AS build

RUN apt-get --assume-yes install wget

WORKDIR /src
COPY ["", ""]

ENV DOTNET_SYSTEM_GLOBALIZATION_INVARIANT=1

RUN dotnet restore

WORKDIR "/src/Web.API"
RUN dotnet build "Web.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish /p:AssemblyVersion=0.5.2.0 "Web.API.csproj" -c Release -o /app/publish

FROM base AS final
RUN apt-get update && \
      apt-get -y install sudo 
      
RUN useradd -m docker && echo "docker:docker" | chpasswd && adduser docker sudo

ENV DOTNET_SYSTEM_GLOBALIZATION_INVARIANT=1

WORKDIR /app
COPY --from=publish /app/publish .

ENV ASPNETCORE_URLS=http://*:80

ENTRYPOINT ["dotnet", "Web.API.dll"]
