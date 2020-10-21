# IdentityServer4 统一授权认证系统

#### 介绍
基于ASP.NET CORE+IdentityServer4+Vue+Sqlserver 的统一授权认证系统，包含系统管理界面。

#### 软件架构
ASP.NET CORE+IdentityServer4+Vue+Sqlserver
涉及：  
Autofac  
AutoMapper  
IdentityServer4  
Swagger  
...

#### 使用说明
1. 新功能在dev分支进行开发，请切换dev查看
2. 前端使用 VUE cli3搭建  
3. 进入web 运行 
    ```
    npm install  
    npm run serve
    ```
4. 后台Asp.net Core。先通过EF更新数据库，然后进入项目目录运行   
    **迁移数据库**
    ```
    #sqlserver 默认项目选择（WQLIdentity.Infra.Data）
    Add-Migration InitialCreate -Context ConfigurationDbContext -OutputDir Migrations\Configuration\SqlServer
    Add-Migration InitialCreate -Context PersistedGrantDbContext -OutputDir Migrations\PersistedGrant\SqlServer
    Add-Migration InitialCreate -Context ApplicationDbContext -OutputDir Migrations\Application\SqlServer
    #mysql  默认项目选择（WQLIdentity.Infra.Data.Mysql）
    Add-Migration InitialCreate -Context MysqlConfigurationDbContext -OutputDir Migrations\Configuration\Mysql
    Add-Migration InitialCreate -Context MysqlPersistedGrantDbContext -OutputDir Migrations\PersistedGrant\Mysql
    Add-Migration InitialCreate -Context MysqlApplicationDbContext -OutputDir Migrations\Application\Mysql
    ```
    **更新数据库**  
    ```
    #sqlserver
    update-database  -context ConfigurationDbContext
    update-database  -context PersistedGrantDbContext
    update-database  -context ApplicationDbContext
    #mysql
    update-database  -context MysqlConfigurationDbContext
    update-database  -context MysqlPersistedGrantDbContext
    update-database  -context MysqlApplicationDbContext
    ```
    **生成种子数据**
    ```
    dotnet run seed //运行项目
    dotnet WQLIdentityServerAPI.dll seed //运行程序
    ```
    
5. 数据库切换请打开appsettings.json，修改对应的数据库连接及数据库类型即可。



#### 参考
https://github.com/skoruba/IdentityServer4.Admin