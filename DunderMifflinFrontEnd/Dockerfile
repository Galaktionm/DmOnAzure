FROM node:latest as build

# Set the working directory
WORKDIR /usr/local/app

COPY ./ /usr/local/app/

# Install all the dependencies
RUN npm install

RUN npm run build


FROM nginx:latest

# Copy the build output to replace the default nginx contents.
COPY --from=build /usr/local/app/dist/dunder-mifflin-front-end /usr/share/nginx/html

EXPOSE 80