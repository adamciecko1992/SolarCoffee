#project variables

PROJECT_NAME ?= SOLARCOFFEE
ORG_NAME?= SOLARCOFFEE
REPO_NAME?= SOLARCOFFEE

#JESLI KTORAS ZMIENNA NIE MA WARTOSCI TO PRZYPISZ JEJ WLASNIE TO CO PO PRAWEJ

.PHONY migrations db 
migrations: cd ../solarcoffee.data && dotnet ef --startup-project "../solarcoffee.web" migrations add $(mname) && cd ..
database: cd ../solarcoffee.data && dotnet ef --startup-project "../solarcoffee.web" database update && cd..