FROM microsoft/dotnet
COPY . /app
WORKDIR /app
RUN ["dotnet", "restore"]
RUN ["dotnet", "build"]
EXPOSE 6379/tcp
RUN chmod +x ./entrypoint.sh
CMD /bin/bash ./entrypoint.sh