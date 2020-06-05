# _Hair Salon_

#### _MVC application for keeping track of hair stylists and their clients. 5/29/2020_

### Made by DJ Zevenbergen


## Description
_This C#/.NET Core MVC application uses a MySQL database to allow the user to add Treats and Flavors in a many-to-many relationship. It allows the user to see specific details about any flavor or treat for details about it and a list of all items belonging to the other class._


## Setup/Installation Requirements
* Clone repository from GitHub in terminal or console
* ensure that C#/.netcore2.2 is installed on your computer
* ensure that mysql is installed on your computer
* if you have mysql workbench, use administration>Data import/restore>import from self-contained file>PierresTreats.Solution/david_zevenbergen.sql>start import
* if you don't have mysql workbench, here are the create statements:


        CREATE DATABASE `david_zevenbergen` /*!40100 DEFAULT CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci */;
        
        USE david_zevenbergen;
        
        CREATE TABLE `clients` (
          `ClientId` int(11) NOT NULL AUTO_INCREMENT,
          `FirstName` varchar(45) DEFAULT NULL,
          `LastName` varchar(45) DEFAULT NULL,
          `PhoneNumber` varchar(15) DEFAULT NULL,
          `StylistId` int(11) NOT NULL,
          PRIMARY KEY (`ClientId`),
          KEY `StylistId_idx` (`StylistId`),
          CONSTRAINT `StylistId` FOREIGN KEY (`StylistId`) REFERENCES `stylists` (`StylistId`) ON DELETE CASCADE
        ) ENGINE=InnoDB AUTO_INCREMENT=17 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

        CREATE TABLE `stylists` (
          `StylistId` int(11) NOT NULL AUTO_INCREMENT,
          `FirstName` varchar(45) DEFAULT 'null',
          `LastName` varchar(45) DEFAULT 'null',
          `PhoneNumber` varchar(45) DEFAULT 'null',
          PRIMARY KEY (`StylistId`)
        ) ENGINE=InnoDB AUTO_INCREMENT=10 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;




* create a file named "appsettings.json" in the HairSalon folder
* populate appsettings.json with the following text (making sure you use your port number, uid, and password):

        {
          "ConnectionStrings": {
            "DefaultConnection": "Server=localhost;Port={your default port number here!};database=david_zevenbergen;uid={your mysql userid here!};pwd={your password for mysql here!};"
          }
        }

* in the terminal navigate to the project's root directory
* use "dotnet restore PierresTreats"
* navigate into PierresTreats folder "cd PierresTreats"
* to start the application use "dotnet run"

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
  * Input: "Eclair" "Chocolate" "$2.00"
  * Output: "/"

7. User can search for Flavors or Treats
  * Input: "choc" "search by flavor" "search"
  * Output: "Chocolate"

8. All of this functionality could be executed on Flavors or Treats


## Support
_Open an issue on github_


## Built With
C#/ASP.NET Core 

### License
This project is licensed under the MIT License

Copyright (c) 2020 **_DJ Zevenbergen_**