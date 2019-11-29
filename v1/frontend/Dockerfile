# todo, currently is build process not dockerized, dockerize
# build stage
#FROM node:9.11.1-alpine as build-stage
#WORKDIR /app
#COPY package*.json ./
#RUN npm install
#COPY . .
#RUN npm run build

# production stage
FROM nginx:1.13.12-alpine as production-stage

COPY nginx.conf /etc/nginx/conf.d/default.conf
COPY /dist /usr/share/nginx/html
# todo, implement following line after build-stage in docker is implemented, and remove copy line before this line
#COPY --from=build-stage /app/dist /usr/share/nginx/html
EXPOSE 80
CMD ["nginx", "-g", "daemon off;"]
