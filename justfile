dotnet_config := "Release"

build-runtime:
    dotnet build -c {{ dotnet_config }} -p:PublishSingleFile=true -p:PublishTrimmed=true

build-linux:
    dotnet publish -c {{ dotnet_config }} -r linux-x64 -p:PublishTrimmed=true -p:PublishAot=true