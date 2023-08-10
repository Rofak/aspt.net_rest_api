# aspt.net_rest_api
Create Rest API
##Todo: <br>
1. Create Crud Book <br>
2. Create Crud User <br>
3. Implement User Login `JWT`,`X-API-KEY`

## How To Deploy Asp.net Web Api On Ubuntu Server

### 1.Preparing For Project Folder For Production

#### Step.1 Create dir.
`sudo mkdir -p /var/www/your_domain/html`
#### Step.2 Assign Ownership of Directory
`sudo chown -R $USER:$USER /var/www/your_domain/html`
###@ Step.3 Allow Ownership to read,write,execute file
`sudo chmod -R 755 /var/www/your_domain`
#### Step.4 Go to project folder. 
  1.run `dotnet build`  (builds the project and its dependencies into a set of binaries) </br>
  2.run `dotnet publish --configuration Release` Publishes the application and its dependencies to a folder for deployment to a hosting 
     system.
 ### 2. Config Nginx.
#### Step.1 `sudo nano /etc/nginx/sites-available/your_domain` 
    server {
     listen        100; default port:80  //don't forget to allow port sudo ufw allow 100
     location / {
        proxy_pass         http://127.0.0.1:5000;
        proxy_http_version 1.1;
        proxy_set_header   Upgrade $http_upgrade;
        proxy_set_header   Connection keep-alive;
        proxy_set_header   Host $host;
        proxy_cache_bypass $http_upgrade;
        proxy_set_header   X-Forwarded-For $proxy_add_x_forwarded_for;
        proxy_set_header   X-Forwarded-Proto $scheme;
     }
    }
#### Step.2 `sudo ln -s /etc/nginx/sites-available/your_domain /etc/nginx/sites-enabled/`
#### Step.3 Create Serivce for run project in background
 1. `nano /etc/systemd/system/service_name.service`
 2.  </br>
    Unit]
    Description=ASP.NET Core Web App running on Ubuntu
    [Service]
    WorkingDirectory=/var/www/app
    ExecStart=/usr/bin/dotnet /var/www/app/DeployingToLinuxWithNginx.dll
    Restart=always
    RestartSec=10
    KillSignal=SIGINT
    SyslogIdentifier=dotnet-web-app
    User=www-data
    Environment=ASPNETCORE_ENVIRONMENT=Production
    [Install]
    WantedBy=multi-user.target
  3. `sudo systemctl enable serivce_name.service`
  4. `sudo systemctl start serivce_name.service`
  5. `sudo systemctl restart nginx.service`
![Capture](https://github.com/Rofak/aspt.net_rest_api/assets/55318711/fc9095e6-83d7-4851-9f5b-2decd10512c2)
 
![auth](https://github.com/Rofak/aspt.net_rest_api/assets/55318711/7f83b5d2-a181-47f9-9afd-1d6deffc0e15)
