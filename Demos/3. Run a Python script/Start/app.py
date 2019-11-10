import requests

page = requests.get('https://www.teamupit.nl/')

print(page.content)