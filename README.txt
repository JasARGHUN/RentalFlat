To start just make migrate to DataAccess

if you'll publish this app don't forget:
1 - RentalFlat_Server/Services/DbInitializer - here you can change admin data before migration.

2 - Don't forget to change API-settings in RentalFlat_API/appsettings.json/ "APISettings"/ "ValidAudience" and "ValidIssuer"
    PS Change your API Url (Go to RentalFlat_API properties/Debug/App URL).

3 - To change images and load gif go to ClientApp/wwwroot/images
    Using Local Storage in ClientApp

4 - The app using Stripe Payment.//Invoke Stripe in ClientApp/wwwroot/js/StripePayment - add your publishable key!
    and change Stripe ApiKey in RentalFlat_API/appsettings.json

ATTEMPT:
-Dont forget to change settings in ClientApp/appsettings.json and RentalFlat_API/appsettings.json
-Dont forget to change settings in RentalFlat_API/appsettings.json/"Client.Url": "https://your url/"

If you have TypeError: Failed to fetch - /Go to Solution prop/Set Startup Projects/Activate Multiple startup projects 
and choise ClientApp and RentalFlat_API

PS Make migration only to DataAccess!

The app using Stripe Payment and MailJet form mail send.