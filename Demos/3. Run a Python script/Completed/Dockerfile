FROM python:3.8

ENV url=https://google.com

WORKDIR /app

COPY . /app

RUN pip3 install -r requirements.txt

ENTRYPOINT [ "python", "app.py"]