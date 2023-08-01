# KafkaDotnet
- Working Kafka service
- You can run this application using `dotnet run` 
- Bring up swagger ``https://localhost:7226/swagger/index.html``
- Make a `POST` call with valid Parameters


## Set up notes

Installation of Package:
``PM> Install-Package Confluent.Kafka``

1. Open the file `server.properties`.
2. Find and replace the line `log.dirs=/tmp/kafka-logs` with `log.dirs=D:/kafka/kafka-logs
`

3. Now open the file `zookeeper.properties`.
4. Find and replace the line `dataDir=/tmp/zookeeper` with `dataDir=D:/kafka/zookeeper-data`



`server.properties` to change
- `listeners=PLAINTEXT://:9092` to `listeners=PLAINTEXT://127.0.0.1:9092`

From <https://stackoverflow.com/questions/62535706/how-to-resolve-kafka-error-connection-to-node-0-could-not-be-established> 


To start the servers:
- `./bin/windows/zookeeper-server-start.bat config/zookeeper.properties`
- `./bin/windows/kafka-server-start.bat config/server.properties`


To create a topic: 
- ``bin/windows/kafka-topics.bat --create --topic quickstart-events --bootstrap-server localhost:9092``

To List the topics available: 
- ``bin/windows/kafka-topics.bat --list --bootstrap-server localhost:9092 ``


Display All Kafka Messages in a Topic: 
- ``./bin/windows/kafka-console-consumer.bat --bootstrap-server localhost:9092 --topic quickstart-events --from-beginning``


Shut Down Zookeeper and Kafka: 
- `./bin/windows/zookeeper-server-stop.bat`
- `./bin/windows/kafka-server-stop.bat`

