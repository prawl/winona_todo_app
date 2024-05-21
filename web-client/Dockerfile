FROM node:18.14

ENV APP_HOME=/app

# Create app directory and set it as the working directory
WORKDIR $APP_HOME

# Install global dependencies
RUN npm install -g webpack @angular/cli@latest

# Copy package.json and package-lock.json (if available)
COPY package*.json ./

RUN rm -rf node_modules package-lock.json

# Install app dependencies
RUN npm install

# Copy the rest of the application code
COPY . .

# Build the application
RUN npm run build -- --configuration production

# Expose the port the app runs on
EXPOSE 4200

# Start the server
CMD ["ng", "serve", "--host", "0.0.0.0", "--port", "4200"]
