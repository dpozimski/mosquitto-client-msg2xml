# Overview
Mosquitto client which listen for all topics and convert the messages into the xml document. The output data will be send to the dummy rest server.

[Wath how it works on YouTube!](https://youtu.be/EqdYlqggDsc)

# Template system
The converter is looking for xml pattern using incoming topic from each request.
In the best scenario program is finding the Regex pattern from appsettings.json file and the incoming data will be placed into choosed template and then the complete message will be redirected as a post request to external REST Server.
Converter has primitive fallback mechanism. If there is no any pattern to choose the default one will be used.
It is done by this part of appsettings.json.
```json
{
    "xmlTemplates": [
        {
            "topicSearchPattern": "^(?=.*?\\bhome\\b)(?=.*?\\bgarden\\b)(?=.*?\\bfountain\\b).*$",
            "template": "<client><property>#1</property><area>#2</area><object>#3</object><measureType>temperature</measureType><measure>#Content</measure></client>"
        },
        {
            "topicSearchPattern": "default",
            "template": "<client><location>#1</location><device>#2</device><type>#3</type><value>#Content</value></client>"
        }
    ]
}
```

# Authentication
To connect to the mosquitto broker we will using credentials (username && password).
```json
{
    "mqttCredentials": {
        "username": "admin",
        "password": "test123"
    }
}
```

# Redirecting messages
Converted messages will be redirected as a HTML request to host choosed in settings.
For our scenario the mock rest server is used.
```json
{
  "xmlDocumentsReceiverEndpoint": "https://jsonplaceholder.typicode.com/posts"
}
```
