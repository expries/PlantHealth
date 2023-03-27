Wir haben uns für 
MongoDb entschieden, da noSQL Datenbanken wie MongoDB 
leichter horizontal skalierbar sind als handelsübliche Relatione DB.

MongoDB ist auf das schreiben von Daten optimiert. Da wir einen eigenen Producer haben, welcher
Daten automatisch in Kafka einspeist, war es uns wichtig, dass die Datnebank schnell viele Inserts
behandeln kann, welcher genau die spezialität von MongoDb ist.

Dafür ist MongoDb nicht so gut optimiert auf das lesen von Daten wie andere relationale DB.

Da MongoDB kein Schema besitzt wie in einer RelationenDb funktioniert das migrieren deutlich leichter.

Außerdem wollten wir mal eine NoSQL Datenbank ausprobieren :=)