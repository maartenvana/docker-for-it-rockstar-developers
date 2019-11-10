import requests
import os

page = requests.get(os.environ['url'])

print(page.content)