# IDS4Admin
在skoruba/IdentityServer4.Admin(https://github.com/skoruba/IdentityServer4.Admin) 项目基础上创建的IdentityServer4认证管理中心，根据自己的需要，进行了如下修改：

* 增加对非SSL的支持，使认证服务和管理应用都可以部署在http://开头的网址
* 为管理应用增加BasePath，使管理应用可以部署在子路径，这样管理应用和认证服务可以部署在同一域名下的不同路径（可以使用IIS或者反向代理服务如Nginx）
* 原项目部署到MySql有点问题，进行了一点修改
* 去掉了Web Api项目，只保留认证服务和管理应用两部分。
* 简化了Docker部署，将认证服务和管理应用分别生成镜像，在Docker部署时更简单
* 增加了一个配置文件生成工具，防止修改配置文件时出错。


