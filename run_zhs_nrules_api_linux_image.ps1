docker build -f ./src/ZHS.Nrules.API/Dockerfile .  -t zhsnrules;
docker run -d -p 12180:80 zhsnrules;