# Team-For-WUP-Mobile-Track
Teamwork for Telerik Academy 2015


###Contributors:
	-Velimira Madjarova (TA user: velimira.madjarova)
	-Yordan Gergov (TA user: YordanGergov)

Youtube: 

###Project information:
Our user will be Happy HTTP Server, who will have to do as much work as possible. 
The server will have work goals, which will consist of how many successfull requests will need to be processed in order to get to the next level.
However, there will be bad requests that will need to be avoided and security updates that boosts up for some time the successfull requests.
There will be different cities, in which our Happy HTTP Server will need to work. If you complete a certain level in all cities - you get a bonus level.
You can change to a different city even within a game in another city. Your progress in the previous city is not lost. However, it does not work as pause. Hence, the position of the server and the requests and upgrades will be reset to initial position.
How to play:
Long Tap -> makes the Happy HTTP Server bigger so we can collect more good requests.
Double Tap -> shrinks the Happy HTTP Server to minimum size so we can avoid bad requests easier.
Pinch in -> switch to the city on the right
Pinch out -> switch to the city on the left
We move our Happy HTTP Server with the Accelerometer.
The successfull requests will actually be from your friends listed in the contact list. At the end of each level there will be information for which friend has helped you the most.
We use Geolocation to determine the city from which you start.
There will be music.

####Visual:

#####Initial Page


#####How to Play Pages 
(three pages that will explain the rules of the game) - you will swipe through these pages

#####Play Page

explanation on the side numbers: there will be numbers in the colours of the object that is coming with information after how many seconds this object will comm from this side.


####Details:
We will save current level (per city) and Best Score in Local Storage (SQLite)
We will save Scoreboard, Game statistics and Advices on game strategies in the Web Page (Web Api)
