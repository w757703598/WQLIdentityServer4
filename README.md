<p></p>
<p></p>

<p align="center">
<img src="/Web/IdentityServerSites/src/assets//imgs/wqlapi.png" height="80"/>
</p>
<div align="center">  

[![star](https://gitee.com/wangqianlong1993/IdentityServer4/badge/star.svg)](https://gitee.com/wangqianlong1993/IdentityServer4.git) [![fork](https://gitee.com/wangqianlong1993/IdentityServer4/badge/fork.svg)](https://gitee.com/wangqianlong1993/IdentityServer4.git)

</div>

# :smile: IdentityServer4 统一授权认证系统

##  :pencil2:  介绍
在学习授权认证的过程中，发现了IdentityServer4框架。官方的例程大多是基于本地或者内存配置。没有一个完整的后台界面。github上有一些优秀的管理系统，大多都是基于MPA开发。于是基于.NET5和vue开发了这一套授权管理系统。

## :surfer: 界面
- 服务端
![服务端](/doc/imgs/server.bmp)
- 客户端
![服务端](/doc/imgs/client.bmp)
## :surfer: 演示地址
[API地址](http://47.119.119.183:8081/)：统一授权认证登录
[客户端](http://47.119.119.183:8082/)：vue客户端。管理界面
>默认账户：admin/123456
>云服务配置较差,仅供演示.

## :orange_book: 文档
**待完善.....**

## :pushpin: 软件架构
- [dotNet5](https://docs.microsoft.com/zh-cn/dotnet/)：后台webapi
- [vue](https://cn.vuejs.org/)： 前端显示
- Autofac  
- AutoMapper  
- IdentityServer4  
- Swashbuckle  
- MiniProfiler
...
## :bomb: 环境要求

- Visual Studio 2019 16.9 +
- .NET 5 SDK +
- .Net Standard 2.1 +
- VUE 2.6
- node 12.15

## :dart: 支持平台

- 运行环境
  - Windows
  - Linux
  - 其余待测试
- 数据库
  - SqlServer
  - MySql
- 应用部署
  - Kestrel
  - Nginx

## :video_game: Stars 趋势图

[![Stargazers over time](https://whnb.wang/stars/wangqianlong1993/IdentityServer4?e=43200)](https://whnb.wang/dotnetchina/Furion?e=43200)
## :smiling_imp: 开发说明
### :heart:后台
1. :sweat_drops: 最新功能在IdentityServer4_v4.0分支进行开发，请切换IdentityServer4_v4.0查看
    ```
    git checkout IdentityServer4_v4.0
    ```
2. :sweat_drops: 还原
    - nuget会自动还原
    - 前端js包，使用了node进行还原，需要安装node包。有需要修改增加的可以看下面的指令.
    ```json
        //package.json文件.在项目目录下面。
        //可以在vs【工具】-【选项】-【web包管理】-【程序包还原】里面设置保存时还原。vs会根据里面的版本自动下载
        {
            "version": "1.0.0",
            "name": "asp.net",
            "private": true,
            "dependencies": {
            "bootstrap": "4.6.0",
            "jquery": "3.6.0"
            },
            "devDependencies": {
            "gulp": "^4.0.2",
            "gulp-watch": "5.0.1",
            "gulp-concat": "2.6.1",
            "gulp-clean-css": "^4.2.0",
            "gulp-concat-css": "^3.1.0",
            "gulp-less": "^4.0.1",
            "gulp-rename": "^2.0.0",
            "gulp-sourcemaps": "^2.6.5",
            "gulp-uglify": "^3.0.2",
            "del": "6.0.0"
            }
        }
    ```
    - node下载的npm包，通过gulp自动清理合并。详细可了解[gulp](https://www.gulpjs.com.cn/).

3. :sweat_drops: 数据迁移
**采用EF完成orm功能。目前支持sqlserver和mysql两种功能。**
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
    **切换配置**
    ```json
    {
        "urls": "http://localhost:5001;https://localhost:5002",
        "IdentityServer4": {
            "authUrls": "http://localhost:5001",//swagger认证地址，如果nginx更改了，需要配置为服务端发布地址
            "Audience": "IdentityServer4"
        },
        "Logging": {
            "LogLevel": {
            "Default": "Warning"
            }
        },
        "AllowedHosts": "*",

        //切换数据库地址
        "Settings": {
            //mysql or sqlserver
            "DatabaseType": "mysql",

            //数据库连接字符串
            "SqlServerConnection": "Server=.;Database=IdentityServer ;Trusted_Connection=True;MultipleActiveResultSets=true",
            //"MySqlConnection": "Server=localhost;Port=3306;Database=IdentityServer;Uid=root;Pwd=xiucaibbx0528;"
            "MySqlConnection": "Server=localhost;Port=3306;Database=IdentityServer;Uid=wql;Pwd=asdfghjkl;",
            "UseMinProfiler": false //是否启用miniprofiler
        }
    }

    ```
### :green_heart: 前端
 **前端使用 VUE cli3搭建**  
1. :sweat_drops: 修改认证配置文件
    ```json
    {
        "authority": "http://47.119.119.183:8081",//认证地址
        "clientId": "IdentityServer4", //认证客户端id
        "redirectUri": "http://localhost:8082/oidc-callback",//回调地址
        "popupRedirectUri": "http://localhost:8082/oidc-popup-callback", "responseType": "id_token token", //认证类型
        "scope": "openid profile offline_access IdentityServer.API", "automaticSilentRenew": true,
        "automaticSilentSignin": false, 
        "silentRedirectUri": "http://localhost:8082/silent-renew-oidc.html" , "post_logout_redirect_uri": "http://localhost:8082" //退出回调地址
    }
    ```
2. :sweat_drops: 运行
    ```
    npm install 
    npm run dev
    ```




#### :heart: 鸣谢
- :+1: [IdentityServer4.Admin]( https://github.com/skoruba/IdentityServer4.Admin)
- :+1: [vue-element-admin ](https://gitee.com/panjiachen/vue-element-admin)