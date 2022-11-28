FROM node:12.18-alpine

WORKDIR /usr/src/app

COPY ./web .

RUN npm install

CMD ["node", "app.js"]