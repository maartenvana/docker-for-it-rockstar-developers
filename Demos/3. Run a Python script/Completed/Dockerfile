FROM python:3.8

WORKDIR /app

COPY . .

RUN pip3 install -r requirements.txt

ENV SITE_URL=https://google.com

ENTRYPOINT [ "python", "app.py"]