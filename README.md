# FinalProject
Team Members: Giorgi Qviria, Nino Grigalashvili, Mikheil Chkheidze

Visual Studio Solution Description

The web application is written, mostly, using Code-first approach. We have used NuGet
Package Manager Console to connect to SQL database and created migrations. Then we 
deployed both the web app and the database on Azure portal. However, the code given 
connects to the local SQL server. If you wish to clone or download it in order to run 
it on your own computer, this is possible, but you will need to change the server name 
with your server name in the "defaultConnection" string in the appsettings.json file, 
and further alter it if you have an username and a password.  Then run all migrations 
to create the database.

Website Description (deployed on Azure portal):

On this website, there are two types of login options: admin and user. Admins are those 
who publish info about book reviews from different reviewers that are listed on the Reviewers 
page (only admins can see and edit this page). They publish information such as date of review, 
book reviewed, etc. Admins can add, update (except for their email address) or delete users as 
well as books for users. When admins add reviews for books, both admins and users can see the 
history of reviews. However, only admins can alter them. Currently there is only one admin. 
Only admins can register other users as admins. This can be achieved by coping the Register Page 
URL, logging in as an admin and opening the Register URL in a separate tab. You will see isAdmin 
checkbox. This is a Boolean value in our code, which makes sure that the user will be added with 
admin privileges.
Users are authors who create profiles on the website and post the books they want to be reviewed 
before publishing. They are able to see which of their writings have been reviewed and by whom 
(as well as other details). They can also print the review record. Users can submit books for 
different authors, thus be representatives of more than one writer, or they can be writers on 
their own and upload their own books only. Users can be searched by their name, email, or phone.

Database Description:

Admins and users are tracked by two different IDs in the AspNetUserRoles table. Except for User and 
User related tables, we have created Books table to store information about books, Reviews table to 
store information about book reviews, and ReviewTypes table to store the list of reviewers. Books 
table is related to AspNetUserRoles table with a foreign key UserId, and Reviews can also be tracked 
with foreign keys BookId and Reviewer. This makes it possible to display Past Review Records to an 
admin (this can be seen when creating New Review on the website).
