Eventlst is a distributed, cleanly coded, test driven web application. It comes in two parts :

**Front End : Javascript SPA (AngularJS) [hosted on Heroku]**

Lineman.js is used as a wrapper around an angularJs appliction.
To see the live site please go [here][1].

  [1]: http://eventlstui.herokuapp.com

The source code is here : [https://github.com/peterheard01/EventLst.Ui][2].

  [2]: https://github.com/peterheard01/EventLst.Ui


**Back End : C# backend (.net Web-Api) [hosted on Appharbor]**

Asp.net Web-Api has been used to reach into the data providers and get the data. As this application does not host the data itself the api performs security handshakes with the providers such as meetup.com.

The source code is here : [https://github.com/peterheard01/EventLst.Api][2].

  [2]: https://github.com/peterheard01/EventLst.Api


The code base is fully implemented using TDD techniques, the style of TDD is classic: meaning that a number of the assertions check state. Stub items are the most common type of test double and some spies are also used.

Here are the (most important) design considerations:

 - The code is cleaned and refactored into patterns often to reduce duplication and increase maintainability.
 - The framework (angularJS) is kept away from the domain, this is a common mistake which is made with Single Page Applications. Angular is used for what it is good at, rendering views and some other arbitrary features such as Dependency Injection and XMLHttp.
 - Tests are written on a 'use case' perspective.

You can see the deployed application at...

See the live site : [http://eventlstui.herokuapp.com][2].

  [2]: http://eventlstui.herokuapp.com
