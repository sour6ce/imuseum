#!/bin/sh
echo "Choose a way to start"
echo "1. Default Start"
echo "2. Frontend Only Start"
echo "3. Frontend Testing watch mode"
echo "4. Run npm install into Frontend"
read -p "Enter your choice: " choice
while [$choice -ne 1 -a $choice -ne 2 -a $choice -ne 3 -a $choice -ne 4]
  do
    echo "Invalid choice"
    read -p "Enter your choice: " choice
  done
if [ $choice -eq 1 ]
  then
    echo "Starting default"
    cd ./IMuseum.Api
    dotnet run Program.cs
elif [ $choice -eq 2 ]
  then
    echo "Starting frontend only"
    cd .IMuseum.Presentation/ClientApp
    npm start
elif [ $choice -eq 3 ]
  then
    echo "Starting frontend testing watch mode"
    cd ./IMuseum.Presentation/ClientApp
    npm run test
elif [ $choice -eq 4 ]
  then
    echo "Running npm install into frontend"
    cd ./IMuseum.Presentation/ClientApp
    npm install
fi
