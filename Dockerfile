FROM docker-registry-002.zeuslearning.com/zeuslearning/dotnet/aspnet:10.0-alpine AS runtime
WORKDIR /app

ENV ASPNETCORE_ENVIRONMENT=Development
ENV DOTNET_RUNNING_IN_CONTAINER=true
 
COPY ./TraineeManagement/publish .
 
ENTRYPOINT ["dotnet", "TraineeManagement.dll"]