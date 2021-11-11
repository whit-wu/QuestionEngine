# Question Engine

## Purpose
Question Engine is a proof of concept application meant to demonstrate the viability of Full Stack Web Development with Blazor WASM. The application is being developed using a Test Driven Development paradigm, and will include integration with Github Actions so that changes can be assesed and/or declined for any broken unit tests. 

## Intended Function
Question Engine allows users to create surveys for users to complete.  All questions in the survey are multiple choice, and each user's selection will be saved in a database so it can be reviewed later.  

## Using the App
To be clear, this is not meant to act as an application to be used in production; the overall goal is to help me learn Fullstack Web Development using Blazor WASM.  What I have learned, along with the pros and cons of Blazor WASM, will be called out in this README as my work continues.  Since there is no intention for the app to be used in a real-world setting, you may notice some shortcuts have been taken (i.e. Using SQL Lite in place for an actual DB).  Such shortcuts exist to accelerate the learning process without being hampered by design or infrastructure nuances.  

## What I Have Learned So Far

### Using a Shared Libary For Model Validation
It is possible in Blazor WASM to create rules (i.e. validation) that can be shared by both the front and back end.  When a rule is created, it can be called as a Data Annotation for your models.  This process was learned by following [MS documentation from 2019](https://docs.microsoft.com/en-us/archive/msdn-magazine/2019/march/web-development-full-stack-csharp-with-blazor), which was made before Blazor WASM was production ready, and some parts of the tutorial no longer worked as expected.  I have addressed these issues with a modified shared libary in the QuestionEngine.Shared project.

At the time of this writing, the libary is working for both the front and back end.  Running the unit tests in QuestionEngine.BackendTests will verify that validation works fine for the backend.  To verify that validation works correctly in the front end, do the following
1. Run the app and click on Add Survey in the nav bar.  
1. Input a survey name, then hit tab.  This will show an Add Question button.
1. Click on the button, and you will see an input box with a Question Desc label.  Below the box you will see an error message stating `Question must have a valid description`.  This is because the input box currently does not have a value.
1. Give Question Desc a value then hit tab.  The error will go away.

The advantage here is clear; you could write your model validtion once and have it persist throughout your entire app!  The tradeoff is that you need to drill down another layer to find your validation logic (in our case, the QuestionEngine.Shared project), but that may be a small price to pay if it helps keep your Unit of Work and web page logic cleaner.  


### Routing Done Easy
Excluding Blazor WASM, my only experience with front-end development frameworks are with AngularJS and Angular 2+.  In Angular 2+, routes must be defined in each component and called out in an app or routing module.  With Blazor, you simply create a new Razor page, and define the route at the top like so:
`@page "/myTestRoute"`

You can even define parameters in this fashion, which I will do in later verison of this app. 


### Less Headaches Sending Data Between Front And Backends
Since Blazor WASM is all C#, you get access to POCOs in both the front and backends.  Addtionally, you can share your objects with the front and backends, meaning you can send and receive your Models/DTOs as the are.  No need to write them twice for JS and C#!

