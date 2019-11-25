Battleship State Tracker
========================

Web API for classic Battleship game.

Application design approach
---------------------------
* Core application code is around Board and Battleship domain objects.
* The UI is responsible for rehydrating the domain object and ask whether an operation is possible,
	e.g. UI ask board object whether it can add battleship or take an attack.
* If the domain object indicates the requested operation is possible then UI will persist the corresponding entities in database.

There is no persistance store, application uses in memory database

Currently the app has following operations
* Create a board
* Add battle ship
* Take an attack on any block

Following are the cURL request
* To create board
	curl -X POST "https://battleapi20191125055931.azurewebsites.net/api/Board" -H "accept: */*"
* To add battleship to board
	curl -X POST "https://battleapi20191125055931.azurewebsites.net/api/board/1/BattleShip" -H "accept: */*" -H "Content-Type: application/json-patch+json" -d "{\"blockNumbers\":[10,20,30]}"
* To attack a block
	curl -X POST "https://battleapi20191125055931.azurewebsites.net/api/board/1/Attack" -H "accept: */*" -H "Content-Type: application/json-patch+json" -d "{\"number\":10}"


