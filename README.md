# _Pierres Treats_

#### _MVC application for keeping track of treats and their flavors. 6/5/2020_

### Made by DJ Zevenbergen


## Description
_This C#/.NET Core MVC application uses a MySQL database to allow the user to add Treats and Flavors in a many-to-many relationship. It allows the user to see specific details about any flavor or treat for details about it and a list of all items belonging to the other class._


## Setup/Installation Requirements
* Clone repository from GitHub in terminal or console
* ensure that C#/.netcore2.2 is installed on your computer
* ensure that mysql is installed on your computer
* create a file named "appsettings.json" in the PierresTreats folder
* populate appsettings.json with the following text (making sure you use your server, port number, uid, and password):

  {
    "ConnectionStrings": {
      "DefaultConnection": "Server={servername};Port={port-number};database=david_zevenbergen;uid={your-uid};pwd={your-password};",
      "LibraryContextConnection": "Server=(localdb)\\mssqllocaldb;Database=david_zevenbergen;Trusted_Connection=True;MultipleActiveResultSets=true"
    }
  }

* in the terminal navigate to the project's root directory
* use "dotnet restore PierresTreats"
* navigate into PierresTreats folder "cd PierresTreats"
* ensure that the program builds by running "dotnet build"
* to start the database use  "dotnet ef database update"
* to start the program use "dotnet run"

## Specs
1. User is presented with a homepage, and sees a table of all flavors and treats.
   If there are none, the headers are clickable to take the user to a page to add treats/flavors
   
  * Input: click - "Flavors"
  * Output: "/flavors/create"
2. User is prompted to login in order to create new flavor
  * Input: "Username" "Password"
  * Output: "Welcome 'Username'" 
            "Redirect to home"
2. User can create a new flavor
  * Input: "Flavor"
  * Output: "/flavors/create"
3. User fills out flavor form and is redirected to now populated home page
  * Input: "Chocolate" "no treats to choose from yet!"
  * Output: "/" "Treats"  "Flavors"
                 none   |  Chocolate
                        |
                        |
4. User can click on a flavor/treat to see details incl. all instances of the other class that belong to that class
  * Input: "Click 'Chocolate'"
  * Output: "Chocolate Details:"

            "Treats List"
            "No treats yet!"
            "Back to List"

          ~~Logged in users only~
            "Add treat"
            "Edit flavor"
            "Delete Flavor"
            
5. User can click Add Treat on that flavor's page
  * Input: "Add Treat"
  * Output: "/treats/create"
6. User fills out a Treat form and is then redirected to the home page" 
  * Input: "Eclair" "Chocolate"
  * Output: "/"

7. User can search for Flavors or Treats
  * Input: "choc" "search by flavor" "search"
  * Output: "Chocolate"

8. All of this functionality could be executed on Flavors or Treats


## Support
_Open an issue on github_


## Built With
C#/ASP.NET Core, Entity, MySQL

### License
This project is licensed under the MIT License

Copyright (c) 2020 **_DJ Zevenbergen_**