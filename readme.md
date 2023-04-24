die jeweiligen docker-composes starten für Kafka und MongoDb (Order ./Kafka bzw. ./MongoDb)
den BackendServer starten (PlantHealth.sln öffnen builden und starten als HTTP  (HTTPS funktioniert mit NEXT.js in der DEV environment nicht) )
planthealth.frontend mit npm run dev starten
ggf in der .env.local die url zum backend server anpassen

im ./Kafka ordner befindet sich auch eine Solution mit Producer/Consumer um Kafka mit Fake Daten zu füllen

evtl müssen node_modules über npm i installiert werden bzw. für die Backend Api dotnet restore ausgeführt werden