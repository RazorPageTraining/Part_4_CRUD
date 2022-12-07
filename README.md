## Create Read Update Delete

| NO. | TITLE |
| ----------- | ----------- |
| 1 | [Introduction](https://github.com/RazorPageTraining/Part_1_Introduction) |
| 2 | [Installation](https://github.com/RazorPageTraining/Part_2_Installation) |
| 3 | [Identity](https://github.com/RazorPageTraining/Part_3_Identity ) |
| 4 | [Net 5 CRUD](https://github.com/muhamaddarulhadi/RazorPage) ***(Example ONLY)***|
| 5 | [How to run this project](#how-to-run-this-project) |
| 6 | [Insert Library](#insert-library) |
| 7 | [CRUD code](#crud-code) </br> - [View](#view) </br> - [Insert](#insert) </br> - [Update](#update) </br> - [Delete](#delete) |


</BR>

***

#### How to run this project

1. Download this project and open your terminal or VS Code.
2. On your terminal type this command, but first you need to make sure you know where your project folder is. For my case, I put it on my desktop
   
   > ![image](https://user-images.githubusercontent.com/47632993/146669384-e9b4b021-077d-4de5-acc3-e8aced9863b2.png)
    
   ```console 
   code -r TrainingRazor
   ``` 
   > ![image](https://user-images.githubusercontent.com/47632993/204572344-4005b6a9-2cdc-468c-804a-6338a70b0972.png)

3. "TrainingRazor" name on this command is the folder name of the project. The name is depends on you.
4. System will open the project on your Code Editor.
5. Other than that, by using VS Code, you can just drag project folder on VS Code and and trust the author.     
6. If VS Code authorize you to insert some assets, just click ***Yes/Accept***    
7. [Back to Menu](#create-read-update-delete)
</BR>

***

#### Insert Library

1. Before we start to code, we need to download library called [Datatable](https://datatables.net/download/), just use default setting when download Datatable. Other than that, we need to download library called [FontAwesome](https://fontawesome.com/v4.7/get-started/).

   > What is [Datatable](https://datatables.net/)?

   > What is [FontAwesome](https://fontawesome.com/v4.7/)?

2. Insert the Datatable library and FontAwesome inside folder lib on wwwroot folder.
   
   > ![image](https://user-images.githubusercontent.com/47632993/146663424-e2b4257e-0943-4ee4-8d0d-0519adc045d1.png)

3. After that insert this code 
    
   ```HTML+Razor 
   <link rel="stylesheet" type="text/css" href="~/lib/DataTables/datatables.min.css"/>
   <link rel="stylesheet" type="text/css" href="~/lib/font-awesome-4.7.0/css/font-awesome.min.css" />   
   ```
       
   inside _Layout.cshtml on
   
   > ![image](https://user-images.githubusercontent.com/47632993/146326780-1329954c-bae1-4b38-81ca-816f3ac477cc.png)
   
   > ![image](https://user-images.githubusercontent.com/47632993/146663387-86f2a3c5-4f82-4e3b-96b1-0f52eddd9dde.png)


4. Then, we need to insert this code
   
   ```HTML+Razor 
   <script src="~/lib/DataTables/datatables.min.js"></script>
   ``` 

   inside  _Layout.cshtml on
    
   > ![image](https://user-images.githubusercontent.com/47632993/146662388-b66cc6a4-0757-45d4-90c1-efd33cd24029.png)

5. [Back to Menu](#create-read-update-delete)
</BR>

***

### [CRUD](https://www.sumologic.com/glossary/crud/#:~:text=CRUD%20Meaning%3A%20CRUD%20is%20an,%2C%20read%2C%20update%20and%20delete.) Code


#### View

1. For this project, we gonna use page Index to show one table that consist all data from database table that we have created.
2. Open file Index.cshtml and paste this code (remove old code first) :

