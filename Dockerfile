# STAGE 1 - Use the base Essentia image
FROM mtgupf/essentia:latest as cpp-builder

# Install necessary build tools
RUN apt-get update && \
    apt-get install -y \
    cmake \
    g++

# Set the working directory (app for c++)
WORKDIR /app

# Copy your C++ source code into the container
COPY ./FreddysEssentia/ /app

# Compile your C++ application
RUN mkdir build && \
    cd build && \
    cmake .. && \
    make

# STAGE 2 - Use the base Microsoft .NET SDK image
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS dotnet-builder

# Set the working directory (App for .NET)
WORKDIR /App

# Copy the C# project
COPY ./Freddy/ ./

# Restore NuGet packages
RUN dotnet restore
RUN dotnet publish -c Release -o out

# STAGE 3 - Build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:8.0

# Set the working directory (Run for the final image)
WORKDIR /Run

# Copy the C# application and the C++ application
COPY --from=dotnet-builder /App/out .
COPY --from=cpp-builder /app/build/src .
COPY --from=cpp-builder /usr/local/lib/libessentia.so .

# Set entrypoint
ENTRYPOINT ["dotnet", "Freddy.dll"]
