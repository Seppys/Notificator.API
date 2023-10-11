# Notificator

## Description

Notificator API is an ASP.Net Core API that allows emails and Telegram messages sending.

## Features

* Telegram messages sendind: by passing a Telegram username who has a started chat with the API Telegram bot it's possible to send messages
* Emails sending: by passing an receiver email address, an subject and optionally a body content you can send him emails.

## Use examples

### Telegram messages sending

```python
import requests

url = "https://notificatorapi.azurewebsites.net/api/telegram/send"
payload = {
    "username": "yourTelegramUsername",
    "text": "Message sent from Notificator API"
}
response = requests.post(url, json=payload)

print(response.text)
```

### Emails sending

```python
import requests

url = "https://notificatorapi.azurewebsites.net/api/email/send"
payload = {
    "address": "emailAddress@example.com",
    "subject": "Test email",
    "body": "Email sent from Notificator API"
}
response = requests.post(url, json=payload)

print(response)
```

## Requirements

- A Gmail account which will send emails and its application password.
To generate a application password enter in [Manage your Google's account > Security](https://myaccount.google.com/security) >
Two-Step Authentication > Application Passwords
- A Telegram bot and its token. You can create a bot and get its token in Telegram app through [BotFather](https://t.me/botfather) typing /newbot
- Modify .envExample file changing variables and refactor it to .env
