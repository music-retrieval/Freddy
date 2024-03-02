# Use Node.js as base image for building the React app
FROM node:latest AS build

# Set the working directory inside the container
WORKDIR /app

# Copy package.json and package-lock.json to the working directory
COPY package.json package-lock.json ./

# Install dependencies including http-server
# RUN npm cache clean --force 
RUN npm install --no-optional && npm install -g http-server

# Copy the rest of the application code to the working directory
COPY . .

# Build the React app
RUN npm run build

# Expose port 3000 to the outside world
EXPOSE 3000

# Command to start http-server serving the built React app
CMD ["http-server", "-p", "3000", "build", "--cors"]
