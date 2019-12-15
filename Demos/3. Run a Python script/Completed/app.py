import requests
import os

page = requests.get(os.environ['SITE_URL'])

print(page.content)