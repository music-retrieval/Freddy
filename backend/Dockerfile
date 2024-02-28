# STAGE 1 - Use the base Essentia image
# NOTE: this an x86 image
FROM ghcr.io/mtg/essentia:bullseye-v2.1_beta5 as cpp-builder

# Install necessary build tools
RUN apt-get update && \
    apt-get install -y \
    cmake \
    g++

# Set the working directory (app for c++)
WORKDIR /app

# Copy the C++ source code into the container
COPY ./FrettysEssentia/ /app

# Compile the C++ application into a shared object
RUN mkdir build && \
    cd build && \
    cmake .. && \
    make

# STAGE 2 - Use the base Microsoft .NET SDK image
FROM mcr.microsoft.com/dotnet/sdk:latest AS dotnet-builder

# Set the working directory (App for .NET)
WORKDIR /App
EXPOSE 80

# Copy the C# project
COPY ./Fretty/ ./

# Restore NuGet packages
RUN dotnet restore
RUN dotnet publish -c Release -o out

# STAGE 3 - Build runtime image
# NOTE: This must match the architecture of the C++ image (x86)
FROM mcr.microsoft.com/dotnet/aspnet:8.0-bookworm-slim-amd64

# Set the working directory (Run for the final image)
WORKDIR /Run

# Copy the audio files if present
COPY ./audio /Run/

# Copy the C# application
COPY --from=dotnet-builder /App/out .

# Copy the C++ interop files
COPY --from=cpp-builder /app/build/src .
COPY --from=cpp-builder /usr/local/lib/* .
COPY --from=cpp-builder /usr/lib/* .
COPY --from=cpp-builder /lib/x86_64-linux-gnu/libpcre.so.3 .
COPY --from=cpp-builder /lib/x86_64-linux-gnu/libexpat.so.1 .
COPY --from=cpp-builder /lib/x86_64-linux-gnu/libkeyutils.so.1 .

# Ensure dotnet can find the C++ shared objects
ENV LD_LIBRARY_PATH $LD_LIBRARY_PATH:/Run/

# Set entrypoint, this runs the application
ENTRYPOINT ["dotnet", "Fretty.dll"]
